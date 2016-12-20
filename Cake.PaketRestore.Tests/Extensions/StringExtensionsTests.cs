using Cake.Core.IO;
using Cake.PaketRestore.Extensions;
using Cake.PaketRestore.Tests.Fixtures;
using Cake.PaketRestore.Tests.HelperExtensions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;

namespace Cake.PaketRestore.Tests.Extensions
{
    public class StringExtensionsTests
    {
        #region Public Methods

        [Test]
        public void DirectoryIsCreatedIfItDoesNotExist()
        {
            // arrange
            var logDummy = new CakeLogFixture();
            var directory = Guid.NewGuid().ToString();
            var directoryPath = DirectoryPath.FromString(directory);
            Directory.Exists(directory).Should().BeFalse();

            // act
            directoryPath.CheckAndCreateDirectory(logDummy);

            // assert
            Directory.Exists(directory).Should().BeTrue();

            DirectoryHelper.DeleteDirectory(directory);
        }

        #endregion
    }
}