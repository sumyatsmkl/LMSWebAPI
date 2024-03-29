﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DTO;
using Domain.Entities;
using Domain.Enums;
using Domain.Tools;
using Domain.Utilities.Helpers;
using Domain.Utilities.Request;
using Domain.Utilities.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32.SafeHandles;

namespace DataAccess.Repositories.Profile
{
    public class ProfileRepository:IProfileRepository
    {
        private readonly LMSDBContext _dbContext;
        public ProfileRepository(LMSDBContext context, IConfiguration configuration) 
        {
            _dbContext = context;
            _configuration = configuration;
        }
        WebResponseContent webResponse = new WebResponseContent();

        private readonly IConfiguration _configuration;
        public async Task<IReadOnlyCollection<TProfile>> GetAll()
            => await _dbContext.TProfiles.ToListAsync();

        public async Task<TProfile> Get(Guid profileId)
        {
            return await Find(profileId) ?? throw new Exception($"Profile does not exist.");
        }
        public async Task<TProfile> Find(Guid profileId)
        {
            return await _dbContext.TProfiles
                .Include(profile => profile.TProfileAccounts)
                .Include(profile => profile.TProfileEducations)
                .SingleOrDefaultAsync(profile => profile.ProfileId == profileId);
        }

        private async Task<bool> IsProfileExistAsync(Guid profileId)
        {
            return await _dbContext.TProfiles.AnyAsync(profile => profile.ProfileId == profileId);
        }

        private async Task<bool> IsUserNameExist(string userName)
        {
            return await _dbContext.TProfileAccounts.AnyAsync(profile => profile.UserName == userName);
        }

        private async Task<bool> IsEmailExist(string email)
        {
            return await _dbContext.TProfileAccounts.AnyAsync(profile => profile.Email == email);
        }

        private async Task<bool> IsIDNoExist(string idNo)
        {
            return await _dbContext.TProfiles.AnyAsync(profile => profile.Idno == idNo);
        }

        public async Task<TProfile> Update(TProfile profile)
        {
            await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await transaction.CreateSavepointAsync("BeforeUpdatingProfile");

                if (!IsProfileExistAsync(profile.ProfileId).Result)
                    throw new Exception($"Profile to update doesn't exist");

                EntityEntry<TProfile> updatedProfile = _dbContext.TProfiles.Update(profile);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return updatedProfile.Entity;
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("BeforeUpdatingProfile");
                throw;
            }
        }

