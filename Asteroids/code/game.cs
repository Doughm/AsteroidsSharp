using SFML.Window;
using System;

namespace Asteroids
{
    class Game
    {
        Window window;
        Audio audio;
        AssetLoader assetLoader;
        Ticker ticker = new Ticker();
        TextEdit textEdit = new TextEdit();
        LoadINI loadINI = new LoadINI("engine.ini");

        private Random random = new Random();
        private Entity ship = new Entity(new Vector2f(320, 240));
        private Ticker respawnCounter = new Ticker();
        private Entity[] largeAsteroids = new Entity[12];
        private Entity[] mediumAsteroids = new Entity[24];
        private int mediumAsteroidsCounter = 0;
        private Entity[] smallAsteroids = new Entity[48];
        private int smallAsteroidsCounter = 0;
        private Entity[] bullets = new Entity[4];
        private int[] bulletsCounter = new int[4] { 0, 0, 0, 0 };
        private const int bulletTime = 40;
        private Vector2f[] explosions = new Vector2f[4];
        private int explosionCounter = 0;
        private Entity ufo = new Entity(new Vector2f(-500, -500));
        private int level = 0;
        private int score = 0;
        private int lives = 2;
        private int bulletCounter = 0;
        private int asteroidsLeft = 4;
        private bool isPaused;
        private string fireKey;
        private string pauseKey;
        private string leftKey;
        private string rightKey;
        private string hyperspaceKey;
        private string accelerateKey;
        private string[] highScoreNames = new string[4];
        private int[] highScores = new int[4];
        private bool isMenu = true;
        private string mouseClick;
        private string keyboard;
        private string keyboardDown;
        private string keyboardSingle;
        private string tempStr = string.Empty;
        private string setKey = string.Empty;
        private bool keyPress = false;
        private bool typing = false;
        private Vector2f tempVector = new Vector2f();

        public float gameSpeed { get; private set; }

        public Game(Window passedWindow, Audio passedAudio, AssetLoader passedAsset)
        {
            window = passedWindow;
            audio = passedAudio;
            assetLoader = passedAsset;

            updateFromFile();
            isPaused = false;
            ship.isVisible = true;
            ufo.speed = 3;
            for (int i = 0; i < 48; i++)
            {
                smallAsteroids[i] = new Entity();
                smallAsteroids[i].speed = 6;
                if (i < 24)
                {
                    mediumAsteroids[i] = new Entity(new Vector2f(-500, -500));
                    mediumAsteroids[i].speed = 2;
                }
                if (i < 12)
                {
                    largeAsteroids[i] = new Entity(new Vector2f(-500, -500));
                    largeAsteroids[i].speed = .5f;
                }
                if (i < 4)
                {
                    explosions[i] = new Vector2f(-500, -500);
                    largeAsteroids[i].isVisible = true;
                    bullets[i] = new Entity(new Vector2f(-500, -500));
                    bullets[i].speed = 10;
                }
            }
            randomiseAsteroids();
        }

        //updates the settings from the INI
        private void updateFromFile()
        {
            gameSpeed = Convert.ToInt32(loadINI.getValue("GameSpeed"));
            fireKey = loadINI.getValue("FireButton");
            pauseKey = loadINI.getValue("Pause");
            leftKey = loadINI.getValue("LeftButton");
            rightKey = loadINI.getValue("RightButton");
            accelerateKey = loadINI.getValue("Accelerate");
            hyperspaceKey = loadINI.getValue("HyperSpace");
            audio.setSoundOn(Convert.ToBoolean(loadINI.getValue("Sound")));
            highScoreNames[0] = loadINI.getValue("HighScoreName1");
            highScoreNames[1] = loadINI.getValue("HighScoreName2");
            highScoreNames[2] = loadINI.getValue("HighScoreName3");
            highScoreNames[3] = loadINI.getValue("HighScoreName4");
            highScores[0] = Convert.ToInt32(loadINI.getValue("HighScore1"));
            highScores[1] = Convert.ToInt32(loadINI.getValue("HighScore2"));
            highScores[2] = Convert.ToInt32(loadINI.getValue("HighScore3"));
            highScores[3] = Convert.ToInt32(loadINI.getValue("HighScore4"));
            gameSpeed = 1000 / gameSpeed;
        }

        //updates the INI from the current settings
        private void updateToFile()
        {
            loadINI.updateFile("FireButton", fireKey);
            loadINI.updateFile("Pause", pauseKey);
            loadINI.updateFile("LeftButton", leftKey);
            loadINI.updateFile("RightButton", rightKey);
            loadINI.updateFile("Accelerate", accelerateKey);
            loadINI.updateFile("HyperSpace", hyperspaceKey);
            loadINI.updateFile("Sound", audio.getSoundOn().ToString());
            loadINI.updateFile("HighScoreName1", highScoreNames[0]);
            loadINI.updateFile("HighScoreName2", highScoreNames[1]);
            loadINI.updateFile("HighScoreName3", highScoreNames[2]);
            loadINI.updateFile("HighScoreName4", highScoreNames[3]);
            loadINI.updateFile("HighScore1", highScores[0].ToString());
            loadINI.updateFile("HighScore2", highScores[1].ToString());
            loadINI.updateFile("HighScore3", highScores[2].ToString());
            loadINI.updateFile("HighScore4", highScores[3].ToString());
        }

