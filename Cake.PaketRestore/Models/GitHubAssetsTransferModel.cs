using Newtonsoft.Json;

namespace Cake.PaketRestore.Models
{
    /// <summary>
    /// Github asset model wrapper
    /// </summary>
    public class GitHubAssetsTransferModel
    {
        #region Properties

        /// <summary>
        /// Get or Set the url where the asset can be downloaded from
        /// </summary>
        [JsonProperty("browser_download_url")]
        public string BrowserUrl { get; set; }

        /// <summary>
        /// Get or Set the name of the asset
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        #endregion
    }
}