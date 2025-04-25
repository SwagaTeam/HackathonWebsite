
using HackathonWebsite.DataLayer;
using HackathonWebsite.Initializers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using HackathonWebsite.BusinessLayer.Services.AuthService;
using HackathonWebsite.BusinessLayer.Services.AuthService.Abstractions;
using HackathonWebsite.BusinessLayer.Services.AuthService.Implementations;
using HackathonWebsite.BusinessLayer.Services.CaseService;
using HackathonWebsite.BusinessLayer.Services.HackathonService;
using HackathonWebsite.BusinessLayer.Services.TeamService;
using HackathonWebsite.BusinessLayer.Services.UserService;
using HackathonWebsite.DataLayer.Repositories.Abstractions;
using HackathonWebsite.DataLayer.Repositories.Implementations;
using HackathonWebsite.Middleware;
using HackathonWebsite.BusinessLayer.Services.CaseService;
using HackathonWebsite.BusinessLayer.Services.HackathonService;
using HackathonWebsite.BusinessLayer.Services.MailService;

namespace HackathonWebsite
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            ConfigureServices(builder.Services, builder.Configuration);
            await Console.Out.WriteLineAsync(builder.Configuration["Jwt:Key"]);
            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            using var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await DbContextInitializer.Migrate(appDbContext);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<JwtBlacklistMiddleware>();

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services, IConfigurationManager configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEncrypt, Encrypt>();
            services.AddScoped<IHackathonRepository, HackathonRepository>();
            services.AddScoped<ICaseRepository, CaseRepository>();
            services.AddScoped<ICaseService, CaseService>();
            services.AddScoped<IHackathonService, HackathonService>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddTransient<IMailService, MailService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IBlackListService, BlackListService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ITeamRepository, TeamRepository>();


            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            AddAuthentication(services, configuration);

            AddSwagger(services);

            DbContextInitializer.Initialize(services, configuration["DefaultConnection"]!);
        }

        private static void AddAuthentication(IServiceCollection services, IConfigurationManager configuration)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                        RoleClaimType = ClaimTypes.Role
                    };
                });
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskBoard API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "������� 'Bearer' [������] ��� �����������",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

            });
        }
    }
}