        //updates the highscore table
        private void updateHighScoreTable(string newName, int newScore)
        {
            if (newScore >= highScores[0])
            {
                highScores[3] = highScores[2];
                highScores[2] = highScores[1];
                highScores[1] = highScores[0];
                highScores[0] = newScore;
                highScoreNames[3] = highScoreNames[2];
                highScoreNames[2] = highScoreNames[1];
                highScoreNames[1] = highScoreNames[0];
                highScoreNames[0] = newName;
            }
            else if (newScore >= highScores[1])
            {
                highScores[3] = highScores[2];
                highScores[2] = highScores[1];
                highScores[1] = newScore;
                highScoreNames[3] = highScoreNames[2];
                highScoreNames[2] = highScoreNames[1];
                highScoreNames[1] = newName;
            }
            else if (newScore >= highScores[2])
            {
                highScores[2] = highScores[1];
                highScores[2] = newScore;
                highScoreNames[2] = highScoreNames[1];
                highScoreNames[2] = newName;
            }
            else if (newScore >= highScores[3])
            {
                highScores[3] = newScore;
                highScoreNames[3] = newName;
            }
        }

        //randomly set up the asteroids
        private void randomiseAsteroids()
        {
            for (int i = 0; i < level + 4; i++)
            {
                if (i == 12)
                {
                    break;
                }

                largeAsteroids[i].position.X = random.Next(640);
                largeAsteroids[i].position.Y = random.Next(480);
                largeAsteroids[i].angle = random.Next(360);
                largeAsteroids[i].isVisible = true;
                while (true)
                {
                    if ((largeAsteroids[i].position.X > 160 && largeAsteroids[i].position.X < 400))
                    {
                        largeAsteroids[i].position.X = random.Next(640);
                        largeAsteroids[i].position.Y = random.Next(480);
                    }
                    else
                        break;
                }
                largeAsteroids[i].vector = calculateVector(largeAsteroids[i].position, largeAsteroids[i].speed, largeAsteroids[i].angle);
            }
        }

        //starts a new game at given level
        private void reset(int newLevel)
        {
            asteroidsLeft = level + 4;
            mediumAsteroidsCounter = 0;
            smallAsteroidsCounter = 0;
            lives = 2;
            level = newLevel;
            if (newLevel == 0)
            {
                score = 0;
            }
            ship.position = new Vector2f(302, 228);
            ship.angle = 0;
            ship.vector.X = 0;
            ship.vector.Y = 0;
            bulletCounter = 0;
            ufo.isVisible = false;
            for (int i = 0; i < 48; i++)
            {
                smallAsteroids[i].angle = 0;
                smallAsteroids[i].position.X = -500;
                smallAsteroids[i].position.Y = -500;
                smallAsteroids[i].isVisible = false;
                if (i < 24)
                {
                    mediumAsteroids[i].angle = 0;
                    mediumAsteroids[i].position.X = -500;
                    mediumAsteroids[i].position.Y = -500;
                    mediumAsteroids[i].isVisible = false;
                }
                if (i < 12)
                {
                    largeAsteroids[i].angle = 0;
                    largeAsteroids[i].position.X = -500;
                    largeAsteroids[i].position.Y = -500;
                    largeAsteroids[i].isVisible = false;
                }
                if (i < 4)
                {
                    largeAsteroids[i].isVisible = true;
                    bullets[i].position.X = -500;
                    bullets[i].position.Y = -500;
                    bullets[i].vector.X = 0;
                    bullets[i].vector.Y = 0;
                    bullets[i].isVisible = false;
                }
            }
            randomiseAsteroids();
        }


        //calculates a vector from speed, angle, and position
        private Vector2f calculateVector(Vector2f position, float speed, int angle)
        {
            angle *= -1;
            tempVector.X = position.X + (float)Math.Cos(Math.PI * (angle) / 180.0) * speed;
            tempVector.Y = position.Y - (float)Math.Sin(Math.PI * (angle) / 180.0) * speed;
            tempVector.X -= position.X;
            tempVector.Y -= position.Y;
            return tempVector;
        }

