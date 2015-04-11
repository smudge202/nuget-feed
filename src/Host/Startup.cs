using Owin;

namespace NuGet.Feed.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseErrorPage();

            builder.MapWhen(m => m.Request.Method == "PUT", app =>
            {
                app.Use<Service.Middleware.ApiKeyCheck>();

                app.Run(context =>
                {
                    context.Response.ContentType = "text/plain";
                    return context.Response.WriteAsync("PUT something");
                });
            });

            builder.Run(context =>
            {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("Hello, world.");
            });

        }
    }
}
