using Owin;

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
