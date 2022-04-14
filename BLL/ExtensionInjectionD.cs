using BLL.Interfaces;
using BLL.Services;
using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("TESTUnit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace BLL
{
    public static class ExtensionInjectionD
    {
        /// <summary>
        /// this -> methode d'extension devant toujours etre dans une classe statique.
        /// Cette methode permet d'ajouter dans l'injecteur de dépendance, l'interface ITraceService et son implémentation TraceService PUIS
        /// permet également d'ajouter dans l'injecteur de dépendance, l'interface IOffeService et son implémentation OffreService.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddBLL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITraceService, TraceService>();//pour toute la durée de vie du serveur.
            services.AddScoped<IOffreService, OffreService>();

            services.AddDAL(configuration);
        }
    }
}
