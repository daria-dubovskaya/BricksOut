using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BricksOut
{
    /// <summary>
    /// A parent class for all the menu screens.
    /// Inherits from Screen class.
    /// </summary>
    public class MenuScreen : Screen
    {
        private Texture2D menuFrameSprite;
        private Texture2D menuBackgroundSprite;
        private Rectangle backgroundDrawRect;
        private Rectangle frameDrawRect;

        private const int TitleFrameHeight = 53;
        private Vector2 frameTitlePosition;
        protected string frameTitle;

        protected Texture2D buttonSprite;
        protected List<MenuButton> buttons = new List<MenuButton>();
        protected int horizontalButtonOffset;
        protected int verticalButtonOffset;
        protected int verticalButtonSpasing;

        protected SoundEffect buttonSelectedSound;

        /// <summary>
        /// Loads the content for the menu screen.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            buttonSprite = content.Load<Texture2D>(@"graphics\button");
            menuFrameSprite = content.Load<Texture2D>(@"graphics\menu_frame");
            menuBackgroundSprite = content.Load<Texture2D>(@"graphics\menu_background");
            backgroundDrawRect = new Rectangle(0, 0, menuBackgroundSprite.Width, menuBackgroundSprite.Height);
            frameDrawRect = new Rectangle(backgroundDrawRect.Center.X - menuFrameSprite.Width / 2,
                backgroundDrawRect.Center.Y - menuFrameSprite.Height / 2, menuFrameSprite.Width, menuFrameSprite.Height);
            frameTitlePosition = new Vector2(frameDrawRect.Center.X - gameFont18.MeasureString(frameTitle).X / 2,
                frameDrawRect.Y + TitleFrameHeight / 2 - gameFont18.MeasureString(frameTitle).Y / 2);
            horizontalButtonOffset = frameDrawRect.Center.X - buttonSprite.Width / 4;
            buttonSelectedSound = content.Load<SoundEffect>(@"audio\button_select");
        }

        /// <summary>
        /// Updates the menu screen.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            foreach (MenuButton button in buttons)
            {
                button.Update(mouse);

                if (button.IsPressed)
                {
                    button.OnPressed();
                    break;
                }
            }
        }

        /// <summary>
        /// Draws the menu screen.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(menuBackgroundSprite, backgroundDrawRect, Color.White);
            spriteBatch.Draw(menuFrameSprite, frameDrawRect, Color.White);
            spriteBatch.DrawString(gameFont18, frameTitle, frameTitlePosition, fontColor);
            foreach (MenuButton button in buttons)
                button.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}