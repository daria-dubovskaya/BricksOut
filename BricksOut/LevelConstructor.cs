using System.IO;

namespace BricksOut
{
    /// <summary>
    /// A class for constructing game levels.
    /// </summary>
    static class LevelConstructor
    {                
        private static Brick[][] bricks;
        private static string[] lines;
        private static int lineLenght;             

        public static Brick[][] CreateLevel(int currentLevel)
        {
            switch (currentLevel)
            {
                case 1:
                    GameScreen.BonusPropability = 15;
                    lineLenght = 13;
                    lines = File.ReadAllLines("Content/levels/level1.txt");
                    bricks = new Brick[lines.Length][];
                    for (int k = 0; k < bricks.Length; k++)
                        bricks[k] = new Brick[lineLenght];

                    int xPos = 72;
                    int yPos = 73;
                    int i = 0;
                    int j = 0;
                    int n = 0;

                    foreach (string line in lines)
                    {
                        foreach (char c in line)
                        {
                            if (c == 'A')
                            {
                                bricks[i][j] = new WoodBrick(GameScreen.woodBrickSprite, xPos, yPos, GameScreen.woodHitSound);
                            }
                            if (c == 'B')
                            {
                                bricks[i][j] = new ClayBrick(GameScreen.clayBrickSprite, xPos, yPos, GameScreen.clayHitSound, GameScreen.clayBrickDestructionSound);
                            }
                            if (c == 'C')
                            {
                                bricks[i][j] = new MagicBrick(GameScreen.magicBrickSprite, xPos, yPos, GameScreen.magicExplosionSound);
                                bricks[i][j].IsDetonating = true;
                            }
                            if (c == 'D')
                                bricks[i][j] = new MetalBrick(GameScreen.metalBrickSprite, xPos, yPos, GameScreen.metalHitSound);

                            if (bricks[i][j] != null)
                                n++;

                            j++;
                            xPos += 54;
                        }
                        i++;
                        j = 0;
                        xPos = 72;
                        yPos += 23;
                    }

                    GameScreen.BricksAmount = n;

                    break;

                case 2:
                    GameScreen.BonusPropability = 25;
                    lineLenght = 13;
                    lines = File.ReadAllLines("Content/levels/level2.txt");
                    bricks = new Brick[lines.Length][];
                    for (int k = 0; k < bricks.Length; k++)
                        bricks[k] = new Brick[lineLenght];

                    xPos = 71;
                    yPos = 73;
                    i = 0;
                    j = 0;
                    n = 0;

                    foreach (string line in lines)
                    {
                        foreach (char c in line)
                        {
                            if (c == 'A')
                            {
                                bricks[i][j] = new WoodBrick(GameScreen.woodBrickSprite, xPos, yPos, GameScreen.woodHitSound);
                            }
                            if (c == 'B')
                            {
                                bricks[i][j] = new ClayBrick(GameScreen.clayBrickSprite, xPos, yPos, GameScreen.clayHitSound, GameScreen.clayBrickDestructionSound);
                            }
                            if (c == 'C')
                            {
                                bricks[i][j] = new MagicBrick(GameScreen.magicBrickSprite, xPos, yPos, GameScreen.magicExplosionSound);
                                bricks[i][j].IsDetonating = true;
                            }

                            if (bricks[i][j] != null)
                                n++;

                            j++;
                            xPos += 54;
                        }
                        i++;
                        j = 0;
                        xPos = 71;
                        yPos += 23;
                    }

                    GameScreen.BricksAmount = n;

                    break;

            }

            return bricks;
        }        
    }
}
