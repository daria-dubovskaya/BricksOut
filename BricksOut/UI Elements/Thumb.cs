using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BricksOut
{
    /// <summary>
    /// A class for a Thumb element.
    /// </summary>
    public class Thumb : Button
    {
        private bool clickStarted;

        /// <summary>
        /// Creates a new thumb element.
        /// </summary>
        /// <param name="spriteTexture">THe sprite texture for the thumb element.</param>
        /// <param name="position">The position of the thumb element.</param>
        public Thumb(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
        }      
        
        /// <summary>
        /// Gets the x position of the thumb.
        /// </summary>
        public int X
        {
            get { return drawRectangle.X; }
        } 

        /// <summary>
        /// Updates the thumb element.
        /// </summary>
        /// <param name="mouse">The mouse state.</param>
        /// <param name="leftEdge">The slider left edge x position.</param>
        /// <param name="rightEdge">The slider right edge x position.</param>
        public void Update(MouseState mouse, int leftEdge, int rightEdge)
        {
            if (clickStarted == false)
            {
                if (drawRectangle.Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed)
                    clickStarted = true;
            }
            else
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    sourceRectangle.X = buttonWidth + 1;
                    drawRectangle.X = mouse.X - buttonWidth / 2;
                    if (drawRectangle.X < leftEdge)
                        drawRectangle.X = leftEdge;
                    if (drawRectangle.Right > rightEdge)
                        drawRectangle.X = rightEdge - buttonWidth;
                }
                else
                {
                    clickStarted = false;
                    sourceRectangle.X = 0;
                }
            }
        }
    }
}