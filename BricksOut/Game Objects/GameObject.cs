using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BricksOut
{
    /// <summary>
    /// A parent class for all the game objects.
    /// </summary>
    public class GameObject
    {
        protected Texture2D sprite;
        protected Rectangle drawRectangle;

        /// <summary>
        /// Creates a game object with the specified sprite texture.
        /// </summary>
        /// <param name="spriteTexture">The sprite texture for the game object.</param>
        public GameObject(Texture2D spriteTexture)
        {
            sprite = spriteTexture;
        }

        /// <summary>
        /// Gets the draw rectangle of the game object.
        /// </summary>
        public virtual Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        /// <summary>
        /// Draws the game object.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, Color.White);
        }       
    }
}