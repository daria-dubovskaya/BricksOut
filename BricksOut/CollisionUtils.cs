using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BricksOut
{
    /// <summary>
    /// Provides utilities for checking collisions
    /// </summary>
    public static class CollisionUtils
    {
        /// <summary>
        /// An anumerator of possible collision sides.
        /// </summary>
        enum CollisionSide
        {
            Bottom,
            Left,
            Right,
            Top,
            Corner
        }

        /// <summary>
        /// A helper bounding rectangle class.
        /// </summary>
        private class BoundingBallRect
        {
            public Rectangle drawRectangle;
            public Rectangle collisionRectangle;
        }

        #region Public methods     

        /// <summary>
        /// Finds a moment of ball-brick collision. 
        /// Change position and velocity of the ball after collision.
        /// </summary>
        /// <param name="timeStep">The time step.</param>
        /// <param name="ball">The ball.</param>
        /// <param name="brick">The brick.</param>
        /// <returns>The collision resolution info object.</returns>
        public static CollisionResolutionInfo CheckCollision(int timeStep, Ball ball, Brick brick)
        {
            BoundingBallRect initialBallRect = new BoundingBallRect();
            BoundingBallRect currentBallRect = new BoundingBallRect();

            if (ball.CollisionRectangle.Intersects(brick.CollisionRectangle))
            {
                if (!ball.IsFlaming)
                {
                    // initialize non-changing properties
                    currentBallRect.drawRectangle.Width = ball.DrawRectangle.Width;
                    currentBallRect.drawRectangle.Height = ball.DrawRectangle.Height;

                    currentBallRect.collisionRectangle = ball.CollisionRectangle;

                    // back up ball to it's location before the time step 
                    float dX = (ball.Velocity.X * timeStep);
                    float dY = (ball.Velocity.Y * timeStep);
                    initialBallRect.drawRectangle.X = ball.DrawRectangle.X - (int)dX;
                    initialBallRect.drawRectangle.Y = ball.DrawRectangle.Y - (int)dY;
                    initialBallRect.drawRectangle.Width = ball.DrawRectangle.Width;
                    initialBallRect.drawRectangle.Height = ball.DrawRectangle.Height;
                    initialBallRect.collisionRectangle.X = ball.CollisionRectangle.X - (int)dX;
                    initialBallRect.collisionRectangle.Y = ball.CollisionRectangle.Y - (int)dY;

                    // find a moment of the ball-brick collision
                    // at fixed time step of 60 fps, time increment can only be 8, 4, 2, or 1
                    int timeIncrement = timeStep / 2;
                    int collisionDt = timeStep;    // we know we have a collision or we wouldn't be here
                    int dt = timeIncrement;
                    while (timeIncrement > 0)
                    {
                        // move ball forward by dt from its initial position
                        dX = ball.Velocity.X * dt;
                        dY = ball.Velocity.Y * dt;

                        // update ball bounding rectangle
                        currentBallRect.drawRectangle.X = initialBallRect.drawRectangle.X + (int)dX;
                        currentBallRect.drawRectangle.Y = initialBallRect.drawRectangle.Y + (int)dY;
                        currentBallRect.collisionRectangle.X = initialBallRect.collisionRectangle.X + (int)dX;
                        currentBallRect.collisionRectangle.Y = initialBallRect.collisionRectangle.Y + (int)dY;

                        // cut time increment in half as we search for the time of collision
                        timeIncrement /= 2;

                        if (currentBallRect.collisionRectangle.Intersects(brick.CollisionRectangle))
                        {
                            // collision detected, so save collision dt and reduce dt to make it earlier
                            collisionDt = dt;
                            dt -= timeIncrement;
                        }
                        else
                        {
                            // no collision detected, so increase dt to make it later
                            dt += timeIncrement;
                        }
                    }

                    // get rectangle location at start of collision
                    int collisionStartTime = collisionDt;
                    dX = ball.Velocity.X * collisionStartTime;
                    dY = ball.Velocity.Y * collisionStartTime;

                    currentBallRect.drawRectangle.X = initialBallRect.drawRectangle.X + (int)dX;
                    currentBallRect.drawRectangle.Y = initialBallRect.drawRectangle.Y + (int)dY;
                    currentBallRect.collisionRectangle.X = initialBallRect.collisionRectangle.X + (int)dX;
                    currentBallRect.collisionRectangle.Y = initialBallRect.collisionRectangle.Y + (int)dY;

                    // find the area where ball overlaps brick
                    Rectangle intersectionArea = Rectangle.Intersect(currentBallRect.collisionRectangle, brick.CollisionRectangle);
                    CollisionSide collisionSide = GetCollisionSide(currentBallRect.collisionRectangle,
                        intersectionArea, ball.Velocity);

                    // move objects through complete time step
                    int preCollisionDuration = collisionStartTime - 1;
                    int postCollisionDuration = timeStep - collisionStartTime + 1;
                    CollisionResolutionInfo cri = BounceBall(ball.Velocity, initialBallRect.drawRectangle,
                        preCollisionDuration, postCollisionDuration,
                        collisionSide);

                return cri;
                }
                else
                {
                    return new CollisionResolutionInfo(ball.Velocity, ball.DrawRectangle);
                }
            }
            else
            {
                // no collision
                return null;
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Gets the collision side of the ball collision rectangle and 
        /// the ball-brick intersection area. 
        /// </summary>
        /// <param name="ballRect">The ball collision rectangle.</param>
        /// <param name="intersectionRect">Ball-Brick intersection area.</param>
        /// <param name="ballVelocity">The velocity of the ball.</param>
        /// <returns>The collision side.</returns>
        private static CollisionSide GetCollisionSide(Rectangle ballRect, Rectangle intersectionRect, Vector2 ballVelocity)
        {
            List<CollisionSide> collisionSides = GetCollisionSides(ballRect, intersectionRect);
          
            if (collisionSides.Count == 1)
            {
                return collisionSides[0];
            }
            else if (collisionSides.Count == 2)
            {
                // figure out correct side
                CollisionSide topBottomCollisionSide;
                if (collisionSides.Contains(CollisionSide.Top))
                {
                    topBottomCollisionSide = CollisionSide.Top;
                }
                else
                {
                    topBottomCollisionSide = CollisionSide.Bottom;
                }
                CollisionSide leftRightCollisionSide;
                if (collisionSides.Contains(CollisionSide.Left))
                {
                    leftRightCollisionSide = CollisionSide.Left;
                }
                else
                {
                    leftRightCollisionSide = CollisionSide.Right;
                }

                if (intersectionRect.Width > intersectionRect.Height)
                    return topBottomCollisionSide;
                else if (intersectionRect.Height > intersectionRect.Width)
                    return leftRightCollisionSide;
                else return CollisionSide.Corner;
            }
            else
            {
                // there are three collision sides in the list
                if (collisionSides.Contains(CollisionSide.Top) &&
                    collisionSides.Contains(CollisionSide.Bottom))
                {
                    if (collisionSides.Contains(CollisionSide.Left))
                    {
                        return CollisionSide.Left;
                    }
                    else
                    {
                        return CollisionSide.Right;
                    }
                }
                else
                {
                    // must be colliding with both left and right
                    if (collisionSides.Contains(CollisionSide.Top))
                    {
                        return CollisionSide.Top;
                    }
                    else
                    {
                        return CollisionSide.Bottom;
                    }
                }
            }
        }


        /// <summary>
        /// Gets the list of possible collision sides for the ball 
        /// collision rectangle and ball-brick intersection area.
        /// </summary>
        /// <param name="ballRectangle">The ball rectangle.</param>
        /// <param name="intersectionRect">Ball-Brick intersection area.</param>
        /// <returns>The list of possible collision sides.</returns>
        private static List<CollisionSide> GetCollisionSides(Rectangle ballRectangle, Rectangle intersectionRect)
        {
            List<CollisionSide> collisionSides = new List<CollisionSide>();
            if (intersectionRect.Left == ballRectangle.Left)
            {
                collisionSides.Add(CollisionSide.Left);
            }
            if (intersectionRect.Right == ballRectangle.Right)
            {
                collisionSides.Add(CollisionSide.Right);
            }
            if (intersectionRect.Top == ballRectangle.Top)
            {
                collisionSides.Add(CollisionSide.Top);
            }
            if (intersectionRect.Bottom == ballRectangle.Bottom)
            {
                collisionSides.Add(CollisionSide.Bottom);
            }
            return collisionSides;
        }

   
        /// <summary>
        /// Bounces the ball off the brick and sets new velocity and draw rectangle for the ball.
        /// </summary>
        /// <param name="ballVelocity">The ball velocity.</param>
        /// <param name="ballDrawRectangle">The draw rectangle of the ball.</param>
        /// <param name="preCollisionDuration">The time duration before the collision.</param>
        /// <param name="postCollisionDuration">The time duration after the collision.</param>
        /// <param name="collisionSide">The collision side.</param>
        /// <returns>The collision resolution info object.</returns>
        private static CollisionResolutionInfo BounceBall(Vector2 ballVelocity, Rectangle ballDrawRectangle,
             int preCollisionDuration, int postCollisionDuration, CollisionSide collisionSide)
        {
            //// save speeds for later
            //float ballSpeed = ballVelocity.Length();

            // move forward up to collision
            Rectangle newBallDrawRectangle = MoveForward(ballVelocity, ballDrawRectangle,
                preCollisionDuration);

            // change velocities as appropriate
            Vector2 newBallVelocity = GetNewVelocity(ballVelocity, collisionSide);

            // move ball forward after collision
            newBallDrawRectangle = MoveForward(newBallVelocity, newBallDrawRectangle,
                postCollisionDuration);       

            return new CollisionResolutionInfo(newBallVelocity, newBallDrawRectangle);
        }

        /// <summary>
        /// Moves the ball forward along its velocity for the given duration.
        /// </summary>
        /// <param name="velocity">The ball velocity.</param>
        /// <param name="drawRectangle">The ball draw rectangle.</param>
        /// <param name="duration">The time duration.</param>
        /// <returns>The new ball draw rectangle.</returns>
        private static Rectangle MoveForward(Vector2 velocity, Rectangle drawRectangle,
            int duration)
        {
            Rectangle newRectangle = new Rectangle(
                drawRectangle.X, drawRectangle.Y,
                drawRectangle.Width, drawRectangle.Height);
            newRectangle.X = (int)(newRectangle.X + velocity.X * duration);
            newRectangle.Y = (int)(newRectangle.Y + velocity.Y * duration);
            return newRectangle;
        }

        /// <summary>
        /// Gets new velocity for the ball.
        /// </summary>
        /// <param name="oldVelocity">Ball velocity before collision.</param>
        /// <param name="collisionSide">Collision side.</param>
        /// <returns>The ball velocity after the collision.</returns>
        private static Vector2 GetNewVelocity(Vector2 oldVelocity, CollisionSide collisionSide)
        {
            switch (collisionSide)
            {
                case CollisionSide.Top:
                    return new Vector2(oldVelocity.X, oldVelocity.Y * -1);
                case CollisionSide.Bottom:
                    return new Vector2(oldVelocity.X, oldVelocity.Y * -1);
                case CollisionSide.Left:
                    return new Vector2(oldVelocity.X * -1, oldVelocity.Y);
                case CollisionSide.Right:
                    return new Vector2(oldVelocity.X * -1, oldVelocity.Y);
                default:
                    return new Vector2(oldVelocity.X * -1, oldVelocity.Y * -1);
            }
        }

        #endregion
    }
}