        //updates the position of all entities
        private void updatePositions()
        {
            //ship
            if (ship.isVisible == true)
            {
                ship.position.X += ship.vector.X;
                ship.position.Y += ship.vector.Y;
            }
            //bullet
            for (int i = 0; i < 4; i++)
            {
                if (bullets[i].isVisible == true)
                {
                    bulletsCounter[i]++;
                    bullets[i].position.X += bullets[i].vector.X;
                    bullets[i].position.Y += bullets[i].vector.Y;
                }
                if (bulletsCounter[i] == bulletTime)
                {
                    bullets[i].position.Y = -500;
                    bullets[i].position.X = -500;
                    bulletsCounter[i] = 0;
                    bullets[i].isVisible = false;
                }
            }
            //large asteroids
            for (int i = 0; i < 12; i++)
            {
                if (largeAsteroids[i].isVisible == true)
                {
                    largeAsteroids[i].position.X += largeAsteroids[i].vector.X;
                    largeAsteroids[i].position.Y += largeAsteroids[i].vector.Y;
                }
            }
            //medium asteroids
            for (int i = 0; i < 24; i++)
            {
                if (mediumAsteroids[i].isVisible == true)
                {
                    mediumAsteroids[i].position.X += mediumAsteroids[i].vector.X;
                    mediumAsteroids[i].position.Y += mediumAsteroids[i].vector.Y;
                }
            }
            //small asteroids
            for (int i = 0; i < 48; i++)
            {
                if (smallAsteroids[i].isVisible == true)
                {
                    smallAsteroids[i].position.X += smallAsteroids[i].vector.X;
                    smallAsteroids[i].position.Y += smallAsteroids[i].vector.Y;
                }
            }
            //ufo
            if (ufo.isVisible == true && ufo.position.X > -34)
            {
                ufo.position.X -= ufo.speed;
            }
            else
            {
                ufo.position.X = -500;
                ufo.position.Y = -500;
            }
        }

        //moves the ships position or direction
        private void moveShip(string key)
        {
            if (ship.isVisible == true)
            {
                if (key.Contains(leftKey))
                {
                    ship.angle -= 5;
                }
                if (key.Contains(rightKey))
                {
                    ship.angle += 5;
                }
                if (key.Contains(accelerateKey))
                {
                    if (ship.speed < 5)
                    {
                        ship.speed += .002f;
                    }

                    tempVector = calculateVector(ship.position, ship.speed, ship.angle);

                    if (ship.vector.X + tempVector.X < 5.1f && ship.vector.X + tempVector.X > -5.1f)
                    {
                        ship.vector.X += tempVector.X;
                    }
                    if (ship.vector.Y + tempVector.Y < 5.1f && ship.vector.Y + tempVector.Y > -5.1f)
                    {
                        ship.vector.Y += tempVector.Y;
                    }
                }
                else
                {
                    ship.speed = 0;
                }
            }
        }

        //randomly creates a UFO
        private void makeUFO()
        {
            if (random.Next(1000) < 2 && ufo.isVisible == false)
            {
                ufo.isVisible = true;
                ufo.position.X = 640;
                ufo.position.Y = random.Next(460);
            }
        }

        //fires a bullet
        private void fireBullet(string key)
        {
            if (key.Contains(fireKey) && ship.isVisible == true)
            {
                if (bulletCounter < 3)
                {
                    bulletCounter++;
                }
                else
                {
                    bulletCounter = 0;
                }
                audio.samplePlay("shotSnd");
                bullets[bulletCounter].isVisible = true;
                bullets[bulletCounter].position.X = ship.position.X + 10;
                bullets[bulletCounter].position.Y = ship.position.Y + 10;
                bullets[bulletCounter].vector = calculateVector(bullets[bulletCounter].position, bullets[bulletCounter].speed, ship.angle);
                bullets[bulletCounter].angle = ship.angle;
            }
        }

        //jumps the ship to a random spot
        private void hyperspaceJump(string key)
        {
            if (key.Contains(hyperspaceKey))
            {
                ship.position.X = random.Next(640);
                ship.position.Y = random.Next(480);
            }
        }

        //respawns the ship after you die
        private void shipRespawn()
        {
            respawnCounter.increment();
            if (respawnCounter.atAmount(30))
            {
                respawnCounter.resetCounter();
                ship.isVisible = true;
                ship.position.X = 302;
                ship.position.Y = 228;
                ship.angle = 0;
                ship.vector.X = 0;
                ship.vector.Y = 0;
                lives--;
            }
        }

        //creates and explosion at given point
        private void createExplosion(Vector2f position)
        {
            if (explosionCounter > 3)
            {
                explosionCounter = 0;
            }
            explosions[explosionCounter] = position;
            explosionCounter++;
        }

        //destroys a bullet
        private void destroyBullet(int index)
        {
            bullets[index].position.X = -500;
            bullets[index].position.Y = -500;
            bullets[index].isVisible = false;
            bulletsCounter[index] = 0;
        }

