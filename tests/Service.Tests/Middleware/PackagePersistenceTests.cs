using FluentAssertions;
using NuGet.Feed.Service.Middleware;
using System;
using Xunit;

namespace NuGet.Feed.Service.Tests.Middleware
{
    public class PackagePersistenceTests
    {
        [Fact]
        public void GivenNullContext_WhenInvoked_ThenThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => { new PackagePersistence(null).Invoke(null); })
                .ParamName.Should().Be("context");
        }
    }
}
