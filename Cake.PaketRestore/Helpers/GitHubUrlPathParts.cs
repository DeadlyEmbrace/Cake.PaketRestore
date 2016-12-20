namespace Cake.PaketRestore.Helpers
{
    /// <summary>
    /// Contains the parts required to build GitHub api URLs
    /// </summary>
    public class GitHubUrlPathParts
    {
        #region Constructor

        /// <summary>
        /// Parameterless Constructor
        /// </summary>
        public GitHubUrlPathParts()
        {
            Base = "https://api.github.com";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Base URL path
        /// </summary>
        public string Base { get; set; }

        /// <summary>
        /// Latest path part
        /// </summary>
        public string Latest => "latest";

        /// <summary>
        /// Releases path pat
        /// </summary>
        public string Releases => "releases";

        /// <summary>
        /// Repos path part
        /// </summary>
        public string Repos => "repos";

        #endregion
    }
}