        //destroys a large asteroid
        private void destroyLargeAsteroid(int index)
        {
            if (largeAsteroids[index].isVisible == true)
            {
                asteroidsLeft += 1;
                score += 20;
                if (mediumAsteroidsCounter >= 24)
                {
                    mediumAsteroidsCounter = 0;
                }
                mediumAsteroids[mediumAsteroidsCounter].angle = random.Next(360);
                mediumAsteroids[mediumAsteroidsCounter].position.X = largeAsteroids[index].position.X;
                mediumAsteroids[mediumAsteroidsCounter].position.Y = largeAsteroids[index].position.Y;
                mediumAsteroids[mediumAsteroidsCounter].isVisible = true;
                mediumAsteroids[mediumAsteroidsCounter].vector = calculateVector(mediumAsteroids[mediumAsteroidsCounter].position, mediumAsteroids[mediumAsteroidsCounter].speed, mediumAsteroids[mediumAsteroidsCounter].angle);
                mediumAsteroidsCounter++;
                mediumAsteroids[mediumAsteroidsCounter].angle = random.Next(360);
                mediumAsteroids[mediumAsteroidsCounter].position.X = largeAsteroids[index].position.X;
                mediumAsteroids[mediumAsteroidsCounter].position.Y = largeAsteroids[index].position.Y;
                mediumAsteroids[mediumAsteroidsCounter].isVisible = true;
                mediumAsteroids[mediumAsteroidsCounter].vector = calculateVector(mediumAsteroids[mediumAsteroidsCounter].position, mediumAsteroids[mediumAsteroidsCounter].speed, mediumAsteroids[mediumAsteroidsCounter].angle);
                mediumAsteroidsCounter++;
                largeAsteroids[index].position.X = -500;
                largeAsteroids[index].position.Y = -500;
                largeAsteroids[index].isVisible = false;
            }
        }

        //destroys a medium asteroid
        private void destroyMediumAsteroid(int index)
        {
            if (mediumAsteroids[index].isVisible == true)
            {
                asteroidsLeft += 1;
                score += 30;
                if (smallAsteroidsCounter >= 48)
                {
                    smallAsteroidsCounter = 0;
                }
                smallAsteroids[smallAsteroidsCounter].angle = random.Next(360);
                smallAsteroids[smallAsteroidsCounter].position.X = mediumAsteroids[index].position.X;
                smallAsteroids[smallAsteroidsCounter].position.Y = mediumAsteroids[index].position.Y;
                smallAsteroids[smallAsteroidsCounter].isVisible = true;
                smallAsteroids[smallAsteroidsCounter].vector = calculateVector(smallAsteroids[smallAsteroidsCounter].position, smallAsteroids[smallAsteroidsCounter].speed, smallAsteroids[smallAsteroidsCounter].angle);
                smallAsteroidsCounter++;
                smallAsteroids[smallAsteroidsCounter].angle = random.Next(360);
                smallAsteroids[smallAsteroidsCounter].position.X = mediumAsteroids[index].position.X;
                smallAsteroids[smallAsteroidsCounter].position.Y = mediumAsteroids[index].position.Y;
                smallAsteroids[smallAsteroidsCounter].isVisible = true;
                smallAsteroids[smallAsteroidsCounter].vector = calculateVector(smallAsteroids[smallAsteroidsCounter].position, smallAsteroids[smallAsteroidsCounter].speed, smallAsteroids[smallAsteroidsCounter].angle);
                smallAsteroidsCounter++;
                mediumAsteroids[index].position.X = -500;
                mediumAsteroids[index].position.Y = -500;
                mediumAsteroids[index].isVisible = false;
            }
        }

        //destroys a small asteroid
        private void destroySmallAsteroid(int index)
        {
            if (smallAsteroids[index].isVisible == true)
            {
                asteroidsLeft -= 1;
                score += 40;
                smallAsteroids[index].position.X = -500;
                smallAsteroids[index].position.Y = -500;
                smallAsteroids[index].isVisible = false;
            }
        }

        //destroys the ship
        private void destroyShip()
        {
            ship.position.X = -500;
            ship.position.Y = -500;
            ship.isVisible = false;
        }

        //destroys the UFO
        private void destroyUFO()
        {
            ufo.position.X = -500;
            ufo.position.Y = -500;
            score += 100;
        }

        //destroys an explosion
        private void destroyExplosion(int index)
        {
            explosions[index].X = -500;
            explosions[index].Y = -500;
        }

