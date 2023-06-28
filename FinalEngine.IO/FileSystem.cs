// <copyright file="FileSystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.IO;

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using FinalEngine.IO.Invocation;

/// <summary>
/// Provides a standard implementation of an <see cref="IFileSystem"/>.
/// </summary>
/// <remarks>
/// The <see cref="FileSystem"/> contains functions that invoke methods from the <see cref="System.IO"/> namespace. In it's current state the methods provided in this implementation simply just invoke their <see cref="System.IO"/> counterparts. For example, <see cref="FileSystem.CreateDirectory(string)"/> simply invokes <see cref="System.IO.Directory.CreateDirectory(string)"/>. Because of this it's important to consult the Microsoft documentation if an unhandled exception occurs.
///
/// Also, please note that you should typically use the <see cref="IFileSystem"/> interface provided to you by the runtime factory. You generally shouldn't need to instantiate <see cref="FileSystem"/> and should rely on dependency injection.
/// </remarks>
/// <seealso cref="IFileSystem"/>
public class FileSystem : IFileSystem
{
    /// <summary>
    /// The directory invoker.
    /// </summary>
    private readonly IDirectoryInvoker directory;

    /// <summary>
    /// The file invoker.
    /// </summary>
    private readonly IFileInvoker file;

    /// <summary>
    /// The path invoker.
    /// </summary>
    private readonly IPathInvoker path;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileSystem"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public FileSystem()
        : this(new FileInvoker(), new DirectoryInvoker(), new PathInvoker())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileSystem"/> class.
    /// </summary>
    /// <param name="file">
    /// Specifies an <see cref="IFileInvoker"/> that represents the invoker used to handle file operations.
    /// </param>
    /// <param name="directory">
    /// Specifies an <see cref="IDirectoryInvoker"/> that represents the invoker used to handle directory operations.
    /// </param>
    /// <param name="path">
    /// Specifies an <see cref="IPathInvoker"/> that represents the invoker used to handle path operations.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="file"/> or <paramref name="directory"/> parameter cannot be null.
    /// </exception>
    public FileSystem(IFileInvoker file, IDirectoryInvoker directory, IPathInvoker path)
    {
        this.file = file ?? throw new ArgumentNullException(nameof(file));
        this.directory = directory ?? throw new ArgumentNullException(nameof(directory));
        this.path = path ?? throw new ArgumentNullException(nameof(path));
    }

