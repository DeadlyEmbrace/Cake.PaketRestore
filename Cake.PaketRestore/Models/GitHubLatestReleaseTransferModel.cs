using Newtonsoft.Json;

namespace Cake.PaketRestore.Models
{
    /// <summary>
    /// Github latest release model wrapper.
    /// <see>
    ///     <cref>https://developer.github.com/v3/repos/releases/#get-the-latest-release</cref>
    /// </see>
    /// </summary>
    public class GitHubLatestReleaseTransferModel
    {
        #region Properties

        /// <summary>
        /// Get or Set array of Assests associated with the release
        /// </summary>
        [JsonProperty("assets")]
        public GitHubAssetsTransferModel[] GitHubAssetsTransferModel { get; set; }

        /// <summary>
        /// Get or Set release status as a pre-release
        /// </summary>
        [JsonProperty("prerelease")]
        public bool PreRelease { get; set; }

        /// <summary>
        /// Gets or Sets the tag for the releae
        /// </summary>
        [JsonProperty("tag_name")]
        public string TagName { get; set; }

        #endregion
    }
}