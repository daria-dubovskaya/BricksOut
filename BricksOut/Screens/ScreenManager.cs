using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BricksOut.SreenManager
{
    /// <summary>
    ///  The Screen Manager is a component that inherits
    ///  from the DrawableGameComponent class and manages 
    ///  all the screens in the game.
    /// </summary>
    public class ScreenManager : DrawableGameComponent
    {
        private List<Screen> screens = new List<Screen>();
        private bool initialized;
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Creates a screen manager component.
        /// </summary>
        /// <param name="game"></param>
        public ScreenManager(Game game) : base(game)
        {
        }

        /// <summary>
        /// Gets the list of all the added screens.
        /// </summary>
        public List<Screen> Screens
        {
            get { return screens; }
        }

        /// <summary>
        /// Initializes the screen manager.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            initialized = true;
        }

        /// <summary>
        /// Load all the screens content. 
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (Screen screen in screens)
                screen.LoadContent();
        }

        /// <summary>
        /// Updates active screens.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < screens.Count; i++)
            {
                if (screens[i].IsActive)
                    screens[i].Update(gameTime);
            }
        }

        /// <summary>
        /// Draws active screens. 
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(GameTime gameTime)
        {
            foreach (Screen screen in screens)
            {
                if (screen.IsActive)
                    screen.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Adds a new screen to the screen manager.
        /// </summary>
        /// <param name="screen">The screen is to be added.</param>
        public void AddScreen(Screen screen)
        {
            screen.ScreenManager = this;
            if (initialized)
                screen.LoadContent();
            screens.Add(screen);
        }

        /// <summary>
        /// Removes the screen from the screen manager.
        /// </summary>
        /// <param name="screen">The screen is to be removed.</param>
        public void RemoveScreen(Screen screen)
        {
            screen.UnloadContent();
            screens.Remove(screen);
        }
    }
}
