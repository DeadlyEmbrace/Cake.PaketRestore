using Cake.PaketRestore.Attributes;
using Cake.PaketRestore.Interfaces;

namespace Cake.PaketRestore
{
    /// <summary>
    /// Setting for use with Paket Update.
    /// <see>
    ///     <cref>https://fsprojects.github.io/Paket/paket-update.html</cref>
    /// </see>
    /// </summary>
    public class PaketUpdateSettings : IRestoreSetting
    {
        #region Properties

        /// <summary>
        /// Removes all binding redirects that are not specified
        /// by Paket.
        /// </summary>
        [SwitchArgument("--clean-redirects", 6)]
        public bool CleanRedirects { get; set; }

        /// <summary>
        /// Creates binding redirect files if needed.
        /// </summary>
        [SwitchArgument("--createnewbindingfiles", 5)]
        public bool CreateNewBindingFiles { get; set; }

        /// <summary>
        /// Treat the nuget parameter as a regex to filter
        /// packages rather than an exact match.
        /// </summary>
        [SwitchArgument("--filter", 11)]
        public bool Filter { get; set; }

        /// <summary>
        /// Forces the download and reinstallation of all
        /// packages.
        /// </summary>
        [SwitchArgument("--force", 3)]
        public bool Force { get; set; }

        /// <summary>
        /// GitHub OAuthToken - Used to increase Rate Limit when used on a shared build system.
        /// Has no effect if <see cref="IRestoreSetting.RetrieveBootstrapper"/> is not used.
        /// Token should be created <see>
        ///         <cref>https://github.com/settings/tokens/new</cref>
        ///     </see> and require no scopes
        /// </summary>
        public string GitHubOAuthToken { get; set; }

        /// <summary>
        /// Allows to specify the dependency group.
        /// </summary>
        [StringArgument("group", 1)]
        public string Group { get; set; }

        /// <summary>
        ///  Allows only updates that are not changing the major
        /// version of the NuGet packages.
        /// </summary>
        [SwitchArgument("--keep-major", 8)]
        public bool KeepMajor { get; set; }

        /// <summary>
        /// Allows only updates that are not changing the minor
        /// version of the NuGet packages.
        /// </summary>
        [SwitchArgument("--keep-minor", 9)]
        public bool KeepMinor { get; set; }

        /// <summary>
        /// Allows only updates that are not changing the patch
        /// version of the NuGet packages.
        /// </summary>
        [SwitchArgument("--keep-patch", 10)]
        public bool KeepPatch { get; set; }

        /// <summary>
        /// Specify a log file for the paket process.
        /// </summary>
        [StringArgument("--log-file", 14)]
        public string LogFile { get; set; }

        /// <summary>
        /// Skips paket install process (patching of csproj,
        /// fsproj, ... files) after the generation of paket.lock
        /// file.
        /// </summary>
        [SwitchArgument("--no-install", 7)]
        public bool NoInstall { get; set; }

        /// <summary>
        /// NuGet package id.
        /// </summary>
        [StringArgument("nuget", 0)]
        public string Nuget { get; set; }

        /// <summary>
        /// Creates binding redirects for the NuGet packages.
        /// </summary>
        [SwitchArgument("--redirects", 4)]
        public bool Redirects { get; set; }

        /// <summary>
        /// Should the Paket Bootstrapper be retrieved
        /// </summary>
        public bool RetrieveBootstrapper { get; set; }

        /// <summary>
        /// Should the Paket executable be retrieved
        /// </summary>
        public bool RetrievePaketExecutable { get; set; }

        /// <summary>
        /// Suppress console output for the paket process.
        /// </summary>
        [StringArgument("--silent", 15)]
        public bool Silent { get; set; }

        /// <summary>
        /// Touches project files referencing packages which are
        /// affected, to help incremental build tools detecting
        /// the change.
        /// </summary>
        [SwitchArgument("--touch-affected-refs", 12)]
        public bool TouchAffectedRefs { get; set; }

        /// <summary>
        /// Enable verbose console output for the paket process.
        /// </summary>
        [SwitchArgument("--verbose", 13)]
        public bool Verbose { get; set; }

        /// <summary>
        /// Allows to specify version of the package.
        /// </summary>
        [StringArgument("version", 1)]
        public string Version { get; set; }

        #endregion
    }
}