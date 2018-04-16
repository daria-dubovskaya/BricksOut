using BricksOut.SreenManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BricksOut
{
    /// <summary>
    /// A parent class for all the screens in the game.
    /// </summary>
    public class Screen
    {
        private ScreenManager screenManager;

        protected bool isActive = true;
        protected ContentManager content;
        protected Color fontColor = new Color(216, 216, 216);
        protected SpriteFont gameFont13;
        protected SpriteFont gameFont18;
        protected SpriteFont gameFont35;

        /// <summary>
        /// Creates a new instance of screen.
        /// </summary>
        public Screen()
        {
        }

        #region Properties

        /// <summary>
        /// Gets and sets whether the screen is active.
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        
        /// <summary>
        /// Gets and sets the manager this screen belongs to. 
        /// </summary>
        public ScreenManager ScreenManager
        {
            get { return screenManager; }
            set { screenManager = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads all the content of the screen.
        /// </summary>
        public virtual void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameFont13 = content.Load<SpriteFont>(@"fonts\gamefont13,5");
            gameFont18 = content.Load<SpriteFont>(@"fonts\gamefont18");
            gameFont35 = content.Load<SpriteFont>(@"fonts\gamefont35");
        }

        /// <summary>
        /// Unloads screen content.
        /// </summary>
        public void UnloadContent()
        {
            if (content != null)
                content.Unload();
        }

        /// <summary>
        /// Updates the screen.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Draws the screen.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        /// <summary>
        /// Inactivates and removes the screen.
        /// </summary>
        public void ExitScreen()
        {
            isActive = false;
            screenManager.RemoveScreen(this);
        }

        #endregion
    }
}