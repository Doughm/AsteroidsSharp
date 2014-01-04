//Asteroids 1.1
//Programer Douglas Harvey-Marose
//
//  Version Changes 1.1
//  updated the used engine to version 1.2
//  game speed is now independent from the frame rate 
//
// - Known isues -
//  all collision detection is bounding box, can look odd
//  rarely a level doesn't end, not sure why
//
//  Assets from:
//  opengameart.org
//  The Games Factory Pro
//

using System;
using SFML.Window;
using SFML.Graphics;

namespace Asteroids
{
    class Program
    {
        GameTimer testTimer = new GameTimer();
        double tempInt;

        Window window = new Window();
        Audio audio = new Audio();
        Game game = new Game();
        GameTimer gameTimer = new GameTimer();
        Ticker ticker = new Ticker();
        bool isMenu = true;
        bool explosion = false;
        string mouseClick;
        string keyboard;
        string keyboardDown;
        string keyboardSingle;
        string tempStr;
        string setKey;
        bool keyPress = false;
        bool typing = false;
        Vector2f tempVector = new Vector2f();

        public void start()
        {
            //loads in the game assets and sets it to the main menu
            assetLoader("MainAssets");
            assetLoader("MainMenu");
            gameTimer.restartWatch();

            while (window.isOpen())
            {
                if (gameTimer.getTimeMilliseconds() >= game.getGameSpeed())
                {
                    //input update
                    mouseClick = window.inputMouseClick();
                    keyboard = window.inputKeyboard();
                    keyboardDown = window.inputKeyboardDown();
                    keyboardSingle = window.inputKeyboardSingle();

                    //menus
                    if (isMenu == true)
                    {
                        if (mouseClick == "Leftbutton")
                        {
                            if (window.isWithin("PlayButtonSpr", window.mousePositionView()))
                            {
                                isMenu = false;
                                game.updateFromFile();
                                game.reset(0);
                                assetLoader("Clear");
                                assetLoader("Game");
                            }
                            else if (window.isWithin("HighScoreButtonSpr", window.mousePositionView()))
                            {
                                assetLoader("Clear");
                                assetLoader("HighScore");
                                window.setText("ScoreTxt1", game.getHighScoreName(0) + "\t\t" + game.getHighScore(0));
                                window.setText("ScoreTxt2", game.getHighScoreName(1) + "\t\t" + game.getHighScore(1));
                                window.setText("ScoreTxt3", game.getHighScoreName(2) + "\t\t" + game.getHighScore(2));
                                window.setText("ScoreTxt4", game.getHighScoreName(3) + "\t\t" + game.getHighScore(3));
                            }
                            else if (window.isWithin("OptionsButtonSpr", window.mousePositionView()))
                            {
                                assetLoader("Clear");
                                assetLoader("Options");
                                game.updateFromFile();
                                if (game.getSoundOn() == true)
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
                                assetLoader("Clear");
                                assetLoader("MainMenu");
                            }
                            else if (window.isWithin("ApplyButtonSpr", window.mousePositionView()) && keyPress == false)
                            {
                                game.updateToFile();
                            }
                            else if (window.isWithin("SoundButtonSpr", window.mousePositionView()))
                            {
                                if (game.getSoundOn() == true)
                                {
                                    game.setSoundOn(false);
                                    window.setText("SoundTxt", "Sound Off");
                                }
                                else
                                {
                                    game.setSoundOn(true);
                                    window.setText("SoundTxt", "Sound On");
                                }
                            }
                            else if (window.isWithin("KeyboardButtonSpr", window.mousePositionView()))
                            {
                                assetLoader("Clear");
                                assetLoader("KeyboardSetup");
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
                                game.updateHighScoreTable(tempStr, game.getScore());
                                game.updateToFile();
                                assetLoader("Clear");
                                assetLoader("HighScore");
                                window.setText("ScoreTxt1", game.getHighScoreName(0) + "\t\t" + game.getHighScore(0));
                                window.setText("ScoreTxt2", game.getHighScoreName(1) + "\t\t" + game.getHighScore(1));
                                window.setText("ScoreTxt3", game.getHighScoreName(2) + "\t\t" + game.getHighScore(2));
                                window.setText("ScoreTxt4", game.getHighScoreName(3) + "\t\t" + game.getHighScore(3));
                            }
                        }
                        window.setText("FireKeyTxt", game.getFireKey());
                        window.setText("PauseKeyTxt", game.getPauseKey());
                        window.setText("HyperspaceKeyTxt", game.getHyperspaceKey());
                        window.setText("RightKeyTxt", game.getRightKey());
                        window.setText("LeftKeyTxt", game.getLeftKey());
                        window.setText("AccelerateKeyTxt", game.getAccelerateKey());
                    }

                    //takes in a single key and adds it to the keyboard bindings
                    if (keyPress == true)
                    {
                        window.moveEntity("PressKeyTxt", new Vector2f(195, 420));
                        if (keyboardSingle != "")
                        {
                            switch (setKey)
                            {
                                case "Fire":
                                    game.setFireKey(keyboardSingle);
                                    keyPress = false;
                                    break;
                                case "Pause":
                                    game.setPauseKey(keyboardSingle);
                                    keyPress = false;
                                    break;
                                case "HyperSpace":
                                    game.setHyperspaceKey(keyboardSingle);
                                    keyPress = false;
                                    break;
                                case "Right":
                                    game.setRightKey(keyboardSingle);
                                    keyPress = false;
                                    break;
                                case "Left":
                                    game.setLeftKey(keyboardSingle);
                                    keyPress = false;
                                    break;
                                case "Accelerate":
                                    game.setAccelerateKey(keyboardSingle);
                                    keyPress = false;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        window.moveEntity("PressKeyTxt", new Vector2f(-500, -500));
                    }

                    //takes in a string one key at a time
                    if (typing == true)
                    {
                        ticker.increment();
                    }
                    if (typing == true && ticker.atAmount(5) && keyboard != "")
                    {
                        if (keyboard == "Backspace")
                        {
                            if (tempStr.Length != 0)
                            {
                                tempStr = tempStr.Remove(tempStr.Length - 1);
                            }
                        }
                        else if (keyboardSingle == "Return")
                        {
                            typing = false;
                            game.updateHighScoreTable(tempStr, game.getScore());
                            game.updateToFile();
                            assetLoader("Clear");
                            assetLoader("HighScore");
                            window.setText("ScoreTxt1", game.getHighScoreName(0) + "\t\t" + game.getHighScore(0));
                            window.setText("ScoreTxt2", game.getHighScoreName(1) + "\t\t" + game.getHighScore(1));
                            window.setText("ScoreTxt3", game.getHighScoreName(2) + "\t\t" + game.getHighScore(2));
                            window.setText("ScoreTxt4", game.getHighScoreName(3) + "\t\t" + game.getHighScore(3));
                        }
                        else if (keyboard == "ALshift" || keyboard == "BLshift" || keyboard == "CLshift" ||
                                keyboard == "DLshift" || keyboard == "ELshift" || keyboard == "FLshift" ||
                                keyboard == "GLshift" || keyboard == "HLshift" || keyboard == "ILshift" ||
                                keyboard == "JLshift" || keyboard == "KLshift" || keyboard == "LLshift" ||
                                keyboard == "MLshift" || keyboard == "NLshift" || keyboard == "OLshift" ||
                                keyboard == "PLshift" || keyboard == "QLshift" || keyboard == "RLshift" ||
                                keyboard == "SLshift" || keyboard == "TLshift" || keyboard == "ULshift" ||
                                keyboard == "VLshift" || keyboard == "WLshift" || keyboard == "XLshift" ||
                                keyboard == "YLshift" || keyboard == "ZLshift")
                        {
                            tempStr += keyboard[0];
                        }
                        else if (keyboard == "ARshift" || keyboard == "BRshift" || keyboard == "CRshift" ||
                                keyboard == "DRshift" || keyboard == "ERshift" || keyboard == "FRshift" ||
                                keyboard == "GRshift" || keyboard == "HRshift" || keyboard == "IRshift" ||
                                keyboard == "JRshift" || keyboard == "KRshift" || keyboard == "LRshift" ||
                                keyboard == "MRshift" || keyboard == "NRshift" || keyboard == "ORshift" ||
                                keyboard == "PRshift" || keyboard == "QRshift" || keyboard == "RRshift" ||
                                keyboard == "SRshift" || keyboard == "TRshift" || keyboard == "URshift" ||
                                keyboard == "VRshift" || keyboard == "WRshift" || keyboard == "XRshift" ||
                                keyboard == "YRshift" || keyboard == "ZRshift")
                        {
                            tempStr += keyboard[0];
                        }
                        else if (keyboard == "A" || keyboard == "B" || keyboard == "C" ||
                                keyboard == "D" || keyboard == "E" || keyboard == "F" ||
                                keyboard == "G" || keyboard == "H" || keyboard == "I" ||
                                keyboard == "J" || keyboard == "K" || keyboard == "L" ||
                                keyboard == "M" || keyboard == "N" || keyboard == "O" ||
                                keyboard == "P" || keyboard == "Q" || keyboard == "R" ||
                                keyboard == "S" || keyboard == "T" || keyboard == "U" ||
                                keyboard == "V" || keyboard == "W" || keyboard == "X" ||
                                keyboard == "Y" || keyboard == "Z")
                        {
                            tempStr += keyboard.ToLower();
                        }
                        ticker.resetCounter();
                        window.setText("NameTxt", tempStr);
                    }

                    //exits to the main menu if Esc is pressed during the game
                    //exits the program if its pressed in the menus
                    if (keyboardDown.Contains("Escape"))
                    {
                        if (isMenu == false)
                        {
                            assetLoader("Clear");
                            assetLoader("MainMenu");
                            isMenu = true;
                        }
                        else
                            break;
                    }
                    //unpauses the game
                    if (game.getIsPaused() == true)
                    {
                        if (keyboardDown.Contains(game.getPauseKey()))
                        {
                            window.moveEntity("PausedTxt", new Vector2f(-500, -500));
                            game.setIsPaused(false);
                        }
                    }
                    //game logic
                    else if (game.getIsPaused() == false && isMenu == false)
                    {
                        //pauses the game
                        if (keyboardDown.Contains(game.getPauseKey()))
                        {
                            game.setIsPaused(true);
                            tempVector.X = 270;
                            tempVector.Y = 210;
                            window.moveEntity("PausedTxt", tempVector);
                        }

                        //checks for collisions with bullets
                        for (int i = 1; i < 5; i++)
                        {
                            if (game.getBulletIsVisible(i - 1))
                            {
                                tempStr = window.batchIsOverlapping(("BulletSpr" + i), "LargeAsteroidSpr");
                                for (int ii = 1; ii < 13; ii++)
                                {
                                    if (tempStr.Contains("LargeAsteroidSpr" + ii))
                                    {
                                        tempVector.X = game.getLargeAsteroidsPosition(ii - 1).X + 43;
                                        tempVector.Y = game.getLargeAsteroidsPosition(ii - 1).Y + 57;
                                        game.createExplosion(tempVector);
                                        window.goToFrame("ExpositionAni" + game.getExplosionNumber(), 0);
                                        game.destroyBullet(i - 1);
                                        game.destroyLargeAsteroid(ii - 1);
                                        explosion = true;
                                    }
                                }
                                tempStr = window.batchIsOverlapping(("BulletSpr" + i), "MediumAsteroidSpr");
                                for (int ii = 1; ii < 25; ii++)
                                {
                                    if (tempStr.Contains("MediumAsteroidSpr" + ii))
                                    {
                                        tempVector.X = game.getMediumAsteroidsPosition(ii - 1).X + 15;
                                        tempVector.Y = game.getMediumAsteroidsPosition(ii - 1).Y + 23;
                                        game.createExplosion(tempVector);
                                        window.goToFrame("ExpositionAni" + game.getExplosionNumber(), 0);
                                        game.destroyBullet(i - 1);
                                        game.destroyMediumAsteroid(ii - 1);
                                        explosion = true;
                                    }
                                }
                                tempStr = window.batchIsOverlapping(("BulletSpr" + i), "SmallAsteroidSpr");
                                for (int ii = 1; ii < 25; ii++)
                                {
                                    if (tempStr.Contains("SmallAsteroidSpr" + ii))
                                    {
                                        game.createExplosion(game.getSmallAsteroidsPosition(ii - 1));
                                        window.goToFrame("ExpositionAni" + game.getExplosionNumber(), 0);
                                        game.destroyBullet(i - 1);
                                        game.destroySmallAsteroid(ii - 1);
                                        explosion = true;
                                    }
                                }
                                if (window.isOverlapping("BulletSpr" + i, "UFOSpr"))
                                {
                                    game.createExplosion(game.getUfoPosition());
                                    window.goToFrame("ExpositionAni" + game.getExplosionNumber(), 0);
                                    game.destroyBullet(i - 1);
                                    game.destroyUFO();
                                }
                            }
                        }

                        //checks for collisions with the ship
                        tempStr = window.batchIsOverlapping(("ShipSpr"), "LargeAsteroidSpr");
                        if (tempStr.Contains("LargeAsteroidSpr") && game.getShipIsVisible() == true)
                        {
                            tempVector.X = game.getShipPosition().X;
                            tempVector.Y = game.getShipPosition().Y - 8;
                            game.createExplosion(tempVector);
                            window.goToFrame("ExpositionAni" + game.getExplosionNumber(), 0);
                            game.destroyShip();
                            game.destroyBullet(0);
                            game.destroyBullet(1);
                            game.destroyBullet(2);
                            game.destroyBullet(3);
                            explosion = true;
                        }
                        tempStr = window.batchIsOverlapping(("ShipSpr"), "MediumAsteroidSpr");
                        if (tempStr.Contains("MediumAsteroidSpr") && game.getShipIsVisible() == true)
                        {
                            tempVector.X = game.getShipPosition().X;
                            tempVector.Y = game.getShipPosition().Y - 8;
                            game.createExplosion(tempVector);
                            window.goToFrame("ExpositionAni" + game.getExplosionNumber(), 0);
                            game.destroyShip();
                            game.destroyBullet(0);
                            game.destroyBullet(1);
                            game.destroyBullet(2);
                            game.destroyBullet(3);
                            explosion = true;
                        }
                        tempStr = window.batchIsOverlapping(("ShipSpr"), "SmallAsteroidSpr");
                        if (tempStr.Contains("SmallAsteroidSpr") && game.getShipIsVisible() == true)
                        {
                            tempVector.X = game.getShipPosition().X;
                            tempVector.Y = game.getShipPosition().Y - 8;
                            game.createExplosion(tempVector);
                            window.goToFrame("ExpositionAni" + game.getExplosionNumber(), 0);
                            game.destroyShip();
                            game.destroyBullet(0);
                            game.destroyBullet(1);
                            game.destroyBullet(2);
                            game.destroyBullet(3);
                            explosion = true;
                        }
                        if (window.isOverlapping("ShipSpr", "UFOSpr") && game.getShipIsVisible() == true)
                        {
                            tempVector.X = game.getShipPosition().X;
                            tempVector.Y = game.getShipPosition().Y - 8;
                            game.createExplosion(tempVector);
                            window.goToFrame("ExpositionAni" + game.getExplosionNumber(), 0);
                            game.destroyShip();
                            game.destroyBullet(0);
                            game.destroyBullet(1);
                            game.destroyBullet(2);
                            game.destroyBullet(3);
                            explosion = true;
                        }

                        //ends the game if you are out of lives
                        if (game.getShipIsVisible() == false && game.getLives() > 0)
                        {
                            game.shipRespawn();
                        }
                        else if (game.getShipIsVisible() == false && game.getLives() == 0)
                        {
                            ticker.increment();
                        }
                        if (ticker.atAmount(70) && game.getShipIsVisible() == false)
                        {
                            ticker.resetCounter();
                            isMenu = true;
                            assetLoader("Clear");
                            if (game.getScore() > game.getHighScore(0) ||
                                game.getScore() > game.getHighScore(1) ||
                                game.getScore() > game.getHighScore(2) ||
                                game.getScore() > game.getHighScore(3))
                            {
                                assetLoader("EnterHighScore");
                                tempStr = string.Empty;
                                typing = true;
                            }
                            else
                            {
                                assetLoader("MainMenu");
                            }
                        }

                        //updates all entitys
                        game.makeUFO();
                        game.fireBullet(keyboardDown);
                        game.moveShip(keyboard);
                        game.hyperspaceJump(keyboardDown);
                        game.wrapEntitys();
                        game.updatePositions();
                        window.setText("ScoreBoardTxt", "Score: " + game.getScore().ToString());
                        window.setText("LevelTxt", "Level: " + game.getLevel());
                        window.setText("LivesTxt", "Lives: " + game.getLives());

                        //play sounds
                        if (game.getSoundOn() == true)
                        {
                            if (keyboardDown.Contains(game.getFireKey()))
                            {
                                audio.samplePlay("shotSnd");
                            }
                            if (explosion == true)
                            {
                                explosion = false;
                                audio.samplePlay("boomSnd");
                            }
                        }

                        //changes the level
                        if (game.levelEnded())
                        {
                            ticker.increment();
                        }
                        if (game.levelEnded() && ticker.atAmount(30))
                        {
                            ticker.resetCounter();
                            game.nextLevel();
                        }

                        //graphic update
                        window.rotateEntityCenter("ShipSpr", game.getShipAngle());
                        window.moveEntity("ShipSpr", game.getShipPosition());
                        for (int i = 1; i < 49; i++)
                        {
                            if (i < 5)
                            {
                                window.moveEntity(("BulletSpr" + i), game.getBulletPosition(i - 1));
                                window.moveEntity(("ExpositionAni" + i), game.getExplosionPosition(i - 1));
                                window.rotateEntityCenter("BulletSpr" + i, game.getBulletAngle(i - 1));
                            }
                            if (i < 13)
                            {
                                window.moveEntity(("LargeAsteroidSpr" + i), game.getLargeAsteroidsPosition(i - 1));
                            }
                            if (i < 25)
                            {
                                window.moveEntity(("MediumAsteroidSpr" + i), game.getMediumAsteroidsPosition(i - 1));
                            }
                            window.moveEntity(("SmallAsteroidSpr" + i), game.getSmallAsteroidsPosition(i - 1));
                        }
                        window.moveEntity("UFOSpr", game.getUfoPosition());
                    }

                    //destroys expositions if its animation is at the last frame
                    for (int i = 1; i < 5; i++)
                    {
                        if (window.getFrame("ExpositionAni" + i) == 3)
                        {
                            game.destroyExposition(i - 1);
                        }
                    }
                    gameTimer.restartWatch();
                }

                window.drawAll();
            }
        }

        //loads and unloads items into memory
        private void assetLoader(string asset)
        {
            switch (asset)
            {
                case "MainAssets":
                    window.setFont("fonts/arial.ttf");
                    window.addSpriteSheet("sprites/spritemap.png");
                    window.addSpriteMap("backgroundImg", new Vector2f(0,0), 640, 480);
                    window.addSpriteMap("asteroidLargeImg", new Vector2f(801, 0), 122, 149);
                    window.addSpriteMap("asteroidMediumImg", new Vector2f(801, 333), 66, 81);
                    window.addSpriteMap("asteroidSmallImg", new Vector2f(801, 151), 34, 35);
                    window.addSpriteMap("shipImg", new Vector2f(801, 307), 36, 24);
                    window.addSpriteMap("UFOImg", new Vector2f(870, 307), 35, 29);
                    window.addSpriteMap("bulletImg", new Vector2f(849, 214), 14, 6);
                    window.addSpriteMap("expositionImg1", new Vector2f(801, 220), 37, 34);
                    window.addSpriteMap("expositionImg2", new Vector2f(839, 220), 37, 34);
                    window.addSpriteMap("expositionImg3", new Vector2f(877, 220), 37, 34);
                    window.addSpriteMap("expositionImg4", new Vector2f(915, 220), 37, 34);
                    window.addSpriteMap("buttonImg", new Vector2f(801, 255), 150, 50);
                    window.addSpriteMap("smallButtonImg", new Vector2f(800, 415), 150, 50);
                    audio.addSample("boomSnd", "sound/boom2.wav");
                    audio.addSample("shotSnd", "sound/pop1.wav");
                    break;

                case "MainMenu":
                    window.addSprite("BackgroundSpr", "backgroundImg", new Vector2f(0,0));
                    window.addSprite("PlayButtonSpr", "buttonImg", new Vector2f(245, 240));
                    window.addSprite("HighScoreButtonSpr", "buttonImg", new Vector2f(245, 300));
                    window.addSprite("OptionsButtonSpr", "buttonImg", new Vector2f(245, 360));
                    window.addText("TitleTxt", new Vector2f(175,50), Color.White, 70, "Asteroids");
                    window.addText("PlayTxt", new Vector2f(290, 245), Color.White, 30, "Play");
                    window.addText("HighScoresTxt", new Vector2f(251, 310), Color.White, 25, "High Scores");
                    window.addText("SettingsTxt", new Vector2f(267, 365), Color.White, 30, "Settings");
                    break;

                case "HighScore":
                    window.addSprite("BackgroundSpr", "backgroundImg", new Vector2f(0, 0));
                    window.addSprite("MenuButtonSpr", "buttonImg", new Vector2f(245, 360));
                    window.addText("TitleTxt", new Vector2f(180, 50), Color.White, 50, "High Scores");
                    window.addText("BackTxt", new Vector2f(288, 365), Color.White, 30, "Back");
                    window.addText("ScoreTxt1", new Vector2f(200, 150), Color.White, 30, "-");
                    window.addText("ScoreTxt2", new Vector2f(200, 200), Color.White, 30, "-");
                    window.addText("ScoreTxt3", new Vector2f(200, 250), Color.White, 30, "-");
                    window.addText("ScoreTxt4", new Vector2f(200, 300), Color.White, 30, "-");
                    break;

                case "Options":
                    window.addSprite("BackgroundSpr", "backgroundImg", new Vector2f(0, 0));
                    window.addSprite("MenuButtonSpr", "buttonImg", new Vector2f(245, 360));
                    window.addSprite("KeyboardButtonSpr", "buttonImg", new Vector2f(245, 180));
                    window.addSprite("SoundButtonSpr", "buttonImg", new Vector2f(245, 240));
                    window.addSprite("ApplyButtonSpr", "buttonImg", new Vector2f(245, 300));
                    window.addText("TitleTxt", new Vector2f(225, 50), Color.White, 50, "Settings");
                    window.addText("BackTxt", new Vector2f(288, 365), Color.White, 30, "Back");
                    window.addText("ApplyTxt", new Vector2f(282, 305), Color.White, 30, "Apply");
                    window.addText("SoundTxt", new Vector2f(255, 245), Color.White, 29, "Sound On");
                    window.addText("KeyboardTxt", new Vector2f(257, 186), Color.White, 30, "Keyboard");
                    break;

                case "Game":
                    window.addSprite("BackgroundSpr", "backgroundImg", new Vector2f(0, 0));
                    window.addSprite("ShipSpr", "shipImg", new Vector2f(302, 228));
                    window.addSprite("UFOSpr", "UFOImg", new Vector2f(-500, -500));
                    window.batchAddSprite(4, "BulletSpr", "bulletImg", new Vector2f(-500, -500));
                    window.batchAddSprite(12, "LargeAsteroidSpr", "asteroidLargeImg", new Vector2f(-500, -500));
                    window.batchAddSprite(24,"MediumAsteroidSpr", "asteroidMediumImg", new Vector2f(-500, -500));
                    window.batchAddSprite(48, "SmallAsteroidSpr", "asteroidSmallImg", new Vector2f(-500, -500));
                    window.batchAddAnimation(4, "ExpositionAni", new string[] { "expositionImg1", "expositionImg2", "expositionImg3", "expositionImg4" }, new Vector2f(-500, -500), 5);
                    window.addText("ScoreBoardTxt", new Vector2f(10, 0), Color.White, 25, "Score: 0");
                    window.addText("LevelTxt", new Vector2f(280, 0), Color.White, 25, "Level: 1");
                    window.addText("LivesTxt", new Vector2f(540, 0), Color.White, 25, "Lives: 3");
                    window.addText("PausedTxt", new Vector2f(-500, -500), Color.White, 40, "Pause");
                    break;

                case "KeyboardSetup":
                    window.addSprite("BackgroundSpr", "backgroundImg", new Vector2f(0, 0));
                    window.addSprite("OptionsButtonSpr", "buttonImg", new Vector2f(245, 360));
                    window.addSprite("ApplyButtonSpr", "buttonImg", new Vector2f(245, 300));
                    window.addSprite("FireButtonSpr", "buttonImg", new Vector2f(50, 30));
                    window.addSprite("PauseButtonSpr", "buttonImg", new Vector2f(50, 130));
                    window.addSprite("HyperspaceButtonSpr", "buttonImg", new Vector2f(50, 230));
                    window.addSprite("RightButtonSpr", "buttonImg", new Vector2f(350, 30));
                    window.addSprite("LeftButtonSpr", "buttonImg", new Vector2f(350, 130));
                    window.addSprite("AccelerateButtonSpr", "buttonImg", new Vector2f(350, 230));
                    window.addText("FireTxt", new Vector2f(95, 30), Color.White, 40, "Fire");
                    window.addText("PauseTxt", new Vector2f(72, 130), Color.White, 40, "Pause");
                    window.addText("HyperspaceTxt", new Vector2f(56, 237), Color.White, 26, "Hyperspace");
                    window.addText("RightTxt", new Vector2f(380, 30), Color.White, 40, "Right");
                    window.addText("LeftTxt", new Vector2f(390, 130), Color.White, 40, "Left");
                    window.addText("AccelerateTxt", new Vector2f(356, 235), Color.White, 30, "Accelerate");
                    window.addText("FireKeyTxt", new Vector2f(220, 30), Color.Green, 40, "-");
                    window.addText("PauseKeyTxt", new Vector2f(220, 130), Color.Green, 40, "-");
                    window.addText("HyperspaceKeyTxt", new Vector2f(220, 230), Color.Green, 40, "-");
                    window.addText("RightKeyTxt", new Vector2f(520, 30), Color.Green, 40, "-");
                    window.addText("LeftKeyTxt", new Vector2f(520, 130), Color.Green, 40, "-");
                    window.addText("AccelerateKeyTxt", new Vector2f(520, 230), Color.Green, 40, "-");
                    window.addText("BackTxt", new Vector2f(288, 365), Color.White, 30, "Back");
                    window.addText("ApplyTxt", new Vector2f(282, 305), Color.White, 30, "Apply");
                    window.addText("PressKeyTxt", new Vector2f(-500, -500), Color.Yellow, 40, "Press Any Key");
                    break;

                case "EnterHighScore":
                    window.addSprite("BackgroundSpr", "backgroundImg", new Vector2f(0, 0));
                    window.addText("CongratsTxt", new Vector2f(75, 50), Color.White, 70, "Congratulations");
                    window.addText("MadeScoreTxt", new Vector2f(105, 130), Color.White, 40, "You Made A High Score!");
                    window.addText("EnterNameTxt", new Vector2f(170, 180), Color.Yellow, 40, "Enter Your Name");
                    window.addText("NameTxt", new Vector2f(170, 250), Color.Red, 40, "");
                    window.addSprite("EnterButtonSpr", "buttonImg", new Vector2f(245, 360));
                    window.addText("BackTxt", new Vector2f(285, 365), Color.White, 30, "Done");
                    break;

                case "Clear":
                    window.deleteAllEntitys();
                    break;
            }
        }
    }

    class main
    {
        static void Main(string[] args)
        {
            Program game = new Program();
            game.start();
        }
    }
}
