using NuGet.Feed.Service;
using Owin;

namespace NuGet.Feed.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage();
            app.UseNuGetApiCheck();
        }
    }
}
