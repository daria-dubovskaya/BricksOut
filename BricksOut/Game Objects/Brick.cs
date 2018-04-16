using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace BricksOut
{
    /// <summary>
    /// A parent class for bricks.
    /// </summary>
    public class Brick : GameObject
    {
        private bool isDestroyed = false;
        private bool isActive = true;

        protected bool isDetonating;
        protected bool destroyable;

        protected int hits;
        protected int maxHits;

        protected SoundEffect destructionSound;
        protected SoundEffect hitSound;

        protected int timeDelay;
        private int elapsedTime;

        /// <summary>
        /// Creates a Brick object with the specified sprite texture.
        /// </summary>
        /// <param name="spriteTexture">The sprite texture for the brick.</param>
        public Brick(Texture2D spriteTexture) : base(spriteTexture)
        {
        }

        /// <summary>
        /// Creates a Brick object with the specified sprite texture and location.
        /// </summary>
        /// <param name="spriteTexture">The sprite texture for the brick.</param>
        /// <param name="x">The x location of the brick.</param>
        /// <param name="y">The y location of the btick.</param>
        public Brick(Texture2D spriteTexture, int x, int y) : base(spriteTexture)
        {
            drawRectangle = new Rectangle(x, y, sprite.Width, sprite.Height);
        }

        #region Properties

        /// <summary>
        /// Gets whether the brick is destroyed.
        /// </summary>
        public bool IsDestroyed
        {
            get { return isDestroyed; }
        }

        /// <summary>
        /// Gets whether the brick is active.
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
        }

        /// <summary>
        /// Gets and sets whether the brick is detonating.
        /// </summary>
        public bool IsDetonating
        {
            get { return isDetonating; }
            set { isDetonating = value; }
        }

        /// <summary>
        /// Gets the brick position.
        /// </summary>
        public Vector2 Position
        {
            get { return new Vector2(drawRectangle.X, drawRectangle.Y); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the brick object.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void Update(GameTime gameTime)
        {
            if (isDestroyed)
            {
                elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedTime >= timeDelay)
                    DestroyBrick();
            }
        }

        /// <summary>
        /// Hits the brick by the ball.
        /// </summary>
        /// <param name="flamingBall">Whether the ball was flaming.</param>
        /// <param name="detonated">Whether this brick was hitted by the detonation.</param>
        /// <param name="delay">The time delay before the brick has to be destroyed.</param>
        public void Hit(bool flamingBall, bool detonated, int delay)
        {
            if (flamingBall)
            {
                MarkAsDestroyed(delay);
                destructionSound = GameScreen.fireExplosionSound;
            }
            else if (detonated)
            {
                MarkAsDestroyed(delay);
            }
            else if (!destroyable)
            {
                if (hitSound != null)
                    BricksOutGame.PlaySound(hitSound);
            }
            else
            {
                hits++;

                if (hits == maxHits)
                    MarkAsDestroyed(delay);
                else if (hitSound != null)
                    BricksOutGame.PlaySound(hitSound);
            }
        }

        #endregion

        #region Private Methods

        private void MarkAsDestroyed(int delay)
        {
            isDestroyed = true;
            timeDelay = delay;
        }

        private void DestroyBrick()
        {
            if (destructionSound != null)
                BricksOutGame.PlaySound(destructionSound);
            isActive = false;
        }

        #endregion
    }
}