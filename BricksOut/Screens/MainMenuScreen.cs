using BricksOut.SreenManager;
using Microsoft.Xna.Framework;
using System;

namespace BricksOut
{
    /// <summary>
    /// A class for a Main Menu Screen.
    /// Inherits from MenuScreen class.
    /// </summary>
    public class MainMenuScreen : MenuScreen
    {    
        private MenuButton gameButton;
        private MenuButton exitButton;
        private MenuButton settingsButton;
        
        /// <summary>
        /// Creates a new Main Menu Screen.
        /// </summary>
        public MainMenuScreen()
        {
            verticalButtonOffset = 291;
            verticalButtonSpasing = 91;
            frameTitle = "MENU";
        }

        /// <summary>
        /// Loads a content for the screen.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            gameButton = new MenuButton(buttonSprite, 
                new Vector2(horizontalButtonOffset, verticalButtonOffset), 
                "Play", gameFont18, buttonSelectedSound);
            settingsButton = new MenuButton(buttonSprite, 
                new Vector2(horizontalButtonOffset, verticalButtonOffset + verticalButtonSpasing), 
                "Settings", gameFont18, buttonSelectedSound);
            exitButton = new MenuButton(buttonSprite,
                new Vector2(horizontalButtonOffset, verticalButtonOffset + verticalButtonSpasing * 2), 
                "Exit", gameFont18, buttonSelectedSound);

            gameButton.Pressed += NewGameSelected;
            settingsButton.Pressed += SettingsSelected;
            exitButton.Pressed += ExitSelected;

            buttons.Add(gameButton);
            buttons.Add(settingsButton);
            buttons.Add(exitButton);
        }

        private void NewGameSelected(object obj, EventArgs arg)
        {
            ExitScreen();
            ScreenManager.AddScreen(new GameScreen());
        }

        private void SettingsSelected(object obj, EventArgs arg)
        {
            ExitScreen();
            ScreenManager.AddScreen(new SettingsScreen(GetType()));
        }

        private void ExitSelected(object obj, EventArgs arg)
        {
            ScreenManager.Game.Exit();
        }       
    }
}