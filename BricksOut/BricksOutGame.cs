using BricksOut.SreenManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace BricksOut
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class BricksOutGame : Game
    {
        public const int WindowWidth = 1024;
        public const int WindowHeight = 768;

        GraphicsDeviceManager graphics;
        ScreenManager screenManager;

        SoundEffect backgroundMusic;
        static SoundEffectInstance musicInst;
        static float soundVol = 0.1f;

        public BricksOutGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            screenManager = new ScreenManager(this);
            Components.Add(screenManager);
            screenManager.AddScreen(new MainMenuScreen());
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            backgroundMusic = Content.Load<SoundEffect>(@"audio\background_music");
            musicInst = backgroundMusic.CreateInstance();
            musicInst.IsLooped = true;
            musicInst.Volume = 0.2f;
            musicInst.Play();
        }
           
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {          
            base.Update(gameTime);
        }        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(11, 18, 26));
                     
            base.Draw(gameTime);
        }
        

        public static float MusicVolume
        {
            get { return musicInst.Volume; }
            set { musicInst.Volume = value; }
        }

        public static float SoundVolume
        {
            get { return soundVol; }
            set { soundVol = value; }
        }

        public static void PlaySound(SoundEffect sound)
        {
            if (sound != null)
            {
                SoundEffectInstance soundInst = sound.CreateInstance();
                soundInst.Volume = soundVol;
                soundInst.Play();
            }
        }
    }
}
