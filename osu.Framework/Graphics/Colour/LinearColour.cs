// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics.Contracts;

namespace osu.Framework.Graphics.Colour
{
    /// <summary>
    /// A colour in linearized (not gamma-corrected) sRGB colour space. This should only be used to do calculations on colours.
    /// Otherwise, <see cref="SRGBColour"/> should be preferred.
    /// To convert from the linearized to a gamma-corrected sRGB representation, use the <see cref="ToSRGB"/> method.
    /// </summary>
    public readonly struct LinearColour : IEquatable<LinearColour>
    {
        /// <summary>
        /// The colour values stored in this <see cref="LinearColour"/> struct.
        /// </summary>
        public readonly Colour4 Linear;

        /// <summary>
        /// Create a <see cref="LinearColour"/> from a <see cref="Colour4"/>, treating
        /// the contained colour components as being in linearized sRGB colour space.
        /// </summary>
        /// <param name="colour">The raw colour values to store.</param>
        public LinearColour(Colour4 colour) => Linear = colour;

        /// <summary>
        /// Convert this <see cref="LinearColour"/> into an <see cref="SRGBColour"/> by applying
        /// a gamma correction to the chromatic (RGB) components. The alpha component is untouched.
        /// </summary>
        /// <returns>An <see cref="SRGBColour"/> struct containing the converted values.</returns>
        [Pure]
        public SRGBColour ToSRGB() => new SRGBColour(Linear.ToSRGB());

        public static LinearColour operator +(LinearColour first, LinearColour second) => new LinearColour(first.Linear + second.Linear);

        public static LinearColour operator *(LinearColour first, LinearColour second) => new LinearColour(first.Linear * second.Linear);
        public static LinearColour operator *(LinearColour first, float scalar) => new LinearColour(first.Linear * scalar);
        public static LinearColour operator *(float scalar, LinearColour second) => new LinearColour(second.Linear * scalar);

        public static LinearColour operator /(LinearColour first, float scalar) => new LinearColour(first.Linear / scalar);

        #region Equality

        public bool Equals(LinearColour other) => Linear.Equals(other.Linear);
        public override bool Equals(object? obj) => obj is LinearColour other && Equals(other);
        public override int GetHashCode() => Linear.GetHashCode();

        #endregion
    }
}
