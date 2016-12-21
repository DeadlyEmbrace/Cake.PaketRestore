using Cake.Core;
using Cake.Core.IO;
using Cake.PaketRestore.Models;
using Cake.PaketRestore.Tests.Fixtures;
using Cake.PaketRestore.Tests.HelperExtensions;
using FluentAssertions;
using HttpMock;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using Path = System.IO.Path;

namespace Cake.PaketRestore.Tests
{
    public class CakePaketRestoreAliasTests
    {
        #region Public Methods

        [Test]
        public void ErrorDuringBootstrapperDownloadShouldThrowException()
        {
            // arrange
            var fixture = new CakePaketRestoreAliasFixture();
            const string fakeUrl = "http://localhost:9955";
            var directory = new DirectoryPath(Guid.NewGuid().ToString());
            var bootstrapperPath = new FilePath(Path.Combine(directory.FullPath, PaketBootstrapper));
            var transferModelDummy = new GitHubLatestReleaseTransferModel
            {
                GitHubAssetsTransferModel = new[]
                {
                    new GitHubAssetsTransferModel
                    {
                        BrowserUrl = $"{fakeUrl}/{PaketBootstrapper}",
                        Name = PaketBootstrapper
                    }
                }
            };

            var httpMock = HttpMockRepository.At(fakeUrl);
            CakePaketRestoreAlias.GithubUrlPath = fakeUrl;

            fixture.FileSysteMock.Setup(t => t.GetFile(bootstrapperPath).Exists).Returns(false);
            httpMock.Stub(x => x.Get(BootStrapperUrl))
                .Return("application/json")
                .Return(JsonConvert.SerializeObject(transferModelDummy))
                .OK();

            var act = new Action(() => fixture.GetCakeContext.RetrievePaketBootloader(directory));

            // act
            // assert
            act.ShouldThrow<CakeException>("An error occured while trying to retrieve the Paket Bootstrapper");

            DirectoryHelper.DeleteDirectory(directory.FullPath);
        }

        [Test]
        public void ErrorDuringBootstrapperRetrievalThrowsAnException()
        {
            // arrange
            var fixture = new CakePaketRestoreAliasFixture();
            var directory = new DirectoryPath(Guid.NewGuid().ToString());
            var bootstrapperPath = new FilePath(Path.Combine(directory.FullPath, PaketBootstrapper));

            const string fakeUrl = "http://localhost:9955";
            CakePaketRestoreAlias.GithubUrlPath = fakeUrl;

            fixture.FileSysteMock.Setup(t => t.GetFile(bootstrapperPath).Exists).Returns(false);

            var act = new Action(() => fixture.GetCakeContext.RetrievePaketBootloader(directory));

            // act
            // assert
            act.ShouldThrow<CakeException>("Failed to retrieve link for latest Paket Bootstrapper");

            DirectoryHelper.DeleteDirectory(directory.FullPath);
        }

        [Test]
        public void FailedPaketExecutableRetrievalShouldThrowAnException()
        {
            // arrange
            var fixture = new CakePaketRestoreAliasFixture(1);

            var act = new Action(() => fixture.GetCakeContext.RetrievePaketExecutable(fixture.GetDirectoryPath));

            // act
            // assert
            act.ShouldThrow<CakeException>("Error occured during Paket boostrapper execution");
        }

        [Test]
        public void FailedPaketRestoreShouldThrowAnException()
        {
            // arrange
            var fixture = new CakePaketRestoreAliasFixture(1);

            var act = new Action(() => fixture.GetCakeContext.PaketRestore(fixture.GetDirectoryPath));

            // act
            // assert
            act.ShouldThrow<CakeException>("Error occured during Paket restore");
        }

        [Test]
        public void FailedPaketUpdateShouldThrowAnException()
        {
            // arrange
            var fixture = new CakePaketRestoreAliasFixture(1);

            var act = new Action(() => fixture.GetCakeContext.PaketUpdate(fixture.GetDirectoryPath));

            // act
            // assert
            act.ShouldThrow<CakeException>("Error occured during Paket restore");
        }

