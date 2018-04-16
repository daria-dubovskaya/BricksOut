using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BricksOut
{
    /// <summary>
    /// A class for a Ball object.
    /// </summary>
    public class Ball : GameObject
    {
        // animation support
        private const int FramesPerRow = 6;
        private const int NumRows = 6;
        private int frameWidth;
        private int frameHeight;
        private Rectangle sourceRectangle;
        private int elapsedTime;

        private SoundEffect hitSound;
        private Texture2D fireBallSprite;
        private Vector2 ballVelocity;
        private const int BallDiameter = 21;
        private const float BallSpeed = 0.5f;

        private bool isMoving = true;
        private bool isActive = true;
        private bool isFlaming = false;

        /// <summary>
        /// Creates a ball at the giving position and with given velocity.
        /// </summary>
        /// <param name="spriteTexture">The sprite texture for the ball.</param>
        /// <param name="x">The x location of the ball.</param>
        /// <param name="y">The y location of the ball.</param>
        /// <param name="angle">The angle of the ball.</param>
        /// <param name="flaming">Whether ball is flaming.</param>
        /// <param name="fireBall">The sprite texture for tne flaming ball.</param>
        /// <param name="sound">The sound effect of the ball hitting against the walls.</param>
        public Ball(Texture2D spriteTexture, int x, int y, float angle, bool flaming, Texture2D fireBall, SoundEffect sound) :
            base(spriteTexture)
        {
            frameWidth = sprite.Width / FramesPerRow;
            frameHeight = sprite.Height / NumRows;

            drawRectangle = new Rectangle(x, y, frameWidth, frameHeight);
            sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);

            SetBallVelocity(angle);

            fireBallSprite = fireBall;
            hitSound = sound;

            if (flaming)
                isFlaming = true;
        }

        /// <summary>
        /// Creates a ball on the top center of the board with given velocity.
        /// </summary>
        /// <param name="spriteTexture">The sprite texture for the ball.</param>
        /// <param name="boardRect">The drawrwctangle of the board.</param>
        /// <param name="angle">The angle of the ball.</param>
        /// <param name="flaming">Whether ball is flaming.</param>
        /// <param name="fireBall">The sprite texture for tne flaming ball.</param>
        /// <param name="sound">he sound effect of the ball hitting against the walls.</param>
        public Ball(Texture2D spriteTexture, Rectangle boardRect, float angle, bool flaming, Texture2D fireBall, SoundEffect sound) :
            base(spriteTexture)
        {
            frameWidth = sprite.Width / FramesPerRow;
            frameHeight = sprite.Height / NumRows;

            int posX = boardRect.Center.X - frameWidth / 2;
            int posY = boardRect.Y - BallDiameter - (frameHeight - BallDiameter) / 2;
            drawRectangle = new Rectangle(posX, posY, frameWidth, frameHeight);
            sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);

            SetBallVelocity(angle);

            fireBallSprite = fireBall;
            hitSound = sound;

            if (flaming)
                isFlaming = true;
        }

        #region Properties

        /// <summary>
        /// Gets whether the ball is active.
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
        }

        /// <summary>
        /// Gets and sets whether the ball is moving at the moment.
        /// </summary>
        public bool IsMoving
        {
            get { return isMoving; }
            set { isMoving = value; }
        }

        /// <summary>
        /// Gets the collision rectangle of the ball.
        /// </summary>
        public override Rectangle CollisionRectangle
        {
            get
            {
                // calculate the collision rectangle taking into account the glow of the ball object
                int x = drawRectangle.X + (drawRectangle.Width - BallDiameter) / 2;
                int y = drawRectangle.Y + (drawRectangle.Height - BallDiameter) / 2;

                return new Rectangle(x, y, BallDiameter, BallDiameter);
            }
        }

        /// <summary>
        /// Gets and sets the draw rectangle of the ball.
        /// </summary>
        public Rectangle DrawRectangle
        {
            get { return drawRectangle; }
            set { drawRectangle = value; }
        }

        /// <summary>
        /// Sets the sprite texture of the ball object.
        /// </summary>
        public Texture2D Texture
        {
            set { sprite = value; }
        }

        /// <summary>
        /// Sets the x location of the ball.
        /// </summary>
        public int X
        {
            set { drawRectangle.X = value; }
        }

        /// <summary>
        /// Sets the y location of the ball.
        /// </summary>
        public int Y
        {
            set { drawRectangle.Y = value; }
        }

        /// <summary>
        /// Gets and sets the ball velocity.
        /// </summary>
        public Vector2 Velocity
        {
            get { return ballVelocity; }
            set { ballVelocity = value; }
        }

        /// <summary>
        /// Gets the ball angle.
        /// </summary>
        public float Angle
        {
            get { return (float)Math.Acos(ballVelocity.X / BallSpeed); }
        }

        /// <summary>
        /// Gets and sets whether the ball is flaming.
        /// </summary>
        public bool IsFlaming
        {
            get { return isFlaming; }
            set { isFlaming = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Uodates the ball object.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            PlayAnimation(gameTime);

            if (isMoving)
            {
                drawRectangle.X += (int)(ballVelocity.X * gameTime.ElapsedGameTime.Milliseconds);
                drawRectangle.Y += (int)(ballVelocity.Y * gameTime.ElapsedGameTime.Milliseconds);

                BounceBorders();

                // check if the ball is out of the window
                if (drawRectangle.Y > BricksOutGame.WindowHeight)
                    isActive = false;
            }
        }

        /// <summary>
        /// Draws the ball object.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isFlaming)
                spriteBatch.Draw(fireBallSprite, drawRectangle, sourceRectangle, Color.White);
            else
                spriteBatch.Draw(sprite, drawRectangle, sourceRectangle, Color.White);
        }

        /// <summary>
        /// Sets a ball velocity with respect to the ball angle.
        /// </summary>
        /// <param name="angle">The angle of the ball.</param>
        public void SetBallVelocity(float angle)
        {
            ballVelocity = new Vector2(BallSpeed * (float)(Math.Cos(angle)),
                BallSpeed * (float)(Math.Sin(-angle)));
        }

        #endregion

        #region Private Methods

        private void PlayAnimation(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            if (elapsedTime >= GetFrameDuration())
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

        private int GetFrameDuration()
        {
            if (isFlaming)
                return 30;
            else
                return 45;
        }

        private void BounceBorders()
        {
            if (CollisionRectangle.Y < GameScreen.TopBorderWidth)
            {
                drawRectangle.Y = GameScreen.TopBorderWidth - (drawRectangle.Height - CollisionRectangle.Height) / 2;
                ballVelocity.Y *= -1;
                BricksOutGame.PlaySound(hitSound);
            }
            else if (CollisionRectangle.X < GameScreen.LeftBorderWidth)
            {
                drawRectangle.X = GameScreen.LeftBorderWidth - (drawRectangle.Width - CollisionRectangle.Width) / 2;
                ballVelocity.X *= -1;
                BricksOutGame.PlaySound(hitSound);
            }
            else if (CollisionRectangle.Right > BricksOutGame.WindowWidth - GameScreen.InfoFieldWidth)
            {
                drawRectangle.X = BricksOutGame.WindowWidth - GameScreen.InfoFieldWidth - drawRectangle.Width +
                    (drawRectangle.Width - CollisionRectangle.Width) / 2;
                ballVelocity.X *= -1;
                BricksOutGame.PlaySound(hitSound);
            }
        }

        #endregion
    }
}