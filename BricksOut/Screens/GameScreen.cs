using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace BricksOut
{
    /// <summary>
    /// A class for a main screen of the game. 
    /// It is a screen with all game logic.
    /// </summary>
    public class GameScreen : Screen
    {
        public const int TopBorderWidth = 58;
        public const int LeftBorderWidth = 27;
        public const int InfoFieldWidth = 224;
        public const int GameFieldBottomEdge = 727;

        // display support
        private const int HorizontalInfoOffset = 855;
        private readonly Vector2 LevelLocation = new Vector2(HorizontalInfoOffset, 145);
        private readonly Vector2 ScoreLocation = new Vector2(HorizontalInfoOffset, 200);
        private readonly Vector2 LivesLocation = new Vector2(HorizontalInfoOffset, 240);

        private const int BallFlamingDuration = 8000;

        private int score;
        private int lives;

        private Color messageColor = new Color(255, 170, 76);
        private Texture2D background;
        private Texture2D border;
        private Texture2D boardSprite;
        private StopBoard stopBoard;
        private Texture2D boardLeftFireSprite;
        private Texture2D boardRightFireSprite;
        private Texture2D ballSprite;
        private Texture2D fireBallSprite;
        private Texture2D bonusBallSprite;
        private List<Ball> balls = new List<Ball>();

        public static Texture2D woodBrickSprite;
        public static Texture2D metalBrickSprite;
        public static Texture2D clayBrickSprite;
        public static Texture2D magicBrickSprite;
        private Brick[][] bricks;

        private static int bonusProbability;
        private static int activeBricksCount;

        public static Texture2D woodBrickDestructionSprite1;
        public static Texture2D woodBrickDestructionSprite2;
        public static Texture2D clayBrickDestructionSprite1;
        public static Texture2D clayBrickDestructionSprite2;
        public static Texture2D fireExplosionSprite1;
        public static Texture2D fireExplosionSprite2;
        public static Texture2D fireExplosionSprite3;
        public static Texture2D magicExplosionSprite1;
        public static Texture2D magicExplosionSprite2;
        public static Texture2D magicExplosionSprite3;
        public static Texture2D magicExplosionSprite4;
        private List<Destruction> destructions = new List<Destruction>();

        public static Texture2D extraLifeBonusSprite;
        public static Texture2D fireBallBonusSprite;
        public static Texture2D extraBallBonusSprite;
        private List<Bonus> bonuses = new List<Bonus>();

        private SoundEffect bonusSound;
        private SoundEffect boardHitSound;
        private SoundEffect wallHitSound;
        public static SoundEffect clayBrickDestructionSound;
        public static SoundEffect woodHitSound;
        public static SoundEffect metalHitSound;
        public static SoundEffect clayHitSound;
        public static SoundEffect fireExplosionSound;
        public static SoundEffect magicExplosionSound;

        private const int MaxLevel = 2;
        private const int NextLevelScreenDuration = 4000;
        private int currentLevel = 1;
        private bool levelFinished = false;
        private int levelFinishedStatusElapsedTime = 0;
        private bool afterLevelScreenPlaying = false;

        private bool flamingBalls = false;
        private int elapsedFlamingBallsTime = 0;

        private Random rand = new Random();

        /// <summary>
        /// Creates a new Game Screen.
        /// </summary>
        public GameScreen()
        {
        }

        /// <summary>
        /// Sets the bonus propability for current level.
        /// </summary>
        public static int BonusPropability
        {
            set { bonusProbability = value; }
        }

        public static int BricksAmount
        {
            set { activeBricksCount = value; }
        }

        /// <summary>
        /// Loads a content of the screen.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            background = content.Load<Texture2D>(@"graphics\background_ingame");
            border = content.Load<Texture2D>(@"graphics\gameborder");

            ballSprite = content.Load<Texture2D>(@"graphics\generalBall");
            fireBallSprite = content.Load<Texture2D>(@"graphics\fireBall");
            bonusBallSprite = content.Load<Texture2D>(@"graphics\bonusBall");

            boardSprite = content.Load<Texture2D>(@"graphics\stopboard");
            boardLeftFireSprite = content.Load<Texture2D>(@"graphics\fire_left");
            boardRightFireSprite = content.Load<Texture2D>(@"graphics\fire_right");
            woodBrickSprite = content.Load<Texture2D>(@"graphics\woodbrick");

            metalBrickSprite = content.Load<Texture2D>(@"graphics\metalbrick");
            magicBrickSprite = content.Load<Texture2D>(@"graphics\magicbrick");
            clayBrickSprite = content.Load<Texture2D>(@"graphics\claybrick");

            woodBrickDestructionSprite1 = content.Load<Texture2D>(@"graphics\woodbrick_destruction1");
            woodBrickDestructionSprite2 = content.Load<Texture2D>(@"graphics\woodbrick_destruction2");
            clayBrickDestructionSprite1 = content.Load<Texture2D>(@"graphics\claybrick_destruction1");
            clayBrickDestructionSprite2 = content.Load<Texture2D>(@"graphics\claybrick_destruction2");
            fireExplosionSprite1 = content.Load<Texture2D>(@"graphics\fire_explosion_1");
            fireExplosionSprite2 = content.Load<Texture2D>(@"graphics\fire_explosion_2");
            fireExplosionSprite3 = content.Load<Texture2D>(@"graphics\fire_explosion_3");
            magicExplosionSprite1 = content.Load<Texture2D>(@"graphics\magic_explosion_1");
            magicExplosionSprite2 = content.Load<Texture2D>(@"graphics\magic_explosion_2");
            magicExplosionSprite3 = content.Load<Texture2D>(@"graphics\magic_explosion_3");
            magicExplosionSprite4 = content.Load<Texture2D>(@"graphics\magic_explosion_4");

            extraLifeBonusSprite = content.Load<Texture2D>(@"graphics\extraLife");
            fireBallBonusSprite = content.Load<Texture2D>(@"graphics\fireBonus");
            extraBallBonusSprite = content.Load<Texture2D>(@"graphics\extraBall");

            bonusSound = content.Load<SoundEffect>(@"audio\bonus");
            boardHitSound = content.Load<SoundEffect>(@"audio\board_hit");
            wallHitSound = content.Load<SoundEffect>(@"audio\kick_wall");
            clayBrickDestructionSound = content.Load<SoundEffect>(@"audio\clay_fall");
            woodHitSound = content.Load<SoundEffect>(@"audio\wood_hit");
            metalHitSound = content.Load<SoundEffect>(@"audio\metal_hit");
            clayHitSound = content.Load<SoundEffect>(@"audio\clay_hit");
            fireExplosionSound = content.Load<SoundEffect>(@"audio\explosion_fire");
            magicExplosionSound = content.Load<SoundEffect>(@"audio\explosion_magic");

            StartNewGame();
        }   
  
        /// <summary>
        /// Updates the game screen. 
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                isActive = false;
                ScreenManager.AddScreen(new PauseMenuScreen());
            }

            if (!levelFinished)
            {
                stopBoard.Update(Keyboard.GetState(), gameTime);

                UpdateBalls(gameTime);

                UpdateBricks(gameTime);

                foreach (Destruction destruction in destructions)
                    destruction.Update(gameTime);

                foreach (Bonus bonus in bonuses)
                {
                    bonus.Update(gameTime);
                    CheckBoardBonusCollisions(bonus);
                }

                UpdateBurning(gameTime);

                DeleteInactiveObjects();
            }
            else
            {
                if (destructions.Count > 0)
                    FinishDestructions(gameTime);
                else
                    FinishLevel(gameTime);
            }
        }        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Begin();

            if (!afterLevelScreenPlaying)
                DrawGameProcess(spriteBatch);
            else
                DrawLevelFinishing(spriteBatch);

            spriteBatch.End();
        }

        
        #region Private Methods

        private void DrawGameProcess(SpriteBatch spriteBatch)
        {
            // draw background and border
            spriteBatch.Draw(background, new Vector2(LeftBorderWidth, TopBorderWidth), Color.White);
            spriteBatch.Draw(border, new Vector2(0, 0), Color.White);

            // Draw game information
            spriteBatch.DrawString(gameFont18, "Level " + currentLevel, LevelLocation, fontColor);
            spriteBatch.DrawString(gameFont13, "Score " + score, ScoreLocation, fontColor);
            spriteBatch.DrawString(gameFont13, "Lives " + lives, LivesLocation, fontColor);

            stopBoard.Draw(spriteBatch);

            foreach (Ball ball in balls)
                ball.Draw(spriteBatch);

            for (int i = 0; i < bricks.Length; i++)
            {
                for (int j = 0; j < bricks[i].Length; j++)
                {
                    if (bricks[i][j] != null && bricks[i][j].IsActive)
                        bricks[i][j].Draw(spriteBatch);
                }
            }

            foreach (Destruction destruction in destructions)
                destruction.Draw(spriteBatch);

            foreach (Bonus bonus in bonuses)
            {
                bonus.Draw(spriteBatch);
            }
        }

        private void DrawLevelFinishing(SpriteBatch spriteBatch)
        {
            string message;

            if (currentLevel <= MaxLevel)
                message = "Level " + currentLevel;
            else
                message = "You Won";

            spriteBatch.DrawString(gameFont35, message, GetMessagePosition(message), messageColor);
        }

        private Vector2 GetMessagePosition(string text)
        {
            Vector2 position = new Vector2(BricksOutGame.WindowWidth / 2 - gameFont35.MeasureString(text).X / 2,
                BricksOutGame.WindowHeight / 2 - gameFont35.MeasureString(text).Y / 2);
            return position;
        }


        #region Collisions        

        private void CheckBallBoardCollision(Ball ball)
        {
            if (ball.CollisionRectangle.Intersects(stopBoard.CollisionRectangle))
            {
                if (Vector2.Distance(new Vector2(ball.CollisionRectangle.Center.X, ball.CollisionRectangle.Center.Y),
                    new Vector2(stopBoard.DrawRectangle.Center.X, stopBoard.DrawRectangle.Center.Y)) <=
                    ball.CollisionRectangle.Width / 2 + stopBoard.DrawRectangle.Width / 2)
                {
                    if (ball.CollisionRectangle.Bottom <= stopBoard.CollisionRectangle.Bottom &&
                        ball.CollisionRectangle.Right >= stopBoard.CollisionRectangle.Left &&
                        ball.CollisionRectangle.Left <= stopBoard.CollisionRectangle.Right)
                    {
                        stopBoard.IsBouncing = true;
                        BricksOutGame.PlaySound(boardHitSound);
                        GetNewBallVelocity(ball);
                    }
                }
            }
        }

        private void GetNewBallVelocity(Ball ball)
        {
            /// to calculate new ball velocity gets the distance from ball center
            /// to the left edge of the board and then calculates the ratio of this
            /// distance to board width. New angle is between π/1.5 and π/3 and depends on
            /// the ratio value.
            float distance = ball.CollisionRectangle.Center.X - stopBoard.CollisionRectangle.X;
            float ratio = distance / stopBoard.CollisionRectangle.Width;
            float angle = (float)(Math.PI / 1.5f) - ratio * (float)(Math.PI / 3);

            ball.SetBallVelocity(angle);
        }


        private void CheckBallBricksCollision(Ball ball, GameTime gameTime)
        {
            for (int i = 0; i < bricks.Length; i++)
            {
                for (int j = 0; j < bricks[i].Length; j++)
                {
                    if (bricks[i][j] != null && IsOpenForHit(i, j))
                    {
                        if (!bricks[i][j].IsDestroyed)
                        {
                            Brick brick = bricks[i][j];
                            CollisionResolutionInfo collisionInfo = CollisionUtils.CheckCollision(gameTime.ElapsedGameTime.Milliseconds, ball, brick);
                            if (collisionInfo != null)
                            {
                                HitBrick(i, j, ball.IsFlaming, bricks[i][j].IsDetonating, 0);
                                ball.Velocity = collisionInfo.BallVelocity;
                                ball.DrawRectangle = collisionInfo.BallDrawRectangle;
                            }
                        }
                    }
                }
            }
        }

        bool IsOpenForHit(int row, int col)
        {
            if (row - 1 < 0 || row + 1 == bricks.Length ||
                           col - 1 < 0 || col + 1 == bricks[row].Length)
                return true;
            else if (bricks[row - 1][col] == null || bricks[row + 1][col] == null ||
                bricks[row][col - 1] == null || bricks[row][col + 1] == null)
                return true;
            else return false;
        }

        private void HitBrick(int row, int col, bool ballIsFire, bool detonated, int timeDelay)
        {
            int delay = timeDelay;
            int newDelay = delay + 190;

            bricks[row][col].Hit(ballIsFire, detonated, timeDelay);

            if (bricks[row][col].IsDestroyed)
                DestroyBrick(ballIsFire, bricks[row][col], detonated, delay);

            if (bricks[row][col].IsDetonating)
            {
                if (currentLevel == 1)
                {
                    // find and destroy up and down neighbours
                    if (row + 1 < bricks.Length && bricks[row + 1][col] != null && bricks[row + 1][col].IsDestroyed == false)
                        HitBrick(row + 1, col, ballIsFire, detonated, newDelay);
                    if (row - 1 >= 0 && bricks[row - 1][col] != null && bricks[row - 1][col].IsDestroyed == false)
                        HitBrick(row - 1, col, ballIsFire, detonated, newDelay);
                }

                if (currentLevel == 2)
                {
                    // find and destroy all neighbouring detonating bricks
                    if (row + 1 < bricks.Length && bricks[row + 1][col] != null && bricks[row + 1][col].IsDetonating && bricks[row + 1][col].IsDestroyed == false)
                        HitBrick(row + 1, col, ballIsFire, detonated, newDelay);
                    if (row - 1 >= 0 && bricks[row - 1][col] != null && bricks[row - 1][col].IsDetonating && bricks[row - 1][col].IsDestroyed == false)
                        HitBrick(row - 1, col, ballIsFire, detonated, newDelay);
                    if (col + 1 < bricks[row].Length && bricks[row][col + 1] != null && bricks[row][col + 1].IsDetonating && bricks[row][col + 1].IsDestroyed == false)
                        HitBrick(row, col + 1, ballIsFire, detonated, newDelay);
                    if (col - 1 >= 0 && bricks[row][col - 1] != null && bricks[row][col - 1].IsDetonating && bricks[row][col - 1].IsDestroyed == false)
                        HitBrick(row, col - 1, ballIsFire, detonated, newDelay);
                }
            }
        }

        private void DestroyBrick(bool ballIsFire, Brick brick, bool detonated, int timeDelay)
        {
            score += 10;

            CreateBonus(brick);

            CreateDestruction(brick, ballIsFire, detonated, timeDelay);
        }

        private void CreateBonus(Brick brick)
        {
            if (rand.Next(bonusProbability) == 1)
                bonuses.Add(new Bonus(brick.Position, content));
        }

        private void CreateDestruction(Brick brick, bool ballIsFire, bool detonated, int timeDelay)
        {
            if (detonated)
                destructions.Add(new MagicExplosion(brick.Position, timeDelay));
            else
            {
                if (ballIsFire)
                    destructions.Add(new FireExplosion(brick.Position, timeDelay));
                else
                {
                    if (brick is WoodBrick)
                        destructions.Add(new WoodDestruction(brick.Position, timeDelay));
                    else destructions.Add(new ClayDestruction(brick.Position, timeDelay));
                }
            }
        }

        private void CheckBoardBonusCollisions(Bonus bonus)
        {
            if (bonus.CollisionRectangle.Intersects(stopBoard.CollisionRectangle))
            {
                bonus.IsActive = false;
                BricksOutGame.PlaySound(bonusSound);

                if (bonus.Type == BonusType.FireBall)
                {
                    flamingBalls = true;
                    elapsedFlamingBallsTime = 0;

                    foreach (Ball ball in balls)
                        ball.IsFlaming = true;
                }
                else if (bonus.Type == BonusType.ExtraLife)
                    lives++;
                else
                    CreateExtraBalls();
            }
        }

        #endregion

        private void UpdateBalls(GameTime gameTime)
        {
            foreach (Ball ball in balls)
            {
                ball.Update(gameTime);

                if (!ball.IsMoving)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        ball.IsMoving = true;
                    }
                    else
                    {
                        ball.X = stopBoard.CollisionRectangle.Center.X - ball.DrawRectangle.Width / 2;
                        ball.Y = stopBoard.CollisionRectangle.Y - ball.CollisionRectangle.Height -
                            (ball.DrawRectangle.Height - ball.CollisionRectangle.Height) / 2;
                    }
                }
                else
                {
                    CheckBallBoardCollision(ball);
                    CheckBallBricksCollision(ball, gameTime);
                }
            }
        }

        private void UpdateBricks(GameTime gameTime)
        {
            for (int i = 0; i < bricks.Length; i++)
            {
                for (int j = 0; j < bricks[i].Length; j++)
                {
                    if (bricks[i][j] != null)
                        bricks[i][j].Update(gameTime);
                }
            }
        }

        private void UpdateBurning(GameTime gameTime)
        {
            if (flamingBalls)
            {
                elapsedFlamingBallsTime += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedFlamingBallsTime >= BallFlamingDuration)
                {
                    elapsedFlamingBallsTime = 0;
                    flamingBalls = false;
                    foreach (Ball ball in balls)
                        ball.IsFlaming = false;
                }
            }
        }

        private void StartNewGame()
        {
            lives = 3;
            score = 0;

            bricks = LevelConstructor.CreateLevel(currentLevel);
            CreateStopBoard();
            CreateInitialBall(stopBoard.DrawRectangle);
        }              

        private void CreateStopBoard()
        {
            int windowCenterX = (BricksOutGame.WindowWidth - LeftBorderWidth - InfoFieldWidth) / 2;
            int boardYPos = 725;
            stopBoard = new StopBoard(boardSprite, windowCenterX, boardYPos, boardLeftFireSprite, boardRightFireSprite);
        }
            
        private void CreateInitialBall(Rectangle boardRect)
        {
            float angle = (float)(Math.PI / 2.4f);
            Ball ball = new Ball(ballSprite, boardRect, angle, flamingBalls, fireBallSprite, wallHitSound);
            ball.IsMoving = false;
            balls.Add(ball);
        }
      
        private void CreateExtraBalls()
        {
            float angleDifference = 0.52f;

            Ball initBall = balls[0];
            initBall.Texture = bonusBallSprite;

            balls.Add(new Ball(bonusBallSprite, initBall.DrawRectangle.X, initBall.DrawRectangle.Y, initBall.Angle + angleDifference,
                initBall.IsFlaming, fireBallSprite, wallHitSound));

            balls.Add(new Ball(bonusBallSprite, initBall.DrawRectangle.X, initBall.DrawRectangle.Y, initBall.Angle - angleDifference,
                initBall.IsFlaming, fireBallSprite, wallHitSound));
        }                    
  
        private void DeleteInactiveObjects()
        {
            DeleteInactiveBalls();

            DeleteInactiveBricks();

            DeleteInactiveDestructions();

            DeleteInactiveBonuses();
        }        

        private void DeleteInactiveBalls()
        {
            for (int i = balls.Count - 1; i >= 0; i--)
            {
                if (!balls[i].IsActive)
                    balls.RemoveAt(i);
            }

            if (balls.Count == 0)
            {
                lives--;
                flamingBalls = false;

                bonuses.Clear();

                if (lives < 0)
                {
                    isActive = false;
                    ScreenManager.AddScreen(new GameOverMenuScreen());
                }
                else
                {
                    CreateInitialBall(stopBoard.DrawRectangle);
                }
            }
        }

        private void DeleteInactiveBricks()
        {
            for (int i = 0; i < bricks.Length; i++)
            {
                for (int j = 0; j < bricks[i].Length; j++)
                {
                    if (bricks[i][j] != null && !bricks[i][j].IsActive)
                    {
                        bricks[i][j] = null;
                        activeBricksCount--;

                        if (activeBricksCount == 0)
                        {
                            levelFinished = true;
                            break;
                        }
                    }
                }

                if (levelFinished)
                    break;
            }
        }

        private void DeleteInactiveDestructions()
        {
            for (int i = destructions.Count - 1; i >= 0; i--)
            {
                if (destructions[i].Finished)
                    destructions.RemoveAt(i);
            }
        }

        private void DeleteInactiveBonuses()
        {
            for (int i = bonuses.Count - 1; i >= 0; i--)
            {
                if (!bonuses[i].IsActive)
                    bonuses.RemoveAt(i);
            }
        }

        private void FinishDestructions(GameTime gameTime)
        {
            foreach (Destruction destruction in destructions)
                destruction.Update(gameTime);
            foreach (Bonus bonus in bonuses)
                bonus.Update(gameTime);
            foreach (Ball ball in balls)
                ball.Update(gameTime);

            DeleteInactiveDestructions();

            if (destructions.Count == 0)
            {
                afterLevelScreenPlaying = true;
                currentLevel++;
            }
        }

        private void FinishLevel(GameTime gameTime)
        {
            levelFinishedStatusElapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            if (levelFinishedStatusElapsedTime >= NextLevelScreenDuration)
            {
                levelFinishedStatusElapsedTime = 0;
                afterLevelScreenPlaying = false;

                if (currentLevel <= MaxLevel)
                {
                    CreateNewLevel();
                }
                else
                {
                    ExitScreen();
                    ScreenManager.AddScreen(new MainMenuScreen());
                }
            }
        }

        private void CreateNewLevel()
        {
            for (int i = bonuses.Count - 1; i >= 0; i--)
                bonuses.RemoveAt(i);
            for (int i = balls.Count - 1; i >= 0; i--)
                balls.RemoveAt(i);
            levelFinished = false;
            flamingBalls = false;
            bricks = LevelConstructor.CreateLevel(currentLevel);
            CreateStopBoard();
            CreateInitialBall(stopBoard.DrawRectangle);
        }

        #endregion

        #region Public Methods

        public void RestartLevel()
        {
            flamingBalls = false;

            // delete all objects from the previous game
            for (int i = bonuses.Count - 1; i >= 0; i--)
                bonuses.RemoveAt(i);
            for (int i = balls.Count - 1; i >= 0; i--)
                balls.RemoveAt(i);
            for (int i = destructions.Count - 1; i >= 0; i--)
                destructions.RemoveAt(i);

            StartNewGame();
        }  
        
        #endregion
    }
}