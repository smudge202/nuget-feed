﻿using Microsoft.Owin;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NuGet.Feed.Service.Middleware
{
    public class ApiKeyCheck : OwinMiddleware
    {
        private const string _key = "12345";

        public ApiKeyCheck(OwinMiddleware next) : base(next) { }

        public override Task Invoke(IOwinContext context)
        {
			if (context == null) throw new ArgumentNullException(nameof(context));
            if (context.Request.Headers.Single(x => x.Key.ToUpperInvariant() == "X-NUGET-APIKEY").Value.Single() != _key)
                context.Response.StatusCode = 401;
            return Next.Invoke(context);
        }
    }
}
