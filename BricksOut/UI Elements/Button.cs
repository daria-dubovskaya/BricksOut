using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BricksOut
{
    /// <summary>
    /// A class for a Button element.
    /// Inherits from UIElement class.
    /// </summary>
    public class Button : UIElement
    {
        protected const int ImagesPerRow = 2;
        protected int buttonWidth;
        protected Rectangle sourceRectangle;

        /// <summary>
        /// Creates a new Button element.
        /// </summary>
        /// <param name="sprite">The sprite texture for the button element.</param>
        /// <param name="position">The position of the button element.</param>
        public Button(Texture2D sprite, Vector2 position) : base(sprite)
        {
            buttonWidth = (spriteTexture.Width - 1) / ImagesPerRow;
            sourceRectangle = new Rectangle(0, 0, buttonWidth, spriteTexture.Height);
            drawRectangle = new Rectangle((int)position.X, (int)position.Y, buttonWidth, spriteTexture.Height);
        }

        /// <summary>
        /// Draws the button.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, drawRectangle, sourceRectangle, Color.White);
        }
    }
}