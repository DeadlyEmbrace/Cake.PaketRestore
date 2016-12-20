using Cake.Core.Diagnostics;
using Cake.PaketRestore.Helpers;
using Cake.PaketRestore.Tests.Fixtures;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace Cake.PaketRestore.Tests.Helpers
{
    public class CakeRetrieverLogTests
    {
        #region Public Methods

        [Test]
        public void ErrorMessagesAreCorrectlyPassedToCakeLogger()
        {
            // arrange
            const string message = "Test log";
            const string argument = "Test Argument";
            var args = new[] { argument };

            var cakeLogDummy = new CakeLogFixture();
            var sut = new CakeRetrieverLog(cakeLogDummy);

            // act
            sut.Error(message, args);

            // assert
            cakeLogDummy.Messages.Count.Should().Be(1);
            var logMessage = cakeLogDummy.Messages.First();
            logMessage.LogLevel.Should().Be(LogLevel.Error);
            logMessage.Verbosity.Should().Be(Verbosity.Normal);
            logMessage.Format.Should().Be(message);
            logMessage.Arguments.Length.Should().Be(1);
            logMessage.Arguments.First().Should().Be(argument);
        }

        [Test]
        public void ErrorWithExceptionMessagesAreCorrectlyPassedToCakeLogger()
        {
            // arrange
            const string message = "Test log";
            const string argument = "Test Argument";
            var args = new[] { argument };
            var exception = new Exception("Exception");

            var cakeLogDummy = new CakeLogFixture();
            var sut = new CakeRetrieverLog(cakeLogDummy);

            // act
            sut.Error(exception, message, args);

            // assert
            cakeLogDummy.Messages.Count.Should().Be(1);
            var logMessage = cakeLogDummy.Messages.First();
            logMessage.LogLevel.Should().Be(LogLevel.Error);
            logMessage.Verbosity.Should().Be(Verbosity.Normal);
            logMessage.Format.Should().Be(message);
            logMessage.Arguments.Length.Should().Be(1);
            logMessage.Arguments.First().Should().Be(argument);
        }

        [Test]
        public void InformationMessagesAreCorrectlyPassedToCakeLogger()
        {
            // arrange
            const string message = "Test log";
            const string argument = "Test Argument";
            var args = new[] { argument };

            var cakeLogDummy = new CakeLogFixture();
            var sut = new CakeRetrieverLog(cakeLogDummy);

            // act
            sut.Information(message, args);

            // assert
            cakeLogDummy.Messages.Count.Should().Be(1);
            var logMessage = cakeLogDummy.Messages.First();
            logMessage.LogLevel.Should().Be(LogLevel.Information);
            logMessage.Verbosity.Should().Be(Verbosity.Normal);
            logMessage.Format.Should().Be(message);
            logMessage.Arguments.Length.Should().Be(1);
            logMessage.Arguments.First().Should().Be(argument);
        }

        #endregion
    }
}