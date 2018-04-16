using System;
using Microsoft.Xna.Framework;

namespace BricksOut
{
    /// <summary>
    /// A class for  Pause Menu Screen.
    /// Inherits from MenuScreen class.
    /// </summary>
    public class PauseMenuScreen : MenuScreen
    {
        private MenuButton continueButton;
        private MenuButton restartButton;
        private MenuButton exitButton;
        private MenuButton settingsButton;

        /// <summary>
        /// Creates a new Pause Menu Screen.
        /// </summary>
        public PauseMenuScreen()
        {
            verticalButtonOffset = 259;
            verticalButtonSpasing = 80;
        }

        /// <summary>
        /// Loads a content for the screen.
        /// </summary>
        public override void LoadContent()
        {
            frameTitle = "PAUSE";

            base.LoadContent();

            continueButton = new MenuButton(buttonSprite, 
                new Vector2(horizontalButtonOffset, verticalButtonOffset), 
                "Continue", gameFont18, buttonSelectedSound);
            restartButton = new MenuButton(buttonSprite, 
                new Vector2(horizontalButtonOffset, verticalButtonOffset + verticalButtonSpasing), 
                "Restart", gameFont18, buttonSelectedSound);
            settingsButton = new MenuButton(buttonSprite, 
                new Vector2(horizontalButtonOffset, verticalButtonOffset + verticalButtonSpasing * 2), 
                "Settings", gameFont18, buttonSelectedSound);
            exitButton = new MenuButton(buttonSprite,
                new Vector2(horizontalButtonOffset, 
                verticalButtonOffset + verticalButtonSpasing * 3), 
                "Exit", gameFont18, buttonSelectedSound);

            continueButton.Pressed += ContinueSelected;
            restartButton.Pressed += RestartSelected;
            settingsButton.Pressed += SettingsSelected;
            exitButton.Pressed += ExitSelected;

            buttons.Add(continueButton);
            buttons.Add(restartButton);
            buttons.Add(settingsButton);
            buttons.Add(exitButton);
        }

        private void ContinueSelected(object obj, EventArgs arg)
        {
            ExitScreen();

            foreach (Screen screen in ScreenManager.Screens)
            {
                if (screen is GameScreen)
                    screen.IsActive = true;
            }
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

        private void SettingsSelected(object obj, EventArgs arg)
        {
            ExitScreen();
            ScreenManager.AddScreen(new SettingsScreen(GetType()));            
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