using Cake.Core.IO;
using System;
using System.IO;

namespace Cake.PaketRestore.Tests.HelperExtensions
{
    public static class DirectoryHelper
    {
        #region Public Methods

        public static void DeleteDirectory(string relativePath)
        {
            try
            {
                var fullPath = DirectoryPath.FromString(relativePath);
                Directory.Delete(fullPath.FullPath, true);
            }
            catch (Exception)
            {
                // We simply swallow the exception
            }
        }

        #endregion
    }
}