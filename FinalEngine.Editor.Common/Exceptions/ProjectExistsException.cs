// <copyright file="ProjectExistsException.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    ///   Provides an exception that is thrown when a project already exists on the current file system.
    /// </summary>
    /// <seealso cref="Exception"/>
    public class ProjectExistsException : Exception
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="ProjectExistsException"/> class.
        /// </summary>
        public ProjectExistsException()
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="ProjectExistsException"/> class.
        /// </summary>
        /// <param name="message">
        ///   The message that describes the error.
        /// </param>
        public ProjectExistsException(string? message)
            : base(message)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="ProjectExistsException"/> class.
        /// </summary>
        /// <param name="message">
        ///   The message.
        /// </param>
        /// <param name="fileLocation">
        ///   The file location.
        /// </param>
        public ProjectExistsException(string? message, string? fileLocation)
            : base(message)
        {
            this.FileLocation = fileLocation;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="ProjectExistsException"/> class.
        /// </summary>
        /// <param name="message">
        ///   The error message that explains the reason for the exception.
        /// </param>
        /// <param name="innerException">
        ///   The exception that is the cause of the current exception, or a null reference ( <see langword="Nothing"/> in Visual Basic) if no inner exception is specified.
        /// </param>
        public ProjectExistsException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="ProjectExistsException"/> class.
        /// </summary>
        /// <param name="info">
        ///   The <see cref=" SerializationInfo"/> that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        ///   The <see cref="StreamingContext"/> that contains contextual information about the source or destination.
        /// </param>
        protected ProjectExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        ///   Gets the file location of the project that already exists.
        /// </summary>
        /// <value>
        ///   The file location of the project that already exists.
        /// </value>
        public string? FileLocation { get; }
    }
}