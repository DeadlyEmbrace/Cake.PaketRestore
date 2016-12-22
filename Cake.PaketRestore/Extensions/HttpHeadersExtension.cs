using Cake.PaketRestore.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Cake.PaketRestore.Extensions
{
    /// <summary>
    /// Extension methods for HttpHeaders
    /// </summary>
    public static class HttpHeadersExtension
    {
        #region Public Methods

        /// <summary>
        /// Check if GitHub has applied rate limiting to our call
        /// </summary>
        /// <param name="headers">Headers returned alongside API call</param>
        /// <param name="log">Cake log</param>
        /// <returns>True - We have been rate limited</returns>
        public static bool HasGitHubRateLimitedUs(this HttpHeaders headers, IRetrieverLog log)
        {
            var result = false;
            IEnumerable<string> rateLimit;
            IEnumerable<string> remainingRate;

            if (headers.TryGetValues("X-RateLimit-Limit", out rateLimit)
                && headers.TryGetValues("X-RateLimit-Remaining", out remainingRate))
            {
                var limit = int.Parse(rateLimit.FirstOrDefault() ?? "0");
                var remaining = int.Parse(remainingRate.FirstOrDefault() ?? "0");

                log.Information($"GitHub API Rate Limit: {limit} per hour");

                if (remaining <= 0)
                {
                    log.Warning("GitHub API has been rate limited\r\n" +
                        "- If you haven't done so already please provide a token to up your limit");
                    result = true;
                }
                else
                {
                    log.Information($"GitHub API Remaining before limit: {remaining}");
                }
            }
            else
            {
                log.Warning("There were no Rate Limit headers");
                result = true;
            }

            return result;
        }

        #endregion
    }
}