        public async Task<TProfile> Delete(Guid profileId)
        {
            await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await transaction.CreateSavepointAsync("BeforeProfileRemoved");

                TProfile profileToRemove = await Get(profileId);

                _dbContext.TProfiles.Remove(profileToRemove);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return profileToRemove;
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("BeforeProfileRemoved");
                throw;
            }
        }

        public async Task<WebResponseContent> Login(LoginInfo loginInfo)
        {
            string msg = string.Empty;
            try
            {
                TProfileAccount profileResult = await _dbContext.TProfileAccounts.Where(x => x.UserName == loginInfo.UserName).FirstOrDefaultAsync();

                if (profileResult == null)
                    return webResponse.Warning(ResponseCode.Login_UserNameNotFound.ToString(), "User name does not exist!");
                          

                if (!HashPasswordHelper.VerifyPassword(loginInfo.Password, profileResult.Password))
                {
                    return webResponse.Warning(ResponseCode.Login_IncorrectPassword.ToString(), "Incorrect password!");
                }
               
                var token = CreateJwt(loginInfo);
                profileResult.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
                string generatedToken = new JwtSecurityTokenHandler().WriteToken(token);
                DateTime expiredDate = token.ValidTo;
             
                _dbContext.TProfileAccounts.Update(profileResult);
                _dbContext.SaveChanges();               
                webResponse.Data = profileResult;

                return webResponse.LoginOK(generatedToken,expiredDate,Convert.ToInt32(ResponseCode.Login_Success).ToString(), "Login Successfully!!",profileResult);
               
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;               
                return webResponse.Error(ResponseType.ServerError);
            }
            finally
            {

            }
        }

        public async Task<WebResponseContent> Register(RegisterRequest registerData)
        {
            string msg = string.Empty;
            await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                await transaction.CreateSavepointAsync("BeforeInsert");


                if (IsUserNameExist(registerData.UserName).Result)
                    return webResponse.Warning(ResponseCode.Register_DuplicateUserName.ToString(),"User name alrady exists!");

                if (IsEmailExist(registerData.Email).Result)
                    return webResponse.Warning(ResponseCode.Register_DuplicateEmail.ToString(),"This email is already registered!");

                if (IsIDNoExist(registerData.IdNo).Result)
                    return webResponse.Warning(ResponseCode.Register_DuplicateIDNo.ToString(),"ID number already exists!");

                var passMessage = HashPasswordHelper.CheckPasswordStrength(registerData.Password);
                if (!string.IsNullOrEmpty(passMessage))
                    return webResponse.Warning(ResponseCode.Register_InvalidPasswordStrength.ToString(), passMessage.ToString());

                TProfile profile = new TProfile();
                profile.ProfileId = Guid.NewGuid();
                profile.FullName = registerData.FullName;
                profile.TenantSubId = _dbContext.TTenantSubs.Where(x => x.Status == (int)CommonStatus.Active).FirstOrDefault().TenantSubId;
                profile.Idno = registerData.IdNo;
                profile.IdtypeId = registerData.IdType;
                profile.ProfileStatus = (int)ProfileStatus.Active;
                EntityEntry<TProfile> newProfile = await _dbContext.TProfiles.AddAsync(profile);

                TProfileAccount portalAcc = new TProfileAccount();
                portalAcc.ProfileAccountId = Guid.NewGuid();
                portalAcc.TenantSubId = profile.TenantSubId;
                portalAcc.ProfileId = profile.ProfileId;
                portalAcc.UserName = registerData.UserName;
                portalAcc.Password = HashPasswordHelper.HashPassword(registerData.Password);
                portalAcc.AccessToken = "test";
                portalAcc.Email = registerData.Email;
                portalAcc.AccountStatus = (int)AccountStatus.Active;
                portalAcc.IsMustChangePassword = true;
                portalAcc.LastLoginTime = DateTime.Now;
                EntityEntry<TProfileAccount> newPortalAcc = await _dbContext.TProfileAccounts.AddAsync(portalAcc);

                TProfileRole profileRole = new TProfileRole();
                profileRole.ProfileRoleId = Guid.NewGuid();
                profileRole.TenantSubId = profile.TenantSubId;
                profileRole.ProfileId = profile.ProfileId;
                profileRole.RoleId = registerData.RoleId;
                profileRole.Status = (int)CommonStatus.Active;
                EntityEntry<TProfileRole> newProfileRole = await _dbContext.TProfileRoles.AddAsync(profileRole);               
                
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return webResponse.OK(Convert.ToInt32(ResponseCode.Register_Success).ToString(),"You have successfully registered!");
            }
            catch (Exception ex)
            {
                await transaction.RollbackToSavepointAsync("BeforeInsert");
                throw;
            }
            finally
            {

            }
        }

       
        #region JwtAccessToken
        private SecurityToken CreateJwt(LoginInfo user)
        {
            int expiredMinutes = Convert.ToInt32(_configuration["JWT:TokenValidityInMinutes"]);
            TProfileRole profileRole;
            TRole role = new TRole();
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysceret.....");
            TProfileAccount profileResult = _dbContext.TProfileAccounts.Where(x => x.UserName == user.UserName).First();
            if (HashPasswordHelper.VerifyPassword(user.Password, profileResult.Password))
            {
                profileRole = _dbContext.TProfileRoles.Where(x => x.ProfileId == profileResult.ProfileId).FirstOrDefault();
                role = _dbContext.TRoles.Where(x => x.RoleId == profileRole.RoleId).FirstOrDefault();
            }

            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, role.RoleName),
                new Claim(ClaimTypes.Name,$"{user.UserName}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddMinutes(expiredMinutes),                
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);            
            return token;
        }
        private string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            var tokenInUser = _dbContext.TProfileAccounts
                .Any(a => a.AccessToken == refreshToken);
            if (tokenInUser)
            {
                return CreateRefreshToken();
            }
            return refreshToken;
        }
        private ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            var key = Encoding.ASCII.GetBytes("veryverysceret.....");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("This is Invalid Token");
            return principal;

        }
        private JwtSecurityToken CreateToken_V2(LoginInfo user)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        #endregion

    }
}
