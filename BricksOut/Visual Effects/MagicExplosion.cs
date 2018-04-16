using Microsoft.Xna.Framework;

namespace BricksOut
{
    /// <summary>
    /// A class for an animated brick magic explosion.
    /// This explosion type has four possible variants of sprite.
    /// Constructor sets one of them randomly.
    /// </summary>
    public class MagicExplosion : Destruction
    {
        private const int FramesPerRow = 6;
        private const int NumRows = 6;

        private readonly Vector2 explosionPos1 = new Vector2(52, 94);
        private readonly Vector2 explosionPos2 = new Vector2(42, 70);
        private readonly Vector2 explosionPos3 = new Vector2(48, 60);
        private readonly Vector2 explosionPos4 = new Vector2(63, 45);

        /// <summary>
        /// Creates a new magic explosion of brick.
        /// </summary>
        /// <param name="brickPosition">The brick position.</param>
        /// <param name="delay">The time delay.</param>
        public MagicExplosion(Vector2 brickPosition, int delay) : base(delay)
        {
            SetExplosion(brickPosition);

            frameWidth = spriteStrip.Width / FramesPerRow;
            frameHeight = spriteStrip.Height / NumRows;
            drawRectangle = new Rectangle(posX, posY, frameWidth, frameHeight);
            sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
        }

        private void SetExplosion(Vector2 brickPosition)
        {
            int randNumber = rand.Next(4);

            switch (randNumber)
            {
                case 0:
                    {
                        spriteStrip = GameScreen.magicExplosionSprite1;
                        posX = (int)brickPosition.X - (int)explosionPos1.X;
                        posY = (int)brickPosition.Y - (int)explosionPos1.Y;
                        break;
                    }
                case 1:
                    {
                        spriteStrip = GameScreen.magicExplosionSprite2;
                        posX = (int)brickPosition.X - (int)explosionPos2.X;
                        posY = (int)brickPosition.Y - (int)explosionPos2.Y;
                        break;
                    }
                case 2:
                    {
                        spriteStrip = GameScreen.magicExplosionSprite3;
                        posX = (int)brickPosition.X - (int)explosionPos3.X;
                        posY = (int)brickPosition.Y - (int)explosionPos3.Y;
                        break;
                    }
                default:
                    {
                        spriteStrip = GameScreen.magicExplosionSprite4;
                        posX = (int)brickPosition.X - (int)explosionPos4.X;
                        posY = (int)brickPosition.Y - (int)explosionPos4.Y;
                        break;
                    }
            }
        }
    }
}