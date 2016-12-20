using Cake.PaketRestore.Interfaces;
using System;
using System.Collections.Generic;

namespace Cake.PaketRestore.Tests.Helpers
{
    public class RetrieverLogFixture : IRetrieverLog
    {
        #region Constructor

        public RetrieverLogFixture()
        {
            LoggedMessages = new List<LogCapture>();
        }

        #endregion

        #region Properties

        public List<LogCapture> LoggedMessages { get; }

        #endregion

        #region Public Methods

        public void Error(string messageTemplate, params string[] args)
        {
            LoggedMessages.Add(new LogCapture(LogType.Error, messageTemplate, args));
        }

        public void Error(Exception exception, string messageTemplate, params string[] args)
        {
            LoggedMessages.Add(new LogCapture(LogType.Error, exception, messageTemplate, args));
        }

        public void Information(string messageTemplate, params string[] args)
        {
            LoggedMessages.Add(new LogCapture(LogType.Information, messageTemplate, args));
        }

        #endregion
    }

    public class LogCapture
    {
        #region Constructor

        public LogCapture(LogType type, string messageTemplate, params string[] messageArgs)
            : this(type, null, messageTemplate, messageArgs)
        { }

        public LogCapture(LogType type, Exception exception, string messageTemplate, params string[] messageArgs)
        {
            Type = type;
            MessageException = exception;
            MessageTemplate = messageTemplate;
            MessageArguments = messageArgs;
        }

        #endregion

        #region Properties

        public string[] MessageArguments { get; }

        public Exception MessageException { get; }

        public string MessageTemplate { get; }

        public LogType Type { get; }

        #endregion
    }

    public enum LogType
    {
        Information,
        Error
    }
}