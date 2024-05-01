// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osuTK;
using osuTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;
using System;

namespace osu.Framework.Graphics.Colour
{
    /// <summary>
    /// A wrapper struct around Color4 that takes care of converting between sRGB and linear colour spaces.
    /// Internally this struct stores the colour in sRGB space, which is exposed by the <see cref="SRGB"/> member.
    /// This struct converts to linear space by using the <see cref="Linear"/> member.
    /// </summary>
    public readonly struct SRGBColour : IEquatable<SRGBColour>
    {
        /// <summary>
        /// A <see cref="Color4"/> representation of this colour in the sRGB space.
        /// </summary>
        public readonly Color4 SRGB;

        /// <summary>
        /// A <see cref="Color4"/> representation of this colour in the linear space.
        /// </summary>
        public Color4 Linear => SRGB.ToLinear();

        /// <summary>
        /// The alpha component of this colour.
        /// </summary>
        public float Alpha => SRGB.A;

        private SRGBColour(Color4 color) => SRGB = color;
        private SRGBColour(Colour4 colour) => SRGB = colour;

        public static SRGBColour FromLinear(Color4 color) => new SRGBColour(color.ToSRGB());
        public static SRGBColour FromLinear(Colour4 colour) => new SRGBColour(colour.ToSRGB());

        public static SRGBColour FromSRGB(Color4 color) => new SRGBColour(color);
        public static SRGBColour FromSRGB(Colour4 colour) => new SRGBColour(colour);

        [Obsolete("Use FromSRGB or FromLinear instead")]
        public static implicit operator SRGBColour(Color4 value) => new SRGBColour(value);

        [Obsolete("Use FromSRGB or FromLinear instead")]
        public static implicit operator Color4(SRGBColour value) => value.SRGB;

        [Obsolete("Use FromSRGB or FromLinear instead")]
        public static implicit operator SRGBColour(Colour4 value) => new SRGBColour(value);

        [Obsolete("Use FromSRGB or FromLinear instead")]
        public static implicit operator Colour4(SRGBColour value) => value.SRGB;

        public static SRGBColour operator *(SRGBColour first, SRGBColour second)
        {
            var firstLinear = first.Linear;
            var secondLinear = second.Linear;

            return FromLinear
            (
                new Color4(
                    firstLinear.R * secondLinear.R,
                    firstLinear.G * secondLinear.G,
                    firstLinear.B * secondLinear.B,
                    firstLinear.A * secondLinear.A)
            );
        }

        public static SRGBColour operator *(SRGBColour first, float second)
        {
            var firstLinear = first.Linear;

            return FromLinear
            (
                new Color4(
                    firstLinear.R * second,
                    firstLinear.G * second,
                    firstLinear.B * second,
                    firstLinear.A * second)
            );
        }

        public static SRGBColour operator /(SRGBColour first, float second) => first * (1 / second);

        public static SRGBColour operator +(SRGBColour first, SRGBColour second)
        {
            var firstLinear = first.Linear;
            var secondLinear = second.Linear;

            return FromLinear
            (
                new Color4(
                    firstLinear.R + secondLinear.R,
                    firstLinear.G + secondLinear.G,
                    firstLinear.B + secondLinear.B,
                    firstLinear.A + secondLinear.A)
            );
        }

        public Vector4 ToVector() => new Vector4(SRGB.R, SRGB.G, SRGB.B, SRGB.A);
        public static SRGBColour FromVector(Vector4 v) => new SRGBColour(new Color4(v.X, v.Y, v.Z, v.W));

        /// <summary>
        /// Returns a new <see cref="SRGBColour"/> with the alpha value multiplied by the given factor.
        /// </summary>
        /// <param name="alpha">The alpha factor to multiply with.</param>
        /// <returns>A new <see cref="SRGBColour"/> instance with the adjusted alpha value.</returns>
        public SRGBColour WithMultipliedAlpha(float alpha) => new SRGBColour(new Color4(SRGB.R, SRGB.G, SRGB.B, SRGB.A * alpha));

        public bool Equals(SRGBColour other) => SRGB.Equals(other.SRGB);
        public override string ToString() => $"srgb: {SRGB}, linear: {Linear}";
    }
}
