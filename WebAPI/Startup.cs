using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI.Extensions;



namespace WebAPI
{
    

    public class Startup
    {
        public string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            string corsUrls = Configuration["CorsUrls"];
            if (string.IsNullOrEmpty(corsUrls))
            {
                throw new Exception("CorsURL exception!");
            }
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                        builder =>
                        {
                            builder.AllowAnyOrigin()
                           .SetPreflightMaxAge(TimeSpan.FromSeconds(2520))
                            .AllowAnyHeader().AllowAnyMethod();
                        });
            });

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerServices();
            services.AddAuthServices();
            services.AddServices(builder =>
            {
                builder.EnableSensitiveDataLogging();
                builder.UseSqlServer(Configuration.GetConnectionString("DBConnection"));
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryverysceret.....")),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

            services.AddAutoMapper(typeof(Startup));
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                if (!env.IsDevelopment())
                    c.RoutePrefix = string.Empty;

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "eHealthLMS v1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            //app.UseCors(MyAllowSpecificOrigins);
            //app.UseMiddleware<CustomAuthorizationMiddleware>();
            //app.UseMiddleware<CustomCachingMiddleware>();
            //app.UseMiddleware<RequestValidationMiddleware>();
            app.UseCors();
            app.UseAuthentication();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

          
        }
    }
}
