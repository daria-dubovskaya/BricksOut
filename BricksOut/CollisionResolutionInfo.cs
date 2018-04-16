using Microsoft.Xna.Framework;

namespace BricksOut
{
    /// <summary>
    /// Information for resolving a collision. Contains the new velocity and 
    /// draw rectangle for the ball. 
    /// </summary>
    public class CollisionResolutionInfo
    {
        Vector2 ballVelocity;
        Rectangle ballRectangle;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="velocity">Ball velocity.</param>
        /// <param name="drawRectangle">Ball draw rectangle.</param>
        public CollisionResolutionInfo(Vector2 velocity,Rectangle drawRectangle)
        {
            ballVelocity = velocity;
            ballRectangle = drawRectangle;          
        }

        /// <summary>
        /// Gets the ball velocity.
        /// </summary>
        public Vector2 BallVelocity
        {
            get { return ballVelocity; }
        }

        /// <summary>
        /// Gets the ball draw rectangle.
        /// </summary>
        public Rectangle BallDrawRectangle
        {
            get { return ballRectangle; }
        }       
    }
}
