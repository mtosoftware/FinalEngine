namespace FinalEngine.Rendering
{
    using System;

    public static class ObjectExtensions
    {
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