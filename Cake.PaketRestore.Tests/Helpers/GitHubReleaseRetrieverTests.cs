using Cake.PaketRestore.Helpers;
using Cake.PaketRestore.Tests.Data;
using Cake.PaketRestore.Tests.HelperExtensions;
using FluentAssertions;
using HttpMock;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace Cake.PaketRestore.Tests.Helpers
{
    public class GitHubReleaseRetrieverTests
    {
        #region Public Methods

        [Test]
        public void DownloadDirectoryIsCreatedIfItDoesNotExist()
        {
            // arrange
            var fixture = new GitHutReleaseRetrieverFixture(string.Empty);
            var directory = Guid.NewGuid().ToString();
            const string fileName = "TestFile.txt";
            const string urlPath = "/TestFile.txt";
            fixture.HttpMock.Stub(t => t.Get(urlPath))
                .ReturnFile(Path.Combine(fixture.ExecutionPath, "Data", fileName))
                .OK();
            var sut = fixture.GetInstance;

            // act
            var result = sut.DownloadFileAsync(fixture.BasePath + urlPath, Path.Combine(fixture.ExecutionPath, directory), fileName).Result;

            // assert
            result.Should().BeTrue();
            Directory.Exists(Path.Combine(fixture.ExecutionPath, directory)).Should().BeTrue();
            File.Exists(Path.Combine(fixture.ExecutionPath, directory, fileName)).Should().BeTrue();

            DirectoryHelper.DeleteDirectory(directory);
        }

        [Test]
        public void IfDownloadFailsFalseIsReturned()
        {
            // arrange
            var fixture = new GitHutReleaseRetrieverFixture(string.Empty);
            var directory = Guid.NewGuid().ToString();
            const string fileName = "TestFile.txt";
            const string urlPath = "/TestFile.txt";
            fixture.HttpMock.Stub(t => t.Get(urlPath))
                .ReturnFile(Path.Combine(fixture.ExecutionPath, "Data", fileName))
                .WithStatus(HttpStatusCode.BadGateway);
            var sut = fixture.GetInstance;

            // act
            var result = sut.DownloadFileAsync(fixture.BasePath + urlPath, Path.Combine(fixture.ExecutionPath, directory), fileName).Result;

            // assert
            result.Should().BeFalse();
            var loggedMessage = fixture.LogDummy.LoggedMessages.Last();
            loggedMessage.MessageTemplate.Should().Be("An error occured while retrieving the asset");
            loggedMessage.MessageException.Should().NotBeNull();

            DirectoryHelper.DeleteDirectory(directory);
        }

        [Test]
        public void RetrievingReleaseUrlReturnsAnError()
        {
            // arrange
            const string assetName = "test.exe";
            var fixture = new GitHutReleaseRetrieverFixture(assetName);
            fixture.HttpMock.Stub(t => t.Get($"/repos/{fixture.Owner}/{fixture.Repo}/releases/latest"))
                .NotFound();
            var sut = fixture.GetInstance;

            // act
            var response = sut.GetLatestReleaseUrlAsync(fixture.Owner, fixture.Repo, assetName).Result;

            // assert
            var logMessage = fixture.LogDummy.LoggedMessages.First();
            logMessage.MessageTemplate.Should()
                .Be("Error occured while looking up latest details. Server responded with {StatusCode} - {Reason}");
            logMessage.MessageArguments.First().Should().Be("404");
            logMessage.MessageArguments.Last().Should().Be(HttpStatusCode.NotFound.ToString());
            response.Should().Be(string.Empty);
        }

        [Test]
        public void RetrievingReleaseUrlReturnsEmptyResponseIfAssetCouldNotBeFound()
        {
            // arrange
            const string assetName = "text.exe";
            var fixture = new GitHutReleaseRetrieverFixture(assetName);
            fixture.HttpMock.Stub(t => t.Get($"/repos/{fixture.Owner}/{fixture.Repo}/releases/latest"))
                .Return(ValidResponseData.GetValidResponseString)
                .OK();
            var sut = fixture.GetInstance;

            // act
            var response = sut.GetLatestReleaseUrlAsync(fixture.Owner, fixture.Repo, assetName).Result;

            // assert
            fixture.LogDummy.LoggedMessages.First().MessageTemplate.Should().Be("Cannot find requested asset in the response");
            response.Should().Be(string.Empty);
        }

        [Test]
        public void RetrievingReleaseUrlReturnsValidResponse()
        {
            // arrange
            const string assetName = "MAL.Net.1.0.0.3.zip";
            const string expectedUrl = "https://github.com/NinetailLabs/MAL.Net---A-.net-API-for-MAL/releases/download/v1.0.0.3/MAL.Net.1.0.0.3.zip";
            var fixture = new GitHutReleaseRetrieverFixture(assetName);
            fixture.HttpMock.Stub(t => t.Get($"/repos/{fixture.Owner}/{fixture.Repo}/releases/latest"))
                .Return(ValidResponseData.GetValidResponseString)
                .OK();
            var sut = fixture.GetInstance;

            // act
            var response = sut.GetLatestReleaseUrlAsync(fixture.Owner, fixture.Repo, assetName).Result;

            // assert
            response.Should().Be(expectedUrl);
        }

        #endregion

        public class GitHutReleaseRetrieverFixture
        {
            #region Constructor

            public GitHutReleaseRetrieverFixture(string assetName)
            {
                AssetName = assetName;
                HttpMock = HttpMockRepository.At(BasePath);
                PathPartMock = new GitHubUrlPathParts
                {
                    Base = BasePath
                };
                UrlHelper = new GitHubApiUrlHelper(PathPartMock);
                LogDummy = new RetrieverLogFixture();
                GetInstance = new GitHubReleaseRetriever(UrlHelper, LogDummy);
            }

            #endregion

            #region Properties

            public string AssetName { get; }
            public string BasePath => "http://localhost:8899";
            public string ExecutionPath => AppDomain.CurrentDomain.BaseDirectory;
            public GitHubReleaseRetriever GetInstance { get; }
            public IHttpServer HttpMock { get; }
            public RetrieverLogFixture LogDummy { get; }
            public string Owner => "NinetailLabs";
            public GitHubUrlPathParts PathPartMock { get; }
            public string Repo => "VaraniumSharp";
            public GitHubApiUrlHelper UrlHelper { get; }

            #endregion
        }
    }
}