        [Test]
        public void PaketRestoreWithBootloaderAndPaketExecutableRetrieval()
        {
            // arrange
            var fixture = new CakePaketRestoreAliasFixture();
            var restoreSettingsDummy = new PaketRestoreSettings
            {
                RetrieveBootstrapper = true,
                RetrievePaketExecutable = true
            };

            const string fakeUrl = "http://localhost:9955";
            var directory = new DirectoryPath(Guid.NewGuid().ToString());
            var bootstrapperPath = new FilePath(Path.Combine(directory.FullPath, PaketBootstrapper));
            var transferModelDummy = new GitHubLatestReleaseTransferModel
            {
                GitHubAssetsTransferModel = new[]
                {
                    new GitHubAssetsTransferModel
                    {
                        BrowserUrl = $"{fakeUrl}/{PaketBootstrapper}",
                        Name = PaketBootstrapper
                    }
                }
            };

            var httpMock = HttpMockRepository.At(fakeUrl);
            CakePaketRestoreAlias.GithubUrlPath = fakeUrl;

            fixture.FileSysteMock.Setup(t => t.GetFile(bootstrapperPath).Exists).Returns(false);
            httpMock.Stub(x => x.Get(BootStrapperUrl))
                .Return("application/json")
                .Return(JsonConvert.SerializeObject(transferModelDummy))
                .OK();
            httpMock.Stub(x => x.Get($"/{PaketBootstrapper}"))
                .ReturnFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "TestFile.txt"))
                .OK();
            var act = new Action(() => fixture.GetCakeContext.PaketRestore(fixture.GetDirectoryPath, restoreSettingsDummy));

