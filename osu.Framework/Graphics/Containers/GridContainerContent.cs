﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Lists;

namespace osu.Framework.Graphics.Containers
{
    /// <summary>
    /// A wrapper that provides access to the <see cref="GridContainer.Content"/> with element change notifications.
    /// </summary>
    public class GridContainerContent : ObservableArray<ObservableArray<Drawable>>
    {
        private readonly Drawable[][] source;

        public static implicit operator Drawable[][](GridContainerContent content) => content.source;

        public static implicit operator GridContainerContent(Drawable[][] drawables)
        {
            if (drawables == null)
                return null;

            return new GridContainerContent(drawables);
        }

        private GridContainerContent(Drawable[][] drawables)
            : base(new ObservableArray<Drawable>[drawables.Length])
        {
            source = drawables;

            for (int i = 0; i < drawables.Length; i++)
            {
                if (drawables[i] != null)
                {
                    var observableArray = new ObservableArray<Drawable>(drawables[i]);
                    this[i] = observableArray;
                    observableArray.ArrayElementChanged += OnArrayElementChanged;
                }
            }
        }
    }
}
