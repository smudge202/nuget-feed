using Microsoft.Owin.Hosting;
using System;

namespace NuGet.Feed.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:9000/")) {
                Console.ReadKey();
            }
        }
    }
}
