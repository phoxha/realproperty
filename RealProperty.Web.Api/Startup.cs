using RealProperty.Data;
using RealProperty.Data.Entities;
using RealProperty.Data.Repositories.Abstract;
using RealProperty.Data.Repositories.Concrete;
using RealProperty.Model.Auth;
using RealProperty.Model.Mappings;
using RealProperty.Model.Services.Absctract;
using RealProperty.Model.Services.Concrete;
using RealProperty.Model.Settings;
using RealProperty.Web.Api.Utilities.Filters;
using RealProperty.Web.Api.Utilities.Middlewares;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace RealProperty.Web.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly ISettingsService _settingsService;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var jwtSettings = new JwtSettings();

            Configuration.Bind(nameof(JwtSettings), jwtSettings);

            _settingsService = new SettingsService(jwtSettings);
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>();

            services.AddIdentity<User, Role>(opts =>
            {
                opts.Password.RequiredLength = 5;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
                opts.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _settingsService.JwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settingsService.JwtSettings.Key)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddCors();
            services.AddResponseCaching();

            services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(options =>
            {
                options.CreateMissingTypeMaps = false;
                options.AddProfile<UserMapping>();
            })));

            services.AddMvc(
                config =>
                {
                    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                    config.Filters.Add(new AuthorizeFilter(policy));
                    config.Filters.Add(new ValidateModelStateFilter());
                }
            ).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<LoginModel>());
            
            // Data & Repositories
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.AddTransient<IUserRepository, UserRepository>();

            // Services
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();

            // Settings
            services.AddSingleton(_settingsService);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDatabaseInitializer databaseInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                databaseInitializer.Initialize();
            }

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("X-File-Name"));

            app.UseAuthentication();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvc();
        }
    }
}
