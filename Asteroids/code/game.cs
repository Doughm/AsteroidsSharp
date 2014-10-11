using SFML.Window;
using System;

namespace Asteroids
{
    class Game
    {
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
        private int[] bulletsCounter = new int[4]{0,0,0,0};
        private const int bulletTime = 40;
        private Vector2f[] explosions = new Vector2f[4];
        private int explosionCounter = 0;
        private Entity ufo = new Entity(new Vector2f(-500, -500));
        private int level = 0;
        private int score = 0;
        private int lives = 2;
        private int bulletCounter = 0;
        private int asteroidsLeft = 4;
        private Vector2f tempVector;
        private bool isPaused;
        private string fireKey;
        private string pauseKey;
        private string leftKey;
        private string rightKey;
        private string hyperspaceKey;
        private string accelerateKey;
        private bool soundOn;
        private string[] highScoreNames = new string[4];
        private int[] highScores = new int[4];
        private float gameSpeed;

        public Game()
        {
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
                    explosions[i] = new Vector2f(-500,-500);
                    largeAsteroids[i].isVisible = true;
                    bullets[i] = new Entity(new Vector2f(-500, -500));
                    bullets[i].speed = 10;
                }
            }
            randomiseAsteroids();
        }

        //updates the settings from the INI
        public void updateFromFile()
        {
            gameSpeed = Convert.ToInt32(loadINI.getValue("GameSpeed"));
            fireKey = loadINI.getValue("FireButton");
            pauseKey = loadINI.getValue("Pause");
            leftKey = loadINI.getValue("LeftButton");
            rightKey = loadINI.getValue("RightButton");
            accelerateKey = loadINI.getValue("Accelerate");
            hyperspaceKey = loadINI.getValue("HyperSpace");
            soundOn = Convert.ToBoolean(loadINI.getValue("Sound"));
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
        public void updateToFile()
        {
            loadINI.updateFile("GameSpeed", gameSpeed.ToString());
            loadINI.updateFile("FireButton", fireKey);
            loadINI.updateFile("Pause", pauseKey);
            loadINI.updateFile("LeftButton", leftKey);
            loadINI.updateFile("RightButton", rightKey);
            loadINI.updateFile("Accelerate", accelerateKey);
            loadINI.updateFile("HyperSpace", hyperspaceKey);
            loadINI.updateFile("Sound", soundOn.ToString());
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
        public void updateHighScoreTable(string newName, int newScore)
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
            for(int i = 0 ; i < level + 4; i++)
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
        public void reset(int newLevel)
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
        public void updatePositions()
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
        public void moveShip(string key)
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
        public void makeUFO()
        {
            if (random.Next(1000) < 2 && ufo.isVisible == false)
            {
                ufo.isVisible = true;
                ufo.position.X = 640;
                ufo.position.Y = random.Next(460);
            }
        }

        //fires a bullet
        public void fireBullet(string key)
        {
            if (key.Contains(fireKey))
            {
                if (bulletCounter < 3)
                {
                    bulletCounter++;
                }
                else
                {
                    bulletCounter = 0;
                }
                bullets[bulletCounter].isVisible = true;
                bullets[bulletCounter].position.X = ship.position.X + 10;
                bullets[bulletCounter].position.Y = ship.position.Y + 10;
                bullets[bulletCounter].vector = calculateVector(bullets[bulletCounter].position, bullets[bulletCounter].speed, ship.angle);
                bullets[bulletCounter].angle = ship.angle;
            }
        }

        //jumps the ship to a random spot
        public void hyperspaceJump(string key)
        {
            if (key.Contains(hyperspaceKey))
            {
                ship.position.X = random.Next(640);
                ship.position.Y = random.Next(480);
            }
        }

        //respawns the ship after you die
        public void shipRespawn()
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
        public void createExplosion(Vector2f position)
        {
            if (explosionCounter > 3)
            {
                explosionCounter = 0;
            }
            explosions[explosionCounter] = position;
            explosionCounter++;
        }

        //destroys a bullet
        public void destroyBullet(int index)
        {
            bullets[index].position.X = -500;
            bullets[index].position.Y = -500;
            bullets[index].isVisible = false;
            bulletsCounter[index] = 0;
        }

        //destroys a large asteroid
        public void destroyLargeAsteroid(int index)
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
        public void destroyMediumAsteroid(int index)
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
        public void destroySmallAsteroid(int index)
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
        public void destroyShip()
        {
            ship.position.X = -500;
            ship.position.Y = -500;
            ship.isVisible = false;
        }

        //destroys the UFO
        public void destroyUFO()
        {
            ufo.position.X = -500;
            ufo.position.Y = -500;
            score += 100;
        }

        //destroys an exposition
        public void destroyExposition(int index)
        {
            explosions[index].X = -500;
            explosions[index].Y = -500;
        }

        //wraps an entity around the screen if it is out of bounds
        public void wrapEntitys()
        {
            //ship
            if(ship.position.X >= 640)
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
        public bool levelEnded()
        {
            if (asteroidsLeft == 0)
            {
                return true;
            }
            return false;
        }

        //changes to the next level
        public void nextLevel()
        {
            level++;
            reset(level);
        }


        //returns the position of the ship
        public Vector2f getShipPosition()
        {
            return ship.position;
        }

        //returns the angle of the ship
        public float getShipAngle()
        {
            return ship.angle;
        }

        //returns if the ship is active
        public bool getShipIsVisible()
        {
            return ship.isVisible;
        }

        //returns the position of an explosion
        public Vector2f getExplosionPosition(int index)
        {
            return explosions[index];
        }

        //returns the position of a given large asteroid
        public Vector2f getLargeAsteroidsPosition(int index)
        {
            return largeAsteroids[index].position;
        }

        //returns the position of a given medium asteroid
        public Vector2f getMediumAsteroidsPosition(int index)
        {
            return mediumAsteroids[index].position;
        }

        //returns the position of a given small asteroid
        public Vector2f getSmallAsteroidsPosition(int index)
        {
            return smallAsteroids[index].position;
        }

        //returns the position of the UFO
        public Vector2f getUfoPosition()
        {
            return ufo.position;
        }

        //returns the angle of a bullet
        public float getBulletAngle(int index)
        {
            return bullets[index].angle;
        }

        //gets the position of a bullet entity
        public Vector2f getBulletPosition(int index)
        {
            return bullets[index].position;
        }

        //returns if a bullet is active
        public bool getBulletIsVisible(int index)
        {
            return bullets[index].isVisible;
        }

        //returns if the game is paused
        public bool getIsPaused()
        {
            return isPaused;
        }

        //returns the current level the player is on
        public int getLevel()
        {
            return level + 1;
        }

        //returns the name of the pause key
        public string getPauseKey()
        {
            return pauseKey;
        }

        //returns the name of the fire key
        public string getFireKey()
        {
            return fireKey;
        }

        //returns the name of the left key
        public string getLeftKey()
        {
            return leftKey;
        }

        //returns the name of the right key
        public string getRightKey()
        {
            return rightKey;
        }

        //returns the name of the accelerate key
        public string getAccelerateKey()
        {
            return accelerateKey;
        }

        //returns the name of the hyperspace key
        public string getHyperspaceKey()
        {
            return hyperspaceKey;
        }

        //returns if the sound is on or off
        public bool getSoundOn()
        {
            return soundOn;
        }

        //returns the given highscore name
        public string getHighScoreName(int index)
        {
            return highScoreNames[index];
        }

        //returns the given highscore
        public int getHighScore(int index)
        {
            return highScores[index];
        }

        //returns the score
        public int getScore()
        {
            return score;
        }

        //returns how many lives you have
        public int getLives()
        {
            return lives;
        }

        //returns what explosion you're on
        public int getExplosionNumber()
        {
            return explosionCounter;
        }

        //returns the speed of the game
        public float getGameSpeed()
        {
            return gameSpeed;
        }

        //sets if the ship is active
        public void setIsVisible(bool visible)
        {
            ship.isVisible = visible;
        }

        //moves an explosion
        public void moveExplostion(int index, Vector2f position)
        {
            explosions[index] = position;
        }

        //set if the game is paused
        public void setIsPaused(bool pause)
        {
            isPaused = pause;
        }

        //sets the given highscore name
        public void setHighScoreName(int index, string name)
        {
            highScoreNames[index] = name;
        }

        //sets the given highscore
        public void setHighScore(int index, int score)
        {
            highScores[index] = score;
        }

        //sets if the sound is on or off
        public void setSoundOn(bool sound)
        {
            soundOn = sound;
        }

        //sets the speed of the game
        public void setGameSpeed(int speed)
        {
            gameSpeed = speed;
        }

        //sets the fire key
        public void setFireKey(string key)
        {
            fireKey = key;
        }

        //sets the pause key
        public void setPauseKey(string key)
        {
            pauseKey = key;
        }

        //sets the left key
        public void setLeftKey(string key)
        {
            leftKey = key;
        }

        //sets the right key
        public void setRightKey(string key)
        {
            rightKey = key;
        }

        //sets the accelerate key
        public void setAccelerateKey(string key)
        {
            accelerateKey = key;
        }

        //sets the hyperspace key
        public void setHyperspaceKey(string key)
        {
            hyperspaceKey = key;
        }
    }

    //keeps all information needed for an entity on the screen
    class Entity
    {
        public Vector2f position;
        public Vector2f vector;
        public int angle;
        public float speed;
        public bool isVisible;

        public Entity()
        {
            position = new Vector2f(0, 0);
            vector = new Vector2f();
            angle = 0;
            speed = 0;
            isVisible = false;
        }

        public Entity(Vector2f startingPos)
        {
            position = startingPos;
            vector = new Vector2f(0,0);
            angle = 0;
            speed = 0;
            isVisible = false;
        }
    }
}
