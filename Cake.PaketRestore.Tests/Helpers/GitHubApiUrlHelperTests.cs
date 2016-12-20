using Cake.PaketRestore.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace Cake.PaketRestore.Tests.Helpers
{
    public class GitHubApiUrlHelperTests
    {
        #region Public Methods

        [Test]
        public void LatestReleaseUrlIsConstructedCorrectly()
        {
            // arrange
            const string owner = "NinetailLabs";
            const string repo = "VaraniumSharp";
            var expectedUrl = $"https://api.github.com/repos/{owner}/{repo}/releases/latest";
            var parts = new GitHubUrlPathParts();
            var sut = new GitHubApiUrlHelper(parts);

            // act
            var retrievalUrl = sut.LatestReleaseUrl(owner, repo);

            // asset
            retrievalUrl.Should().Be(expectedUrl);
        }

        #endregion
    }
}