        //wraps an entity around the screen if it is out of bounds
        private void wrapEntitys()
        {
            //ship
            if (ship.position.X >= 640)
            {
                ship.position.X = -30;
            }
            else if (ship.position.X <= -30)
            {
                ship.position.X = 640;
            }
            else if (ship.position.Y >= 480)
            {
                ship.position.Y = -30;
            }
            else if (ship.position.Y <= -30)
            {
                ship.position.Y = 480;
            }
            //bullets
            for (int i = 0; i < 4; i++)
            {
                if (bullets[i].position.X >= 640)
                {
                    bullets[i].position.X = -15;
                }
                else if (bullets[i].position.X <= -15)
                {
                    bullets[i].position.X = 640;
                }
                else if (bullets[i].position.Y >= 480)
                {
                    bullets[i].position.Y = -15;
                }
                else if (bullets[i].position.Y <= -15)
                {
                    bullets[i].position.Y = 480;
                }
            }
            //large asteroids
            for (int i = 0; i < 12; i++)
            {
                if (largeAsteroids[i].position.X >= 640)
                {
                    largeAsteroids[i].position.X = -125;
                }
                else if (largeAsteroids[i].position.X <= -125)
                {
                    largeAsteroids[i].position.X = 640;
                }
                else if (largeAsteroids[i].position.Y >= 480)
                {
                    largeAsteroids[i].position.Y = -125;
                }
                else if (largeAsteroids[i].position.Y <= -125)
                {
                    largeAsteroids[i].position.Y = 480;
                }
            }
            //medium asteroids
            for (int i = 0; i < 24; i++)
            {
                if (mediumAsteroids[i].position.X >= 640)
                {
                    mediumAsteroids[i].position.X = -70;
                }
                else if (mediumAsteroids[i].position.X <= -70)
                {
                    mediumAsteroids[i].position.X = 640;
                }
                else if (mediumAsteroids[i].position.Y >= 480)
                {
                    mediumAsteroids[i].position.Y = -70;
                }
                else if (mediumAsteroids[i].position.Y <= -70)
                {
                    mediumAsteroids[i].position.Y = 480;
                }
            }
            //small asteroids
            for (int i = 0; i < 24; i++)
            {
                if (smallAsteroids[i].position.X >= 640)
                {
                    smallAsteroids[i].position.X = -34;
                }
                else if (smallAsteroids[i].position.X <= -34)
                {
                    smallAsteroids[i].position.X = 640;
                }
                else if (smallAsteroids[i].position.Y >= 480)
                {
                    smallAsteroids[i].position.Y = -34;
                }
                else if (smallAsteroids[i].position.Y <= -34)
                {
                    smallAsteroids[i].position.Y = 480;
                }
            }
        }

        //tests if the level has ended
        private bool levelEnded()
        {
            if (asteroidsLeft == 0)
            {
                return true;
            }
            return false;
        }

        //changes to the next level
        private void nextLevel()
        {
            level++;
            reset(level);
        }

        //updates the input
        public void inputUpdate()
        {
            mouseClick = window.inputMouseClick();
            keyboard = window.inputKeyboard();
            keyboardDown = window.inputKeyboardDown();
            keyboardSingle = window.inputKeyboardSingle();
        }

