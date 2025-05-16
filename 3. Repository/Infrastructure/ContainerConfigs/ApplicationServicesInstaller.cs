using Abstractions.Interfaces;
using Abstractions.Interfaces.Mail;
using Common.Runtime.Session;
using EntityFrameworkCore.Audits;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Services.Implementations;
using Services.Implementations.Mail;

namespace Infrastructure.ContainerConfigs
{
    public static class ApplicationServicesInstaller
    {
        public static void ConfigureApplicationServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuditHelper, AuditHelper>();
            services.AddSingleton<ITokenService, JwtTokenService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISendMailService, SendMailService>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUserSession, UserSession>();
        }
    }
}
