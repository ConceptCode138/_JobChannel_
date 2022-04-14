using System;
using DAL.UOW;
using DAL.UOW.Repositories;
using DAL.UOW.Repositories.SQLServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    /// <summary>
    /// Cette classe va permettre d'étendre l'injecteur de dépendence pour pouvoir enrgistrer (les services) l'UOW et les répertoires
    /// </summary>
    public static class DALExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public static void AddDAL(this IServiceCollection services, IConfiguration configuration)
        {
            //Afin d'enregistrer la DbSession pour un temps de vie lier à la requête
            services.AddScoped<IDbSession, DbSession>();

            //Afin d'enregistrer le 'Unit Of Work'
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //Repositories
            services.AddTransient<IOffreRepository, OffreRepositorySQLServer>();

            services.AddTransient<ITypeContratRepository, TypeContratRepositorySQLServer>();

            services.AddTransient<ITypePosteRepository, TypePosteRepositorySQLServer>();
        }
    }
}
