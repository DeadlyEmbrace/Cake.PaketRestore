using System;

namespace Cake.PaketRestore.Interfaces
{
    /// <summary>
    /// Logging interface used by GitHubReleaseRetriever
    /// </summary>
    public interface IRetrieverLog
    {
        #region Public Methods

        /// <summary>
        /// Log an Error
        /// </summary>
        /// <param name="messageTemplate">Log template</param>
        /// <param name="args">Arguments for the template</param>
        void Error(string messageTemplate, params string[] args);

        /// <summary>
        /// Log an Error
        /// </summary>
        /// <param name="exception">Exception that caused the error</param>
        /// <param name="messageTemplate">Log template</param>
        /// <param name="args">Arguments for the template</param>
        void Error(Exception exception, string messageTemplate, params string[] args);

        /// <summary>
        /// Log information
        /// </summary>
        /// <param name="messageTemplate">Log template</param>
        /// <param name="args">Arguments for the template</param>
        void Information(string messageTemplate, params string[] args);

        /// <summary>
        /// Log a warning
        /// </summary>
        /// <param name="messageTemplate">Log template</param>
        /// <param name="args">Arguments for the template</param>
        void Warning(string messageTemplate, params string[] args);

        #endregion
    }
}