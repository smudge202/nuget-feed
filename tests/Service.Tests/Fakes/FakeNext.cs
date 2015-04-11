using Microsoft.Owin;
using System.Threading.Tasks;

namespace NuGet.Feed.Service.Tests.Fakes
{
    class FakeNext : OwinMiddleware
    {
        public FakeNext(OwinMiddleware next) : base(next) { }
        public override Task Invoke(IOwinContext context)
        {
            return new Task(() => { return; });
        }
    }
}
