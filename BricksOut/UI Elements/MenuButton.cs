using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BricksOut
{
    /// <summary>
    /// A class for a Menu Button element.
    /// Inherits from Button class.
    /// </summary>
    public class MenuButton : Button
    {
        public event EventHandler Pressed;

        private bool clickStarted = false;
        private bool buttonReleased = true;
        private bool isPressed = false;
        private bool isSelected = false;

        private string buttonName;
        private Vector2 buttonNamePosition;
        private SpriteFont buttonFont;
        private SoundEffect soundEffect;
        private readonly Color fontColor = new Color(216, 216, 216);

        /// <summary>
        /// Creates a new Menu Button element.
        /// </summary>
        /// <param name="spriteTexture">The sprite texture of the menu button.</param>
        /// <param name="position">The menu button position.</param>
        /// <param name="name">The name of the button.</param>
        /// <param name="font">The font of the name of the button.</param>
        /// <param name="sound">The sound effect of button selection.</param>
        public MenuButton(Texture2D spriteTexture, Vector2 position, string name, SpriteFont font, SoundEffect sound) :
            base(spriteTexture, position)
        {
            buttonName = name;
            buttonFont = font;
            buttonNamePosition = new Vector2(drawRectangle.Center.X - buttonFont.MeasureString(buttonName).X / 2,
                drawRectangle.Center.Y - buttonFont.MeasureString(buttonName).Y / 2);
            soundEffect = sound;           
        }

        /// <summary>
        /// Gets whether the menu button was pressed.
        /// </summary>
        public bool IsPressed
        {
            get { return isPressed; }
        }

        #region Public Methods

        /// <summary>
        /// Updates the menu button.
        /// </summary>
        /// <param name="mouse">The mose state.</param>
        public void Update(MouseState mouse)
        {
            if (drawRectangle.Contains(mouse.X, mouse.Y))
            {
                if (!isSelected)
                {
                    isSelected = true;
                    BricksOutGame.PlaySound(soundEffect);
                }

                // highlight button
                sourceRectangle.X = buttonWidth + 1;

                // check for click started on the button
                if (mouse.LeftButton == ButtonState.Pressed &&
                    buttonReleased)
                {
                    clickStarted = true;
                    buttonReleased = false;
                }
                else if (mouse.LeftButton == ButtonState.Released)
                {
                    buttonReleased = true;

                    // if click finished on button, button was pressed 
                    if (clickStarted)
                    {
                        clickStarted = false;
                        isPressed = true;
                    }
                }
            }
            else
            {
                sourceRectangle.X = 0;
                isSelected = false;
            }
        }

        /// <summary>
        /// Draws the menu button.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawString(buttonFont, buttonName, buttonNamePosition, fontColor);
        }

        #endregion

        /// <summary>
        /// This method is called to raise Pressed event.
        /// </summary>
        public void OnPressed()
        {
            Pressed?.Invoke(this, EventArgs.Empty);
        }
    }
}