    /// <inheritdoc/>
    /// <exception cref="System.ArgumentException">
    /// The specified <paramref name="path" /> parameter cannot be null, empty or consist of only whitespace characters.
    /// </exception>
    /// <example>
    /// The following example creates and deletes the specified directory.
    /// <code title="CreateDirectoryExample.cs">
    /// namespace FileSystemExample;
    ///
    /// using System;
    /// using FinalEngine.IO;
    /// using FinalEngine.IO.Invocation;
    ///
    /// public class CreateDirectoryExample
    /// {
    ///     public static void Main()
    ///     {
    ///         // You shouldn't need to create a file system when using an appropriate runtime.
    ///         // Instead you can access the protected FileSystem property within your game.
    ///         // This is provided as an example to show how to instantiate the class if needed.
    ///         var file = new FileInvoker();
    ///         var directory = new DirectoryInvoker();
    ///         var fileSystem = new FileSystem(file, directory);
    ///
    ///         const string path = @"C:\MyDir";
    ///
    ///         try
    ///         {
    ///             // Determine whether the directory exists.
    ///             if (fileSystem.DirectoryExists(path))
    ///             {
    ///                 Console.WriteLine("That path already exists.");
    ///                 return;
    ///             }
    ///
    ///             // Try to create the directory.
    ///             fileSystem.CreateDirectory(path);
    ///
    ///             Console.WriteLine($"The directory at path: {path} was created.");
    ///
    ///             // Now lets delete it.
    ///             fileSystem.DeleteDirectory(path);
    ///
    ///             Console.WriteLine($"The directory at path: {path} was deleted.");
    ///         }
    ///         catch (Exception ex)
    ///         {
    ///             Console.WriteLine("The process failed: {0}", ex.ToString());
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    public void CreateDirectory(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException($"The specified {nameof(path)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(path));
        }

        this.directory.CreateDirectory(path);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="path" /> parameter is null, empty of contains only whitespace characters.
    /// </exception>
    /// <example>
    /// Below is an example of how to create and write to a file.
    /// <code title="CreateFileExample.cs">
    /// namespace FileSystemExample;
    ///
    /// using System;
    /// using System.IO;
    /// using FinalEngine.IO;
    /// using FinalEngine.IO.Invocation;
    ///
    /// public class CreateFileExample
    /// {
    ///     public static void Main()
    ///     {
    ///         // You shouldn't need to create a file system when using an appropriate runtime.
    ///         // Instead you can access the protected FileSystem property within your game.
    ///         // This is provided as an example to show how to instantiate the class if needed.
    ///         var file = new FileInvoker();
    ///         var directory = new DirectoryInvoker();
    ///         var fileSystem = new FileSystem(file, directory);
    ///
    ///         const string filePath = @"C:\MyFile.txt";
    ///
    ///         try
    ///         {
    ///             // Determine whether the file exists.
    ///             if (fileSystem.FileExists(filePath))
    ///             {
    ///                 Console.WriteLine("That file already exists.");
    ///                 return;
    ///             }
    ///
    ///             // Try to create the file.
    ///             var stream = fileSystem.CreateFile(filePath);
    ///
    ///             // Now you can use the stream to write to if you like
    ///             using (var writer = new StreamWriter(stream))
    ///             {
    ///                 writer.WriteLine("Hello, World!");
    ///             }
    ///         }
    ///         catch (Exception ex)
    ///         {
    ///             Console.WriteLine("The process failed: {0}", ex.ToString());
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    public Stream CreateFile(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException($"The specified {nameof(path)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(path));
        }

        return this.file.Create(path);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="path"/> parameter is null, empty of contains only whitespace characters.
    /// </exception>
    /// <example>
    /// The following example creates and deletes the specified directory.
    /// <code title="CreateDirectoryExample.cs">
    /// namespace FileSystemExample;
    ///
    /// using System;
    /// using FinalEngine.IO;
    /// using FinalEngine.IO.Invocation;
    ///
    /// public class CreateDirectoryExample
    /// {
    ///     public static void Main()
    ///     {
    ///         // You shouldn't need to create a file system when using an appropriate runtime.
    ///         // Instead you can access the protected FileSystem property within your game.
    ///         // This is provided as an example to show how to instantiate the class if needed.
    ///         var file = new FileInvoker();
    ///         var directory = new DirectoryInvoker();
    ///         var fileSystem = new FileSystem(file, directory);
    ///
    ///         const string path = @"C:\MyDir";
    ///
    ///         try
    ///         {
    ///             // Determine whether the directory exists.
    ///             if (fileSystem.DirectoryExists(path))
    ///             {
    ///                 Console.WriteLine("That path already exists.");
    ///                 return;
    ///             }
    ///
    ///             // Try to create the directory.
    ///             fileSystem.CreateDirectory(path);
    ///
    ///             Console.WriteLine($"The directory at path: {path} was created.");
    ///
    ///             // Now lets delete it.
    ///             fileSystem.DeleteDirectory(path);
    ///
    ///             Console.WriteLine($"The directory at path: {path} was deleted.");
    ///         }
    ///         catch (Exception ex)
    ///         {
    ///             Console.WriteLine("The process failed: {0}", ex.ToString());
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    public void DeleteDirectory(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException($"The specified {nameof(path)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(path));
        }

        this.directory.Delete(path, true);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="path" /> parameter is null, empty of contains only whitespace characters.
    /// </exception>
    /// <example>
    /// Below is an example of how to delete a file.
    /// <code title="DeleteFileExample.cs">
    /// namespace FileSystemExample;
    ///
    /// using System;
    /// using FinalEngine.IO;
    /// using FinalEngine.IO.Invocation;
    ///
    /// public class DeleteFileExample
    /// {
    ///     public static void Main()
    ///     {
    ///         // You shouldn't need to create a file system when using an appropriate runtime.
    ///         // Instead you can access the protected FileSystem property within your game.
    ///         // This is provided as an example to show how to instantiate the class if needed.
    ///         var file = new FileInvoker();
    ///         var directory = new DirectoryInvoker();
    ///         var fileSystem = new FileSystem(file, directory);
    ///
    ///         const string filePath = @"C:\MyFile.txt";
    ///
    ///         try
    ///         {
    ///             // Determine whether the file exists.
    ///             if (!fileSystem.FileExists(filePath))
    ///             {
    ///                 Console.WriteLine("That file doesn't exist.");
    ///                 return;
    ///             }
    ///
    ///             // Finally, delete the file.
    ///             fileSystem.DeleteFile(filePath);
    ///         }
    ///         catch (Exception ex)
    ///         {
    ///             Console.WriteLine("The process failed: {0}", ex.ToString());
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    public void DeleteFile(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException($"The specified {nameof(path)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(path));
        }

        this.file.Delete(path);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="path" /> parameter is null, empty of contains only whitespace characters.
    /// </exception>
    /// <example>
    /// The below example checks if a directory doesn't exist and if so, creates a new file and writes to it.
    /// <code title="DirectoryExistsExample.cs">
    /// namespace FileSystemExample;
    ///
    /// using System;
    /// using System.IO;
    /// using FinalEngine.IO;
    /// using FinalEngine.IO.Invocation;
    ///
    /// public class DirectoryExistsExample
    /// {
    ///     public static void Main()
    ///     {
    ///         // You shouldn't need to create a file system when using an appropriate runtime.
    ///         // Instead you can access the protected FileSystem property within your game.
    ///         // This is provided as an example to show how to instantiate the class if needed.
    ///         var file = new FileInvoker();
    ///         var directory = new DirectoryInvoker();
    ///         var fileSystem = new FileSystem(file, directory);
    ///
    ///         const string path = @"C:\MyDir";
    ///
    ///         try
    ///         {
    ///             // If the directory doesn't exist.
    ///             if (!fileSystem.DirectoryExists(path))
    ///             {
    ///                 // Create the directory.
    ///                 fileSystem.CreateDirectory(path);
    ///
    ///                 // Now we can create files and add it to the new directory.
    ///                 const string filePath = $"{path}\\textFile.txt";
    ///
    ///                 // Create the file.
    ///                 using (var stream = fileSystem.CreateFile(filePath))
    ///                 {
    ///                     // Write to the file.
    ///                     using (var writer = new StreamWriter(stream))
    ///                     {
    ///                         writer.WriteLine("Hello, World!");
    ///                     }
    ///                 }
    ///
    ///                 // And, let's be sure and check if the file exists.
    ///                 if (!fileSystem.FileExists(filePath))
    ///                 {
    ///                     Console.WriteLine("Something went horribly wrong!");
    ///                     return;
    ///                 }
    ///             }
    ///         }
    ///         catch (Exception ex)
    ///         {
    ///             Console.WriteLine("The process failed: {0}", ex.ToString());
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    public bool DirectoryExists(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException($"The specified {nameof(path)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(path));
        }

        return this.directory.Exists(path);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="path" /> parameter is null, empty of contains only whitespace characters.
    /// </exception>
    /// <example>
    /// The example below check if a file doesn't exist and if so, creates a new text file and writes to it.
    /// <code title="FileExistsExample.cs">
    /// namespace FileSystemExample;
    ///
    /// using System;
    /// using System.IO;
    /// using FinalEngine.IO;
    /// using FinalEngine.IO.Invocation;
    ///
    /// public class FileExistsExample
    /// {
    ///     public static void Main()
    ///     {
    ///         // You shouldn't need to create a file system when using an appropriate runtime.
    ///         // Instead you can access the protected FileSystem property within your game.
    ///         // This is provided as an example to show how to instantiate the class if needed.
    ///         var file = new FileInvoker();
    ///         var directory = new DirectoryInvoker();
    ///         var fileSystem = new FileSystem(file, directory);
    ///
    ///         const string filePath = @"C:\text.txt";
    ///
    ///         try
    ///         {
    ///             // If the file doesn't exist.
    ///             if (!fileSystem.FileExists(filePath))
    ///             {
    ///                 // Create the file and write to it.
    ///                 using (var stream = fileSystem.CreateFile(filePath))
    ///                 {
    ///                     // Write to the file.
    ///                     using (var writer = new StreamWriter(stream))
    ///                     {
    ///                         writer.WriteLine("This is new file.");
    ///                     }
    ///                 }
    ///             }
    ///         }
    ///         catch (Exception ex)
    ///         {
    ///             Console.WriteLine("The process failed: {0}", ex.ToString());
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    public bool FileExists(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException($"The specified {nameof(path)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(path));
        }

        return this.file.Exists(path);
    }

    /// <summary>
    /// Gets the extension from the file at the specified <paramref name="path" /> (including the period).
    /// </summary>
    /// <param name="path">
    /// The path of the file.
    /// </param>
    /// <returns>
    /// The extension of the file at the specified <paramref name="path" /> (including the period).
    /// </returns>
    /// <exception cref="ArgumentException">
    /// The specified <paramref name="path"/> parameter cannot be null, empty or consist of only whitespace characters.
    /// </exception>
    public string? GetExtension(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException($"The specified {nameof(path)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(path));
        }

        return this.path.GetExtension(path);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="path" /> parameter is null, empty of contains only whitespace characters.
    /// </exception>
    /// <example>
    /// The example below creates a text file, disposes of the returned stream, reopens the write to it and then reopens the file once more to read what was written.
    /// <code title="OpenFileExample.cs">
    /// namespace FileSystemExample;
    ///
    /// using System;
    /// using System.IO;
    /// using FinalEngine.IO;
    /// using FinalEngine.IO.Invocation;
    ///
    /// public class OpenFileExample
    /// {
    ///     public static void Main()
    ///     {
    ///         // You shouldn't need to create a file system when using an appropriate runtime.
    ///         // Instead you can access the protected FileSystem property within your game.
    ///         // This is provided as an example to show how to instantiate the class if needed.
    ///         var file = new FileInvoker();
    ///         var directory = new DirectoryInvoker();
    ///         var fileSystem = new FileSystem(file, directory);
    ///
    ///         const string filePath = @"C:\text.txt";
    ///
    ///         try
    ///         {
    ///             // If the file doesn't exist, create one just to be sure.
    ///             if (!fileSystem.FileExists(filePath))
    ///             {
    ///                 var stream = fileSystem.CreateFile(filePath);
    ///                 stream.Dispose(); // Don't forget to dispose of the stream.
    ///             }
    ///
    ///             // For the example, let's open the file and write to it now.
    ///             // We'll give the stream explicit write only access.
    ///             using (var stream = fileSystem.OpenFile(filePath, FileAccessMode.Write))
    ///             {
    ///                 using (var writer = new StreamWriter(stream))
    ///                 {
    ///                     writer.WriteLine("Hello, World!");
    ///                 }
    ///             }
    ///
    ///             // Now, let's reopen the file with read only access.
    ///             // Note: you can also open the file with read and write access.
    ///             using (var stream = fileSystem.OpenFile(filePath, FileAccessMode.Read))
    ///             {
    ///                 using (var reader = new StreamReader(stream))
    ///                 {
    ///                     string line = reader.ReadLine();
    ///
    ///                     if (line != null)
    ///                     {
    ///                         Console.WriteLine($"Line Read: {line}");
    ///                     }
    ///                 }
    ///             }
    ///         }
    ///         catch (Exception ex)
    ///         {
    ///             Console.WriteLine("The process failed: {0}", ex.ToString());
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    public Stream OpenFile(string path, FileAccessMode mode)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException($"The specified {nameof(path)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(path));
        }

        var access = mode switch
        {
            FileAccessMode.Read => FileAccess.Read,
            FileAccessMode.Write => FileAccess.Write,
            FileAccessMode.ReadAndWrite => FileAccess.ReadWrite,
            _ => FileAccess.Read,
        };

        return this.file.Open(path, FileMode.Open, access);
    }
}
