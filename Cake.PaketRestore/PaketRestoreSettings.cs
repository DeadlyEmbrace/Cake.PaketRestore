using Cake.Core.IO;
using Cake.PaketRestore.Attributes;
using Cake.PaketRestore.Interfaces;
using System.Collections.Generic;

namespace Cake.PaketRestore
{
    /// <summary>
    /// Settings for use with Paket Restore
    /// <see>
    ///     <cref>https://fsprojects.github.io/Paket/paket-restore.html</cref>
    /// </see>
    /// </summary>
    public class PaketRestoreSettings : IRestoreSetting
    {
        #region Properties

        /// <summary>
        /// Causes the restore to fail if any of the checks fail.
        /// </summary>
        [SwitchArgument("--fail-on-checks", 4)]
        public bool FailOnChecks { get; set; }

        /// <summary>
        /// Forces the download of all packages.
        /// </summary>
        [SwitchArgument("--force", 0)]
        public bool Force { get; set; }

        /// <summary>
        /// Allows to restore a single group.
        /// </summary>
        [StringArgument("group", 5)]
        public string Group { get; set; }

        /// <summary>
        /// Skips the test if paket.dependencies and paket.lock
        /// are in sync.
        /// </summary>
        [SwitchArgument("ignore-checks", 3)]
        public bool IgnoreChecks { get; set; }

        /// <summary>
        /// Specify a log file for the paket process.
        /// </summary>
        [SwitchArgument("--log-file", 9)]
        public FilePath LogFile { get; set; }

        /// <summary>
        /// Allows to restore packages that are referenced in
        /// paket.references files, instead of all packages in
        /// paket.dependencies.
        /// </summary>
        [SwitchArgument("--only-referenced", 1)]
        public bool OnlyReferences { get; set; }

        /// <summary>
        /// Allows to restore dependencies for a project.
        /// </summary>
        [StringArgument("--project", 6)]
        public string Project { get; set; }

        /// <summary>
        /// Allows to restore all packages from the given
        /// paket.references files.This implies
        /// --only-referenced.
        /// </summary>
        [StringArrayArgument("--referenced-files", 7)]
        public ISet<string> ReferencedFiles { get; set; }

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
        [SwitchArgument("--silent", 10)]
        public bool Silent { get; set; }

        /// <summary>
        /// Touches project files referencing packages which are
        /// being restored, to help incremental build tools
        /// detecting the change.
        /// </summary>
        [SwitchArgument("--touch-affected-IReferenceService", 2)]
        public bool TouchAffectedRefs { get; set; }

        /// <summary>
        /// Enable verbose console output for the paket process.
        /// </summary>
        [SwitchArgument("-verbose", 8)]
        public bool Verbose { get; set; }

        #endregion
    }
}