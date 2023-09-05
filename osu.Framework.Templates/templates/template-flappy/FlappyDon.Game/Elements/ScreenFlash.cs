using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Shapes;

namespace FlappyDon.Game.Elements
{
    /// <summary>
    /// A full-screen white flash used to indicate a collision with a pipe, or when the game scene resets after a game over.
    /// </summary>
    public partial class ScreenFlash : Box
    {
        public ScreenFlash()
        {
            Colour = SRGBColour.White;
            RelativeSizeAxes = Axes.Both;
            Alpha = 0.0f;
        }

        public void Flash(double fadeInDuration, double fadeOutDuration)
        {
            this.FadeIn(fadeInDuration).Then().FadeOut(fadeOutDuration);
        }
    }
}
