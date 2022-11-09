// <copyright file="FileSystemTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.IO
{
    using System;
    using System.IO;
    using FinalEngine.IO;
    using FinalEngine.IO.Invocation;
    using Moq;
    using NUnit.Framework;

    public class FileSystemTests
    {
        private const string Path = "test/path/to/file.txt";

        private Mock<IDirectoryInvoker> directory;

        private Mock<IFileInvoker> file;

        private FileSystem fileSystem;

        [Test]
        public void ConstructorShouldNotThrowExceptionWhenParametersArentNull()
        {
            // Arrange, act and assert
            Assert.DoesNotThrow(() =>
            {
                new FileSystem(new Mock<IFileInvoker>().Object, new Mock<IDirectoryInvoker>().Object);
            });
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenDirectoryIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new FileSystem(new Mock<IFileInvoker>().Object, null);
            });
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenFileIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new FileSystem(null, new Mock<IDirectoryInvoker>().Object);
            });
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
        public void CreateDirectoryShouldThrowArgumentExceptionWhenPathIsEmpty()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.CreateDirectory(string.Empty);
            });
        }

        [Test]
        public void CreateDirectoryShouldThrowArgumentExceptionWhenPathIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.CreateDirectory(null);
            });
        }

        [Test]
        public void CreateDirectoryShouldThrowArgumentExceptionWhenPathIsWhiteSpace()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.CreateDirectory("\t\r\n");
            });
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
            var actual = this.fileSystem.CreateFile(Path);

            // Assert
            Assert.AreSame(expected, actual);
        }

        [Test]
        public void CreateFileShouldThrowArgumentExceptionWhenPathIsEmpty()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.CreateFile(string.Empty);
            });
        }

        [Test]
        public void CreateFileShouldThrowArgumentExceptionWhenPathIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.CreateFile(null);
            });
        }

        [Test]
        public void CreateFileShouldThrowArgumentExceptionWhenPathIsWhiteSpace()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.CreateFile("\t\n\r");
            });
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
        public void DeleteDirectoryShouldThrowArgumentExceptionWhenPathIsEmpty()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.DeleteDirectory(string.Empty);
            });
        }

        [Test]
        public void DeleteDirectoryShouldThrowArgumentExceptionWhenPathIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.DeleteDirectory(null);
            });
        }

        [Test]
        public void DeleteDirectoryShouldThrowArgumentExceptionWhenPathIsWhiteSpace()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.DeleteDirectory("\t\r\n");
            });
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
        public void DeleteFileShouldThrowArgumentExceptionWhenPathIsEmpty()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.DeleteFile(string.Empty);
            });
        }

        [Test]
        public void DeleteFileShouldThrowArgumentExceptionWhenPathIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.DeleteFile(null);
            });
        }

        [Test]
        public void DeleteFileShouldThrowArgumentExceptionWhenPathIsWhiteSpace()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.DeleteFile("\t\r\n");
            });
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
            const bool expected = true;

            this.directory.Setup(x => x.Exists(Path)).Returns(expected);

            // Act
            bool actual = this.fileSystem.DirectoryExists(Path);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DirectoryExistsShouldThrowArgumentExceptionWhenPathIsEmpty()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.DirectoryExists(string.Empty);
            });
        }

        [Test]
        public void DirectoryExistsShouldThrowArgumentExceptionWhenPathIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.DirectoryExists(null);
            });
        }

        [Test]
        public void DirectoryExistsShouldThrowArgumentExceptionWhenPathIsWhiteSpace()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.DirectoryExists("\t\r\n");
            });
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
            const bool expected = true;

            this.file.Setup(x => x.Exists(Path)).Returns(expected);

            // Act
            bool actual = this.fileSystem.FileExists(Path);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FileExistsShouldThrowArgumentExceptionWhenPathIsEmpty()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.FileExists(string.Empty);
            });
        }

        [Test]
        public void FileExistsShouldThrowArgumentExceptionWhenPathIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.FileExists(null);
            });
        }

        [Test]
        public void FileExistsShouldThrowArgumentExceptionWhenPathIsWhiteSpace()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.FileExists("\t\r\n");
            });
        }

        [Test]
        public void OpenFileShouldInvokeFileOpenReadAccessWhenModeIsNotFound()
        {
            // Arrange
            Assert.IsFalse(Enum.IsDefined(typeof(FileAccessMode), int.MaxValue));

            // Act
            this.fileSystem.OpenFile(Path, (FileAccessMode)int.MaxValue);

            // Assert
            this.file.Verify(x => x.Open(Path, FileMode.Open, FileAccess.Read));
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
            var actual = this.fileSystem.OpenFile(Path, FileAccessMode.Read);

            // Assert
            Assert.AreSame(expected, actual);
        }

        [Test]
        public void OpenFileShouldThrowArgumentExceptionWhenPathIsEmpty()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.OpenFile(string.Empty, FileAccessMode.Read);
            });
        }

        [Test]
        public void OpenFileShouldThrowArgumentExceptionWhenPathIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.OpenFile(null, FileAccessMode.Read);
            });
        }

        [Test]
        public void OpenFileShouldThrowArgumentExceptionWhenPathIsWhiteSpace()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.fileSystem.OpenFile("\r\t\n", FileAccessMode.Read);
            });
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.file = new Mock<IFileInvoker>();
            this.directory = new Mock<IDirectoryInvoker>();
            this.fileSystem = new FileSystem(this.file.Object, this.directory.Object);
        }
    }
}
