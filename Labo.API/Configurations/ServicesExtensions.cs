using System.Net.Mail;
using System.Net;
using Labo.Application.Interfaces.Repositories;
using Labo.Application.Interfaces.Services;
using Labo.Application.Services;
using Labo.Infrastructure.Repositories;

namespace Labo.API.Configurations
{
    public static class ServicesExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<ITournamentRepository, TournamentRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<ITournamentService, TournamentService>();
        }

        public static void AddSmtp(this IServiceCollection services, ConfigurationManager config)
        {
            SmtpConfig conf = config.GetSection("Smtp").Get<SmtpConfig>() ?? throw new Exception("Missing SMTP Config");

            services.AddScoped(b => new SmtpClient
            {
                Host = conf.Host,
                Port = conf.Port,
                Credentials = new NetworkCredential
                {
                    UserName = conf.Username,
                    Password = conf.Password,
                },
                EnableSsl = conf.Ssl
            });
        }
    }
}