            // act
            // assert
            act.ShouldNotThrow<CakeException>("Error occured during Paket restore");
            fixture.GetCakeLog.Messages.Should().Contain(x => x.Format == "Completed retrieval of the Paket Bootstrapper");
            DirectoryHelper.DeleteDirectory(directory.FullPath);
        }

        [Test]
        public void PaketUpdateWithBootloaderAndPaketExecutableRetrieval()
        {
            // arrange
            var fixture = new CakePaketRestoreAliasFixture();
            var updateSettingsDummy = new PaketUpdateSettings
            {
                RetrieveBootstrapper = true,
                RetrievePaketExecutable = true
            };

            const string fakeUrl = "http://localhost:9955";
            var directory = new DirectoryPath(Guid.NewGuid().ToString());
            var bootstrapperPath = new FilePath(Path.Combine(directory.FullPath, PaketBootstrapper));
            var transferModelDummy = new GitHubLatestReleaseTransferModel
            {
                GitHubAssetsTransferModel = new[]
                {
                    new GitHubAssetsTransferModel
                    {
                        BrowserUrl = $"{fakeUrl}/{PaketBootstrapper}",
                        Name = PaketBootstrapper
                    }
                }
            };

            var httpMock = HttpMockRepository.At(fakeUrl);
            CakePaketRestoreAlias.GithubUrlPath = fakeUrl;

            fixture.FileSysteMock.Setup(t => t.GetFile(bootstrapperPath).Exists).Returns(false);
            httpMock.Stub(x => x.Get(BootStrapperUrl))
                .Return("application/json")
                .Return(JsonConvert.SerializeObject(transferModelDummy))
                .OK();
            httpMock.Stub(x => x.Get($"/{PaketBootstrapper}"))
                .ReturnFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "TestFile.txt"))
                .OK();
            var act = new Action(() => fixture.GetCakeContext.PaketUpdate(fixture.GetDirectoryPath, updateSettingsDummy));

            // act
            // assert
            act.ShouldNotThrow<CakeException>("Error occured during Paket update");
            fixture.GetCakeLog.Messages.Should().Contain(x => x.Format == "Completed retrieval of the Paket Bootstrapper");
            DirectoryHelper.DeleteDirectory(directory.FullPath);
        }

        [Test]
        public void RetrieveBootstrapperDoesNothingIfBootstrapperAlreadyExists()
        {
            // arrange
            var directory = new DirectoryPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Guid.NewGuid().ToString()));
            var bootstrapperPath = new FilePath(Path.Combine(directory.FullPath, PaketBootstrapper));
            var fixture = new CakePaketRestoreAliasFixture();
            Directory.CreateDirectory(directory.FullPath);
            File.Create(Path.Combine(directory.FullPath, PaketBootstrapper)).Close();

            fixture.FileSysteMock.Setup(t => t.GetFile(bootstrapperPath).Exists).Returns(true);

            // act
            fixture.GetCakeContext.RetrievePaketBootloader(directory);

            // assert
            fixture.GetCakeLog.Messages.Last()
                .Format.Should()
                .Be("Paket Bootstrapper already exists - skipping download");

            DirectoryHelper.DeleteDirectory(directory.FullPath);
        }

        [Test]
        public void RetrievePaketExecutableSuccessfullyShouldNotThrowAnException()
        {
            // arrange
            var fixture = new CakePaketRestoreAliasFixture();

            var act = new Action(() => fixture.GetCakeContext.RetrievePaketExecutable(fixture.GetDirectoryPath));

            // act
            // assert
            act.ShouldNotThrow<CakeException>("Error occured during Paket boostrapper execution");
        }

        [Test]
        public void SuccessfullyRetrievePaketBootStrapper()
        {
            // arrange
            var fixture = new CakePaketRestoreAliasFixture();
            const string fakeUrl = "http://localhost:9955";
            var directory = new DirectoryPath(Guid.NewGuid().ToString());
            var bootstrapperPath = new FilePath(Path.Combine(directory.FullPath, PaketBootstrapper));
            var transferModelDummy = new GitHubLatestReleaseTransferModel
            {
                GitHubAssetsTransferModel = new[]
                {
                    new GitHubAssetsTransferModel
                    {
                        BrowserUrl = $"{fakeUrl}/{PaketBootstrapper}",
                        Name = PaketBootstrapper
                    }
                }
            };

            var httpMock = HttpMockRepository.At(fakeUrl);
            CakePaketRestoreAlias.GithubUrlPath = fakeUrl;

            fixture.FileSysteMock.Setup(t => t.GetFile(bootstrapperPath).Exists).Returns(false);
            httpMock.Stub(x => x.Get(BootStrapperUrl))
                .Return("application/json")
                .Return(JsonConvert.SerializeObject(transferModelDummy))
                .OK();
            httpMock.Stub(x => x.Get($"/{PaketBootstrapper}"))
                .ReturnFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "TestFile.txt"))
                .OK();

            // act
            fixture.GetCakeContext.RetrievePaketBootloader(directory);

            // assert
            fixture.GetCakeLog.Messages.Last().Format.Should().Be("Completed retrieval of the Paket Bootstrapper");

            DirectoryHelper.DeleteDirectory(directory.FullPath);
        }

        [Test]
        public void SuccessfulPaketRestoreShouldNotThrowAnException()
        {
            // arrange
            var passedArguments = string.Empty;
            var fixture = new CakePaketRestoreAliasFixture();
            fixture.ProcessRunnerMock.Setup(t => t.Start(It.IsAny<FilePath>(), It.IsAny<ProcessSettings>())).Callback(
                (FilePath p, ProcessSettings t) => { passedArguments = t.Arguments.Render(); });

            var act = new Action(() => fixture.GetCakeContext.PaketRestore(fixture.GetDirectoryPath));

            // act
            // assert
            act.ShouldNotThrow<CakeException>("Error occured during Paket restore");
            passedArguments.Should().NotBeNullOrEmpty();
            passedArguments.Should().Be("restore");
        }

        [Test]
        public void SuccessfulPaketUpdateShouldNotThrowAnException()
        {
            // arrange
            var passedArguments = string.Empty;
            var fixture = new CakePaketRestoreAliasFixture();
            fixture.ProcessRunnerMock.Setup(t => t.Start(It.IsAny<FilePath>(), It.IsAny<ProcessSettings>())).Callback(
                (FilePath p, ProcessSettings t) => { passedArguments = t.Arguments.Render(); });

            var act = new Action(() => fixture.GetCakeContext.PaketUpdate(fixture.GetDirectoryPath));

            // act
            // assert
            act.ShouldNotThrow<CakeException>("Error occured during Paket restore");
            passedArguments.Should().NotBeNullOrEmpty();
            passedArguments.Should().Be("update");
        }

        #endregion

        #region Variables

        private const string PaketBootstrapper = "paket.bootstrapper.exe";
        private const string BootStrapperUrl = "/repos/fsprojects/Paket/releases/latest";

        #endregion

        private class CakePaketRestoreAliasFixture
        {
            #region Constructor

            public CakePaketRestoreAliasFixture(int exitCode)
            {
                GetDirectoryPath = Guid.NewGuid().ToString();

                CakeContextMock.Setup(t => t.ProcessRunner).Returns(ProcessRunnerMock.Object);
                CakeContextMock.Setup(t => t.FileSystem).Returns(FileSysteMock.Object);
                CakeContextMock.Setup(t => t.Log).Returns(GetCakeLog);

                ProcessRunnerMock.Setup(t => t.Start(It.IsAny<FilePath>(), It.IsAny<ProcessSettings>()))
                    .Returns(ProcessMock.Object);
                ProcessMock.Setup(t => t.GetExitCode()).Returns(exitCode);
            }

            public CakePaketRestoreAliasFixture()
                : this(0)
            {
            }

            #endregion

            #region Properties

            public Mock<ICakeContext> CakeContextMock { get; } = new Mock<ICakeContext>();

            public Mock<IFileSystem> FileSysteMock { get; } = new Mock<IFileSystem>();

            public ICakeContext GetCakeContext => CakeContextMock.Object;

            public CakeLogFixture GetCakeLog { get; } = new CakeLogFixture();
            public DirectoryPath GetDirectoryPath { get; }

            public Mock<IProcess> ProcessMock { get; } = new Mock<IProcess>();

            public Mock<IProcessRunner> ProcessRunnerMock { get; } = new Mock<IProcessRunner>();

            #endregion
        }
    }
}