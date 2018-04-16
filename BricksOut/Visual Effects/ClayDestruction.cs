using Microsoft.Xna.Framework;

namespace BricksOut
{
    /// <summary>
    /// A class for an animated destruction of clay bricks.
    /// This destruction type has two possible variants of sprite.
    /// Constructor sets one of them randomly.
    /// </summary>
    public class ClayDestruction : Destruction
    {
        private const int FramesPerRow = 6;
        private const int NumRows = 6;

        // position of the destruction relating to the brick.
        private readonly Vector2 destructPos1 = new Vector2(98, 142);
        private readonly Vector2 destructPos2 = new Vector2(102, 197);

        /// <summary>
        /// Creates a new destruction of the clay brick.
        /// </summary>
        /// <param name="brickPosition">The position of the brick.</param>
        /// <param name="delay">The time delay.</param>
        public ClayDestruction(Vector2 brickPosition, int delay) : base(delay)
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
                        spriteStrip = GameScreen.clayBrickDestructionSprite1;
                        posX = (int)brickPosition.X - (int)destructPos1.X;
                        posY = (int)brickPosition.Y - (int)destructPos1.Y;
                        break;
                    }
                default:
                    {
                        spriteStrip = GameScreen.clayBrickDestructionSprite2;
                        posX = (int)brickPosition.X - (int)destructPos2.X;
                        posY = (int)brickPosition.Y - (int)destructPos2.Y;
                        break;
                    }
            }
        }
    }
}