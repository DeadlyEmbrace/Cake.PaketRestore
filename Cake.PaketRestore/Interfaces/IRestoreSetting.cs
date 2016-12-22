namespace Cake.PaketRestore.Interfaces
{
    /// <summary>
    /// Interface for Setting classes that specify whether or not Paket Bootstrapper and executable should be retrieved
    /// </summary>
    public interface IRestoreSetting
    {
        #region Properties

        /// <summary>
        /// GitHub OAuthToken - Used to increase Rate Limit when used on a shared build system.
        /// Has no effect if <see cref="RetrieveBootstrapper"/> is not used.
        /// Token should be created <see>
        ///         <cref>https://github.com/settings/tokens/new</cref>
        ///     </see> and require no scopes
        /// </summary>
        string GitHubOAuthToken { get; set; }

        /// <summary>
        /// Should the Paket Bootstrapper be retrieved
        /// </summary>
        bool RetrieveBootstrapper { get; set; }

        /// <summary>
        /// Should the Paket executable be retrieved
        /// </summary>
        bool RetrievePaketExecutable { get; set; }

        #endregion
    }
}