using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Reflection;// GetTypeInfo() -> permet de récupérer les informations d'une classe un d'un namespace,methode ,attribut

namespace DocFx
{
    public static class DocFxExtensions
    {
        public static IApplicationBuilder UseDocFx(this IApplicationBuilder app, Action<DocFxSettings> configure)
        {
            //options de configuration
            DocFxSettings settings = new DocFxSettings();

            if (configure == null)
            {
                //valeur par défaut
                settings.Path = "/docFx";
            }
            else
            {
                //valeur de l'utilisateur
                configure.Invoke(settings);
            }
            //Fin de récupération de la configuration

            //Servir les fichiers statique à partire d'une url 'Path'.
            app.UseFileServer(new FileServerOptions
            {
                //Path par défaut
                RequestPath = new PathString(settings.Path),

                FileProvider=new EmbeddedFileProvider(typeof(DocFxExtensions).GetTypeInfo().Assembly, "DocFx._site")
            }) ;

            return app; // retour de l'application pour que pipeline puisse continuer.
        }
    }
}
