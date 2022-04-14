
using API.Filters;
using BLL;
using BO;
using DocFx;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;// permet de retrouver toutes les configurations de appsettings.json
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Methode de la classe startup pour ajouter les services dans l'injecteur de dépendance.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddTransient<OffreController>();   A chaque fois que je demande le service
            // services.AddScoped<OffreController>()   ;   Pour toute la durée de vie de la requête
            // services.AddSingleton<OffeController>() ;   demande unique pour toute la durée de vie du serveur

            //Service du versionning
            services.AddSwaggerDocument(options =>
            {
                options.PostProcess = document =>
                {
                    document.Info.Version        = "v1";
                    document.Info.Title          = "_API JobChannel_";
                    document.Info.Description    = "Documentation d'API JobChannel";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact        = new NSwag.OpenApiContact
                    {
                        Name  = "ConceptCode",
                        Email = String.Empty ,
                        Url   = "https://google.com"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url  = "https://example.com/License"
                    };
                };

            });

            services.AddControllers(options =>
            {
                //Ajout du filtre permettant de gérer les exceptions.
                options.Filters.Add<ApiExceptionFilterAttribute>();
            });

            services.AddFluentValidation(options =>
            {
                options.LocalizationEnabled = true;
            });

            //ajoute les services de la BO (validator)
            services.AddBO();

            //Ajout des services de la BLL
            services.AddBLL(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseDocFx(configure =>
            //{
            //    configure.Path = "/doc";//a la racine du serveur
            //});


            // Cet intergiciel (MiddleWare) permet de générer le document JSON qui va décrire l'API
            app.UseOpenApi();

            // Cet intergiciel UseOpenApi() interprête le json généré et créer l'interface utilisateur  -> http://localhost:<port>/swagger (pour voir l’UI de Swagger)
            //                                                                                          -> http://localhost:<port>/swagger/v1/swagger.json pour voir la spécification Swagger
            app.UseSwaggerUi3();


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
