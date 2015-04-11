using NuGet.Feed.Service.Middleware;
using Owin;
using System.IO;

namespace NuGet.Feed.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseErrorPage();

            builder.Map("/api/v2/package", app => {
                app.MapWhen(m => m.Request.Method == "PUT", config => {
                    //check api key
                    //validate package
                    //statistics
                    //persistence
                    app.Use<PackagePersistence>();
                    config.Run(context =>
                    {
                        context.Response.ContentType = "text/plain";
                        return context.Response.WriteAsync("This was a put");
                    });
                });

                app.MapWhen(m => m.Request.Method == "DELETE", config => {
                    //check api key
                    //statistics
                    //persistence
                });
                
                app.MapWhen(m => m.Request.Method == "GET", config => {
                    //statistics
                    //persistence
                });

            });

            builder.Map("/searchingthings", app => { });
        }
    }
}
