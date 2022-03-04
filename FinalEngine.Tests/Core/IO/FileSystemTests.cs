// <copyright file="FileSystemTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.IO
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using FinalEngine.IO;
    using FinalEngine.IO.Invocation;
    using Moq;
    using NUnit.Framework;

    [ExcludeFromCodeCoverage]
    public class FileSystemTests
    {
        private const string Path = "test/path/to/file.txt";

        private Mock<IDirectoryInvoker> directory;

        private Mock<IFileInvoker> file;

        private FileSystem fileSystem;

        private Mock<IPathInvoker> path;

        [Test]
        public void ConstructorShouldNotThrowExceptionWhenParametersArentNull()
        {
            // Arrange, act and assert
            Assert.DoesNotThrow(() => new FileSystem(new Mock<IFileInvoker>().Object, new Mock<IDirectoryInvoker>().Object, new Mock<IPathInvoker>().Object));
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenDirectoryIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new FileSystem(new Mock<IFileInvoker>().Object, null, new Mock<IPathInvoker>().Object));
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenFileIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new FileSystem(null, new Mock<IDirectoryInvoker>().Object, new Mock<IPathInvoker>().Object));
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenPathIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new FileSystem(new Mock<IFileInvoker>().Object, new Mock<IDirectoryInvoker>().Object, null));
        }

        [Test]
        public void CreateDirectoryShouldInvokeDirectoryCreateDirectoryWhenPathIsNotNull()
        {
            // Act
            this.fileSystem.CreateDirectory(Path);

            // Assert
            this.directory.Verify(x => x.CreateDirectory(Path), Times.Once);
        }

        [Test]
        public void CreateDirectoryShouldThrowArgumentNullExceptionWhenPathIsEmpty()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.CreateDirectory(string.Empty));
        }

        [Test]
        public void CreateDirectoryShouldThrowArgumentNullExceptionWhenPathIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.CreateDirectory(null));
        }

        [Test]
        public void CreateDirectoryShouldThrowArgumentNullExceptionWhenPathIsWhiteSpace()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.CreateDirectory("\t\r\n"));
        }

        [Test]
        public void CreateFileShouldInvokeFileCreateWhenPathIsNotNull()
        {
            // Act
            this.fileSystem.CreateFile(Path);

            // Assert
            this.file.Verify(x => x.Create(Path), Times.Once);
        }

        [Test]
        public void CreateFileShouldReturnSameStreamAsFileCreateWhenPathIsNotNull()
        {
            // Arrange
            FileStream expected = null;

            this.file.Setup(x => x.Create(Path)).Returns(expected);

            // Act
            Stream actual = this.fileSystem.CreateFile(Path);

            // Assert
            Assert.AreSame(expected, actual);
        }

        [Test]
        public void CreateFileShouldThrowArgumentNullExceptionWhenPathIsEmpty()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.CreateFile(string.Empty));
        }

        [Test]
        public void CreateFileShouldThrowArgumentNullExceptionWhenPathIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.CreateFile(null));
        }

        [Test]
        public void CreateFileShouldThrowArgumentNullExceptionWhenPathIsWhiteSpace()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.CreateFile("\t\n\r"));
        }

        [Test]
        public void DeleteDirectoryShouldInvokeDirectoryDeleteWhenPathIsNotNull()
        {
            // Act
            this.fileSystem.DeleteDirectory(Path);

            // Assert
            this.directory.Verify(x => x.Delete(Path, true), Times.Once);
        }

        [Test]
        public void DeleteDirectoryShouldThrowArgumentNullExceptionWhenPathIsEmpty()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.DeleteDirectory(string.Empty));
        }

        [Test]
        public void DeleteDirectoryShouldThrowArgumentNullExceptionWhenPathIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.DeleteDirectory(null));
        }

        [Test]
        public void DeleteDirectoryShouldThrowArgumentNullExceptionWhenPathIsWhiteSpace()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.DeleteDirectory("\t\r\n"));
        }

        [Test]
        public void DeleteFileShouldInvokeFileDeleteWhenPathIsNotNull()
        {
            // Act
            this.fileSystem.DeleteFile(Path);

            // Assert
            this.file.Verify(x => x.Delete(Path), Times.Once);
        }

        [Test]
        public void DeleteFileShouldThrowArgumentNullExceptionWhenPathIsEmpty()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.DeleteFile(string.Empty));
        }

        [Test]
        public void DeleteFileShouldThrowArgumentNullExceptionWhenPathIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.DeleteFile(null));
        }

        [Test]
        public void DeleteFileShouldThrowArgumentNullExceptionWhenPathIsWhiteSpace()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.DeleteFile("\t\r\n"));
        }

        [Test]
        public void DirectoryExistsShouldInvokeDirectoryExistsWhenPathIsNotNull()
        {
            // Act
            this.fileSystem.DirectoryExists(Path);

            // Assert
            this.directory.Verify(x => x.Exists(Path), Times.Once);
        }

        [Test]
        public void DirectoryExistsShouldReturnSameAsDirectoryExistsWhenPathisNotNull()
        {
            // Arrange
            const bool Expected = true;

            this.directory.Setup(x => x.Exists(Path)).Returns(Expected);

            // Act
            bool actual = this.fileSystem.DirectoryExists(Path);

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void DirectoryExistsShouldThrowArgumentNullExceptionWhenPathIsEmpty()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.DirectoryExists(string.Empty));
        }

        [Test]
        public void DirectoryExistsShouldThrowArgumentNullExceptionWhenPathIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.DirectoryExists(null));
        }

        [Test]
        public void DirectoryExistsShouldThrowArgumentNullExceptionWhenPathIsWhiteSpace()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.DirectoryExists("\t\r\n"));
        }

        [Test]
        public void FileExistsShouldInvokeFileExistsWhenPathIsNotNull()
        {
            // Act
            this.fileSystem.FileExists(Path);

            // Assert
            this.file.Verify(x => x.Exists(Path), Times.Once);
        }

        [Test]
        public void FileExistsShouldReturnSameAsFileExistsWhenPathIsNotNull()
        {
            // Arrange
            const bool Expected = true;

            this.file.Setup(x => x.Exists(Path)).Returns(Expected);

            // Act
            bool actual = this.fileSystem.FileExists(Path);

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void FileExistsShouldThrowArgumentNullExceptionWhenPathIsEmpty()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.FileExists(string.Empty));
        }

        [Test]
        public void FileExistsShouldThrowArgumentNullExceptionWhenPathIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.FileExists(null));
        }

        [Test]
        public void FileExistsShouldThrowArgumentNullExceptionWhenPathIsWhiteSpace()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.FileExists("\t\r\n"));
        }

        [Test]
        public void OpenFileShouldInvokeFileOpenReadAccessWhenPathIsNotNull()
        {
            // Act
            this.fileSystem.OpenFile(Path, FileAccessMode.Read);

            // Assert
            this.file.Verify(x => x.Open(Path, FileMode.Open, FileAccess.Read));
        }

        [Test]
        public void OpenFileShouldInvokeFileOpenReadAndWriteAccessWhenPathIsNotNull()
        {
            // Act
            this.fileSystem.OpenFile(Path, FileAccessMode.ReadAndWrite);

            // Assert
            this.file.Verify(x => x.Open(Path, FileMode.Open, FileAccess.ReadWrite));
        }

        [Test]
        public void OpenFileShouldInvokeFileOpenWriteAccessWhenPathIsNotNull()
        {
            // Act
            this.fileSystem.OpenFile(Path, FileAccessMode.Write);

            // Assert
            this.file.Verify(x => x.Open(Path, FileMode.Open, FileAccess.Write));
        }

        [Test]
        public void OpenFileShouldReturnSameAsFileOpenWhenPathIsNotNull()
        {
            // Arrange
            FileStream expected = null;

            this.file.Setup(x => x.Open(Path, FileMode.Open, FileAccess.Read)).Returns(expected);

            // Act
            Stream actual = this.fileSystem.OpenFile(Path, FileAccessMode.Read);

            // Assert
            Assert.AreSame(expected, actual);
        }

        [Test]
        public void OpenFileShouldThrowArgumentNullExceptionWhenPathIsEmpty()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.OpenFile(string.Empty, FileAccessMode.Read));
        }

        [Test]
        public void OpenFileShouldThrowArgumentNullExceptionWhenPathIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.OpenFile(null, FileAccessMode.Read));
        }

        [Test]
        public void OpenFileShouldThrowArgumentNullExceptionWhenPathIsWhiteSpace()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.fileSystem.OpenFile("\r\t\n", FileAccessMode.Read));
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.file = new Mock<IFileInvoker>();
            this.directory = new Mock<IDirectoryInvoker>();
            this.path = new Mock<IPathInvoker>();
            this.fileSystem = new FileSystem(this.file.Object, this.directory.Object, this.path.Object);
        }
    }
}