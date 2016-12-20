using Cake.Core.Diagnostics;
using Cake.Core.IO;
using System.IO;

namespace Cake.PaketRestore.Extensions
{
    /// <summary>
    /// Extension methods for strings
    /// </summary>
    public static class DirectoryPathExtensions
    {
        #region Public Methods

        /// <summary>
        /// Check if a directory exist, if it does not exist it will be created
        /// </summary>
        /// <param name="directoryPath">Directory path to check</param>
        /// <param name="log">Cake log</param>
        public static void CheckAndCreateDirectory(this DirectoryPath directoryPath, ICakeLog log)
        {
            if (Directory.Exists(directoryPath.FullPath))
            {
                return;
            }
            Directory.CreateDirectory(directoryPath.FullPath);
            log.Information("{Directory} was created", directoryPath);
        }

        #endregion
    }
}