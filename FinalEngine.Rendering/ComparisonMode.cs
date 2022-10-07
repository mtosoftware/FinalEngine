// <copyright file="ComparisonMode.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    /// <summary>
    ///   Enumerates the available comparison modes that determine how new values are compared with existing values in a depth or stencil comparison.
    /// </summary>
    public enum ComparisonMode
    {
        /// <summary>
        ///   Specifies that the comparison never succeeds.
        /// </summary>
        Never,

        /// <summary>
        ///   Specifies that the comparison succeeds when the new value is less than the existing value.
        /// </summary>
        Less,

        /// <summary>
        ///   Specifies that the comparison succeeds when the new value is equal to the existing value.
        /// </summary>
        Equal,

        /// <summary>
        ///   Specifies that the comparison succeeds when the new value is less than or equal to the existing value.
        /// </summary>
        LessEqual,

        /// <summary>
        ///   Specifies that the comparison succeeds when the new value is greater than the existing value.
        /// </summary>
        Greater,

        /// <summary>
        ///   Specifies that the comparison succeeds when the new value is not equal to the existing value.
        /// </summary>
        NotEqual,

        /// <summary>
        ///   Specifies that the comparison succeeds when the new value is greater than or equal to the existing value.
        /// </summary>
        GreaterEqual,

        /// <summary>
        ///   Specifies that the comparison always succeeds.
        /// </summary>
        Always,
    }
}
