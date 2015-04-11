using Microsoft.Owin;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NuGet.Feed.Service.Middleware
{
    public class PackagePersistence : OwinMiddleware
    {
        private IMakePackages _packageMaker;
        private IFileSystem _fileSystem;

        public PackagePersistence(OwinMiddleware next, IMakePackages packageMaker, IFileSystem fileSystem) : base(next) {
            _packageMaker = packageMaker;
            _fileSystem = fileSystem;
        }

        public override Task Invoke(IOwinContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var package = _packageMaker.Make(context.Request.Body, context.Request.ContentType);

            if (package == null)
                context.Response.StatusCode = 400;

            if (_fileSystem.DirectoryExists(package?.Location))
                context.Response.StatusCode = 500;

            return Next.Invoke(context);
        }
    }

    public interface IMakePackages
    {
        IPackage Make(Stream stream, string boundary);
    }

    public interface IFileSystem
    {
        bool DirectoryExists(string path);
    }

    public interface IPackage
    {
        string Location { get; }
    }
}