        //updates the menus
        public void menuUpdate()
        {
            if (isMenu == true)
            {
                if (mouseClick == "Leftbutton")
                {
                    if (window.isWithin("PlayButtonSpr", window.mousePositionView()))
                    {
                        isMenu = false;
                        updateFromFile();
                        reset(0);
                        assetLoader.loadAsset("Clear");
                        assetLoader.loadAsset("Game");
                    }
                    else if (window.isWithin("HighScoreButtonSpr", window.mousePositionView()))
                    {
                        assetLoader.loadAsset("Clear");
                        assetLoader.loadAsset("HighScore");
                        window.setText("ScoreTxt1", highScoreNames[0]);
                        window.setText("ScoreTxt2", highScoreNames[1]);
                        window.setText("ScoreTxt3", highScoreNames[2]);
                        window.setText("ScoreTxt4", highScoreNames[3]);
                        window.setText("NameTxt1", highScores[0].ToString());
                        window.setText("NameTxt2", highScores[1].ToString());
                        window.setText("NameTxt3", highScores[2].ToString());
                        window.setText("NameTxt4", highScores[3].ToString());
                    }
                    else if (window.isWithin("OptionsButtonSpr", window.mousePositionView()))
                    {
                        assetLoader.loadAsset("Clear");
                        assetLoader.loadAsset("Options");
                        updateFromFile();
                        if (audio.getSoundOn() == true)
                        {
                            window.setText("SoundTxt", "Sound On");
                        }
                        else
                        {
                            window.setText("SoundTxt", "Sound Off");
                        }
                    }
                    else if (window.isWithin("MenuButtonSpr", window.mousePositionView()) && keyPress == false)
                    {
                        assetLoader.loadAsset("Clear");
                        assetLoader.loadAsset("MainMenu");
                    }
                    else if (window.isWithin("ApplyButtonSpr", window.mousePositionView()) && keyPress == false)
                    {
                        updateToFile();
                    }
                    else if (window.isWithin("SoundButtonSpr", window.mousePositionView()))
                    {
                        if (audio.getSoundOn() == true)
                        {
                            audio.setSoundOn(false);
                            window.setText("SoundTxt", "Sound Off");
                        }
                        else
                        {
                            audio.setSoundOn(true);
                            window.setText("SoundTxt", "Sound On");
                        }
                    }
                    else if (window.isWithin("KeyboardButtonSpr", window.mousePositionView()))
                    {
                        assetLoader.loadAsset("Clear");
                        assetLoader.loadAsset("KeyboardSetup");
                        if (fireKey.Contains("Letr") == true)
                        {
                            window.setText("FireKeyTxt", fireKey[4].ToString());
                        }
                        else
                        {
                            window.setText("FireKeyTxt", fireKey);
                        }
                        if (pauseKey.Contains("Letr") == true)
                        {
                            window.setText("PauseKeyTxt", pauseKey[4].ToString());
                        }
                        else
                        {
                            window.setText("PauseKeyTxt", pauseKey);
                        }
                        if (hyperspaceKey.Contains("Letr") == true)
                        {
                            window.setText("HyperspaceKeyTxt", hyperspaceKey[4].ToString());
                        }
                        else
                        {
                            window.setText("HyperspaceKeyTxt", hyperspaceKey);
                        }
                        if (rightKey.Contains("Letr") == true)
                        {
                            window.setText("RightKeyTxt", rightKey[4].ToString());
                        }
                        else
                        {
                            window.setText("RightKeyTxt", rightKey);
                        }
                        if (leftKey.Contains("Letr") == true)
                        {
                            window.setText("LeftKeyTxt", leftKey[4].ToString());
                        }
                        else
                        {
                            window.setText("LeftKeyTxt", leftKey);
                        }
                        if (accelerateKey.Contains("Letr") == true)
                        {
                            window.setText("AccelerateKeyTxt", accelerateKey[4].ToString());
                        }
                        else
                        {
                            window.setText("AccelerateKeyTxt", accelerateKey);
                        }
                    }
                    else if (window.isWithin("FireButtonSpr", window.mousePositionView()))
                    {
                        setKey = "Fire";
                        keyPress = true;
                    }
                    else if (window.isWithin("PauseButtonSpr", window.mousePositionView()))
                    {
                        setKey = "Pause";
                        keyPress = true;
                    }
                    else if (window.isWithin("HyperspaceButtonSpr", window.mousePositionView()))
                    {
                        setKey = "HyperSpace";
                        keyPress = true;
                    }
                    else if (window.isWithin("RightButtonSpr", window.mousePositionView()))
                    {
                        setKey = "Right";
                        keyPress = true;
                    }
                    else if (window.isWithin("LeftButtonSpr", window.mousePositionView()))
                    {
                        setKey = "Left";
                        keyPress = true;
                    }
                    else if (window.isWithin("AccelerateButtonSpr", window.mousePositionView()))
                    {
                        setKey = "Accelerate";
                        keyPress = true;
                    }
                    else if (window.isWithin("EnterButtonSpr", window.mousePositionView()))
                    {
                        typing = false;
                        updateHighScoreTable(textEdit.getString(), score);
                        updateToFile();
                        assetLoader.loadAsset("Clear");
                        assetLoader.loadAsset("HighScore");
                        window.setText("ScoreTxt1", highScoreNames[0] + "\t\t" + highScores[0]);
                        window.setText("ScoreTxt2", highScoreNames[1] + "\t\t" + highScores[1]);
                        window.setText("ScoreTxt3", highScoreNames[2] + "\t\t" + highScores[2]);
                        window.setText("ScoreTxt4", highScoreNames[3] + "\t\t" + highScores[3]);
                    }
                }
            }
        }

        //binds the keyboard keys in the options
        public void keyboardBind()
        {
            if (keyPress == true)
            {
                window.moveEntity("PressKeyTxt", new Vector2f(195, 420));
                if (keyboardSingle != "" && keyboardSingle != "unknownkey")
                {
                    switch (setKey)
                    {
                        case "Fire":
                            fireKey = keyboardSingle;
                            keyPress = false;
                            if (fireKey.Contains("Letr") == true)
                            {
                                window.setText("FireKeyTxt", fireKey[4].ToString());
                            }
                            else
                            {
                                window.setText("FireKeyTxt", fireKey);
                            }
                            break;
                        case "Pause":
                            pauseKey = keyboardSingle;
                            keyPress = false;
                            window.setText("PauseKeyTxt", pauseKey);
                            if (pauseKey.Contains("Letr") == true)
                            {
                                window.setText("PauseKeyTxt", pauseKey[4].ToString());
                            }
                            else
                            {
                                window.setText("PauseKeyTxt", pauseKey);
                            }
                            break;
                        case "HyperSpace":
                            hyperspaceKey = keyboardSingle;
                            keyPress = false;
                            window.setText("HyperspaceKeyTxt", hyperspaceKey);
                            if (hyperspaceKey.Contains("Letr") == true)
                            {
                                window.setText("HyperspaceKeyTxt", hyperspaceKey[4].ToString());
                            }
                            else
                            {
                                window.setText("HyperspaceKeyTxt", hyperspaceKey);
                            }
                            break;
                        case "Right":
                            rightKey = keyboardSingle;
                            keyPress = false;
                            window.setText("RightKeyTxt", rightKey);
                            if (rightKey.Contains("Letr") == true)
                            {
                                window.setText("RightKeyTxt", rightKey[4].ToString());
                            }
                            else
                            {
                                window.setText("RightKeyTxt", rightKey);
                            }
                            break;
                        case "Left":
                            leftKey = keyboardSingle;
                            keyPress = false;
                            window.setText("LeftKeyTxt", leftKey);
                            if (leftKey.Contains("Letr") == true)
                            {
                                window.setText("LeftKeyTxt", leftKey[4].ToString());
                            }
                            else
                            {
                                window.setText("LeftKeyTxt", leftKey);
                            }
                            break;
                        case "Accelerate":
                            accelerateKey = keyboardSingle;
                            keyPress = false;
                            window.setText("AccelerateKeyTxt", accelerateKey);
                            if (accelerateKey.Contains("Letr") == true)
                            {
                                window.setText("AccelerateKeyTxt", accelerateKey[4].ToString());
                            }
                            else
                            {
                                window.setText("AccelerateKeyTxt", accelerateKey);
                            }
                            break;
                    }
                }
            }
            else
            {
                window.moveEntity("PressKeyTxt", new Vector2f(-500, -500));
            }
        }

