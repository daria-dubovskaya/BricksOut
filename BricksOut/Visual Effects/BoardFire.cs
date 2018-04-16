using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BricksOut
{
    /// <summary>
    /// A class for an animated fire 
    /// that burst from the stop board.
    /// </summary>
    public class BoardFire
    {
        private Texture2D spriteStrip;
        private Rectangle drawRectangle;
        private Rectangle sourceRectangle;

        private const int FramesPerRow = 5;
        private const int NumRows = 5;
        private const int FrameDuration = 30;
        private int numFrames;
        private int frameWidth;
        private int frameHeight;
        private int elapsedTime;
        private int currentFrame = 0;

        private bool isFading;
        private bool isFinished;
        private bool playing = true;

        // position of the fire relating to the board.
        private Vector2 posInBoard;

        /// <summary>
        /// Creates a new fire that burst from the board.
        /// </summary>
        /// <param name="sprite">The sprite sheet for the board fire.</param>
        /// <param name="x">The x position relating to the board.</param>
        /// <param name="y">The y position relating to the board.</param>
        /// <param name="board">The stop board.</param>
        public BoardFire(Texture2D sprite, int x, int y, StopBoard board)
        {
            spriteStrip = sprite;

            frameWidth = spriteStrip.Width / FramesPerRow;
            frameHeight = spriteStrip.Height / NumRows;
            numFrames = FramesPerRow * NumRows;

            posInBoard = new Vector2(x, y);
            int xPos = board.DrawRectangle.X + (int)posInBoard.X;
            int yPos = board.DrawRectangle.Y + (int)posInBoard.Y;

            drawRectangle = new Rectangle(xPos, yPos, frameWidth, frameHeight);
            sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
        }

        #region Properties

        /// <summary>
        /// Set whether the fire if fading out.
        /// </summary>
        public bool IsFading
        {
            set { isFading = value; }
        }

        /// <summary>
        /// Gets whether the animation is finished.
        /// </summary>
        public bool IsFinished
        {
            get { return isFinished; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the fire animation.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="board">The stop board the fire is bursting from.</param>
        public void Update(GameTime gameTime, StopBoard board)
        {
            if (playing)
            {
                drawRectangle.X = board.DrawRectangle.X + (int)posInBoard.X;
                drawRectangle.Y = board.DrawRectangle.Y + (int)posInBoard.Y;

                PlayAnimation(gameTime);
            }
        }

        /// <summary>
        /// Draws the fire animation.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (playing)
            {
                spriteBatch.Draw(spriteStrip, drawRectangle, sourceRectangle, Color.White);
            }
        }

        #endregion

        #region Private Methods

        private void PlayAnimation(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            if (elapsedTime >= FrameDuration)
            {
                elapsedTime = 0;

                if (isFading == false)
                {
                    if (currentFrame < 21)
                    {
                        currentFrame++;
                        SetSourceRectangle(currentFrame);
                    }
                    else
                    {
                        currentFrame = 4;
                        SetSourceRectangle(currentFrame);
                    }
                }
                else
                {
                    if (currentFrame < numFrames)
                    {
                        currentFrame++;
                        SetSourceRectangle(currentFrame);
                    }
                    else
                    {
                        playing = false;
                        isFinished = false;
                    }
                }
            }
        }

        // Set sourse rectangle based on frame number. 
        // Only for sprite sheets with equal amount of frames longwise and heightwise.
        private void SetSourceRectangle(int frameNumber)
        {
            sourceRectangle.X = (frameNumber % FramesPerRow) * frameWidth;
            sourceRectangle.Y = (frameNumber / NumRows) * frameHeight;
        }

        #endregion
    }
}