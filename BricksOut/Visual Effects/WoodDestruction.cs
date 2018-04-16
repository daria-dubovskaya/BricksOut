using Microsoft.Xna.Framework;

namespace BricksOut
{
    /// <summary>
    /// A class for an animated destruction of wood bricks.
    /// This destruction type has two possible variants of sprite.
    /// Constructor sets one of them randomly.
    /// </summary>
    public class WoodDestruction : Destruction
    {
        private const int FramesPerRow = 5;
        private const int NumRows = 5;

        private readonly Vector2 destructPos1 = new Vector2(38, 102);
        private readonly Vector2 destructPos2 = new Vector2(60, 101);

        /// <summary>
        /// Creates a new destruction of the wood brick.
        /// </summary>
        /// <param name="brickPosition">The brick position.</param>
        /// <param name="delay">The time delay.</param>
        public WoodDestruction(Vector2 brickPosition, int delay) : base(delay)
        {
            SetDestruction(brickPosition);

            frameWidth = spriteStrip.Width / FramesPerRow;
            frameHeight = spriteStrip.Height / NumRows;
            drawRectangle = new Rectangle(posX, posY, frameWidth, frameHeight);
            sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
        }

        private void SetDestruction(Vector2 brickPosition)
        {
            int randNumber = rand.Next(2);

            switch (randNumber)
            {
                case 0:
                    {
                        spriteStrip = GameScreen.woodBrickDestructionSprite1;
                        posX = (int)brickPosition.X - (int)destructPos1.X;
                        posY = (int)brickPosition.Y - (int)destructPos1.Y;
                        break;
                    }
                default:
                    {
                        spriteStrip = GameScreen.woodBrickDestructionSprite2;
                        posX = (int)brickPosition.X - (int)destructPos2.X;
                        posY = (int)brickPosition.Y - (int)destructPos2.Y;
                        break;
                    }
            }
        }
    }
}