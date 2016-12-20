using Cake.Core.IO;
using Cake.PaketRestore.Attributes;
using Cake.PaketRestore.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace Cake.PaketRestore.Tests.Helpers
{
    public class ArgumentStringBuilderTests
    {
        #region Public Methods

        [Test]
        public void MisedArgumentsAreCorrectlyAppendedToCommandLineArguments()
        {
            // arrange
            var argumentBuilderDummy = new ProcessArgumentBuilder();
            var settingFileDummy = new MixedSettingFixture();

            // act
            argumentBuilderDummy.BuildArgumentString(settingFileDummy);

            // assert
            var argumentString = argumentBuilderDummy.Render();

            argumentString.Should()
                .Be($"{ActiveSwitch1Name} {ValidStringName} {ActiveText} {ValidArrayName} {ArrayValue1} {ArrayValue2}");
        }

        [Test]
        public void StringArgumentsAreCorrectlyAppendedToCommandLineArguments()
        {
            // arrange
            var argumentBuilderDummy = new ProcessArgumentBuilder();
            var settingFileDummy = new StringOnlySettingFixture();

            // act
            argumentBuilderDummy.BuildArgumentString(settingFileDummy);

            // assert
            var argumentString = argumentBuilderDummy.Render();

            argumentString.Should().Be($"{ValidStringName} {ActiveText}");
        }

        [Test]
        public void StringArrayArgumentsAreCorrectlyAppendedToCommandLineArguments()
        {
            // arrange
            var argumentBuilderDummy = new ProcessArgumentBuilder();
            var settingFileDummy = new StringArrayOnlySettingFixture();

            // act
            argumentBuilderDummy.BuildArgumentString(settingFileDummy);

            // assert
            var argumentString = argumentBuilderDummy.Render();

            argumentString.Should().Be($"{ValidArrayName} {ArrayValue1} {ArrayValue2}");
        }

        [Test]
        public void SwitchesAreCorrectlyAppendedToCommandLineArgument()
        {
            // arrange
            var argumentBuilderDummy = new ProcessArgumentBuilder();
            var settingFileDummy = new SwithOnlySettingFixture();

            // act
            argumentBuilderDummy.BuildArgumentString(settingFileDummy);

            // assert
            var argumentString = argumentBuilderDummy.Render();

            argumentString.Should().Be($"{ActiveSwitch1Name} {ActiveSwitch2Name}");
        }

        #endregion

        #region Variables

        private const string ActiveSwitch1Name = "-activeSwitch1";
        private const string ActiveSwitch2Name = "-activeSwitch2";
        private const string ValidStringName = "-validString";
        private const string InvalidStringName = "-invalidString";
        private const string ValidArrayName = "-validArray";
        private const string EmptyArrayName = "-emptyArray";
        private const string NullArrayName = "-nullArray";
        private const string ActiveText = "activetext1";
        private const string ArrayValue1 = "text1";
        private const string ArrayValue2 = "text2";

        #endregion

        private class SwithOnlySettingFixture
        {
            #region Properties

            [SwitchArgument(ActiveSwitch1Name, 0)]
            public bool ActiveSwitch1 => true;

            [SwitchArgument(ActiveSwitch2Name, 1)]
            public bool ActiveSwitch2 => true;

            [SwitchArgument("-InactiveSwitch", 2)]
            public bool InactiveSwitch => false;

            #endregion
        }

        private class StringOnlySettingFixture
        {
            #region Properties

            [StringArgument(InvalidStringName, 0)]
            public string EmptyString => string.Empty;

            [StringArgument(ValidStringName, 1)]
            public string ValidString => ActiveText;

            #endregion
        }

        private class StringArrayOnlySettingFixture
        {
            #region Properties

            [StringArrayArgument(EmptyArrayName, 0)]
            public string[] EmptyArray => new string[0];

            [StringArrayArgument(NullArrayName, 1)]
            public string[] NullArray => null;

            [StringArrayArgument(ValidArrayName, 2)]
            public string[] ValidArray => new[] { ArrayValue1, ArrayValue2 };

            #endregion
        }

        private class MixedSettingFixture
        {
            #region Properties

            [SwitchArgument(ActiveSwitch1Name, 0)]
            public bool ActiveSwitch1 => true;

            [StringArrayArgument(ValidArrayName, 2)]
            public string[] ValidArray => new[] { ArrayValue1, ArrayValue2 };

            [StringArgument(ValidStringName, 1)]
            public string ValidString => ActiveText;

            #endregion
        }
    }
}