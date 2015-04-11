using FluentAssertions;
using System;
using Xunit;

namespace NuGet.Feed.Service.Middleware.Tests
{
	public class ApiKeyCheckTests
	{
		[Fact]
		public void WhenContextNotProvidedThenThrowsException()
		{
			Action act = () => new ApiKeyCheck(null).Invoke(null);
			act.ShouldThrow<ArgumentNullException>();
		}
	}
}
