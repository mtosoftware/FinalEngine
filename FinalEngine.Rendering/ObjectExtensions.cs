namespace FinalEngine.Rendering
{
    using System;

    /// <summary>
    ///   Provides extension methods for an <see cref="object"/>.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///   Converts the specified <paramref name="obj"/> parameter to an object of the specified <typeparamref name="T"/>, type.
        /// </summary>
        /// <typeparam name="T">
        ///   Specifies the type to convert the specified <paramref name="obj"/> too.
        /// </typeparam>
        /// <param name="obj">
        ///   Specifies a <see cref="object"/> that represents the object to convert to the specified <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        ///   The specified <paramref name="obj"/>, converted to a <typeparamref name="T"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="obj"/> parameter is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        ///   The specified <paramref name="obj"/> is not of type <typeparamref name="T"/>.
        /// </exception>
        public static T Cast<T>(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), $"The specified { nameof(obj) } parameter is null.");
            }

            if (!(obj is T casted))
            {
                throw new ArgumentException($"The specified { nameof(obj) } is not of type { nameof(T) }.");
            }

            return casted;
        }
    }
}