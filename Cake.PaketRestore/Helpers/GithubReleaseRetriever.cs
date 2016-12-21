using Cake.PaketRestore.Interfaces;
using Cake.PaketRestore.Interfaces.Helpers;
using Cake.PaketRestore.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cake.PaketRestore.Helpers
{
    /// <summary>
    /// Easily retrieve the latest version of an asset from a GitHub repository
    /// </summary>
    public class GitHubReleaseRetriever
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gitHubApiUrlHelper"></param>
        public GitHubReleaseRetriever(IGitHubApiUrlHelper gitHubApiUrlHelper)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Cake.PaketRestore");
            _gitHubApiUrlHelper = gitHubApiUrlHelper;
        }

        /// <summary>
        /// Constructor used to pass in a <see cref="IRetrieverLog"/> instance.
        /// This allows logging of progress and errors
        /// </summary>
        /// <param name="gitHubApiUrlHelper">Class implementing <see cref="IGitHubApiUrlHelper"/></param>
        /// <param name="log">Instance of a class implementing <see cref="IRetrieverLog"/></param>
        public GitHubReleaseRetriever(IGitHubApiUrlHelper gitHubApiUrlHelper, IRetrieverLog log)
            : this(gitHubApiUrlHelper)
        {
            _log = log;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Downloads a file from a URL
        /// </summary>
        /// <param name="url">URL from which the file should be retrieved</param>
        /// <param name="outputDirectory">Directory where the file should be saved</param>
        /// <param name="filename">The name under which the file will be saved</param>
        /// <returns>False - An error occured during download. True - File retrieved successfully</returns>
        public async Task<bool> DownloadFileAsync(string url, string outputDirectory, string filename)
        {
            if (!Directory.Exists(outputDirectory))
            {
                _log?.Information("Created {Directory}", outputDirectory);
                Directory.CreateDirectory(outputDirectory);
            }

            try
            {
                using (var file = File.Create(Path.Combine(outputDirectory, filename)))
                {
                    var response = await _httpClient.GetStreamAsync(url);
                    await response.CopyToAsync(file);
                    return true;
                }
            }
            catch (Exception exception)
            {
                _log?.Error(exception, "An error occured while retrieving the asset");
                return false;
            }
        }

        /// <summary>
        /// Retrieve the URL for the latest release of an asset on GitHub
        /// </summary>
        /// <param name="owner">Owner of the repository</param>
        /// <param name="repo">Repository name</param>
        /// <param name="assetName">Name of the asset to download</param>
        /// <returns></returns>
        public async Task<string> GetLatestReleaseUrlAsync(string owner, string repo, string assetName)
        {
            var fullUri = _gitHubApiUrlHelper.LatestReleaseUrl(owner, repo);
            var response = await _httpClient.GetAsync(fullUri);
            if (!response.IsSuccessStatusCode)
            {
                _log?.Error(
                    "Error occured while looking up latest details. Server responded with {0} - {1}",
                    ((int)response.StatusCode).ToString(), response.ReasonPhrase);
                return string.Empty;
            }

            var data = await response.Content.ReadAsStringAsync();
            var parsedData = JsonConvert.DeserializeObject<GitHubLatestReleaseTransferModel>(data);
            var bootStrapperUrl = parsedData.GitHubAssetsTransferModel.FirstOrDefault(x => x.Name == assetName)?.BrowserUrl;
            if (!string.IsNullOrEmpty(bootStrapperUrl))
            {
                return bootStrapperUrl;
            }

            _log?.Error("Cannot find requested asset in the response");
            return string.Empty;
        }

        #endregion

        #region Variables

        private readonly IGitHubApiUrlHelper _gitHubApiUrlHelper;

        private readonly HttpClient _httpClient;
        private readonly IRetrieverLog _log;

        #endregion
    }
}