using Cake.Core.Diagnostics;
using Cake.PaketRestore.Extensions;
using Cake.PaketRestore.Interfaces;
using System;

namespace Cake.PaketRestore.Helpers
{
    /// <summary>
    /// Log for GitHubReleaseRetriever to enable it to write Cake logs
    /// </summary>
    public class CakeRetrieverLog : IRetrieverLog
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="log">Cake log</param>
        public CakeRetrieverLog(ICakeLog log)
        {
            _log = log;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public void Error(string messageTemplate, params string[] args)
        {
            _log.Write(Verbosity.Normal, LogLevel.Error, messageTemplate, args.ConvertToObjectArray());
        }

        /// <inheritdoc />
        public void Error(Exception exception, string messageTemplate, params string[] args)
        {
            _log.Write(Verbosity.Normal, LogLevel.Error, messageTemplate, args.ConvertToObjectArray());
        }

        /// <inheritdoc />
        public void Information(string messageTemplate, params string[] args)
        {
            _log.Information(messageTemplate, args.ConvertToObjectArray());
        }

        /// <inheritdoc />
        public void Warning(string messageTemplate, params string[] args)
        {
            _log.Write(Verbosity.Normal, LogLevel.Warning, messageTemplate, args.ConvertToObjectArray());
        }

        #endregion

        #region Variables

        private readonly ICakeLog _log;

        #endregion
    }
}