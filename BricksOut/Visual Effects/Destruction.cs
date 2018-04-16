using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BricksOut
{
    /// <summary>
    /// A parent class for an animated brick destruction.
    /// </summary>
    public class Destruction
    {
        protected int posX;
        protected int posY;

        protected Texture2D spriteStrip;
        protected Rectangle drawRectangle;
        protected Rectangle sourceRectangle;

        protected int frameWidth;
        protected int frameHeight;

        protected Random rand = new Random();

        private const int FrameDuration = 30;
        private int elapsedTime;

        private int timeDelay;

        private bool playing;
        private bool finished;

        public Destruction(int delay)
        {            
            timeDelay = delay;
        }

        /// <summary>
        /// Gets whether the animation is finished.
        /// </summary>
        public bool Finished
        {
            get { return finished; }
        }

        /// <summary>
        /// Updates the destruction.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            if (!playing)
            {
                elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

                if (elapsedTime >= timeDelay)
                {
                    playing = true;
                    elapsedTime = 0;
                }
            }
            else
                PlayAnimation(gameTime);
        }

        /// <summary>
        /// Draws the destruction.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (playing)
            {
                spriteBatch.Draw(spriteStrip, drawRectangle, sourceRectangle, Color.White);
            }
        }
        
        private void PlayAnimation(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            if (elapsedTime >= FrameDuration)
            {
                elapsedTime = 0;                
                sourceRectangle.X += frameWidth;

                if (sourceRectangle.X >= spriteStrip.Width)
                {
                    sourceRectangle.X = 0;
                    sourceRectangle.Y += frameHeight;
                    if (sourceRectangle.Y >= spriteStrip.Height)
                    {
                        playing = false;
                        finished = true;
                    }
                }
            }
        }
    }
}