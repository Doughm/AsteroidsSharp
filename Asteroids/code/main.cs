//Asteroids 1.11
//Programer Douglas Harvey-Marose
//
//  Version Changes 1.11
//  updated the used engine to version 1.31
//  complete refactor of the program
//  fixed a few minor graphic and audio bugs
//
//  Version Changes 1.1
//  updated the used engine to version 1.2
//  game speed is now independent from the frame rate 
//
// - Known isues -
//  all collision detection is bounding box, can look odd
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
    class main
    {
        static void Main(string[] args)
        {
            GameTimer testTimer = new GameTimer();
            Window window = new Window();
            Audio audio = new Audio();
            GameTimer gameTimer = new GameTimer();
            AssetLoader assetLoader = new AssetLoader(window, audio);
            Game game = new Game(window, audio, assetLoader);

            assetLoader.start();
            gameTimer.restartWatch();

            while (window.isOpen())
            {
                if (gameTimer.getTimeMilliseconds() >= game.gameSpeed)
                {
                    game.inputUpdate();
                    game.menuUpdate();
                    game.keyboardBind();
                    game.inputString();
                    game.pause();
                    
                    if(game.isPlaying() == true)
                    {
                        game.checkBulletCollisions();
                        game.checkShipCollisions();
                        game.checkEndOfGame();
                        game.entityUpdate();
                        game.changeLevel();
                        game.graphicUpdate();
                        game.explosionLastFrame();
                    }

                    if (game.escape() == true)
                    {
                        break;
                    }

                    gameTimer.restartWatch();
                }

                window.drawAll();
            }
        }
    }
}
