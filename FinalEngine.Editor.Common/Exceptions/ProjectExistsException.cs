// <copyright file="ProjectExistsException.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    public class ProjectExistsException : Exception
    {
        public ProjectExistsException()
        {
        }

        public ProjectExistsException(string? message, string fileLocation)
            : base(message)
        {
            this.FileLocation = fileLocation;
        }

        public ProjectExistsException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        protected ProjectExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public string FileLocation { get; }
    }
}