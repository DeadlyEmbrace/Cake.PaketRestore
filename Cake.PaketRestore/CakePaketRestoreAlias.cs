using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.PaketRestore.Extensions;
using Cake.PaketRestore.Helpers;
using Cake.PaketRestore.Interfaces;
using System.IO;
using Path = System.IO.Path;

namespace Cake.PaketRestore
{
    /// <summary>
    /// Cater for usage of Paket functionality
    /// </summary>
    [CakeAliasCategory("Paket Restore")]
    public static class CakePaketRestoreAlias
    {
        #region Properties

        /// <summary>
        /// Base URL of the Github API
        /// </summary>
        public static string GithubUrlPath { private get; set; } = "https://api.github.com";

        #endregion

        #region Public Methods

        /// <summary>
        /// Execute a Paket Restore
        /// </summary>
        /// <param name="context">Cake context</param>
        /// <param name="paketDirectory">Location of the .paket folder</param>
        [CakeMethodAlias]
        public static void PaketRestore(this ICakeContext context, DirectoryPath paketDirectory)
        {
            context.PaketRestore(paketDirectory, new PaketRestoreSettings());
        }

        /// <summary>
        /// Execute a Paket Restore
        /// </summary>
        /// <param name="context">Cake context</param>
        /// <param name="paketDirectory">Location of the .paket folder</param>
        /// <param name="paketRestoreSettings">Paket restore settings</param>
        [CakeMethodAlias]
        public static void PaketRestore(this ICakeContext context, DirectoryPath paketDirectory,
            PaketRestoreSettings paketRestoreSettings)
        {
            context.PaketExecutableRetrieval(paketDirectory, paketRestoreSettings);

            var paketPath = FilePath.FromString(Path.Combine(paketDirectory.FullPath, Paket));

            var runner = context.ProcessRunner;
            var arguments = new ProcessArgumentBuilder();
            arguments.Append("restore");
            arguments.BuildArgumentString(paketRestoreSettings);
            context.Log.Information(arguments.RenderSafe());

            var process = runner.Start(paketPath, new ProcessSettings
            {
                Arguments = arguments
            });

            process.WaitForExit();

            var exitCode = process.GetExitCode();
            if (exitCode > 0)
            {
                throw new CakeException("Error occured during Paket restore");
            }
        }

        /// <summary>
        /// Execute a Paket Update
        /// </summary>
        /// <param name="context">Cake context</param>
        /// <param name="paketDirectory">Location of the .paket folder</param>
        [CakeMethodAlias]
        public static void PaketUpdate(this ICakeContext context, DirectoryPath paketDirectory)
        {
            context.PaketUpdate(paketDirectory, new PaketUpdateSettings());
        }

        /// <summary>
        /// Execute a Paket Update
        /// </summary>
        /// <param name="context">Cake context</param>
        /// <param name="paketDirectory">Location of the .paket folder</param>
        /// <param name="paketUpdateSettings">Paket update settings</param>
        [CakeMethodAlias]
        public static void PaketUpdate(this ICakeContext context, DirectoryPath paketDirectory,
            PaketUpdateSettings paketUpdateSettings)
        {
            context.PaketExecutableRetrieval(paketDirectory, paketUpdateSettings);

            var paketPath = FilePath.FromString(Path.Combine(paketDirectory.FullPath, Paket));

            var runner = context.ProcessRunner;
            var arguments = new ProcessArgumentBuilder();
            arguments.Append("update");
            arguments.BuildArgumentString(paketUpdateSettings);
            context.Log.Information(arguments.RenderSafe());

            var process = runner.Start(paketPath, new ProcessSettings()
            {
                Arguments = arguments
            });

            process.WaitForExit();

            var exitCode = process.GetExitCode();
            if (exitCode > 0)
            {
                throw new CakeException("Error occured during Paket restore");
            }
        }

        /// <summary>
        /// Check if the Paket Bootstrapper exists and retrieve the latest version from GitHub if it doesn't
        /// </summary>
        /// <param name="context">Cake context</param>
        /// <param name="paketDirectory">Location of the .paket folder</param>
        [CakeMethodAlias]
        public static void RetrievePaketBootloader(this ICakeContext context, DirectoryPath paketDirectory)
        {
            var urlPartHelper = new GitHubUrlPathParts
            {
                Base = GithubUrlPath
            };

            var urlHelper = new GitHubApiUrlHelper(urlPartHelper);
            var retrieverLog = new CakeRetrieverLog(context.Log);
            var releaseRetriever = new GitHubReleaseRetriever(urlHelper, retrieverLog);

            paketDirectory.CheckAndCreateDirectory(context.Log);
            //if (context.FileSystem.GetFile(new FilePath(Path.Combine(paketDirectory.FullPath, PaketAsset))).Exists)
            if (File.Exists(Path.Combine(paketDirectory.FullPath, PaketAsset)))
            {
                context.Log.Information("Paket Bootstrapper already exists - skipping download");
                return;
            }

            var latestUrl = releaseRetriever.GetLatestReleaseUrlAsync(PaketOwner, PaketRepo, PaketAsset).Result;
            if (string.IsNullOrEmpty(latestUrl))
            {
                context.Log.Error("Failed to retrieve link for latest Paket BootStrapper");
                throw new CakeException("Failed to retrieve link for latest Paket Bootstrapper");
            }

            var result = releaseRetriever.DownloadFileAsync(latestUrl, paketDirectory.FullPath, PaketAsset).Result;
            if (!result)
            {
                context.Log.Error("An error occured while trying to retrieve the Paket Bootstrapper");
                throw new CakeException("Error occured while trying to retrieve the Paket Bootstrapper");
            }

            context.Log.Information("Completed retrieval of the Paket Bootstrapper");
        }

        /// <summary>
        /// Use the Paket Bootstrapper to retrieve or update the Paket executable
        /// </summary>
        /// <param name="context">Cake context</param>
        /// <param name="paketDirectory">Location of the .paket folder</param>
        [CakeMethodAlias]
        public static void RetrievePaketExecutable(this ICakeContext context, DirectoryPath paketDirectory)
        {
            var paketPath = FilePath.FromString(Path.Combine(paketDirectory.FullPath, PaketAsset));

            var runner = context.ProcessRunner;
            var process = runner.Start(paketPath);
            process.WaitForExit();
            var exitCode = process.GetExitCode();

            if (exitCode > 0)
            {
                throw new CakeException("Error occured during Paket boostrapper execution");
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Wraps the PaketBootloader retrieval and Paket.exe retrieval into a single method
        /// </summary>
        /// <param name="context">Cake context</param>
        /// <param name="paketDirectory">Location of the .paket folder</param>
        /// <param name="settings">Settings that implement the IRestoreSetting interface</param>
        private static void PaketExecutableRetrieval(this ICakeContext context, DirectoryPath paketDirectory, IRestoreSetting settings)
        {
            if (settings.RetrieveBootstrapper)
            {
                context.RetrievePaketBootloader(paketDirectory);
            }
            if (settings.RetrievePaketExecutable)
            {
                context.RetrievePaketExecutable(paketDirectory);
            }
        }

        #endregion

        #region Variables

        private const string PaketOwner = "fsprojects";
        private const string PaketRepo = "Paket";
        private const string PaketAsset = "paket.bootstrapper.exe";
        private const string Paket = "paket.exe";

        #endregion
    }
}