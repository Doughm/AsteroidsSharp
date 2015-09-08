using System;
using SFML.Window;
using SFML.Graphics;

namespace Asteroids
{
    class AssetLoader
    {
        Window window;
        Audio audio;

        public AssetLoader(Window passedWindow, Audio passedAudio)
        {
            window = passedWindow;
            audio = passedAudio;
        }

        //loads in the game assets and sets it to the main menu
        public void start()
        {
            loadAsset("MainAssets");
            loadAsset("MainMenu");
        }

        //loads and unloads items into memory
        public void loadAsset(string asset)
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
                    window.addSpriteMap("explosionImg1", new Vector2f(801, 220), 37, 34);
                    window.addSpriteMap("explosionImg2", new Vector2f(839, 220), 37, 34);
                    window.addSpriteMap("explosionImg3", new Vector2f(877, 220), 37, 34);
                    window.addSpriteMap("explosionImg4", new Vector2f(915, 220), 37, 34);
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
                    window.addText("NameTxt1", new Vector2f(380, 150), Color.White, 30, "-");
		            window.addText("NameTxt2", new Vector2f(380, 200), Color.White, 30, "-");
		            window.addText("NameTxt3", new Vector2f(380, 250), Color.White, 30, "-");
		            window.addText("NameTxt4", new Vector2f(380, 300), Color.White, 30, "-");
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
                    window.batchAddAnimation(4, "ExplosionAni", new string[] { "explosionImg1", "explosionImg2", "explosionImg3", "explosionImg4" }, new Vector2f(-500, -500), 5);
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
}
