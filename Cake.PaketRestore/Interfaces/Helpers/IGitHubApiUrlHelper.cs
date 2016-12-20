namespace Cake.PaketRestore.Interfaces.Helpers
{
    /// <summary>
    /// Assist with building of GitHub API Urls
    /// </summary>
    public interface IGitHubApiUrlHelper
    {
        #region Public Methods

        /// <summary>
        /// Get the URL pointing to the latest release for a specific GitHub repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The repository</param>
        /// <returns></returns>
        string LatestReleaseUrl(string owner, string repo);

        #endregion
    }
}