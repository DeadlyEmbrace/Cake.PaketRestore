using Cake.PaketRestore.Interfaces.Helpers;

namespace Cake.PaketRestore.Helpers
{
    /// <summary>
    /// Builds GitHub API urls
    /// </summary>
    public class GitHubApiUrlHelper : IGitHubApiUrlHelper
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parts">Instance of class containing GitHub path parts</param>
        public GitHubApiUrlHelper(GitHubUrlPathParts parts)
        {
            _parts = parts;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the URL pointing to the latest release for a specific GitHub repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The repository</param>
        /// <returns></returns>
        public string LatestReleaseUrl(string owner, string repo) => $"{_parts.Base}/{_parts.Repos}/{owner}/{repo}/{_parts.Releases}/{_parts.Latest}";

        #endregion

        #region Variables

        private readonly GitHubUrlPathParts _parts;

        #endregion
    }
}