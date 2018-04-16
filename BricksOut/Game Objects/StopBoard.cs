using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace BricksOut
{
    /// <summary>
    /// A class for a Stopboard object. 
    /// Stopboard is represented as a part of circle,
    /// where width of board is chord and width of a whole
    /// sprite is a diameter of this circle.
    /// To calculate the actual board width we need
    /// to sustract the known invisible edges of the sprite
    /// from the full sprite width. 
    /// The actual height of the board is the part of the circle radius.
    /// To calculate it we need to:
    /// 1) calculate the visible part of the radius using 
    ///    the Pythagorean theorem(the radius, the half of the chord and unknown
    ///    quantity make a right triangle);
    /// 2) substract this value from the radius.   
    /// </summary>
    public class StopBoard : GameObject
    {
        private const int boardMovAmount = 8;

        private const int NumFrames = 5;
        private const int FrameDuration = 30;
        private int currentFrame = 0;
        private int elapsedTime;

        // left and right invisible edges of the sprite
        private const int InactiveEdgeWidth = 20;

        // visible part of the sprite
        private int activeBoardHeight;

        private int boardSpriteWidth;
        private int boardWidth;
        private Rectangle sourceRectangle;

        private bool bounsing = false;

        private Texture2D leftFireTexture;
        private Texture2D rightFireTexture;
        private BoardFire leftFire;
        private BoardFire rightFire;
        private List<BoardFire> fires = new List<BoardFire>();

        // board fire coordinates relating to the board
        private const int FireYPos = 30;
        private const int LeftFireXPos = 11;
        private const int RightFireXPos = 108;

        bool movementStarted;
        bool movingRight;

        /// <summary>
        /// Creates a stopboard object.
        /// </summary>
        /// <param name="spriteTexture">The sprite texture for the board.</param>
        /// <param name="x">The x position of the center of the board.</param>
        /// <param name="y">The y position of the center of the board.</param>
        /// <param name="leftFire">The sprite texture of the left board fire.</param>
        /// <param name="rightFire">The sprite texture of the right board fire.</param>
        public StopBoard(Texture2D spriteTexture, int x, int y, Texture2D leftFire, Texture2D rightFire) : base(spriteTexture)
        {
            boardSpriteWidth = sprite.Width / NumFrames;
            drawRectangle = new Rectangle(x - boardSpriteWidth / 2, y - sprite.Height / 2, boardSpriteWidth, sprite.Height);
            sourceRectangle = new Rectangle(0, 0, boardSpriteWidth, sprite.Height);
            boardWidth = boardSpriteWidth - InactiveEdgeWidth * 2;

            // calculate the actual board height. 
            double invisibleHeight = Math.Sqrt(boardSpriteWidth / 2 * boardSpriteWidth / 2 - boardWidth / 2 * boardWidth / 2);
            activeBoardHeight = sprite.Height / 2 - (int)invisibleHeight;

            leftFireTexture = leftFire;
            rightFireTexture = rightFire;

        }

        #region Properties

        /// <summary>
        /// Gets the draw rectangle of the board.
        /// </summary>
        public Rectangle DrawRectangle
        {
            get { return drawRectangle; }
        }

        /// <summary>
        /// Gets the collision rectangle of the board.
        /// </summary>
        public override Rectangle CollisionRectangle
        {
            get { return new Rectangle(drawRectangle.X + InactiveEdgeWidth, drawRectangle.Y, boardWidth, activeBoardHeight); }
        }

        /// <summary>
        /// Sets whether the board is bouncing.
        /// </summary>
        public bool IsBouncing
        {
            set { bounsing = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the board.
        /// </summary>
        /// <param name="keyBoard">The keyboard state.</param>
        /// <param name="gameTime">The game time.</param>
        public void Update(KeyboardState keyBoard, GameTime gameTime)
        {
            MoveBoard(keyBoard);

            UpdateFires(gameTime);

            HoldBoardInsideWindow();

            PlayAnimation(gameTime);
        }

        /// <summary>
        /// Draws the board.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, sourceRectangle, Color.White);
            foreach (BoardFire fire in fires)
                fire.Draw(spriteBatch);
        }

        #endregion

        #region Private Methods

        private void MoveBoard(KeyboardState keyBoard)
        {
            if (!movementStarted)
            {
                if (keyBoard.IsKeyDown(Keys.Right))
                {
                    movingRight = true;
                    movementStarted = true;
                    leftFire = new BoardFire(leftFireTexture, LeftFireXPos, FireYPos, this);
                    fires.Add(leftFire);
                }
                else if (keyBoard.IsKeyDown(Keys.Left))
                {
                    movementStarted = true;
                    rightFire = new BoardFire(rightFireTexture, RightFireXPos, FireYPos, this);
                    fires.Add(rightFire);
                }
            }
            else
            {
                if (movingRight)
                {
                    int toRightEdgeSpace = BricksOutGame.WindowWidth - GameScreen.InfoFieldWidth - CollisionRectangle.Right;

                    if (keyBoard.IsKeyDown(Keys.Right))
                    {
                        if (toRightEdgeSpace < boardMovAmount)
                            drawRectangle.X += toRightEdgeSpace;
                        else
                            drawRectangle.X += boardMovAmount;
                    }
                    else
                    {
                        movementStarted = false;
                        movingRight = false;
                        leftFire.IsFading = true;
                    }
                }
                else
                {
                    int toLeftEdgeSpace = CollisionRectangle.Left - GameScreen.LeftBorderWidth;

                    if (keyBoard.IsKeyDown(Keys.Left))
                    {
                        if (toLeftEdgeSpace < boardMovAmount)
                            drawRectangle.X -= toLeftEdgeSpace;
                        else
                            drawRectangle.X -= boardMovAmount;
                    }
                    else
                    {
                        movementStarted = false;
                        rightFire.IsFading = true;
                    }
                }
            }
        }

        private void UpdateFires(GameTime gameTime)
        {
            foreach (BoardFire fire in fires)
                fire.Update(gameTime, this);

            for (int i = fires.Count - 1; i >= 0; i--)
            {
                if (fires[i].IsFinished)
                    fires.RemoveAt(i);
            }
        }

        private void HoldBoardInsideWindow()
        {
            if (CollisionRectangle.X == GameScreen.LeftBorderWidth)
                drawRectangle.X = GameScreen.LeftBorderWidth - InactiveEdgeWidth;
            if (CollisionRectangle.Right == BricksOutGame.WindowWidth - GameScreen.InfoFieldWidth)
                drawRectangle.X = BricksOutGame.WindowWidth - GameScreen.InfoFieldWidth - boardSpriteWidth + InactiveEdgeWidth;
        }

        private void PlayAnimation(GameTime gameTime)
        {
            if (bounsing)
            {
                elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

                if (elapsedTime >= FrameDuration)
                {
                    currentFrame++;

                    if (currentFrame < NumFrames)
                        sourceRectangle.X = currentFrame * boardSpriteWidth;
                    else
                    {
                        bounsing = false;
                        sourceRectangle.X = 0;
                        currentFrame = 0;
                    }
                }
            }
        }

        #endregion
    }
}