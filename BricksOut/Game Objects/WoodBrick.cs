using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace BricksOut
{
    /// <summary>
    /// A class for a Wood Brick.
    /// </summary>
    public class WoodBrick : Brick
    {
        /// <summary>
        /// Creates a wood brick object.
        /// </summary>
        /// <param name="spriteTexture">The sprite texture for the wood brick.</param>
        /// <param name="x">The x location of the brick.</param>
        /// <param name="y">The y location of the brick.</param>
        /// <param name="destSound">The sound effect of the brick destruction.</param>
        public WoodBrick(Texture2D spriteTexture, int x, int y, SoundEffect destSound) : base(spriteTexture, x, y)
        {
            destructionSound = destSound;
            destroyable = true;
            maxHits = 1;
        }
    }
}