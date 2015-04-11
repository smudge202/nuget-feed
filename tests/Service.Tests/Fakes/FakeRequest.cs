using System.Collections.Generic;
using System.IO;

namespace NuGet.Feed.Service.Tests.Fakes
{
    public static class FakeRequest
    {
        public static ICollection<string[]> HeaderValues()
        {
            return new List<string[]> {
                new[] { "Something" },
                new[] { "4775" },
                new[] { "multipart/form-date; boundary=---------------------------41184676334" },
                new[] { "http://localhost:9000" },
                new[] { "Test Projext" }
            };
        }

        public static ICollection<string> KeyValues()
        {
            return new List<string> {
                "X-NuGet-ApiKey",
                "Content-Length",
                "Content-Type",
                "Host",
                "User-Agent"
            };
        }

        public static Stream Body()
        {
            var value = @"-----------------------------8d242a8111686d8
            Something goes in here
            -----------------------------8d242a8111686d8--";

            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(value);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
