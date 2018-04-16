using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BricksOut
{
    /// <summary>
    /// A class for a Settings Screen. 
    /// Inherits from MenuScreen class.
    /// </summary>
    public class SettingsScreen : MenuScreen
    {

        private Texture2D sliderTexture;
        private Texture2D thumbTexture;
        private Thumb musicThumb;
        private Thumb soundThumb;
        private Slider musicSlider;
        private Slider soundSlider;

        private const int MusicSliderYPos = 358;
        private const int MusicTitleYPos = 310;
        private const int SoundSliderYPos = 450;
        private const int SoundTitleYPos = 402;

        private const string MusicTitle = "Music";
        private const string SoundTitle = "Sound";

        private Vector2 musicTitlePos;
        private Vector2 soundTitlePos;

        private Type previousScreen;

        /// <summary>
        /// Creates a new Settings Screen.
        /// </summary>
        /// <param name="previous">Type of the previous screen.</param>
        public SettingsScreen(Type previous)
        {
            previousScreen = previous;
            frameTitle = "SETTINGS";
        }

        #region Public Methods

        /// <summary>
        /// Loads a content for the Settings Screen.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            sliderTexture = content.Load<Texture2D>(@"graphics\slider");
            thumbTexture = content.Load<Texture2D>(@"graphics\thumb");

            musicSlider = new Slider(sliderTexture, 
                new Vector2(BricksOutGame.WindowWidth / 2 - sliderTexture.Width / 2, MusicSliderYPos));
            float musicThumbInitXPos = musicSlider.DrawRectangle.X + musicSlider.DrawRectangle.Width * BricksOutGame.MusicVolume;
            musicThumb = new Thumb(thumbTexture, new Vector2(musicThumbInitXPos,
                musicSlider.DrawRectangle.Y + musicSlider.DrawRectangle.Height / 2 - thumbTexture.Height / 2));
            musicTitlePos = new Vector2(musicSlider.DrawRectangle.X, MusicTitleYPos);

            soundSlider = new Slider(sliderTexture, 
                new Vector2(BricksOutGame.WindowWidth / 2 - sliderTexture.Width / 2, SoundSliderYPos));
            float soundsThumbInitXPos = soundSlider.DrawRectangle.X + soundSlider.DrawRectangle.Width * BricksOutGame.SoundVolume;
            soundThumb = new Thumb(thumbTexture, new Vector2(soundsThumbInitXPos,
                soundSlider.DrawRectangle.Y + soundSlider.DrawRectangle.Height / 2 - thumbTexture.Height / 2));
            soundTitlePos = new Vector2(soundSlider.DrawRectangle.X, SoundTitleYPos);
        }      

        /// <summary>
        /// Updates the Settings Screen.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            musicThumb.Update(mouse, musicSlider.DrawRectangle.Left, musicSlider.DrawRectangle.Right);
            soundThumb.Update(mouse, soundSlider.DrawRectangle.Left, soundSlider.DrawRectangle.Right);

            SetMusicVolume();
            SetSoundsVolume();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ExitScreen();

                if (previousScreen.Equals(typeof(PauseMenuScreen)))
                    ScreenManager.AddScreen(new PauseMenuScreen());
                else
                    ScreenManager.AddScreen(new MainMenuScreen());
            }
        }

        /// <summary>
        /// Draws the Settings Screen.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Begin();
            musicSlider.Draw(spriteBatch);
            musicThumb.Draw(spriteBatch);
            soundSlider.Draw(spriteBatch);
            soundThumb.Draw(spriteBatch);
            spriteBatch.DrawString(gameFont18, MusicTitle, musicTitlePos, fontColor);
            spriteBatch.DrawString(gameFont18, SoundTitle, soundTitlePos, fontColor);
            spriteBatch.End();
        }

        #endregion

        #region Private Methods

        private void SetMusicVolume()
        {
            int thumbPosition = musicThumb.X - musicSlider.DrawRectangle.X;
            float volume = 1.0f / musicSlider.DrawRectangle.Width * thumbPosition;
            BricksOutGame.MusicVolume = volume;
        }

        private void SetSoundsVolume()
        {
            int thumbPosition = soundThumb.X - soundSlider.DrawRectangle.X;
            float volume = 1.0f / soundSlider.DrawRectangle.Width * thumbPosition;
            BricksOutGame.SoundVolume = volume;
        }

        #endregion
    }
}