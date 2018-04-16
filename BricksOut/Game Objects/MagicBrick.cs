using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace BricksOut
{
    /// <summary>
    /// A class for a Magic Brick object.
    /// </summary>
    public class MagicBrick : Brick
    {
        // animation support
        private const int FramesPerRow = 8;
        private const int NumRows = 8;
        private const int FrameDuration = 30;
        private int frameWidth;
        private int frameHeight;
        private Rectangle sourceRectangle;
        private int elapsedTime;

        /// <summary>
        /// Creates a magic brick.
        /// </summary>
        /// <param name="spriteTexture">The sprite texture for the magic brick.</param>
        /// <param name="x">The x location of the magic brick.</param>
        /// <param name="y">The y location of the magic brick.</param>
        /// <param name="destSound">The sound effect of the brick destruction.</param>
        public MagicBrick(Texture2D spriteTexture, int x, int y, SoundEffect destSound) : base(spriteTexture)
        {
            frameWidth = sprite.Width / FramesPerRow;
            frameHeight = sprite.Height / NumRows;

            drawRectangle = new Rectangle(x, y, frameWidth, frameHeight);
            sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);

            destructionSound = destSound;
            destroyable = true;
            maxHits = 1;
        }

        #region Public Methods

        /// <summary>
        /// Updates the magic brick object.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            PlayAnimation(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the magic brick object.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, sourceRectangle, Color.White);
        }

        #endregion

        //protected override void GetDestruction()
        //{
        //    base.GetDestruction();

        //    if (destruction == null)
        //        destruction = new MagicExplosion(Position, 0, contentManager);
        //}

        private void PlayAnimation(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            if (elapsedTime >= FrameDuration)
            {
                elapsedTime = 0;
                sourceRectangle.X += frameWidth;

                if (sourceRectangle.X >= sprite.Width)
                {
                    sourceRectangle.X = 0;
                    sourceRectangle.Y += frameHeight;

                    if (sourceRectangle.Y >= sprite.Height)
                        sourceRectangle.Y = 0;
                }
            }
        }
    }
}