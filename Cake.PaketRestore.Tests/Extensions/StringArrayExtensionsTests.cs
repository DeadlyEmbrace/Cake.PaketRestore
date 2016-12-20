using Cake.PaketRestore.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Cake.PaketRestore.Tests.Extensions
{
    public class StringArrayExtensionsTests
    {
        #region Public Methods

        [Test]
        public void ConversionOfEmptyArrayWorksCorrectly()
        {
            // arrange
            var sut = new string[0];

            // act
            var result = sut.ConvertToObjectArray();

            // assert
            result.Length.Should().Be(0);
        }

        [Test]
        public void ConvertStringArrayToObjectArrayWorksCorrectly()
        {
            // arrange
            const string word1 = "hello";
            const string word2 = "world";

            var sut = new[] { word1, word2 };

            // act
            var result = sut.ConvertToObjectArray();

            // assert
            result.Length.Should().Be(2);
            result[0].ToString().Should().Be(word1);
            result[1].ToString().Should().Be(word2);
        }

        #endregion
    }
}