using Microsoft.Xna.Framework;
using System;

namespace BricksOut
{
    /// <summary>
    /// A class for a Game Over Menu Screen.
    /// Inherits from MenuScreen class.
    /// </summary>
    public class GameOverMenuScreen : MenuScreen
    {
        private MenuButton restartButton;
        private MenuButton exitButton;

        /// <summary>
        /// Creates a new Game Over Menu Screen. 
        /// </summary>
        public GameOverMenuScreen()
        {
            verticalButtonOffset = 321;
            verticalButtonSpasing = 85;
            frameTitle = "GAME OVER";
        }

        /// <summary>
        /// Loads a content for the screen.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            restartButton = new MenuButton(buttonSprite, 
                new Vector2(horizontalButtonOffset, verticalButtonOffset), 
                "Restart", gameFont18, buttonSelectedSound);
            exitButton = new MenuButton(buttonSprite,
                new Vector2(horizontalButtonOffset, verticalButtonOffset + verticalButtonSpasing), 
                "Exit", gameFont18, buttonSelectedSound);

            restartButton.Pressed += RestartSelected;
            exitButton.Pressed += ExitSelected;

            buttons.Add(restartButton);
            buttons.Add(exitButton);
        }

        private void RestartSelected(object obj, EventArgs arg)
        {
            ExitScreen();

            foreach (Screen screen in ScreenManager.Screens)
            {
                if (screen is GameScreen)
                {
                    screen.IsActive = true;
                    GameScreen current = (GameScreen)screen;
                    current.RestartLevel();
                }
            }
        }

        private void ExitSelected(object obj, EventArgs arg)
        {
            ExitScreen();

            foreach (Screen screen in ScreenManager.Screens)
            {
                if (screen is GameScreen)
                {
                    screen.ExitScreen();
                    ScreenManager.AddScreen(new MainMenuScreen());
                    break;
                }
            }
        }
    }
}