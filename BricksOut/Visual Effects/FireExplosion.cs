using Microsoft.Xna.Framework;

namespace BricksOut
{
    /// <summary>
    /// A class for an animated brick fire explosion.
    /// This explosion type has three possible variants of sprite.
    /// Constructor sets one of them randomly.
    /// </summary>
    public class FireExplosion : Destruction
    {
        private const int FramesPerRow = 7;
        private const int NumRows = 7;

        private readonly Vector2 explosionPos1 = new Vector2(80, 125);
        private readonly Vector2 explosionPos2 = new Vector2(55, 102);
        private readonly Vector2 explosionPos3 = new Vector2(52, 88);

        /// <summary>
        /// Creates a new fire explosion of the brick.
        /// </summary>
        /// <param name="brickPosition">The brick position.</param>
        /// <param name="delay">The time delay.</param>
        public FireExplosion(Vector2 brickPosition, int delay) : base(delay)
        {
            SetExplosion(brickPosition);

            frameWidth = spriteStrip.Width / FramesPerRow;
            frameHeight = spriteStrip.Height / NumRows;
            drawRectangle = new Rectangle(posX, posY, frameWidth, frameHeight);
            sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
        }

        private void SetExplosion(Vector2 brickPosition)
        {
            int randNumber = rand.Next(3);

            switch (randNumber)
            {
                case 0:
                    {
                        spriteStrip = GameScreen.fireExplosionSprite1;
                        posX = (int)brickPosition.X - (int)explosionPos1.X;
                        posY = (int)brickPosition.Y - (int)explosionPos1.Y;
                        break;
                    }
                case 1:
                    {
                        spriteStrip = GameScreen.fireExplosionSprite2;
                        posX = (int)brickPosition.X - (int)explosionPos2.X;
                        posY = (int)brickPosition.Y - (int)explosionPos2.Y;
                        break;
                    }
                default:
                    {
                        spriteStrip = GameScreen.fireExplosionSprite3;
                        posX = (int)brickPosition.X - (int)explosionPos3.X;
                        posY = (int)brickPosition.Y - (int)explosionPos3.Y;
                        break;
                    }
            }
        }
    }
}