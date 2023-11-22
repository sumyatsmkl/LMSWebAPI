using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories.Profile
{
    public class ProfileRepository:IProfileRepository
    {
        private readonly LMSDBContext _dbContext;
        public ProfileRepository(LMSDBContext context) => _dbContext = context;
        WebResponseContent webResponse = new WebResponseContent();
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

                //string token = JwtHelper.IssueJwt(new UserInfo()
                //{
                //    User_Id = user.User_Id,
                //    UserName = user.UserName,
                //    Role_Id = user.Role_Id
                //});
                //user.Token = token;
                //webResponse.Data = new { token, userName = user.UserTrueName, img = user.HeadImageUrl };
                //repository.Update(user, x => x.Token, true);
                //UserContext.Current.LogOut(user.User_Id);
                //loginInfo.Password = string.Empty;
               
                return webResponse.OK(Convert.ToInt32(ResponseCode.Login_Success).ToString(), "Login Successfully!!");
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                //if (_context.GetService<Microsoft.AspNetCore.Hosting.IWebHostEnvironment>().IsDevelopment())
                //{
                //    throw new Exception(ex.Message + ex.StackTrace);
                //}
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
    }
}
