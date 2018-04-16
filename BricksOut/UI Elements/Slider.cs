using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BricksOut
{
    /// <summary>
    /// A class for a Slider element.
    /// Inherits from UIElement class.
    /// </summary>
    public class Slider : UIElement
    {
        /// <summary>
        /// Creates a new Slider element.
        /// </summary>
        /// <param name="sprite">The sprite texture for the slider element.</param>
        /// <param name="position">The position of the slider element.</param>
        public Slider(Texture2D sprite, Vector2 position) : base(sprite)
        {
            drawRectangle = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
        }

        /// <summary>
        /// Gets the draw rectangle of the slider.
        /// </summary>
        public Rectangle DrawRectangle
        {
            get { return drawRectangle; }
        }
    }
}