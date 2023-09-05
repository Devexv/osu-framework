// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Transforms;
using osuTK;

namespace osu.Framework.Graphics.Visualisation
{
    internal partial class DrawableTransform : CompositeDrawable
    {
        private readonly Transform transform;
        private readonly Box applied;
        private readonly Box appliedToEnd;
        private readonly SpriteText text;

        public DrawableTransform(Transform transform, float height = 20)
        {
            this.transform = transform;
            RelativeSizeAxes = Axes.X;
            Height = height;
            InternalChildren = new Drawable[]
            {
                applied = new Box { Size = new Vector2(height) },
                appliedToEnd = new Box { X = height + 2, Size = new Vector2(height) },
                text = new SpriteText { X = (height + 2) * 2, Font = FrameworkFont.Regular.With(size: height) },
            };
        }

        protected override void Update()
        {
            base.Update();
            applied.Colour = transform.Applied ? SRGBColour.Green : SRGBColour.Red;
            appliedToEnd.Colour = transform.AppliedToEnd ? SRGBColour.Green : SRGBColour.Red;
            text.Text = transform.ToString();
        }
    }
}
