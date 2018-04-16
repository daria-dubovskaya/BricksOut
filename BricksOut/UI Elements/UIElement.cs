using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BricksOut
{
    /// <summary>
    /// A parent class for all the UI elements.
    /// </summary>
    public class UIElement
    {
        protected Texture2D spriteTexture;
        protected Rectangle drawRectangle;

        /// <summary>
        /// Creates a new UI element.
        /// </summary>
        /// <param name="sprite">The sprite texture of the elemnt.</param>
        public UIElement(Texture2D sprite)
        {
            spriteTexture = sprite;
        }

        /// <summary>
        /// Draws the UI element.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, drawRectangle, Color.White);
        }
    }
}