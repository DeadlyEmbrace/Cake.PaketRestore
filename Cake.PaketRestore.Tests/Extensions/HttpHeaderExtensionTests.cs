using Cake.PaketRestore.Extensions;
using Cake.PaketRestore.Tests.Helpers;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Cake.PaketRestore.Tests.Extensions
{
    public class HttpHeaderExtensionTests
    {
        #region Public Methods

        [Test]
        public void CorrectlyRespondIfNotRateLimited()
        {
            //arrange
            var logDummy = new RetrieverLogFixture();

            var headers = new HttpClient();
            var httpResponse = headers.GetAsync("http://test.com").Result;

            httpResponse.Headers.Add("X-RateLimit-Limit", new List<string> { "2" });
            httpResponse.Headers.Add("X-RateLimit-Remaining", new List<string> { "1" });

            // act
            var result = httpResponse.Headers.HasGitHubRateLimitedUs(logDummy);

            // assert
            var logMessages = logDummy.LoggedMessages;

            result.Should().BeFalse();
            logMessages.First().MessageTemplate.Should().Be("GitHub API Rate Limit: 2 per hour");
            logMessages.Last().MessageTemplate.Should().Be("GitHub API Remaining before limit: 1");
        }

        [Test]
        public void CorrectlyRespondIfRateLimited()
        {
            //arrange
            var logDummy = new RetrieverLogFixture();

            var headers = new HttpClient();
            var httpResponse = headers.GetAsync("http://test.com").Result;

            httpResponse.Headers.Add("X-RateLimit-Limit", new List<string> { "2" });
            httpResponse.Headers.Add("X-RateLimit-Remaining", new List<string> { "0" });

            // act
            var result = httpResponse.Headers.HasGitHubRateLimitedUs(logDummy);

            // assert
            var lastMessage = logDummy.LoggedMessages.Last();

            result.Should().BeTrue();
            lastMessage.MessageTemplate.Should()
                .Be(
                    "GitHub API has been rate limited\r\n- If you haven't done so already please provide a token to up your limit");
            lastMessage.Type.Should().Be(LogType.Warning);
        }

        [Test]
        public void NoHeadersRespondAsThoughRateLimited()
        {
            // arrange
            var logDummy = new RetrieverLogFixture();

            var headers = new HttpClient();
            var httpResponse = headers.GetAsync("http://test.com").Result;

            // act
            var result = httpResponse.Headers.HasGitHubRateLimitedUs(logDummy);

            // assert
            result.Should().BeTrue();
            logDummy.LoggedMessages.Last().MessageTemplate.Should().Be("There were no Rate Limit headers");
            logDummy.LoggedMessages.Last().Type.Should().Be(LogType.Warning);
        }

        #endregion
    }
}