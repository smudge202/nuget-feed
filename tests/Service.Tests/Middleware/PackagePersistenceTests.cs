using FluentAssertions;
using Microsoft.Owin;
using Moq;
using NuGet.Feed.Service.Middleware;
using System;
using Xunit;
using NuGet.Feed.Service.Tests.Fakes;

namespace NuGet.Feed.Service.Tests.Middleware
{
    public class PackagePersistenceTests
    {
        private FakeNext _nextMock;
        private Mock<IMakePackages> _packageMaker;
        private Mock<IFileSystem> _fileSystem;
        private Mock<IHeaderDictionary> _headerMock;
        private Mock<IOwinRequest> _requestMock;
        private Mock<IOwinResponse> _responseMock;
        private Mock<IOwinContext> _contextMock;
        
        public PackagePersistenceTests()
        {
            _nextMock = new FakeNext(null);

            _packageMaker = new Mock<IMakePackages>();
            _fileSystem = new Mock<IFileSystem>();

            _headerMock = new Mock<IHeaderDictionary>();
            _headerMock.SetupAllProperties();

            _requestMock = new Mock<IOwinRequest>();
            _requestMock.SetupAllProperties();

            _responseMock = new Mock<IOwinResponse>();
            _responseMock.SetupAllProperties();

            _contextMock = new Mock<IOwinContext>();

            _contextMock.SetupGet(s => s.Request).Returns(_requestMock.Object);
            _contextMock.SetupGet(s => s.Response).Returns(_responseMock.Object);
            _requestMock.SetupGet(s => s.Headers).Returns(_headerMock.Object);
        }

        [Fact]
        public void GivenNullContext_WhenInvoked_ThenThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => { new PackagePersistence(null, _packageMaker.Object, _fileSystem.Object).Invoke(null); })
                .ParamName.Should().Be("context");
        }

        [Fact]
        public void GivenStreamWithNoFiles_WhenInvoked_ThenSetTheResponseCodeTo400()
        {
            new PackagePersistence(_nextMock, _packageMaker.Object, _fileSystem.Object).Invoke(_contextMock.Object);
            _contextMock.Object.Response.StatusCode.Should().Be(400);
        }

        [Fact]
        public void GivenPackageAlreadyUploaded_WhenInvoked_ThenSetTheResponseCodeTo500()
        {
            SetupMockWithPackage();
            _fileSystem.Setup(s => s.DirectoryExists(null)).Returns(true);

            new PackagePersistence(_nextMock, _packageMaker.Object, _fileSystem.Object).Invoke(_contextMock.Object);
            _contextMock.Object.Response.StatusCode.Should().Be(500);
        }

        private void SetupMockWithPackage()
        {
            _headerMock.Setup(s => s.Values).Returns(FakeRequest.HeaderValues());
            _headerMock.Setup(s => s.Keys).Returns(FakeRequest.KeyValues());
            _requestMock.SetupProperty(s => s.Body, FakeRequest.Body());
        }
    }
}