        //takes input for highscores
        public void inputString()
        {
            if (typing == true)
            {
                textEdit.takeInput(window.inputKeyboard());
                window.setText("NameTxt", textEdit.getString());
            }
        }

        //exits to the main menu if Esc is pressed during the game
        //exits the program if its pressed in the menus
        public bool escape()
        {
            if (keyboardDown.Contains("Escape"))
            {
                if (isMenu == false)
                {
                    assetLoader.loadAsset("Clear");
                    assetLoader.loadAsset("MainMenu");
                    isMenu = true;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        //pauses and unpauses the game
        public void pause()
        {
            if (isMenu == false)
            {
                if (keyboardDown.Contains(pauseKey))
                {
                    if (isPaused == true)
                    {
                        window.moveEntity("PausedTxt", new Vector2f(-500, -500));
                        isPaused = false;
                        for (int i = 1; i < explosions.Length + 1; i++)
                        {
                            window.resumeAnimation("ExplosionAni" + i.ToString());
                        }
                    }
                    else
                    {
                        isPaused = true;
                        tempVector.X = 270;
                        tempVector.Y = 210;
                        window.moveEntity("PausedTxt", tempVector);
                        for (int i = 1; i < explosions.Length + 1; i++)
                        {
                            window.pauseAnimation("ExplosionAni" + i.ToString());
                        }
                    }
                }
            }
        }

        //destroys explosion if its animation is at the last frame
        public void explosionLastFrame()
        {
            for (int i = 1; i < 5; i++)
            {
                if (window.getFrame("ExplosionAni" + i) == 3)
                {
                    destroyExplosion(i - 1);
                }
            }
        }

        //checks if the game is being played
        public bool isPlaying()
        {
            if (isPaused == false && isMenu == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //checks for collisions with bullets
        public void checkBulletCollisions()
        {
            for (int i = 1; i < 5; i++)
            {
                if (bullets[i - 1].isVisible)
                {
                    tempStr = window.batchIsOverlapping(("BulletSpr" + i), "LargeAsteroidSpr");
                    for (int ii = 1; ii < 13; ii++)
                    {
                        if (tempStr.Contains("LargeAsteroidSpr" + ii))
                        {
                            tempVector.X = largeAsteroids[ii - 1].position.X + 43;
                            tempVector.Y = largeAsteroids[ii - 1].position.Y + 57;
                            createExplosion(tempVector);
                            window.goToFrame("ExplosionAni" + explosionCounter, 0);
                            destroyBullet(i - 1);
                            destroyLargeAsteroid(ii - 1);
                            audio.samplePlay("boomSnd");
                        }
                    }
                    tempStr = window.batchIsOverlapping(("BulletSpr" + i), "MediumAsteroidSpr");
                    for (int ii = 1; ii < 25; ii++)
                    {
                        if (tempStr.Contains("MediumAsteroidSpr" + ii))
                        {
                            tempVector.X = mediumAsteroids[ii - 1].position.X + 15;
                            tempVector.Y = mediumAsteroids[ii - 1].position.Y + 23;
                            createExplosion(tempVector);
                            window.goToFrame("ExplosionAni" + explosionCounter, 0);
                            destroyBullet(i - 1);
                            destroyMediumAsteroid(ii - 1);
                            audio.samplePlay("boomSnd");
                        }
                    }
                    tempStr = window.batchIsOverlapping(("BulletSpr" + i), "SmallAsteroidSpr");
                    for (int ii = 1; ii < 25; ii++)
                    {
                        if (tempStr.Contains("SmallAsteroidSpr" + ii))
                        {
                            createExplosion(smallAsteroids[ii - 1].position);
                            window.goToFrame("ExplosionAni" + explosionCounter, 0);
                            destroyBullet(i - 1);
                            destroySmallAsteroid(ii - 1);
                            audio.samplePlay("boomSnd");
                        }
                    }
                    if (window.isOverlapping("BulletSpr" + i, "UFOSpr"))
                    {
                        createExplosion(ufo.position);
                        window.goToFrame("ExplosionAni" + explosionCounter, 0);
                        destroyBullet(i - 1);
                        destroyUFO();
                    }
                }
            }
        }

        //checks for collisions with the ship
        public void checkShipCollisions()
        {
            tempStr = window.batchIsOverlapping(("ShipSpr"), "LargeAsteroidSpr");
            if (tempStr.Contains("LargeAsteroidSpr") && ship.isVisible == true)
            {
                tempVector.X = ship.position.X;
                tempVector.Y = ship.position.Y - 8;
                createExplosion(tempVector);
                window.goToFrame("ExplosionAni" + explosionCounter, 0);
                destroyShip();
                destroyBullet(0);
                destroyBullet(1);
                destroyBullet(2);
                destroyBullet(3);
                audio.samplePlay("boomSnd");
            }
            tempStr = window.batchIsOverlapping(("ShipSpr"), "MediumAsteroidSpr");
            if (tempStr.Contains("MediumAsteroidSpr") && ship.isVisible == true)
            {
                tempVector.X = ship.position.X;
                tempVector.Y = ship.position.Y - 8;
                createExplosion(tempVector);
                window.goToFrame("ExplosionAni" + explosionCounter, 0);
                destroyShip();
                destroyBullet(0);
                destroyBullet(1);
                destroyBullet(2);
                destroyBullet(3);
                audio.samplePlay("boomSnd");
            }
            tempStr = window.batchIsOverlapping(("ShipSpr"), "SmallAsteroidSpr");
            if (tempStr.Contains("SmallAsteroidSpr") && ship.isVisible == true)
            {
                tempVector.X = ship.position.X;
                tempVector.Y = ship.position.Y - 8;
                createExplosion(tempVector);
                window.goToFrame("ExplosionAni" + explosionCounter, 0);
                destroyShip();
                destroyBullet(0);
                destroyBullet(1);
                destroyBullet(2);
                destroyBullet(3);
                audio.samplePlay("boomSnd");
            }
            if (window.isOverlapping("ShipSpr", "UFOSpr") && ship.isVisible == true)
            {
                tempVector.X = ship.position.X;
                tempVector.Y = ship.position.Y - 8;
                createExplosion(tempVector);
                window.goToFrame("ExplosionAni" + explosionCounter, 0);
                destroyShip();
                destroyBullet(0);
                destroyBullet(1);
                destroyBullet(2);
                destroyBullet(3);
                audio.samplePlay("boomSnd");
            }
        }
        
        //ends the game if you are out of lives
        public void checkEndOfGame()
        {
            if (ship.isVisible == false && lives > 0)
            {
                shipRespawn();
            }
            else if (ship.isVisible == false && lives == 0)
            {
                ticker.increment();
            }
            if (ticker.atAmount(70) && ship.isVisible == false)
            {
                ticker.resetCounter();
                isMenu = true;
                assetLoader.loadAsset("Clear");
                if (score > highScores[0] ||
                    score > highScores[1] ||
                    score > highScores[2] ||
                    score > highScores[3])
                {
                    assetLoader.loadAsset("EnterHighScore");
                    tempStr = string.Empty;
                    typing = true;
                }
                else
                {
                    assetLoader.loadAsset("MainMenu");
                }
            }
        }

        //updates all entitys
        public void entityUpdate()
        {
            makeUFO();
            fireBullet(keyboardDown);
            moveShip(keyboard);
            hyperspaceJump(keyboardDown);
            wrapEntitys();
            updatePositions();
            window.setText("ScoreBoardTxt", "Score: " + score.ToString());
            window.setText("LevelTxt", "Level: " + (level + 1));
            window.setText("LivesTxt", "Lives: " + lives);
        }

        //changes the level
        public void changeLevel()
        {
            if (levelEnded())
            {
                ticker.increment();
            }
            if (levelEnded() && ticker.atAmount(30))
            {
                ticker.resetCounter();
                nextLevel();
            }
        }

        //graphic update
        public void graphicUpdate()
        {
            window.rotateEntityCenter("ShipSpr", ship.angle);
            window.moveEntity("ShipSpr", ship.position);
            for (int i = 1; i < 49; i++)
            {
                if (i < 5)
                {
                    window.moveEntity(("BulletSpr" + i), bullets[i - 1].position);
                    window.moveEntity(("ExplosionAni" + i), explosions[i - 1]);
                    window.rotateEntityCenter("BulletSpr" + i, bullets[i - 1].angle);
                }
                if (i < 13)
                {
                    window.moveEntity(("LargeAsteroidSpr" + i), largeAsteroids[i - 1].position);
                }
                if (i < 25)
                {
                    window.moveEntity(("MediumAsteroidSpr" + i), mediumAsteroids[i - 1].position);
                }
                window.moveEntity(("SmallAsteroidSpr" + i), smallAsteroids[i - 1].position);
            }
            window.moveEntity("UFOSpr", ufo.position);
        }
    }
}
