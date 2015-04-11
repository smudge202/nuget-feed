using Microsoft.Owin;
using System;
using System.Threading.Tasks;

namespace NuGet.Feed.Service.Middleware
{
    public class PackagePersistence : OwinMiddleware
    {
        public PackagePersistence(OwinMiddleware next) : base(next) { }

        public override Task Invoke(IOwinContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            return Next.Invoke(context);
        }
    }
}
