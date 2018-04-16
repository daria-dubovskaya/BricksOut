using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BricksOut
{
    /// <summary>
    /// An enumeration for possible bonus types.
    /// </summary>
    public enum BonusType
    {
        FireBall,
        ExtraBalls,
        ExtraLife
    }

    /// <summary>
    /// A class for a Bonus object.
    /// Constructor chooses arbitrarily the type
    /// of the new bonus and loads an appropriate sprite texture.
    /// </summary>
    public class Bonus
    {
        private Texture2D sprite;
        private Rectangle drawRectangle;
        private BonusType type;

        private const float  VelocityY = 0.125f;
        private const int ValidityEdgeY = 640;
        private const int Diameter = 60;
        private bool isActive = true;

        private Random rand = new Random();

        // animation support
        private const int FramesPerRow = 6;
        private const int NumRows = 6;
        private const int FrameDuration = 30;
        private int frameWidth;
        private int frameHeight;
        private Rectangle sourceRectangle;
        private int elapsedTime;            

        /// <summary>
        /// Creates a new bonus at the giving position.
        /// </summary>
        /// <param name="position">The position of the new bonus.</param>
        /// <param name="content">The Content Manager for loading content.</param>
        public Bonus(Vector2 position, ContentManager content)
        {
            type = GetRandomBonusType();
            SetBonusSprite(type);

            frameWidth = sprite.Width / FramesPerRow;
            frameHeight = sprite.Height / NumRows;

            drawRectangle = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
            sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
        }

        #region Properties

        /// <summary>
        /// Gets and sets whether or not the bonus is active.
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        /// <summary>
        /// Gets the collision rectangle of the bonus.
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get
            {
                // calculate the collision rectangle taking into account the glow of the bonus object
                int x = drawRectangle.X + (drawRectangle.Width - Diameter) / 2;
                int y = drawRectangle.Y + (drawRectangle.Height - Diameter) / 2;

                return new Rectangle(x, y, Diameter, Diameter);
            }
        }

        /// <summary>
        /// Gets the bonus type.
        /// </summary>
        public BonusType Type
        {
            get { return type; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the bonus object.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            drawRectangle.Y += (int)(VelocityY * gameTime.ElapsedGameTime.Milliseconds);
            if (drawRectangle.Top > ValidityEdgeY)
                isActive = false;

            PlayAnimation(gameTime);
        }

        /// <summary>
        /// Draws the bonus object.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, sourceRectangle, Color.White);
        }

        #endregion

        #region Private Methods

        private BonusType GetRandomBonusType()
        {
            int x = rand.Next(5);

            if (x == 1 || x == 2)
                return BonusType.FireBall;
            else if (x == 3 || x == 4)
                return BonusType.ExtraBalls;
            else return BonusType.ExtraLife;
        }

        private void SetBonusSprite(BonusType bonusType)
        {
            switch (type)
            {
                case BonusType.ExtraLife:
                    sprite = GameScreen.extraLifeBonusSprite;
                    break;
                case BonusType.FireBall:
                    sprite = GameScreen.fireBallBonusSprite;
                    break;
                default:
                    sprite = GameScreen.extraBallBonusSprite;
                    break;
            }
        }

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

        #endregion
    }
}