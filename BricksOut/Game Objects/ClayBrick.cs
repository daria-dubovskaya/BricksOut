using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace BricksOut
{
    /// <summary>
    /// A class for a clay brick. 
    /// This brick needs 3 hit to be destroyed.
    /// </summary>
    public class ClayBrick : Brick
    {
        private Rectangle sourceRectangle;
        private const int ImagesPerRow = 3;
        private int brickWidth;

        /// <summary>
        /// Creates the three hit brick.
        /// </summary>
        /// <param name="spriteTexture">The sprite texture for the three hit brick.</param>
        /// <param name="x">The x location of the brick.</param>
        /// <param name="y">The y location of the brick.</param>
        /// <param name="hitSound">The sound effect of the ball impact with the brick.</param>
        /// <param name="destSound">The sound effect of the brick destruction.</param>
        public ClayBrick(Texture2D spriteTexture, int x, int y, SoundEffect hitSound, SoundEffect destSound) : 
            base(spriteTexture)
        {
            brickWidth = sprite.Width / ImagesPerRow;
            drawRectangle = new Rectangle(x, y, brickWidth, sprite.Height);
            sourceRectangle = new Rectangle(0, 0, brickWidth, sprite.Height);
            destructionSound = destSound;
            this.hitSound = hitSound;

            destroyable = true;
            maxHits = 3;
        }

        #region Public Methods

        /// <summary>
        /// Updates the brick.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            sourceRectangle.X = brickWidth * hits;

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the brick.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, sourceRectangle, Color.White);
        }

        #endregion
    }
}