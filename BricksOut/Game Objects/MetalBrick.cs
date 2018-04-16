using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BricksOut
{
    /// <summary>
    /// A class for a Metal Brick object
    /// </summary>
    public class MetalBrick : Brick
    {
        /// <summary>
        /// Creates a metal brick.
        /// </summary>
        /// <param name="spriteTexture">The sprite texture for the metal brick.</param>
        /// <param name="x">The x location of the metal brick.</param>
        /// <param name="y">The y location of the metal brick.</param>
        /// <param name="hitSound">The sound effect of the ball impact with the metal brick.</param>
        public MetalBrick(Texture2D spriteTexture, int x, int y, SoundEffect hitSound) : base(spriteTexture, x, y)
        {
            this.hitSound = hitSound;
        }
    }
}