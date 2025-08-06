using CarteirasInvestimento.AppServer.Interfaces;
using CarteirasInvestimento.Repository;
using CarteirasInvestimento.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarteirasInvestimento.AppServer.Configuration
{
    public class Bootstrap
    {
        public static void AddServices(IServiceCollection services, IConfiguration configure)
        {

            services.AddMvcCore().AddApiExplorer();
            services.AddRazorPages();
            services.AddMvc();

            services.AddSingleton<IConfiguration>(configure);

            AddRepository(services);
            AddAppServe(services);
        }

        public static void AddRepository(IServiceCollection services) 
        {
            services.AddScoped<IAtivoRepository, AtivoRepository>();
            services.AddScoped<ICarteiraRepository, CarteiraRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
           // services.AddScoped<IRepository, Repository>();
        }

        public static void AddAppServe(IServiceCollection services)
        {
            services.AddScoped<IAtivoAppServe, AtivoAppServe>();
            services.AddScoped<ICarteiraAppServe, CarteiraAppServe>();
            services.AddScoped<IClienteAppServe, ClienteAppServe>();
        }
    }
}
