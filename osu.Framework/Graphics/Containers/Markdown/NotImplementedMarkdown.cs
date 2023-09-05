﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Markdig.Syntax;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Sprites;

namespace osu.Framework.Graphics.Containers.Markdown
{
    /// <summary>
    /// Visualises a message that displays when a <see cref="IMarkdownObject"/> doesn't have a visual implementation.
    /// </summary>
    public partial class NotImplementedMarkdown : CompositeDrawable, IMarkdownTextComponent
    {
        private readonly IMarkdownObject markdownObject;

        [Resolved]
        private IMarkdownTextComponent parentTextComponent { get; set; } = null!;

        public NotImplementedMarkdown(IMarkdownObject markdownObject)
        {
            this.markdownObject = markdownObject;

            AutoSizeAxes = Axes.Y;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = CreateSpriteText();
        }

        public SpriteText CreateSpriteText()
        {
            var text = parentTextComponent.CreateSpriteText();
            text.Colour = new SRGBColour(255, 0, 0, 255);
            text.Font = text.Font.With(size: 21);
            text.Text = markdownObject.GetType() + " Not implemented.";
            return text;
        }
    }
}
