//controls all aspects of the window including
//keyboard and mouse input, and displaying graphics

using System;
using SFML.Window;
using SFML.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Asteroids
{
    class Window
    {
        private string programName = "Asteroids";
        private uint resolutionX;
        private uint resolutionY;
        private LoadINI loadINI = new LoadINI("engine.ini");
        private RenderWindow gameWindow;
        private Dictionary<string, View> viewDictionary = new Dictionary<string, View>();
        private RenderStates state = new RenderStates();
        private VertexArray vertexArray = new VertexArray(PrimitiveType.Quads);
        private Dictionary<string, Quad> entityDictionary = new Dictionary<string, Quad>();
        private Dictionary<string, FloatRect> spriteMap = new Dictionary<string, FloatRect>();
        private Dictionary<string, CircleShape> circlesDictionary = new Dictionary<String, CircleShape>();
        private Dictionary<string, Text> textDictionary = new Dictionary<String, Text>();
        private Dictionary<string, Color> colorDictionary = new Dictionary<string, Color>();
        private Dictionary<string, Animation> animationDictionary = new Dictionary<string, Animation>();
        private readonly Color nullColor = new Color();
        private Font font;
        private Color background = Color.White;
        private GameTimer gameTimer = new GameTimer();
        private double updateSpeed;
        private int fps;
        private bool FPSLimit;
        private double currentAngle;
        private double distance;
        private string mouseClick = string.Empty;
        private string keyboardDown = string.Empty;
        private string tempStr = string.Empty;
        private Vertex tempVertex = new Vertex();
        private Vector2f middle = new Vector2f(0, 0);

        public Window()
        {
            //loads in all program parameters from engine.ini
            if (loadINI.fileExists("engine.ini"))
            {
                if (loadINI.inFile("ResolutionWidth"))
                {
                    resolutionX = Convert.ToUInt32(loadINI.getValue("ResolutionWidth"));
                }
                else
                {
                    resolutionX = 640;
                }
                if (loadINI.inFile("ResolutionHeight"))
                {
                    resolutionY = Convert.ToUInt32(loadINI.getValue("ResolutionHeight"));
                }
                else
                {
                    resolutionY = 480;
                }
                if (loadINI.inFile("FPSLimit"))
                {
                    fps = Convert.ToInt32(loadINI.getValue("FPSLimit"));
                }
                else
                {
                    fps = 60;
                }
                if (loadINI.inFile("FPSCapped"))
                {
                    FPSLimit = Convert.ToBoolean(loadINI.getValue("FPSCapped"));
                }
                else
                {
                    FPSLimit = true;
                }
            }
            else
            {
                resolutionX = 640;
                resolutionY = 480;
                fps = 60;
                FPSLimit = true;
            }
            updateSpeed = 1000 / (float)fps;
            state = RenderStates.Default;
            gameWindow = new RenderWindow(new VideoMode(resolutionX, resolutionY), programName, Styles.Close);
            gameWindow.Closed += new EventHandler(OnClose);
            gameTimer.restartWatch();
        }

        //close the window when OnClose event is received
        private void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        //checks if window is still open
        public bool isOpen()
        {
            if (gameWindow.IsOpen())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //returns any keyboard input
        public string inputKeyboard()
        {
            tempStr = string.Empty;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                tempStr += "LetrA";
            if (Keyboard.IsKeyPressed(Keyboard.Key.B))
                tempStr += "LetrB";
            if (Keyboard.IsKeyPressed(Keyboard.Key.C))
                tempStr += "LetrC";
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                tempStr += "LetrD";
            if (Keyboard.IsKeyPressed(Keyboard.Key.E))
                tempStr += "LetrE";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F))
                tempStr += "LetrF";
            if (Keyboard.IsKeyPressed(Keyboard.Key.G))
                tempStr += "LetrG";
            if (Keyboard.IsKeyPressed(Keyboard.Key.H))
                tempStr += "LetrH";
            if (Keyboard.IsKeyPressed(Keyboard.Key.I))
                tempStr += "LetrI";
            if (Keyboard.IsKeyPressed(Keyboard.Key.J))
                tempStr += "LetrJ";
            if (Keyboard.IsKeyPressed(Keyboard.Key.K))
                tempStr += "LetrK";
            if (Keyboard.IsKeyPressed(Keyboard.Key.L))
                tempStr += "LetrL";
            if (Keyboard.IsKeyPressed(Keyboard.Key.M))
                tempStr += "LetrM";
            if (Keyboard.IsKeyPressed(Keyboard.Key.N))
                tempStr += "LetrN";
            if (Keyboard.IsKeyPressed(Keyboard.Key.O))
                tempStr += "LetrO";
            if (Keyboard.IsKeyPressed(Keyboard.Key.P))
                tempStr += "LetrP";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
                tempStr += "LetrQ";
            if (Keyboard.IsKeyPressed(Keyboard.Key.R))
                tempStr += "LetrR";
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                tempStr += "LetrS";
            if (Keyboard.IsKeyPressed(Keyboard.Key.T))
                tempStr += "LetrT";
            if (Keyboard.IsKeyPressed(Keyboard.Key.U))
                tempStr += "LetrU";
            if (Keyboard.IsKeyPressed(Keyboard.Key.V))
                tempStr += "LetrV";
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                tempStr += "LetrW";
            if (Keyboard.IsKeyPressed(Keyboard.Key.X))
                tempStr += "LetrX";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Y))
                tempStr += "LetrY";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Z))
                tempStr += "LetrZ";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num0))
                tempStr += "Num0";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num1))
                tempStr += "Num1";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num2))
                tempStr += "Num2";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num3))
                tempStr += "Num3";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num4))
                tempStr += "Num4";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num5))
                tempStr += "Num5";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num6))
                tempStr += "Num6";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num7))
                tempStr += "Num7";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num8))
                tempStr += "Num8";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num9))
                tempStr += "Num9";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                tempStr += "Escape";
            if (Keyboard.IsKeyPressed(Keyboard.Key.LControl))
                tempStr += "Lctrl";
            if (Keyboard.IsKeyPressed(Keyboard.Key.LShift))
                tempStr += "Lshift";
            if (Keyboard.IsKeyPressed(Keyboard.Key.LAlt))
                tempStr += "Lalt";
            if (Keyboard.IsKeyPressed(Keyboard.Key.RControl))
                tempStr += "Rctrl";
            if (Keyboard.IsKeyPressed(Keyboard.Key.RShift))
                tempStr += "Rshift";
            if (Keyboard.IsKeyPressed(Keyboard.Key.RAlt))
                tempStr += "Ralt";
            if (Keyboard.IsKeyPressed(Keyboard.Key.RSystem))
                tempStr += "Rsystem";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Menu))
                tempStr += "Menu";
            if (Keyboard.IsKeyPressed(Keyboard.Key.LBracket))
                tempStr += "Lbracket";
            if (Keyboard.IsKeyPressed(Keyboard.Key.RBracket))
                tempStr += "Rbracket";
            if (Keyboard.IsKeyPressed(Keyboard.Key.SemiColon))
                tempStr += "SemiColon";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Comma))
                tempStr += "Comma";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Period))
                tempStr += "Period";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Quote))
                tempStr += "Quote";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Slash))
                tempStr += "Slash";
            if (Keyboard.IsKeyPressed(Keyboard.Key.BackSlash))
                tempStr += "Backslash";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Tilde))
                tempStr += "Tilde";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Equal))
                tempStr += "Equal";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Dash))
                tempStr += "Dash";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                tempStr += "Space";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
                tempStr += "Return";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Back))
                tempStr += "Backspace";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Tab))
                tempStr += "Tab";
            if (Keyboard.IsKeyPressed(Keyboard.Key.PageUp))
                tempStr += "Pageup";
            if (Keyboard.IsKeyPressed(Keyboard.Key.End))
                tempStr += "End";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Home))
                tempStr += "home";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Insert))
                tempStr += "Insert";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Delete))
                tempStr += "Delete";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Add))
                tempStr += "Plus";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Subtract))
                tempStr += "Minus";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Multiply))
                tempStr += "Star";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Divide))
                tempStr += "Slash";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                tempStr += "Left";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                tempStr += "Right";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                tempStr += "Up";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                tempStr += "Down";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad0))
                tempStr += "Numpad0";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad1))
                tempStr += "Numpad1";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad2))
                tempStr += "Numpad2";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad3))
                tempStr += "Numpad3";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad4))
                tempStr += "Numpad4";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad5))
                tempStr += "Numpad5";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad6))
                tempStr += "Numpad6";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad7))
                tempStr += "Numpad7";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad8))
                tempStr += "Numpad8";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad9))
                tempStr += "Numpad9";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F1))
                tempStr += "F1";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F2))
                tempStr += "F2";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F3))
                tempStr += "F3";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F4))
                tempStr += "F4";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F5))
                tempStr += "F5";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F6))
                tempStr += "F6";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F7))
                tempStr += "F7";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F8))
                tempStr += "F8";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F9))
                tempStr += "F9";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F10))
                tempStr += "F10";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F11))
                tempStr += "F11";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F12))
                tempStr += "F12";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F13))
                tempStr += "F13";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F14))
                tempStr += "F14";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F15))
                tempStr += "F15";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Pause))
                tempStr += "Pause";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Unknown))
                tempStr += "unknownkey";
            return tempStr;
        }

        //returns one keyboard input
        public string inputKeyboardSingle()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                return "LetrA";
            if (Keyboard.IsKeyPressed(Keyboard.Key.B))
                return "LetrB";
            if (Keyboard.IsKeyPressed(Keyboard.Key.C))
                return "LetrC";
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                return "LetrD";
            if (Keyboard.IsKeyPressed(Keyboard.Key.E))
                return "LetrE";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F))
                return "LetrF";
            if (Keyboard.IsKeyPressed(Keyboard.Key.G))
                return "LetrG";
            if (Keyboard.IsKeyPressed(Keyboard.Key.H))
                return "LetrH";
            if (Keyboard.IsKeyPressed(Keyboard.Key.I))
                return "LetrI";
            if (Keyboard.IsKeyPressed(Keyboard.Key.J))
                return "LetrJ";
            if (Keyboard.IsKeyPressed(Keyboard.Key.K))
                return "LetrK";
            if (Keyboard.IsKeyPressed(Keyboard.Key.L))
                return "LetrL";
            if (Keyboard.IsKeyPressed(Keyboard.Key.M))
                return "LetrM";
            if (Keyboard.IsKeyPressed(Keyboard.Key.N))
                return "LetrN";
            if (Keyboard.IsKeyPressed(Keyboard.Key.O))
                return "LetrO";
            if (Keyboard.IsKeyPressed(Keyboard.Key.P))
                return "LetrP";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
                return "LetrQ";
            if (Keyboard.IsKeyPressed(Keyboard.Key.R))
                return "LetrR";
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                return "LetrS";
            if (Keyboard.IsKeyPressed(Keyboard.Key.T))
                return "LetrT";
            if (Keyboard.IsKeyPressed(Keyboard.Key.U))
                return "LetrU";
            if (Keyboard.IsKeyPressed(Keyboard.Key.V))
                return "LetrV";
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                return "LetrW";
            if (Keyboard.IsKeyPressed(Keyboard.Key.X))
                return "LetrX";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Y))
                return "LetrY";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Z))
                return "LetrZ";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num0))
                return "Num0";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num1))
                return "Num1";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num2))
                return "Num2";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num3))
                return "Num3";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num4))
                return "Num4";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num5))
                return "Num5";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num6))
                return "Num6";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num7))
                return "Num7";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num8))
                return "Num8";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num9))
                return "Num9";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                return "Escape";
            if (Keyboard.IsKeyPressed(Keyboard.Key.LControl))
                return "Lctrl";
            if (Keyboard.IsKeyPressed(Keyboard.Key.LShift))
                return "Lshift";
            if (Keyboard.IsKeyPressed(Keyboard.Key.LAlt))
                return "Lalt";
            if (Keyboard.IsKeyPressed(Keyboard.Key.RControl))
                return "Rctrl";
            if (Keyboard.IsKeyPressed(Keyboard.Key.RShift))
                return "Rshift";
            if (Keyboard.IsKeyPressed(Keyboard.Key.RAlt))
                return "Ralt";
            if (Keyboard.IsKeyPressed(Keyboard.Key.RSystem))
                return "Rsystem";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Menu))
                return "Menu";
            if (Keyboard.IsKeyPressed(Keyboard.Key.LBracket))
                return "Lbracket";
            if (Keyboard.IsKeyPressed(Keyboard.Key.RBracket))
                return "Rbracket";
            if (Keyboard.IsKeyPressed(Keyboard.Key.SemiColon))
                return "SemiColon";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Comma))
                return "Comma";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Period))
                return "Period";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Quote))
                return "Quote";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Slash))
                return "Slash";
            if (Keyboard.IsKeyPressed(Keyboard.Key.BackSlash))
                return "Backslash";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Tilde))
                return "Tilde";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Equal))
                return "Equal";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Dash))
                return "Dash";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                return "Space";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return "Return";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Back))
                return "Backspace";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Tab))
                return "Tab";
            if (Keyboard.IsKeyPressed(Keyboard.Key.PageUp))
                return "Pageup";
            if (Keyboard.IsKeyPressed(Keyboard.Key.End))
                return "End";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Home))
                return "home";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Insert))
                return "Insert";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Delete))
                return "Delete";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Add))
                return "Plus";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Subtract))
                return "Minus";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Multiply))
                return "Star";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Divide))
                return "Slash";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                return "Left";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                return "Right";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                return "Up";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                return "Down";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad0))
                return "Numpad0";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad1))
                return "Numpad1";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad2))
                return "Numpad2";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad3))
                return "Numpad3";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad4))
                return "Numpad4";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad5))
                return "Numpad5";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad6))
                return "Numpad6";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad7))
                return "Numpad7";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad8))
                return "Numpad8";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad9))
                return "Numpad9";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F1))
                return "F1";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F2))
                return "F2";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F3))
                return "F3";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F4))
                return "F4";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F5))
                return "F5";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F6))
                return "F6";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F7))
                return "F7";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F8))
                return "F8";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F9))
                return "F9";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F10))
                return "F10";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F11))
                return "F11";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F12))
                return "F12";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F13))
                return "F13";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F14))
                return "F14";
            if (Keyboard.IsKeyPressed(Keyboard.Key.F15))
                return "F15";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Pause))
                return "Pause";
            if (Keyboard.IsKeyPressed(Keyboard.Key.Unknown))
                return "unknownkey";
            return "";
        }

        //returns any mouse input
        public string inputMouse()
        {
            tempStr = string.Empty;
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
                tempStr += "Leftbutton";
            if (Mouse.IsButtonPressed(Mouse.Button.Right))
                tempStr += "Rightbutton";
            if (Mouse.IsButtonPressed(Mouse.Button.Middle))
                tempStr += "Middlebutton";
            if (Mouse.IsButtonPressed(Mouse.Button.XButton1))
                tempStr += "Button3";
            if (Mouse.IsButtonPressed(Mouse.Button.XButton2))
                tempStr += "Button4";
            return tempStr;
        }

        //returns one mouse input
        public string inputMouseSingle()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
                return "Leftbutton";
            if (Mouse.IsButtonPressed(Mouse.Button.Right))
                return "Rightbutton";
            if (Mouse.IsButtonPressed(Mouse.Button.Middle))
                return "Middlebutton";
            if (Mouse.IsButtonPressed(Mouse.Button.XButton1))
                return "Button3";
            if (Mouse.IsButtonPressed(Mouse.Button.XButton2))
                return "Button4";
            return "";
        }

        //returns any keyboard input once if a key is down
        public string inputKeyboardDown()
        {
            tempStr = inputKeyboard();
            if (tempStr != keyboardDown)
            {
                keyboardDown = tempStr;
                return tempStr;
            }
            return "";
        }

        //returns one keyboard input once if a key is down
        public string inputKeyboardDownSingle()
        {
            tempStr = inputKeyboardSingle();
            if (tempStr != keyboardDown)
            {
                keyboardDown = tempStr;
                return tempStr;
            }
            return "";
        }

        //returns any mouse input once if a button is down
        public string inputMouseClick()
        {
            tempStr = inputMouse();
            if (tempStr != mouseClick)
            {
                mouseClick = tempStr;
                return tempStr;
            }
            return "";
        }

        //returns any mouse input once if a button is down
        public string inputMouseClickSingle()
        {
            tempStr = inputMouseSingle();
            if (tempStr != mouseClick)
            {
                mouseClick = tempStr;
                return tempStr;
            }
            return "";
        }

        //gets the position of the mouse in screen coordinates
        public Vector2i mousePositionRaw()
        {
            return Mouse.GetPosition();
        }

        //gets the position of the mouse relative to the window
        public Vector2i mousePositionWindow()
        {
            return Mouse.GetPosition(gameWindow);
        }

        //gets the position of the mouse within the view
        public Vector2f mousePositionView()
        {
            tempVertex.Position.X = Mouse.GetPosition(gameWindow).X;
            tempVertex.Position.Y = Mouse.GetPosition(gameWindow).Y;
            return tempVertex.Position;
        }

        //takes a quad and copies it into the vertex array
        private void copyQuad(Quad quad)
        {
            vertexArray.Append(quad.vertex1);
            vertexArray.Append(quad.vertex2);
            vertexArray.Append(quad.vertex3);
            vertexArray.Append(quad.vertex4);
        }

        //takes a quad and copies it into the vertex array
        private void copyQuad(Quad quad, uint place)
        {
            vertexArray[place] = quad.vertex1;
            vertexArray[place + 1] = quad.vertex2;
            vertexArray[place + 2] = quad.vertex3;
            vertexArray[place + 3] = quad.vertex4;
        }

        //adds a sprite sheet image into the sprite list
        public void addSpriteSheet(string spriteSheet)
        {
            state.Texture = new Texture(spriteSheet);
        }

        //adds an image definition to the sprite map
        public void addSpriteMap(string name, Vector2f pointOfOrigin, int width, int height)
        {
            spriteMap.Add(name, new FloatRect(pointOfOrigin.X, pointOfOrigin.Y, width, height));
        }

        //adds a quad entity
        public void addQuad(string name, Vertex point1, Vertex point2, Vertex point3, Vertex point4)
        {
            entityDictionary.Add(name, new Quad(point1, point2, point3, point4));
            copyQuad(entityDictionary[name]);
            entityDictionary[name].firstPosition = vertexArray.VertexCount - 4;
        }

        //adds a sprite entity
        public void addSprite(string name, string imageName, Vector2f pointOfOrigin)
        {
            if (spriteMap.ContainsKey(imageName))
            {
                entityDictionary.Add(name, new Quad(
                new Vertex(
                    pointOfOrigin,
                    new Vector2f(spriteMap[imageName].Left, spriteMap[imageName].Top)),
                new Vertex
                    (new Vector2f(pointOfOrigin.X + spriteMap[imageName].Width, pointOfOrigin.Y),
                    new Vector2f(spriteMap[imageName].Left + spriteMap[imageName].Width, spriteMap[imageName].Top)),
                new Vertex
                    (new Vector2f(pointOfOrigin.X + spriteMap[imageName].Width, pointOfOrigin.Y + spriteMap[imageName].Height),
                    new Vector2f(spriteMap[imageName].Left + spriteMap[imageName].Width, spriteMap[imageName].Top + spriteMap[imageName].Height)),
                new Vertex
                    (new Vector2f(pointOfOrigin.X, pointOfOrigin.Y + spriteMap[imageName].Height),
                    new Vector2f(spriteMap[imageName].Left, spriteMap[imageName].Top + spriteMap[imageName].Height))));
                copyQuad(entityDictionary[name]);
                entityDictionary[name].firstPosition = vertexArray.VertexCount - 4;
            }
        }

        //adds a rectangle entity
        public void addRectangle(string name, Vector2f pointOfOrigin, int width, int height, Color color)
        {
            entityDictionary.Add(name, new Quad(
            new Vertex(pointOfOrigin, color),
            new Vertex(new Vector2f(pointOfOrigin.X + width, pointOfOrigin.Y), color),
            new Vertex(new Vector2f(pointOfOrigin.X + width, pointOfOrigin.Y + height), color),
            new Vertex(new Vector2f(pointOfOrigin.X, pointOfOrigin.Y + height), color)));
            copyQuad(entityDictionary[name]);
            entityDictionary[name].firstPosition = vertexArray.VertexCount - 4;
        }

        //adds a rectangle entity
        public void addRectangle(string name, Vector2f pointOfOrigin, int width, int height, Color color1, Color color2, Color color3, Color color4)
        {
            entityDictionary.Add(name, new Quad(
            new Vertex(pointOfOrigin, color1),
            new Vertex(new Vector2f(pointOfOrigin.X + width, pointOfOrigin.Y), color2),
            new Vertex(new Vector2f(pointOfOrigin.X + width, pointOfOrigin.Y + height), color3),
            new Vertex(new Vector2f(pointOfOrigin.X, pointOfOrigin.Y + height), color4)));
            copyQuad(entityDictionary[name]);
            entityDictionary[name].firstPosition = vertexArray.VertexCount - 4;
        }

        //adds a text entity
        public void addText(string name, Vector2f position, Color color, uint size, string text)
        {
            textDictionary.Add(name, new Text(text, font, size));
            textDictionary[name].Color = color;
            textDictionary[name].Position = position;
            colorDictionary.Add(name, color);
        }

        //adds a circle entity
        public void addCircle(string name, Vector2f position, Color color, int radius)
        {
            circlesDictionary.Add(name, new CircleShape(radius));
            circlesDictionary[name].FillColor = color;
            circlesDictionary[name].Position = position;
            colorDictionary.Add(name, color);
        }

        //adds a animation entity made up of multiple sprite map images.
        //the size is set by the first frame, and the speed is 0-any
        public void addAnimation(string name, string[] frames, Vector2f pointOfOrigin, int speed)
        {
            //stops the function if an invalid spritemap is detected
            for (int i = 0; i < frames.Length; i++)
            {
                if (spriteMap.ContainsKey(frames[i]) || speed < 0) { }
                else
                {
                    return;
                }
            }
            animationDictionary.Add(name, new Animation(frames, new Quad(
                new Vertex(
                    pointOfOrigin,
                    new Vector2f(spriteMap[frames[0]].Left, spriteMap[frames[0]].Top)),
                new Vertex
                    (new Vector2f(pointOfOrigin.X + spriteMap[frames[0]].Width, pointOfOrigin.Y),
                    new Vector2f(spriteMap[frames[0]].Left + spriteMap[frames[0]].Width, spriteMap[frames[0]].Top)),
                new Vertex
                    (new Vector2f(pointOfOrigin.X + spriteMap[frames[0]].Width, pointOfOrigin.Y + spriteMap[frames[0]].Height),
                    new Vector2f(spriteMap[frames[0]].Left + spriteMap[frames[0]].Width, spriteMap[frames[0]].Top + spriteMap[frames[0]].Height)),
                new Vertex
                    (new Vector2f(pointOfOrigin.X, pointOfOrigin.Y + spriteMap[frames[0]].Height),
                    new Vector2f(spriteMap[frames[0]].Left, spriteMap[frames[0]].Top + spriteMap[frames[0]].Height))), speed));
            copyQuad(animationDictionary[name].positions);
            animationDictionary[name].positions.firstPosition = vertexArray.VertexCount - 4;
        }

        //adds X number of entitys of type quad
        public void batchAddQuad(int numberToMake, string name, Vertex point1, Vertex point2, Vertex point3, Vertex point4)
        {
            for (int i = 0; i < numberToMake; i++)
            {
                addQuad(name + (i + 1), point1, point2, point3, point4);
            }
        }

        //adds X number of entitys of type sprite
        public void batchAddSprite(int numberToMake, string name, string imageName, Vector2f pointOfOrigin)
        {
            for (int i = 0; i < numberToMake; i++)
            {
                addSprite(name + (i + 1), imageName, pointOfOrigin);
            }
        }

        //adds X number of entitys of type rectangle
        public void batchAddRectangle(int numberToMake, string name, Vector2f pointOfOrigin, int height, int width, Color color)
        {
            for (int i = 0; i < numberToMake; i++)
            {
                addRectangle(name + (i + 1), pointOfOrigin, height, width, color);
            }
        }

        //adds X number of entitys of type rectangle
        public void batchAddRectangle(int numberToMake, string name, Vector2f pointOfOrigin, int height, int width, Color color1, Color color2, Color color3, Color color4)
        {
            for (int i = 0; i < numberToMake; i++)
            {
                addRectangle(name + (i + 1), pointOfOrigin, height, width, color1, color2, color3, color4);
            }
        }

        //adds X number of entitys of type text
        public void batchAddText(int numberToMake, string name, Vector2f position, Color color, uint size, string text)
        {
            for (int i = 0; i < numberToMake; i++)
            {
                addText(name + (i + 1), position, color, size, text);
            }
        }

        //adds X number of entitys of type circle
        public void batchAddCircle(int numberToMake, string name, Vector2f position, Color color, int radius)
        {
            for (int i = 0; i < numberToMake; i++)
            {
                addCircle(name + (i + 1), position, color, radius);
            }
        }

        //adds X number of entitys of type animation
        public void batchAddAnimation(int numberToMake, string name, string[] frames, Vector2f pointOfOrigin, int speed)
        {
            for (int i = 0; i < numberToMake; i++)
            {
                addAnimation(name + (i + 1), frames, pointOfOrigin, speed);
            }
        }

        //returns the number in the name of a batch entity [-1 is error]
        public int batchNumber(string name)
        {
            if (entityDictionary.ContainsKey(name) || animationDictionary.ContainsKey(name) ||
                circlesDictionary.ContainsKey(name) || textDictionary.ContainsKey(name))
            {
                return Int32.Parse(Regex.Match(name, @"\d+").Value);
            }

            return -1;
        }

        //deletes one entity from the object
        public void deleteEntity(string name)
        {
            if (circlesDictionary.ContainsKey(name))
            {
                circlesDictionary.Remove(name);
            }
            else if (textDictionary.ContainsKey(name))
            {
                textDictionary.Remove(name);
            }
            else if (entityDictionary.ContainsKey(name))
            {
                vertexArray[entityDictionary[name].firstPosition] = new Vertex();
                vertexArray[entityDictionary[name].firstPosition + 1] = new Vertex();
                vertexArray[entityDictionary[name].firstPosition + 2] = new Vertex();
                vertexArray[entityDictionary[name].firstPosition + 3] = new Vertex();
                entityDictionary.Remove(name);
            }
            else if (animationDictionary.ContainsKey(name))
            {
                vertexArray[animationDictionary[name].positions.firstPosition] = new Vertex();
                vertexArray[animationDictionary[name].positions.firstPosition + 1] = new Vertex();
                vertexArray[animationDictionary[name].positions.firstPosition + 2] = new Vertex();
                vertexArray[animationDictionary[name].positions.firstPosition + 3] = new Vertex();
                animationDictionary.Remove(name);
            }
        }

        //deletes all entitys from the object
        public void deleteAllEntitys()
        {
            vertexArray.Clear();
            entityDictionary.Clear();
            circlesDictionary.Clear();
            textDictionary.Clear();
            colorDictionary.Clear();
            animationDictionary.Clear();
        }

        //deletes everything
        public void deleteAll()
        {
            vertexArray.Clear();
            entityDictionary.Clear();
            circlesDictionary.Clear();
            textDictionary.Clear();
            colorDictionary.Clear();
            animationDictionary.Clear();
            font = new Font("null");
            viewDictionary.Clear();
            state = RenderStates.Default;
            spriteMap.Clear();
            background = nullColor;
            mouseClick = string.Empty;
            keyboardDown = string.Empty;
        }

        //finds if a specific entity exists
        public bool entityExists(string name)
        {
            if (circlesDictionary.ContainsKey(name) ||
                textDictionary.ContainsKey(name) ||
                entityDictionary.ContainsKey(name) ||
                animationDictionary.ContainsKey(name))
            {
                return true;
            }
            return false;
        }

        //moves an entitys position
        public void moveEntity(string name, Vector2f position)
        {
            if (circlesDictionary.ContainsKey(name))
            {
                circlesDictionary[name].Position = position;
            }
            else if (textDictionary.ContainsKey(name))
            {
                textDictionary[name].Position = position;
            }
            else if (entityDictionary.ContainsKey(name))
            {
                position.X = entityDictionary[name].vertex1.Position.X - position.X;
                position.Y = entityDictionary[name].vertex1.Position.Y - position.Y;

                //point 1
                tempVertex = vertexArray[entityDictionary[name].firstPosition];
                tempVertex.Position.X -= position.X;
                tempVertex.Position.Y -= position.Y;
                vertexArray[entityDictionary[name].firstPosition] = tempVertex;
                entityDictionary[name].vertex1.Position.X -= position.X;
                entityDictionary[name].vertex1.Position.Y -= position.Y;
                //point 2
                tempVertex = vertexArray[entityDictionary[name].firstPosition + 1];
                tempVertex.Position.X -= position.X;
                tempVertex.Position.Y -= position.Y;
                vertexArray[entityDictionary[name].firstPosition + 1] = tempVertex;
                entityDictionary[name].vertex2.Position.X -= position.X;
                entityDictionary[name].vertex2.Position.Y -= position.Y;
                //point 3
                tempVertex = vertexArray[entityDictionary[name].firstPosition + 2];
                tempVertex.Position.X -= position.X;
                tempVertex.Position.Y -= position.Y;
                vertexArray[entityDictionary[name].firstPosition + 2] = tempVertex;
                entityDictionary[name].vertex3.Position.X -= position.X;
                entityDictionary[name].vertex3.Position.Y -= position.Y;
                //point 4
                tempVertex = vertexArray[entityDictionary[name].firstPosition + 3];
                tempVertex.Position.X -= position.X;
                tempVertex.Position.Y -= position.Y;
                vertexArray[entityDictionary[name].firstPosition + 3] = tempVertex;
                entityDictionary[name].vertex4.Position.X -= position.X;
                entityDictionary[name].vertex4.Position.Y -= position.Y;
            }
            else if (animationDictionary.ContainsKey(name))
            {
                position.X = animationDictionary[name].positions.vertex1.Position.X - position.X;
                position.Y = animationDictionary[name].positions.vertex1.Position.Y - position.Y;

                //point 1
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition];
                tempVertex.Position.X -= position.X;
                tempVertex.Position.Y -= position.Y;
                vertexArray[animationDictionary[name].positions.firstPosition] = tempVertex;
                animationDictionary[name].positions.vertex1.Position.X -= position.X;
                animationDictionary[name].positions.vertex1.Position.Y -= position.Y;
                //point 2
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 1];
                tempVertex.Position.X -= position.X;
                tempVertex.Position.Y -= position.Y;
                vertexArray[animationDictionary[name].positions.firstPosition + 1] = tempVertex;
                animationDictionary[name].positions.vertex2.Position.X -= position.X;
                animationDictionary[name].positions.vertex2.Position.Y -= position.Y;
                //point 3
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 2];
                tempVertex.Position.X -= position.X;
                tempVertex.Position.Y -= position.Y;
                vertexArray[animationDictionary[name].positions.firstPosition + 2] = tempVertex;
                animationDictionary[name].positions.vertex3.Position.X -= position.X;
                animationDictionary[name].positions.vertex3.Position.Y -= position.Y;
                //point 4
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 3];
                tempVertex.Position.X -= position.X;
                tempVertex.Position.Y -= position.Y;
                vertexArray[animationDictionary[name].positions.firstPosition + 3] = tempVertex;
                animationDictionary[name].positions.vertex4.Position.X -= position.X;
                animationDictionary[name].positions.vertex4.Position.Y -= position.Y;
            }
        }

        //scales an entity (100% size is 1)
        public void scaleEntity(string name, float scaler)
        {
            if (circlesDictionary.ContainsKey(name))
            {
                circlesDictionary[name].Scale = new Vector2f(scaler, scaler);
            }
            else if (textDictionary.ContainsKey(name))
            {
                textDictionary[name].Scale = new Vector2f(scaler, scaler); ;
            }
            else if (entityDictionary.ContainsKey(name))
            {
                //vertex 2
                tempVertex = vertexArray[entityDictionary[name].firstPosition + 1];
                tempVertex.Position.X = (vertexArray[entityDictionary[name].firstPosition + 1].Position.X - vertexArray[entityDictionary[name].firstPosition].Position.X)
                    * scaler + vertexArray[entityDictionary[name].firstPosition].Position.X;
                tempVertex.Position.Y = (vertexArray[entityDictionary[name].firstPosition + 1].Position.Y - vertexArray[entityDictionary[name].firstPosition].Position.Y)
                    * scaler + vertexArray[entityDictionary[name].firstPosition].Position.Y;
                vertexArray[entityDictionary[name].firstPosition + 1] = tempVertex;
                //vertex 3
                tempVertex = vertexArray[entityDictionary[name].firstPosition + 2];
                tempVertex.Position.X = (vertexArray[entityDictionary[name].firstPosition + 2].Position.X - vertexArray[entityDictionary[name].firstPosition].Position.X)
                    * scaler + vertexArray[entityDictionary[name].firstPosition].Position.X;
                tempVertex.Position.Y = (vertexArray[entityDictionary[name].firstPosition + 2].Position.Y - vertexArray[entityDictionary[name].firstPosition].Position.Y)
                    * scaler + vertexArray[entityDictionary[name].firstPosition].Position.Y;
                vertexArray[entityDictionary[name].firstPosition + 2] = tempVertex;
                //vertex 4
                tempVertex = vertexArray[entityDictionary[name].firstPosition + 3];
                tempVertex.Position.X = (vertexArray[entityDictionary[name].firstPosition + 3].Position.X - vertexArray[entityDictionary[name].firstPosition].Position.X)
                    * scaler + vertexArray[entityDictionary[name].firstPosition].Position.X;
                tempVertex.Position.Y = (vertexArray[entityDictionary[name].firstPosition + 3].Position.Y - vertexArray[entityDictionary[name].firstPosition].Position.Y)
                    * scaler + vertexArray[entityDictionary[name].firstPosition].Position.Y;
                vertexArray[entityDictionary[name].firstPosition + 3] = tempVertex;
                entityDictionary[name].scale.X = scaler;
                entityDictionary[name].scale.Y = scaler;
            }
            else if (animationDictionary.ContainsKey(name))
            {
                //vertex 2
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 1];
                tempVertex.Position.X = (vertexArray[animationDictionary[name].positions.firstPosition + 1].Position.X - vertexArray[animationDictionary[name].positions.firstPosition].Position.X)
                    * scaler + vertexArray[animationDictionary[name].positions.firstPosition].Position.X;
                tempVertex.Position.Y = (vertexArray[animationDictionary[name].positions.firstPosition + 1].Position.Y - vertexArray[animationDictionary[name].positions.firstPosition].Position.Y)
                    * scaler + vertexArray[animationDictionary[name].positions.firstPosition].Position.Y;
                vertexArray[animationDictionary[name].positions.firstPosition + 1] = tempVertex;
                //vertex 3
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 2];
                tempVertex.Position.X = (vertexArray[animationDictionary[name].positions.firstPosition + 2].Position.X - vertexArray[animationDictionary[name].positions.firstPosition].Position.X)
                    * scaler + vertexArray[animationDictionary[name].positions.firstPosition].Position.X;
                tempVertex.Position.Y = (vertexArray[animationDictionary[name].positions.firstPosition + 2].Position.Y - vertexArray[animationDictionary[name].positions.firstPosition].Position.Y)
                    * scaler + vertexArray[animationDictionary[name].positions.firstPosition].Position.Y;
                vertexArray[animationDictionary[name].positions.firstPosition + 2] = tempVertex;
                //vertex 4
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 3];
                tempVertex.Position.X = (vertexArray[animationDictionary[name].positions.firstPosition + 3].Position.X - vertexArray[animationDictionary[name].positions.firstPosition].Position.X)
                    * scaler + vertexArray[animationDictionary[name].positions.firstPosition].Position.X;
                tempVertex.Position.Y = (vertexArray[animationDictionary[name].positions.firstPosition + 3].Position.Y - vertexArray[animationDictionary[name].positions.firstPosition].Position.Y)
                    * scaler + vertexArray[animationDictionary[name].positions.firstPosition].Position.Y;
                vertexArray[animationDictionary[name].positions.firstPosition + 3] = tempVertex;
                animationDictionary[name].positions.scale.X = scaler;
                animationDictionary[name].positions.scale.Y = scaler;
            }
        }

        //scales an entity (100% size is 1)
        public void scaleEntity(string name, Vector2f scaler)
        {
            if (circlesDictionary.ContainsKey(name))
            {
                circlesDictionary[name].Scale = scaler;
            }
            else if (textDictionary.ContainsKey(name))
            {
                textDictionary[name].Scale = scaler;
            }
            else if (entityDictionary.ContainsKey(name))
            {
                //vertex 2
                tempVertex = vertexArray[entityDictionary[name].firstPosition + 1];
                tempVertex.Position.X = (vertexArray[entityDictionary[name].firstPosition + 1].Position.X - vertexArray[entityDictionary[name].firstPosition].Position.X)
                    * scaler.X + vertexArray[entityDictionary[name].firstPosition].Position.X;
                tempVertex.Position.Y = (vertexArray[entityDictionary[name].firstPosition + 1].Position.Y - vertexArray[entityDictionary[name].firstPosition].Position.Y)
                    * scaler.Y + vertexArray[entityDictionary[name].firstPosition].Position.Y;
                vertexArray[entityDictionary[name].firstPosition + 1] = tempVertex;
                //vertex 3
                tempVertex = vertexArray[entityDictionary[name].firstPosition + 2];
                tempVertex.Position.X = (vertexArray[entityDictionary[name].firstPosition + 2].Position.X - vertexArray[entityDictionary[name].firstPosition].Position.X)
                    * scaler.X + vertexArray[entityDictionary[name].firstPosition].Position.X;
                tempVertex.Position.Y = (vertexArray[entityDictionary[name].firstPosition + 2].Position.Y - vertexArray[entityDictionary[name].firstPosition].Position.Y)
                    * scaler.Y + vertexArray[entityDictionary[name].firstPosition].Position.Y;
                vertexArray[entityDictionary[name].firstPosition + 2] = tempVertex;
                //vertex 4
                tempVertex = vertexArray[entityDictionary[name].firstPosition + 3];
                tempVertex.Position.X = (vertexArray[entityDictionary[name].firstPosition + 3].Position.X - vertexArray[entityDictionary[name].firstPosition].Position.X)
                    * scaler.X + vertexArray[entityDictionary[name].firstPosition].Position.X;
                tempVertex.Position.Y = (vertexArray[entityDictionary[name].firstPosition + 3].Position.Y - vertexArray[entityDictionary[name].firstPosition].Position.Y)
                    * scaler.Y + vertexArray[entityDictionary[name].firstPosition].Position.Y;
                vertexArray[entityDictionary[name].firstPosition + 3] = tempVertex;
                entityDictionary[name].scale = scaler;
            }
            else if (animationDictionary.ContainsKey(name))
            {
                //vertex 2
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 1];
                tempVertex.Position.X = (vertexArray[animationDictionary[name].positions.firstPosition + 1].Position.X - vertexArray[animationDictionary[name].positions.firstPosition].Position.X)
                    * scaler.X + vertexArray[animationDictionary[name].positions.firstPosition].Position.X;
                tempVertex.Position.Y = (vertexArray[animationDictionary[name].positions.firstPosition + 1].Position.Y - vertexArray[animationDictionary[name].positions.firstPosition].Position.Y)
                    * scaler.Y + vertexArray[animationDictionary[name].positions.firstPosition].Position.Y;
                vertexArray[animationDictionary[name].positions.firstPosition + 1] = tempVertex;
                //vertex 3
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 2];
                tempVertex.Position.X = (vertexArray[animationDictionary[name].positions.firstPosition + 2].Position.X - vertexArray[animationDictionary[name].positions.firstPosition].Position.X)
                    * scaler.X + vertexArray[animationDictionary[name].positions.firstPosition].Position.X;
                tempVertex.Position.Y = (vertexArray[animationDictionary[name].positions.firstPosition + 2].Position.Y - vertexArray[animationDictionary[name].positions.firstPosition].Position.Y)
                    * scaler.Y + vertexArray[animationDictionary[name].positions.firstPosition].Position.Y;
                vertexArray[animationDictionary[name].positions.firstPosition + 2] = tempVertex;
                //vertex 4
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 3];
                tempVertex.Position.X = (vertexArray[animationDictionary[name].positions.firstPosition + 3].Position.X - vertexArray[animationDictionary[name].positions.firstPosition].Position.X)
                    * scaler.X + vertexArray[animationDictionary[name].positions.firstPosition].Position.X;
                tempVertex.Position.Y = (vertexArray[animationDictionary[name].positions.firstPosition + 3].Position.Y - vertexArray[animationDictionary[name].positions.firstPosition].Position.Y)
                    * scaler.Y + vertexArray[animationDictionary[name].positions.firstPosition].Position.Y;
                vertexArray[animationDictionary[name].positions.firstPosition + 3] = tempVertex;
                animationDictionary[name].positions.scale = scaler;
            }
        }

        //rotates an entity around its top left point
        public void rotateEntity(string name, float degrees)
        {
            if (textDictionary.ContainsKey(name))
            {
                textDictionary[name].Rotation = degrees;
            }
            else if (circlesDictionary.ContainsKey(name))
            {
                circlesDictionary[name].Rotation = degrees;
            }
            else if (entityDictionary.ContainsKey(name))
            {
                vertexArray[entityDictionary[name].firstPosition] = entityDictionary[name].vertex1;

                tempVertex = entityDictionary[name].vertex2;
                tempVertex.Position.X = entityDictionary[name].vertex2.Position.X - entityDictionary[name].vertex1.Position.X;
                tempVertex.Position.Y = entityDictionary[name].vertex2.Position.Y - entityDictionary[name].vertex1.Position.Y;
                currentAngle = Math.Atan2(tempVertex.Position.Y, tempVertex.Position.X) * (180 / Math.PI);
                distance = Math.Sqrt((tempVertex.Position.X * tempVertex.Position.X) + (tempVertex.Position.Y * tempVertex.Position.Y));
                entityDictionary[name].rotation = currentAngle - degrees;
                tempVertex.Position.X = entityDictionary[name].vertex1.Position.X + (float)Math.Cos(Math.PI * entityDictionary[name].rotation / 180.0) * (float)distance;
                tempVertex.Position.Y = entityDictionary[name].vertex1.Position.Y - (float)Math.Sin(Math.PI * entityDictionary[name].rotation / 180.0) * (float)distance;
                vertexArray[entityDictionary[name].firstPosition + 1] = tempVertex;

                tempVertex = entityDictionary[name].vertex3;
                tempVertex.Position.X = entityDictionary[name].vertex3.Position.X - entityDictionary[name].vertex1.Position.X;
                tempVertex.Position.Y = entityDictionary[name].vertex3.Position.Y - entityDictionary[name].vertex1.Position.Y;
                currentAngle = Math.Atan2(tempVertex.Position.Y, tempVertex.Position.X) * (180 / Math.PI);
                distance = Math.Sqrt((tempVertex.Position.X * tempVertex.Position.X) + (tempVertex.Position.Y * tempVertex.Position.Y));
                entityDictionary[name].rotation = currentAngle - degrees - (currentAngle * 2);
                tempVertex.Position.X = entityDictionary[name].vertex1.Position.X + (float)Math.Cos(Math.PI * entityDictionary[name].rotation / 180.0) * (float)distance;
                tempVertex.Position.Y = entityDictionary[name].vertex1.Position.Y - (float)Math.Sin(Math.PI * entityDictionary[name].rotation / 180.0) * (float)distance;
                vertexArray[entityDictionary[name].firstPosition + 2] = tempVertex;

                tempVertex = entityDictionary[name].vertex4;
                tempVertex.Position.X = entityDictionary[name].vertex4.Position.X - entityDictionary[name].vertex1.Position.X;
                tempVertex.Position.Y = entityDictionary[name].vertex4.Position.Y - entityDictionary[name].vertex1.Position.Y;
                currentAngle = Math.Atan2(tempVertex.Position.Y, tempVertex.Position.X) * (180 / Math.PI);
                distance = Math.Sqrt((tempVertex.Position.X * tempVertex.Position.X) + (tempVertex.Position.Y * tempVertex.Position.Y));
                entityDictionary[name].rotation = currentAngle - degrees - (currentAngle * 2);
                tempVertex.Position.X = entityDictionary[name].vertex1.Position.X + (float)Math.Cos(Math.PI * entityDictionary[name].rotation / 180.0) * (float)distance;
                tempVertex.Position.Y = entityDictionary[name].vertex1.Position.Y - (float)Math.Sin(Math.PI * entityDictionary[name].rotation / 180.0) * (float)distance;
                vertexArray[entityDictionary[name].firstPosition + 3] = tempVertex;

                if (degrees >= 360 || degrees <= -360)
                {
                    degrees = degrees % 360;
                }
                if (degrees < 0)
                {
                    degrees = degrees + 360;
                }

                entityDictionary[name].rotation = degrees;

                if (entityDictionary[name].scale.X != 0 || entityDictionary[name].scale.Y != 0)
                {
                    scaleEntity(name, entityDictionary[name].scale);
                }
            }
            else if (animationDictionary.ContainsKey(name))
            {
                vertexArray[animationDictionary[name].positions.firstPosition] = animationDictionary[name].positions.vertex1;

                tempVertex = animationDictionary[name].positions.vertex2;
                tempVertex.Position.X = animationDictionary[name].positions.vertex2.Position.X - animationDictionary[name].positions.vertex1.Position.X;
                tempVertex.Position.Y = animationDictionary[name].positions.vertex2.Position.Y - animationDictionary[name].positions.vertex1.Position.Y;
                currentAngle = Math.Atan2(tempVertex.Position.Y, tempVertex.Position.X) * (180 / Math.PI);
                distance = Math.Sqrt((tempVertex.Position.X * tempVertex.Position.X) + (tempVertex.Position.Y * tempVertex.Position.Y));
                animationDictionary[name].positions.rotation = currentAngle - degrees;
                tempVertex.Position.X = animationDictionary[name].positions.vertex1.Position.X + (float)Math.Cos(Math.PI * animationDictionary[name].positions.rotation / 180.0) * (float)distance;
                tempVertex.Position.Y = animationDictionary[name].positions.vertex1.Position.Y - (float)Math.Sin(Math.PI * animationDictionary[name].positions.rotation / 180.0) * (float)distance;
                vertexArray[animationDictionary[name].positions.firstPosition + 1] = tempVertex;

                tempVertex = animationDictionary[name].positions.vertex3;
                tempVertex.Position.X = animationDictionary[name].positions.vertex3.Position.X - animationDictionary[name].positions.vertex1.Position.X;
                tempVertex.Position.Y = animationDictionary[name].positions.vertex3.Position.Y - animationDictionary[name].positions.vertex1.Position.Y;
                currentAngle = Math.Atan2(tempVertex.Position.Y, tempVertex.Position.X) * (180 / Math.PI);
                distance = Math.Sqrt((tempVertex.Position.X * tempVertex.Position.X) + (tempVertex.Position.Y * tempVertex.Position.Y));
                animationDictionary[name].positions.rotation = currentAngle - degrees - (currentAngle * 2);
                tempVertex.Position.X = animationDictionary[name].positions.vertex1.Position.X + (float)Math.Cos(Math.PI * animationDictionary[name].positions.rotation / 180.0) * (float)distance;
                tempVertex.Position.Y = animationDictionary[name].positions.vertex1.Position.Y - (float)Math.Sin(Math.PI * animationDictionary[name].positions.rotation / 180.0) * (float)distance;
                vertexArray[animationDictionary[name].positions.firstPosition + 2] = tempVertex;

                tempVertex = animationDictionary[name].positions.vertex4;
                tempVertex.Position.X = animationDictionary[name].positions.vertex4.Position.X - animationDictionary[name].positions.vertex1.Position.X;
                tempVertex.Position.Y = animationDictionary[name].positions.vertex4.Position.Y - animationDictionary[name].positions.vertex1.Position.Y;
                currentAngle = Math.Atan2(tempVertex.Position.Y, tempVertex.Position.X) * (180 / Math.PI);
                distance = Math.Sqrt((tempVertex.Position.X * tempVertex.Position.X) + (tempVertex.Position.Y * tempVertex.Position.Y));
                animationDictionary[name].positions.rotation = currentAngle - degrees - (currentAngle * 2);
                tempVertex.Position.X = animationDictionary[name].positions.vertex1.Position.X + (float)Math.Cos(Math.PI * animationDictionary[name].positions.rotation / 180.0) * (float)distance;
                tempVertex.Position.Y = animationDictionary[name].positions.vertex1.Position.Y - (float)Math.Sin(Math.PI * animationDictionary[name].positions.rotation / 180.0) * (float)distance;
                vertexArray[animationDictionary[name].positions.firstPosition + 3] = tempVertex;

                if (degrees >= 360 || degrees <= -360)
                {
                    degrees = degrees % 360;
                }
                if (degrees < 0)
                {
                    degrees = degrees + 360;
                }

                animationDictionary[name].positions.rotation = degrees;

                if (animationDictionary[name].positions.scale.X != 0 || animationDictionary[name].positions.scale.Y != 0)
                {
                    scaleEntity(name, animationDictionary[name].positions.scale);
                }
            }
        }

        //rotates an entity around its center (vertex array only)
        public void rotateEntityCenter(string name, float degrees)
        {
            if (entityDictionary.ContainsKey(name))
            {
                rotateEntity(name, degrees);

                tempVertex = entityDictionary[name].vertex3;
                tempVertex.Position.X = ((entityDictionary[name].vertex3.Position.X - entityDictionary[name].vertex1.Position.X) / 2);
                tempVertex.Position.Y = ((entityDictionary[name].vertex3.Position.Y - entityDictionary[name].vertex1.Position.Y) / 2);
                currentAngle = Math.Atan2(tempVertex.Position.Y, tempVertex.Position.X) * (180 / Math.PI);
                distance = Math.Sqrt((tempVertex.Position.X * tempVertex.Position.X) + (tempVertex.Position.Y * tempVertex.Position.Y));
                entityDictionary[name].rotation = currentAngle - degrees - (currentAngle * 2);
                tempVertex.Position.X = entityDictionary[name].vertex1.Position.X + (float)Math.Cos(Math.PI * entityDictionary[name].rotation / 180.0) * (float)distance;
                tempVertex.Position.Y = entityDictionary[name].vertex1.Position.Y - (float)Math.Sin(Math.PI * entityDictionary[name].rotation / 180.0) * (float)distance;
                middle.X = ((entityDictionary[name].vertex2.Position.X - entityDictionary[name].vertex1.Position.X) / 2) + entityDictionary[name].vertex1.Position.X;
                middle.Y = ((entityDictionary[name].vertex3.Position.Y - entityDictionary[name].vertex1.Position.Y) / 2) + entityDictionary[name].vertex1.Position.Y;
                middle.X = middle.X - tempVertex.Position.X;
                middle.Y = middle.Y - tempVertex.Position.Y;

                tempVertex = vertexArray[entityDictionary[name].firstPosition];
                tempVertex.Position.X = tempVertex.Position.X + middle.X;
                tempVertex.Position.Y = tempVertex.Position.Y + middle.Y;
                vertexArray[entityDictionary[name].firstPosition] = tempVertex;

                tempVertex = vertexArray[entityDictionary[name].firstPosition + 1];
                tempVertex.Position.X = tempVertex.Position.X + middle.X;
                tempVertex.Position.Y = tempVertex.Position.Y + middle.Y;
                vertexArray[entityDictionary[name].firstPosition + 1] = tempVertex;

                tempVertex = vertexArray[entityDictionary[name].firstPosition + 2];
                tempVertex.Position.X = tempVertex.Position.X + middle.X;
                tempVertex.Position.Y = tempVertex.Position.Y + middle.Y;
                vertexArray[entityDictionary[name].firstPosition + 2] = tempVertex;

                tempVertex = vertexArray[entityDictionary[name].firstPosition + 3];
                tempVertex.Position.X = tempVertex.Position.X + middle.X;
                tempVertex.Position.Y = tempVertex.Position.Y + middle.Y;
                vertexArray[entityDictionary[name].firstPosition + 3] = tempVertex;
            }
            else if (animationDictionary.ContainsKey(name))
            {
                rotateEntity(name, degrees);

                tempVertex = animationDictionary[name].positions.vertex3;
                tempVertex.Position.X = ((animationDictionary[name].positions.vertex3.Position.X - animationDictionary[name].positions.vertex1.Position.X) / 2);
                tempVertex.Position.Y = ((animationDictionary[name].positions.vertex3.Position.Y - animationDictionary[name].positions.vertex1.Position.Y) / 2);
                currentAngle = Math.Atan2(tempVertex.Position.Y, tempVertex.Position.X) * (180 / Math.PI);
                distance = Math.Sqrt((tempVertex.Position.X * tempVertex.Position.X) + (tempVertex.Position.Y * tempVertex.Position.Y));
                animationDictionary[name].positions.rotation = currentAngle - degrees - (currentAngle * 2);
                tempVertex.Position.X = animationDictionary[name].positions.vertex1.Position.X + (float)Math.Cos(Math.PI * animationDictionary[name].positions.rotation / 180.0) * (float)distance;
                tempVertex.Position.Y = animationDictionary[name].positions.vertex1.Position.Y - (float)Math.Sin(Math.PI * animationDictionary[name].positions.rotation / 180.0) * (float)distance;
                middle.X = ((animationDictionary[name].positions.vertex2.Position.X - animationDictionary[name].positions.vertex1.Position.X) / 2) + animationDictionary[name].positions.vertex1.Position.X;
                middle.Y = ((animationDictionary[name].positions.vertex3.Position.Y - animationDictionary[name].positions.vertex1.Position.Y) / 2) + animationDictionary[name].positions.vertex1.Position.Y;
                middle.X = middle.X - tempVertex.Position.X;
                middle.Y = middle.Y - tempVertex.Position.Y;

                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition];
                tempVertex.Position.X = tempVertex.Position.X + middle.X;
                tempVertex.Position.Y = tempVertex.Position.Y + middle.Y;
                vertexArray[animationDictionary[name].positions.firstPosition] = tempVertex;

                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 1];
                tempVertex.Position.X = tempVertex.Position.X + middle.X;
                tempVertex.Position.Y = tempVertex.Position.Y + middle.Y;
                vertexArray[animationDictionary[name].positions.firstPosition + 1] = tempVertex;

                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 2];
                tempVertex.Position.X = tempVertex.Position.X + middle.X;
                tempVertex.Position.Y = tempVertex.Position.Y + middle.Y;
                vertexArray[animationDictionary[name].positions.firstPosition + 2] = tempVertex;

                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 3];
                tempVertex.Position.X = tempVertex.Position.X + middle.X;
                tempVertex.Position.Y = tempVertex.Position.Y + middle.Y;
                vertexArray[animationDictionary[name].positions.firstPosition + 3] = tempVertex;
            }
        }

        //finds if a point is within an entity
        public bool isWithin(string name, Vector2f point)
        {
            if (circlesDictionary.ContainsKey(name))
            {
                if (point.X >= circlesDictionary[name].Position.X &&
                   point.Y >= circlesDictionary[name].Position.Y &&
                   point.X <= circlesDictionary[name].Position.X + ((circlesDictionary[name].Radius * 2) * circlesDictionary[name].Scale.X) &&
                   point.Y <= circlesDictionary[name].Position.Y + ((circlesDictionary[name].Radius * 2) * circlesDictionary[name].Scale.Y))
                {
                    return true;
                }
                return false;
            }
            else if (entityDictionary.ContainsKey(name))
            {
                if (entityDictionary[name].rotation >= 0 && entityDictionary[name].rotation < 90)
                {
                    if (point.Y >= vertexArray[entityDictionary[name].firstPosition].Position.Y &&
                       point.X <= vertexArray[entityDictionary[name].firstPosition + 1].Position.X &&
                       point.Y <= vertexArray[entityDictionary[name].firstPosition + 2].Position.Y &&
                       point.X >= vertexArray[entityDictionary[name].firstPosition + 3].Position.X)
                    {
                        return true;
                    }
                }
                else if (entityDictionary[name].rotation >= 90 && entityDictionary[name].rotation < 180)
                {
                    if (point.X <= vertexArray[entityDictionary[name].firstPosition].Position.X &&
                        point.Y <= vertexArray[entityDictionary[name].firstPosition + 1].Position.Y &&
                        point.X >= vertexArray[entityDictionary[name].firstPosition + 2].Position.X &&
                        point.Y >= vertexArray[entityDictionary[name].firstPosition + 3].Position.Y)
                    {
                        return true;
                    }
                }
                else if (entityDictionary[name].rotation >= 180 && entityDictionary[name].rotation < 270)
                {
                    if (point.Y <= vertexArray[entityDictionary[name].firstPosition].Position.Y &&
                        point.X >= vertexArray[entityDictionary[name].firstPosition + 1].Position.X &&
                        point.Y >= vertexArray[entityDictionary[name].firstPosition + 2].Position.Y &&
                        point.X <= vertexArray[entityDictionary[name].firstPosition + 3].Position.X)
                    {
                        return true;
                    }
                }
                else if (entityDictionary[name].rotation >= 270)
                {
                    if (point.X >= vertexArray[entityDictionary[name].firstPosition].Position.X &&
                        point.Y >= vertexArray[entityDictionary[name].firstPosition + 1].Position.Y &&
                        point.X <= vertexArray[entityDictionary[name].firstPosition + 2].Position.X &&
                        point.Y <= vertexArray[entityDictionary[name].firstPosition + 3].Position.Y)
                    {
                        return true;
                    }
                }
            }
            else if (animationDictionary.ContainsKey(name))
            {
                if (animationDictionary[name].positions.rotation >= 0 && animationDictionary[name].positions.rotation < 90)
                {
                    if (point.Y >= vertexArray[animationDictionary[name].positions.firstPosition].Position.Y &&
                       point.X <= vertexArray[animationDictionary[name].positions.firstPosition + 1].Position.X &&
                       point.Y <= vertexArray[animationDictionary[name].positions.firstPosition + 2].Position.Y &&
                       point.X >= vertexArray[animationDictionary[name].positions.firstPosition + 3].Position.X)
                    {
                        return true;
                    }
                }
                else if (animationDictionary[name].positions.rotation >= 90 && animationDictionary[name].positions.rotation < 180)
                {
                    if (point.X <= vertexArray[animationDictionary[name].positions.firstPosition].Position.X &&
                        point.Y <= vertexArray[animationDictionary[name].positions.firstPosition + 1].Position.Y &&
                        point.X >= vertexArray[animationDictionary[name].positions.firstPosition + 2].Position.X &&
                        point.Y >= vertexArray[animationDictionary[name].positions.firstPosition + 3].Position.Y)
                    {
                        return true;
                    }
                }
                else if (animationDictionary[name].positions.rotation >= 180 && animationDictionary[name].positions.rotation < 270)
                {
                    if (point.Y <= vertexArray[animationDictionary[name].positions.firstPosition].Position.Y &&
                        point.X >= vertexArray[animationDictionary[name].positions.firstPosition + 1].Position.X &&
                        point.Y >= vertexArray[animationDictionary[name].positions.firstPosition + 2].Position.Y &&
                        point.X <= vertexArray[animationDictionary[name].positions.firstPosition + 3].Position.X)
                    {
                        return true;
                    }
                }
                else if (animationDictionary[name].positions.rotation >= 270)
                {
                    if (point.X >= vertexArray[animationDictionary[name].positions.firstPosition].Position.X &&
                        point.Y >= vertexArray[animationDictionary[name].positions.firstPosition + 1].Position.Y &&
                        point.X <= vertexArray[animationDictionary[name].positions.firstPosition + 2].Position.X &&
                        point.Y <= vertexArray[animationDictionary[name].positions.firstPosition + 3].Position.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //finds if two entitys are overlapping
        public bool isOverlapping(string entity1, string entity2)
        {
            if (entityDictionary.ContainsKey(entity1))
            {
                if (entityDictionary.ContainsKey(entity2))
                {
                    //box 1 in box 2
                    if (isWithin(entity1, entityDictionary[entity2].vertex1.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity1, entityDictionary[entity2].vertex2.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity1, entityDictionary[entity2].vertex3.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity1, entityDictionary[entity2].vertex4.Position))
                    {
                        return true;
                    }

                    //box 2 in box 1
                    if (isWithin(entity2, entityDictionary[entity1].vertex1.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity2, entityDictionary[entity1].vertex2.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity2, entityDictionary[entity1].vertex3.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity2, entityDictionary[entity1].vertex4.Position))
                    {
                        return true;
                    }
                }
                else if (circlesDictionary.ContainsKey(entity2))
                {
                    //circle in a box
                    if (isWithin(entity1, circlesDictionary[entity2].Position))
                    {
                        return true;
                    }
                    tempVertex.Position = circlesDictionary[entity2].Position;
                    tempVertex.Position.X = tempVertex.Position.X + ((circlesDictionary[entity2].Radius * 2) * circlesDictionary[entity2].Scale.X);
                    if (isWithin(entity1, tempVertex.Position))
                    {
                        return true;
                    }
                    tempVertex.Position = circlesDictionary[entity2].Position;
                    tempVertex.Position.Y = tempVertex.Position.Y + ((circlesDictionary[entity2].Radius * 2) * circlesDictionary[entity2].Scale.Y);
                    if (isWithin(entity1, tempVertex.Position))
                    {
                        return true;
                    }
                    tempVertex.Position = circlesDictionary[entity2].Position;
                    tempVertex.Position.X = tempVertex.Position.X + ((circlesDictionary[entity2].Radius * 2) * circlesDictionary[entity2].Scale.X);
                    tempVertex.Position.Y = tempVertex.Position.Y + ((circlesDictionary[entity2].Radius * 2) * circlesDictionary[entity2].Scale.Y);
                    if (isWithin(entity1, tempVertex.Position))
                    {
                        return true;
                    }

                    //box in a circle
                    if (isWithin(entity2, entityDictionary[entity1].vertex1.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity2, entityDictionary[entity1].vertex2.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity2, entityDictionary[entity1].vertex3.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity2, entityDictionary[entity1].vertex4.Position))
                    {
                        return true;
                    }
                }
                else if (animationDictionary.ContainsKey(entity2))
                {
                    //box in animation
                    if (isWithin(entity1, animationDictionary[entity2].positions.vertex1.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity1, animationDictionary[entity2].positions.vertex2.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity1, animationDictionary[entity2].positions.vertex3.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity1, animationDictionary[entity2].positions.vertex4.Position))
                    {
                        return true;
                    }

                    //animation in box
                    if (isWithin(entity2, entityDictionary[entity1].vertex1.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity2, entityDictionary[entity1].vertex2.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity2, entityDictionary[entity1].vertex3.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity2, entityDictionary[entity1].vertex4.Position))
                    {
                        return true;
                    }
                }
                return false;
            }
            else if (circlesDictionary.ContainsKey(entity1))
            {
                if (entityDictionary.ContainsKey(entity2))
                {
                    //circle in a box
                    if (isWithin(entity2, circlesDictionary[entity1].Position))
                    {
                        return true;
                    }
                    tempVertex.Position = circlesDictionary[entity1].Position;
                    tempVertex.Position.X = tempVertex.Position.X + ((circlesDictionary[entity1].Radius * 2) * circlesDictionary[entity1].Scale.X);
                    if (isWithin(entity2, tempVertex.Position))
                    {
                        return true;
                    }
                    tempVertex.Position = circlesDictionary[entity1].Position;
                    tempVertex.Position.Y = tempVertex.Position.Y + ((circlesDictionary[entity1].Radius * 2) * circlesDictionary[entity1].Scale.Y);
                    if (isWithin(entity2, tempVertex.Position))
                    {
                        return true;
                    }
                    tempVertex.Position = circlesDictionary[entity1].Position;
                    tempVertex.Position.X = tempVertex.Position.X + ((circlesDictionary[entity1].Radius * 2) * circlesDictionary[entity1].Scale.X);
                    tempVertex.Position.Y = tempVertex.Position.Y + ((circlesDictionary[entity1].Radius * 2) * circlesDictionary[entity1].Scale.Y);
                    if (isWithin(entity2, tempVertex.Position))
                    {
                        return true;
                    }

                    //box in a circle
                    if (isWithin(entity1, entityDictionary[entity2].vertex1.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity1, entityDictionary[entity2].vertex2.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity1, entityDictionary[entity2].vertex3.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity1, entityDictionary[entity2].vertex4.Position))
                    {
                        return true;
                    }
                }
                else if (circlesDictionary.ContainsKey(entity2))
                {
                    //circle 1 in circle 2
                    if (isWithin(entity2, circlesDictionary[entity1].Position))
                    {
                        return true;
                    }
                    tempVertex.Position = circlesDictionary[entity1].Position;
                    tempVertex.Position.X = tempVertex.Position.X + ((circlesDictionary[entity1].Radius * 2) * circlesDictionary[entity1].Scale.X);
                    if (isWithin(entity2, tempVertex.Position))
                    {
                        return true;
                    }
                    tempVertex.Position = circlesDictionary[entity1].Position;
                    tempVertex.Position.Y = tempVertex.Position.Y + ((circlesDictionary[entity1].Radius * 2) * circlesDictionary[entity1].Scale.Y);
                    if (isWithin(entity2, tempVertex.Position))
                    {
                        return true;
                    }
                    tempVertex.Position = circlesDictionary[entity1].Position;
                    tempVertex.Position.X = tempVertex.Position.X + ((circlesDictionary[entity1].Radius * 2) * circlesDictionary[entity1].Scale.X);
                    tempVertex.Position.Y = tempVertex.Position.Y + ((circlesDictionary[entity1].Radius * 2) * circlesDictionary[entity1].Scale.Y);
                    if (isWithin(entity2, tempVertex.Position))
                    {
                        return true;
                    }

                    //circle 2 in circle 1
                    if (isWithin(entity1, circlesDictionary[entity2].Position))
                    {
                        return true;
                    }
                    tempVertex.Position = circlesDictionary[entity2].Position;
                    tempVertex.Position.X = tempVertex.Position.X + ((circlesDictionary[entity2].Radius * 2) * circlesDictionary[entity2].Scale.X);
                    if (isWithin(entity1, tempVertex.Position))
                    {
                        return true;
                    }
                    tempVertex.Position = circlesDictionary[entity2].Position;
                    tempVertex.Position.Y = tempVertex.Position.Y + ((circlesDictionary[entity2].Radius * 2) * circlesDictionary[entity2].Scale.Y);
                    if (isWithin(entity1, tempVertex.Position))
                    {
                        return true;
                    }
                    tempVertex.Position = circlesDictionary[entity2].Position;
                    tempVertex.Position.X = tempVertex.Position.X + ((circlesDictionary[entity2].Radius * 2) * circlesDictionary[entity2].Scale.X);
                    tempVertex.Position.Y = tempVertex.Position.Y + ((circlesDictionary[entity2].Radius * 2) * circlesDictionary[entity2].Scale.Y);
                    if (isWithin(entity1, tempVertex.Position))
                    {
                        return true;
                    }
                }
                else if (animationDictionary.ContainsKey(entity2))
                {
                    //circle in an animation
                    if (isWithin(entity2, circlesDictionary[entity1].Position))
                    {
                        return true;
                    }
                    tempVertex.Position = circlesDictionary[entity1].Position;
                    tempVertex.Position.X = tempVertex.Position.X + ((circlesDictionary[entity1].Radius * 2) * circlesDictionary[entity1].Scale.X);
                    if (isWithin(entity2, tempVertex.Position))
                    {
                        return true;
                    }
                    tempVertex.Position = circlesDictionary[entity1].Position;
                    tempVertex.Position.Y = tempVertex.Position.Y + ((circlesDictionary[entity1].Radius * 2) * circlesDictionary[entity1].Scale.Y);
                    if (isWithin(entity2, tempVertex.Position))
                    {
                        return true;
                    }
                    tempVertex.Position = circlesDictionary[entity1].Position;
                    tempVertex.Position.X = tempVertex.Position.X + ((circlesDictionary[entity1].Radius * 2) * circlesDictionary[entity1].Scale.X);
                    tempVertex.Position.Y = tempVertex.Position.Y + ((circlesDictionary[entity1].Radius * 2) * circlesDictionary[entity1].Scale.Y);
                    if (isWithin(entity2, tempVertex.Position))
                    {
                        return true;
                    }

                    //animation in a circle
                    if (isWithin(entity1, animationDictionary[entity2].positions.vertex1.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity1, animationDictionary[entity2].positions.vertex2.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity1, animationDictionary[entity2].positions.vertex3.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity1, animationDictionary[entity2].positions.vertex4.Position))
                    {
                        return true;
                    }
                }
            }
            else if (animationDictionary.ContainsKey(entity1))
            {
                if (entityDictionary.ContainsKey(entity2))
                {
                    //animation in box
                    if (isWithin(entity1, entityDictionary[entity2].vertex1.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity1, entityDictionary[entity2].vertex2.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity1, entityDictionary[entity2].vertex3.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity1, entityDictionary[entity2].vertex4.Position))
                    {
                        return true;
                    }

                    //box in animation
                    if (isWithin(entity2, animationDictionary[entity1].positions.vertex1.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity2, animationDictionary[entity1].positions.vertex2.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity2, animationDictionary[entity1].positions.vertex3.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity2, animationDictionary[entity1].positions.vertex4.Position))
                    {
                        return true;
                    }
                }
                else if (circlesDictionary.ContainsKey(entity2))
                {
                    //circle in a animation
                    if (isWithin(entity1, circlesDictionary[entity2].Position))
                    {
                        return true;
                    }
                    tempVertex.Position = circlesDictionary[entity2].Position;
                    tempVertex.Position.X = tempVertex.Position.X + ((circlesDictionary[entity2].Radius * 2) * circlesDictionary[entity2].Scale.X);
                    if (isWithin(entity1, tempVertex.Position))
                    {
                        return true;
                    }
                    tempVertex.Position = circlesDictionary[entity2].Position;
                    tempVertex.Position.Y = tempVertex.Position.Y + ((circlesDictionary[entity2].Radius * 2) * circlesDictionary[entity2].Scale.Y);
                    if (isWithin(entity1, tempVertex.Position))
                    {
                        return true;
                    }
                    tempVertex.Position = circlesDictionary[entity2].Position;
                    tempVertex.Position.X = tempVertex.Position.X + ((circlesDictionary[entity2].Radius * 2) * circlesDictionary[entity2].Scale.X);
                    tempVertex.Position.Y = tempVertex.Position.Y + ((circlesDictionary[entity2].Radius * 2) * circlesDictionary[entity2].Scale.Y);
                    if (isWithin(entity1, tempVertex.Position))
                    {
                        return true;
                    }

                    //animation in a circle
                    if (isWithin(entity2, animationDictionary[entity1].positions.vertex1.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity2, animationDictionary[entity1].positions.vertex2.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity2, animationDictionary[entity1].positions.vertex3.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity2, animationDictionary[entity1].positions.vertex4.Position))
                    {
                        return true;
                    }
                }
                else if (animationDictionary.ContainsKey(entity2))
                {
                    //animation in animation
                    if (isWithin(entity1, animationDictionary[entity2].positions.vertex1.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity1, animationDictionary[entity2].positions.vertex2.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity1, animationDictionary[entity2].positions.vertex3.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity1, animationDictionary[entity2].positions.vertex4.Position))
                    {
                        return true;
                    }

                    //animation in animation
                    if (isWithin(entity2, animationDictionary[entity1].positions.vertex1.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity2, animationDictionary[entity1].positions.vertex2.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity2, animationDictionary[entity1].positions.vertex3.Position))
                    {
                        return true;
                    }
                    if (isWithin(entity2, animationDictionary[entity1].positions.vertex4.Position))
                    {
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

        //returns if an entity is within an area
        public bool isWithinArea(string name, Vector2f point, int width, int height)
        {
            if (entityDictionary.ContainsKey(name))
            {
                if (entityDictionary[name].vertex1.Position.X >= point.X && entityDictionary[name].vertex1.Position.Y >= point.Y &&
                    entityDictionary[name].vertex1.Position.X < width && entityDictionary[name].vertex1.Position.Y < height)
                {
                    return true;
                }
                return false;
            }
            else if (animationDictionary.ContainsKey(name))
            {
                if (animationDictionary[name].positions.vertex1.Position.X >= point.X && animationDictionary[name].positions.vertex1.Position.Y >= point.Y &&
                    animationDictionary[name].positions.vertex1.Position.X < width && animationDictionary[name].positions.vertex1.Position.Y < height)
                {
                    return true;
                }
                return false;
            }
            else if (circlesDictionary.ContainsKey(name))
            {
                if (circlesDictionary[name].Position.X >= point.X && circlesDictionary[name].Position.Y >= point.Y &&
                    circlesDictionary[name].Position.X < width && circlesDictionary[name].Position.Y < height)
                {
                    return true;
                }
                return false;
            }
            else if (textDictionary.ContainsKey(name))
            {
                if (textDictionary[name].Position.X >= point.X && textDictionary[name].Position.Y >= point.Y &&
                    textDictionary[name].Position.X < width && textDictionary[name].Position.Y < height)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        //returns if a given entity is within the window
        public bool isWithinWindow(string name)
        {
            if (entityDictionary.ContainsKey(name))
            {
                if (entityDictionary[name].vertex1.Position.X >= 0 && entityDictionary[name].vertex1.Position.Y >= 0 &&
                    entityDictionary[name].vertex1.Position.X < resolutionX && entityDictionary[name].vertex1.Position.Y < resolutionY)
                {
                    return true;
                }
                return false;
            }
            else if (animationDictionary.ContainsKey(name))
            {
                if (animationDictionary[name].positions.vertex1.Position.X >= 0 && animationDictionary[name].positions.vertex1.Position.Y >= 0 &&
                    animationDictionary[name].positions.vertex1.Position.X < resolutionX && animationDictionary[name].positions.vertex1.Position.Y < resolutionY)
                {
                    return true;
                }
                return false;
            }
            else if (circlesDictionary.ContainsKey(name))
            {
                if (circlesDictionary[name].Position.X >= 0 && circlesDictionary[name].Position.Y >= 0 &&
                    circlesDictionary[name].Position.X < resolutionX && circlesDictionary[name].Position.Y < resolutionY)
                {
                    return true;
                }
                return false;
            }
            else if (textDictionary.ContainsKey(name))
            {
                if (textDictionary[name].Position.X >= 0 && textDictionary[name].Position.Y >= 0 &&
                    textDictionary[name].Position.X < resolutionX && textDictionary[name].Position.Y < resolutionY)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        //returns if a given entity is within the window
        public bool isWithinWindow(string name, int width, int height)
        {
            if (entityDictionary.ContainsKey(name))
            {
                if (entityDictionary[name].vertex1.Position.X >= 0 && entityDictionary[name].vertex1.Position.Y >= 0 &&
                    entityDictionary[name].vertex1.Position.X < width && entityDictionary[name].vertex1.Position.Y < height)
                {
                    return true;
                }
                return false;
            }
            else if (animationDictionary.ContainsKey(name))
            {
                if (animationDictionary[name].positions.vertex1.Position.X >= 0 && animationDictionary[name].positions.vertex1.Position.Y >= 0 &&
                    animationDictionary[name].positions.vertex1.Position.X < width && animationDictionary[name].positions.vertex1.Position.Y < height)
                {
                    return true;
                }
                return false;
            }
            else if (circlesDictionary.ContainsKey(name))
            {
                if (circlesDictionary[name].Position.X >= 0 && circlesDictionary[name].Position.Y >= 0 &&
                    circlesDictionary[name].Position.X < width && circlesDictionary[name].Position.Y < height)
                {
                    return true;
                }
                return false;
            }
            else if (textDictionary.ContainsKey(name))
            {
                if (textDictionary[name].Position.X >= 0 && textDictionary[name].Position.Y >= 0 &&
                    textDictionary[name].Position.X < width && textDictionary[name].Position.Y < height)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        //finds if a point is within a set entities within the given string
        public string batchIsWithin(string name, Vector2f point)
        {
            tempStr = string.Empty;
            for (int i = 0; i < entityDictionary.Count; i++)
            {
                if (entityDictionary.ElementAt(i).Key.Contains(name))
                {
                    if (isWithin(entityDictionary.ElementAt(i).Key, point))
                    {
                        tempStr += entityDictionary.ElementAt(i).Key;
                    }
                }
            }
            for (int i = 0; i < circlesDictionary.Count; i++)
            {
                if (circlesDictionary.ElementAt(i).Key.Contains(name))
                {
                    if (isWithin(circlesDictionary.ElementAt(i).Key, point))
                    {
                        tempStr += circlesDictionary.ElementAt(i).Key;
                    }
                }
            }
            for (int i = 0; i < animationDictionary.Count; i++)
            {
                if (animationDictionary.ElementAt(i).Key.Contains(name))
                {
                    if (isWithin(animationDictionary.ElementAt(i).Key, point))
                    {
                        tempStr += animationDictionary.ElementAt(i).Key;
                    }
                }
            }
            return tempStr;
        }

        //finds if an entity is overlapping a set of entities within the given string
        //the first parameter is the entity being tested, and the second is anything that may collide with it
        public string batchIsOverlapping(string entity, string entitys)
        {
            tempStr = string.Empty;
            for (int i = 0; i < entityDictionary.Count; i++)
            {
                if (entityDictionary.ElementAt(i).Key.Contains(entitys))
                {
                    if (isOverlapping(entity, entityDictionary.ElementAt(i).Key) && entityDictionary.ElementAt(i).Key != entity)
                    {
                        tempStr = entityDictionary.ElementAt(i).Key;
                    }
                }
            }
            for (int i = 0; i < circlesDictionary.Count; i++)
            {
                if (circlesDictionary.ElementAt(i).Key.Contains(entitys))
                {
                    if (isOverlapping(entity, circlesDictionary.ElementAt(i).Key) && circlesDictionary.ElementAt(i).Key != entity)
                    {
                        tempStr = circlesDictionary.ElementAt(i).Key;
                    }
                }
            }
            for (int i = 0; i < animationDictionary.Count; i++)
            {
                if (animationDictionary.ElementAt(i).Key.Contains(entitys))
                {
                    if (isOverlapping(entity, animationDictionary.ElementAt(i).Key) && circlesDictionary.ElementAt(i).Key != entity)
                    {
                        tempStr = animationDictionary.ElementAt(i).Key;
                    }
                }
            }
            return tempStr;
        }

        //plays an animation
        private void playAnimation(string name)
        {
            if (animationDictionary.ContainsKey(name))
            {
                if (animationDictionary[name].paused == false)
                {
                    if (animationDictionary[name].frameCounter == animationDictionary[name].speed)
                    {
                        animationDictionary[name].frameCounter = 0;
                        if (animationDictionary[name].currentFrame + 1 != animationDictionary[name].frames.Length)
                        {
                            animationDictionary[name].currentFrame += 1;
                            //point 1 dictionary
                            animationDictionary[name].positions.vertex1.TexCoords.X = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Left;
                            animationDictionary[name].positions.vertex1.TexCoords.Y = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Top;
                            //point 2 dictionary
                            animationDictionary[name].positions.vertex2.TexCoords.X = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Left + spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Width;
                            animationDictionary[name].positions.vertex2.TexCoords.Y = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Top;
                            //point 3 dictionary
                            animationDictionary[name].positions.vertex3.TexCoords.X = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Left + spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Width;
                            animationDictionary[name].positions.vertex3.TexCoords.Y = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Top + spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Height;
                            //point 4 dictionary
                            animationDictionary[name].positions.vertex4.TexCoords.X = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Left;
                            animationDictionary[name].positions.vertex4.TexCoords.Y = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Top + spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Height;

                            //point 1 vertex
                            tempVertex = vertexArray[animationDictionary[name].positions.firstPosition];
                            tempVertex.TexCoords.X = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Left;
                            tempVertex.TexCoords.Y = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Top;
                            vertexArray[animationDictionary[name].positions.firstPosition] = tempVertex;
                            //point 2 vertex
                            tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 1];
                            tempVertex.TexCoords.X = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Left + spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Width;
                            tempVertex.TexCoords.Y = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Top;
                            vertexArray[animationDictionary[name].positions.firstPosition + 1] = tempVertex;
                            //point 3 vertex
                            tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 2];
                            tempVertex.TexCoords.X = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Left + spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Width;
                            tempVertex.TexCoords.Y = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Top + spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Height;
                            vertexArray[animationDictionary[name].positions.firstPosition + 2] = tempVertex;
                            //point 4 vertex
                            tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 3];
                            tempVertex.TexCoords.X = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Left;
                            tempVertex.TexCoords.Y = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Top + spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Height;
                            vertexArray[animationDictionary[name].positions.firstPosition + 3] = tempVertex;
                        }
                        else
                        {
                            if (animationDictionary[name].loops == true)
                            {
                                animationDictionary[name].currentFrame = 0;
                                //point 1 dictionary
                                animationDictionary[name].positions.vertex1.TexCoords.X = spriteMap[animationDictionary[name].frames[0]].Left;
                                animationDictionary[name].positions.vertex1.TexCoords.Y = spriteMap[animationDictionary[name].frames[0]].Top;
                                //point 2 dictionary
                                animationDictionary[name].positions.vertex2.TexCoords.X = spriteMap[animationDictionary[name].frames[0]].Left + spriteMap[animationDictionary[name].frames[0]].Width;
                                animationDictionary[name].positions.vertex2.TexCoords.Y = spriteMap[animationDictionary[name].frames[0]].Top;
                                //point 3 dictionary
                                animationDictionary[name].positions.vertex3.TexCoords.X = spriteMap[animationDictionary[name].frames[0]].Left + spriteMap[animationDictionary[name].frames[0]].Width;
                                animationDictionary[name].positions.vertex3.TexCoords.Y = spriteMap[animationDictionary[name].frames[0]].Top + spriteMap[animationDictionary[name].frames[0]].Height;
                                //point 4 dictionary
                                animationDictionary[name].positions.vertex4.TexCoords.X = spriteMap[animationDictionary[name].frames[0]].Left;
                                animationDictionary[name].positions.vertex4.TexCoords.Y = spriteMap[animationDictionary[name].frames[0]].Top + spriteMap[animationDictionary[name].frames[0]].Height;

                                //point 1 vertex
                                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition];
                                tempVertex.TexCoords.X = spriteMap[animationDictionary[name].frames[0]].Left;
                                tempVertex.TexCoords.Y = spriteMap[animationDictionary[name].frames[0]].Top;
                                vertexArray[animationDictionary[name].positions.firstPosition] = tempVertex;
                                //point 2 vertex
                                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 1];
                                tempVertex.TexCoords.X = spriteMap[animationDictionary[name].frames[0]].Left + spriteMap[animationDictionary[name].frames[0]].Width;
                                tempVertex.TexCoords.Y = spriteMap[animationDictionary[name].frames[0]].Top;
                                vertexArray[animationDictionary[name].positions.firstPosition + 1] = tempVertex;
                                //point 3 vertex
                                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 2];
                                tempVertex.TexCoords.X = spriteMap[animationDictionary[name].frames[0]].Left + spriteMap[animationDictionary[name].frames[0]].Width;
                                tempVertex.TexCoords.Y = spriteMap[animationDictionary[name].frames[0]].Top + spriteMap[animationDictionary[name].frames[0]].Height;
                                vertexArray[animationDictionary[name].positions.firstPosition + 2] = tempVertex;
                                //point 4 vertex
                                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 3];
                                tempVertex.TexCoords.X = spriteMap[animationDictionary[name].frames[0]].Left;
                                tempVertex.TexCoords.Y = spriteMap[animationDictionary[name].frames[0]].Top + spriteMap[animationDictionary[name].frames[0]].Height;
                                vertexArray[animationDictionary[name].positions.firstPosition + 3] = tempVertex;
                            }
                        }
                    }
                    else
                    {
                        animationDictionary[name].frameCounter++;
                    }
                }
            }
        }

        //pauses an animation
        public void pauseAnimation(string name)
        {
            if (animationDictionary.ContainsKey(name))
            {
                animationDictionary[name].paused = true;
            }
        }

        //resumes an animation
        public void resumeAnimation(string name)
        {
            if (animationDictionary.ContainsKey(name))
            {
                animationDictionary[name].paused = false;
            }
        }

        //sets the speed of an animation. 1 is narmal.
        public void setAnimationSpeed(string name, int animationSpeed)
        {
            if (animationSpeed >= 0 && animationDictionary.ContainsKey(name))
            {
                animationDictionary[name].speed = animationSpeed;
            }
        }

        //goes to a specific animation frame
        public void goToFrame(string name, int frameIndex)
        {
            if (animationDictionary.ContainsKey(name))
            {
                animationDictionary[name].currentFrame = frameIndex;
                //point 1 dictionary
                animationDictionary[name].positions.vertex1.TexCoords.X = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Left;
                animationDictionary[name].positions.vertex1.TexCoords.Y = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Top;
                //point 2 dictionary
                animationDictionary[name].positions.vertex2.TexCoords.X = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Left + spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Width;
                animationDictionary[name].positions.vertex2.TexCoords.Y = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Top;
                //point 3 dictionary
                animationDictionary[name].positions.vertex3.TexCoords.X = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Left + spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Width;
                animationDictionary[name].positions.vertex3.TexCoords.Y = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Top + spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Height;
                //point 4 dictionary
                animationDictionary[name].positions.vertex4.TexCoords.X = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Left;
                animationDictionary[name].positions.vertex4.TexCoords.Y = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Top + spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Height;

                //point 1 vertex
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition];
                tempVertex.TexCoords.X = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Left;
                tempVertex.TexCoords.Y = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Top;
                vertexArray[animationDictionary[name].positions.firstPosition] = tempVertex;
                //point 2 vertex
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 1];
                tempVertex.TexCoords.X = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Left + spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Width;
                tempVertex.TexCoords.Y = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Top;
                vertexArray[animationDictionary[name].positions.firstPosition + 1] = tempVertex;
                //point 3 vertex
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 2];
                tempVertex.TexCoords.X = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Left + spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Width;
                tempVertex.TexCoords.Y = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Top + spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Height;
                vertexArray[animationDictionary[name].positions.firstPosition + 2] = tempVertex;
                //point 4 vertex
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 3];
                tempVertex.TexCoords.X = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Left;
                tempVertex.TexCoords.Y = spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Top + spriteMap[animationDictionary[name].frames[animationDictionary[name].currentFrame]].Height;
                vertexArray[animationDictionary[name].positions.firstPosition + 3] = tempVertex;
            }
        }

        //finds if a point is within a bounding box
        public bool boundingBoxPoint(Vector2f point, Vector2f boxOrigin, int width, int height)
        {
            if (point.X >= boxOrigin.X && point.X <= boxOrigin.X + width &&
                point.Y >= boxOrigin.Y && point.Y <= boxOrigin.Y + height)
            {
                return true;
            }
            return false;
        }

        //finds if a point is within a bounding circle
        public bool boundingCirclePoint(Vector2f point, Vector2f boxOrigin, int radius)
        {
            point.X = point.X - boxOrigin.X;
            point.Y = point.Y - boxOrigin.Y;
            if ((Math.Sqrt((point.X * point.X) + (point.Y * point.Y))) <= radius)
            {
                return true;
            }
            return false;
        }

        //finds if two bounding boxes are overlapping
        public bool boundingBoxOverlap(Vector2f boxOrigin1, int width1, int height1, Vector2f boxOrigin2, int width2, int height2)
        {
            if (boundingBoxPoint(boxOrigin1, boxOrigin2, width2, height2))
            {
                return true;
            }
            boxOrigin1.X += width1;
            if (boundingBoxPoint(boxOrigin1, boxOrigin2, width2, height2))
            {
                return true;
            }
            boxOrigin1.Y += height1;
            if (boundingBoxPoint(boxOrigin1, boxOrigin2, width2, height2))
            {
                return true;
            }
            boxOrigin1.X -= width1;
            if (boundingBoxPoint(boxOrigin1, boxOrigin2, width2, height2))
            {
                return true;
            }
            boxOrigin1.Y -= height1;
            if (boundingBoxPoint(boxOrigin2, boxOrigin1, width1, height1))
            {
                return true;
            }
            boxOrigin2.X += width2;
            if (boundingBoxPoint(boxOrigin2, boxOrigin1, width1, height1))
            {
                return true;
            }
            boxOrigin2.Y += height2;
            if (boundingBoxPoint(boxOrigin2, boxOrigin1, width1, height1))
            {
                return true;
            }
            boxOrigin2.X -= width2;
            if (boundingBoxPoint(boxOrigin2, boxOrigin1, width1, height1))
            {
                return true;
            }
            return false;
        }

        //finds if two bounding circles are overlapping
        public bool boundingCircleOverlap(Vector2f circleOrigin1, int radius1, Vector2f circleOrigin2, int radius2)
        {
            tempVertex.Position.X = circleOrigin1.X - circleOrigin2.X;
            tempVertex.Position.Y = circleOrigin1.Y - circleOrigin2.Y;
            if ((Math.Sqrt((tempVertex.Position.X * tempVertex.Position.X) + (tempVertex.Position.Y * tempVertex.Position.Y))) < (radius1 + radius2))
            {
                return true;
            }
            return false;
        }

        //makes an entity visible
        public void makeVisible(string name)
        {
            if (entityDictionary.ContainsKey(name))
            {
                tempVertex = vertexArray[entityDictionary[name].firstPosition];
                tempVertex.Color = entityDictionary[name].color1;
                vertexArray[entityDictionary[name].firstPosition] = tempVertex;
                tempVertex = vertexArray[entityDictionary[name].firstPosition + 1];
                tempVertex.Color = entityDictionary[name].color2;
                vertexArray[entityDictionary[name].firstPosition + 1] = tempVertex;
                tempVertex = vertexArray[entityDictionary[name].firstPosition + 2];
                tempVertex.Color = entityDictionary[name].color3;
                vertexArray[entityDictionary[name].firstPosition + 2] = tempVertex;
                tempVertex = vertexArray[entityDictionary[name].firstPosition + 3];
                tempVertex.Color = entityDictionary[name].color4;
                vertexArray[entityDictionary[name].firstPosition + 3] = tempVertex;
            }
            else if (animationDictionary.ContainsKey(name))
            {
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition];
                tempVertex.Color = animationDictionary[name].positions.color1;
                vertexArray[animationDictionary[name].positions.firstPosition] = tempVertex;
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 1];
                tempVertex.Color = animationDictionary[name].positions.color2;
                vertexArray[animationDictionary[name].positions.firstPosition + 1] = tempVertex;
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 2];
                tempVertex.Color = animationDictionary[name].positions.color3;
                vertexArray[animationDictionary[name].positions.firstPosition + 2] = tempVertex;
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 3];
                tempVertex.Color = animationDictionary[name].positions.color4;
                vertexArray[animationDictionary[name].positions.firstPosition + 3] = tempVertex;
            }
            else if (circlesDictionary.ContainsKey(name))
            {
                circlesDictionary[name].FillColor = colorDictionary[name];
            }
            else if (textDictionary.ContainsKey(name))
            {
                textDictionary[name].Color = colorDictionary[name];
            }
        }

        //makes an entity invisible
        public void makeInvisible(string name)
        {
            if (entityDictionary.ContainsKey(name))
            {
                tempVertex = vertexArray[entityDictionary[name].firstPosition];
                tempVertex.Color = nullColor;
                vertexArray[entityDictionary[name].firstPosition] = tempVertex;
                tempVertex = vertexArray[entityDictionary[name].firstPosition + 1];
                tempVertex.Color = nullColor;
                vertexArray[entityDictionary[name].firstPosition + 1] = tempVertex;
                tempVertex = vertexArray[entityDictionary[name].firstPosition + 2];
                tempVertex.Color = nullColor;
                vertexArray[entityDictionary[name].firstPosition + 2] = tempVertex;
                tempVertex = vertexArray[entityDictionary[name].firstPosition + 3];
                tempVertex.Color = nullColor;
                vertexArray[entityDictionary[name].firstPosition + 3] = tempVertex;
            }
            else if (animationDictionary.ContainsKey(name))
            {
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition];
                tempVertex.Color = nullColor;
                vertexArray[animationDictionary[name].positions.firstPosition] = tempVertex;
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 1];
                tempVertex.Color = nullColor;
                vertexArray[animationDictionary[name].positions.firstPosition + 1] = tempVertex;
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 2];
                tempVertex.Color = nullColor;
                vertexArray[animationDictionary[name].positions.firstPosition + 2] = tempVertex;
                tempVertex = vertexArray[animationDictionary[name].positions.firstPosition + 3];
                tempVertex.Color = nullColor;
                vertexArray[animationDictionary[name].positions.firstPosition + 3] = tempVertex;
            }
            else if (circlesDictionary.ContainsKey(name))
            {
                circlesDictionary[name].FillColor = nullColor;
            }
            else if (textDictionary.ContainsKey(name))
            {
                textDictionary[name].Color = nullColor;
            }
        }

        //sets the color of the window background
        public void setBackgroundColor(Color color)
        {
            background = color;
        }

        //sets the font used
        public void setFont(string fontName)
        {
            font = new Font(fontName);
        }

        //sets the fill color of an entity
        public void setColor(string name, Color color)
        {
            if (circlesDictionary.ContainsKey(name))
            {
                circlesDictionary[name].FillColor = color;
                colorDictionary[name] = color;
            }
            else if (textDictionary.ContainsKey(name))
            {
                textDictionary[name].Color = color;
                colorDictionary[name] = color;
            }
            else if (entityDictionary.ContainsKey(name))
            {
                entityDictionary[name].vertex1.Color = color;
                entityDictionary[name].color1 = color;
                entityDictionary[name].vertex2.Color = color;
                entityDictionary[name].color2 = color;
                entityDictionary[name].vertex3.Color = color;
                entityDictionary[name].color3 = color;
                entityDictionary[name].vertex4.Color = color;
                entityDictionary[name].color4 = color;
                copyQuad(entityDictionary[name], entityDictionary[name].firstPosition);
            }
            else if (animationDictionary.ContainsKey(name))
            {
                animationDictionary[name].positions.vertex1.Color = color;
                animationDictionary[name].positions.color1 = color;
                animationDictionary[name].positions.vertex2.Color = color;
                animationDictionary[name].positions.color2 = color;
                animationDictionary[name].positions.vertex3.Color = color;
                animationDictionary[name].positions.color3 = color;
                animationDictionary[name].positions.vertex4.Color = color;
                animationDictionary[name].positions.color4 = color;
                copyQuad(animationDictionary[name].positions, animationDictionary[name].positions.firstPosition);
            }
        }

        //sets the fill color of an entity
        public void setColor(string name, Color color1, Color color2, Color color3, Color color4)
        {
            if (entityDictionary.ContainsKey(name))
            {
                entityDictionary[name].vertex1.Color = color1;
                entityDictionary[name].color1 = color1;
                entityDictionary[name].vertex2.Color = color2;
                entityDictionary[name].color1 = color2;
                entityDictionary[name].vertex3.Color = color3;
                entityDictionary[name].color1 = color3;
                entityDictionary[name].vertex4.Color = color4;
                entityDictionary[name].color1 = color4;
                copyQuad(entityDictionary[name], entityDictionary[name].firstPosition);
            }
            else if (animationDictionary.ContainsKey(name))
            {
                animationDictionary[name].positions.vertex1.Color = color1;
                animationDictionary[name].positions.color1 = color1;
                animationDictionary[name].positions.vertex2.Color = color2;
                animationDictionary[name].positions.color2 = color2;
                animationDictionary[name].positions.vertex3.Color = color3;
                animationDictionary[name].positions.color3 = color3;
                animationDictionary[name].positions.vertex4.Color = color4;
                animationDictionary[name].positions.color4 = color4;
                copyQuad(animationDictionary[name].positions, animationDictionary[name].positions.firstPosition);
            }
        }

        //sets the outline of a circle entity
        public void circleOutline(string name, Color color, int thickness)
        {
            if (circlesDictionary.ContainsKey(name))
            {
                circlesDictionary[name].OutlineColor = color;
                circlesDictionary[name].OutlineThickness = thickness;
            }
        }

        //sets the text of a text entity
        public void setText(string name, string text)
        {
            if (textDictionary.ContainsKey(name))
            {
                textDictionary[name].DisplayedString = text;
            }
        }

        //returns the point of origin of a specific entity [-1,-1 is error]
        public Vector2f getPointOfOrigin(string name)
        {
            if (entityDictionary.ContainsKey(name))
            {
                return entityDictionary[name].vertex1.Position;
            }
            else if (circlesDictionary.ContainsKey(name))
            {
                return circlesDictionary[name].Position;
            }
            else if (textDictionary.ContainsKey(name))
            {
                return textDictionary[name].Position;
            }
            else if (animationDictionary.ContainsKey(name))
            {
                return animationDictionary[name].positions.vertex1.Position;
            }
            return new Vector2f(-1, -1);
        }

        //returns the rotation of an entity [-1 is error]
        public double getRotation(string name)
        {
            if (circlesDictionary.ContainsKey(name))
            {
                return circlesDictionary[name].Rotation;
            }
            else if (textDictionary.ContainsKey(name))
            {
                return textDictionary[name].Rotation;
            }
            else if (entityDictionary.ContainsKey(name))
            {
                return entityDictionary[name].rotation;
            }
            else if (animationDictionary.ContainsKey(name))
            {
                return animationDictionary[name].positions.rotation;
            }
            return -1;
        }

        //returns the text of a text entity
        public string getText(string name)
        {
            if (textDictionary.ContainsKey(name))
            {
                return textDictionary[name].DisplayedString;
            }
            return "error";
        }

        //returns if an animation is paused
        public bool getPaused(string name)
        {
            if (animationDictionary.ContainsKey(name))
            {
                return animationDictionary[name].paused;
            }
            return false;
        }

        //returns if an animation loops
        public bool getLoops(string name)
        {
            if (animationDictionary.ContainsKey(name))
            {
                return animationDictionary[name].loops;
            }
            return false;
        }

        //returns the current frame on an animation [-1 is error]
        public int getFrame(string name)
        {
            if (animationDictionary.ContainsKey(name))
            {
                return animationDictionary[name].currentFrame;
            }
            return -1;
        }

        //returns an quad of an entity
        public Quad getQuad(string name)
        {
            if (circlesDictionary.ContainsKey(name))
            {
                return new Quad(new Vertex(circlesDictionary[name].Position),
                    new Vertex(new Vector2f(circlesDictionary[name].Position.X + ((circlesDictionary[name].Radius * 2) * circlesDictionary[name].Scale.X), circlesDictionary[name].Position.Y)),
                    new Vertex(new Vector2f(circlesDictionary[name].Position.X + ((circlesDictionary[name].Radius * 2) * circlesDictionary[name].Scale.X), circlesDictionary[name].Position.Y + ((circlesDictionary[name].Radius * 2) * circlesDictionary[name].Scale.Y))),
                    new Vertex(new Vector2f(circlesDictionary[name].Position.X, circlesDictionary[name].Position.Y + ((circlesDictionary[name].Radius * 2) * circlesDictionary[name].Scale.Y))),
                    circlesDictionary[name].Rotation, circlesDictionary[name].Scale);
            }
            else if (textDictionary.ContainsKey(name))
            {
                return new Quad(new Vertex(textDictionary[name].Position), new Vertex(), new Vertex(), new Vertex(),
                    textDictionary[name].Rotation, textDictionary[name].Scale);
            }
            else if (entityDictionary.ContainsKey(name))
            {
                return entityDictionary[name];
            }
            else if (animationDictionary.ContainsKey(name))
            {
                return animationDictionary[name].positions;
            }
            return new Quad();
        }

        //returns the number of elements in vectorArray
        public int vertexArrayCount()
        {
            return (int)vertexArray.VertexCount;
        }

        //returns the number of elements in entityDictionary
        public int entityDictionaryCount()
        {
            return entityDictionary.Count;
        }

        //returns the number of elements in circlesDictionary
        public int circlesDictionaryCount()
        {
            return circlesDictionary.Count;
        }

        //returns the number of elements in textDictionary
        public int textDictionaryCount()
        {
            return textDictionary.Count;
        }

        //returns the number of elements in animationDictionary
        public int animationDictionaryCount()
        {
            return animationDictionary.Count;
        }

        //returns a list of all entities contained in this object
        public string listEntities()
        {
            tempStr = string.Empty;
            for (int i = 0; i < entityDictionary.Count; i++)
            {
                tempStr += entityDictionary.ElementAt(i).Key + "\n";
            }
            for (int i = 0; i < circlesDictionary.Count; i++)
            {
                tempStr += circlesDictionary.ElementAt(i).Key + "\n";
            }
            for (int i = 0; i < textDictionary.Count; i++)
            {
                tempStr += textDictionary.ElementAt(i).Key + "\n";
            }
            for (int i = 0; i < animationDictionary.Count; i++)
            {
                tempStr += animationDictionary.ElementAt(i).Key + "\n";
            }
            return tempStr;
        }

        //draws everything onto the screen
        public void drawAll()
        {
            //gameTimer.toggleStopwatch();
            if (gameTimer.getTimeMilliseconds() >= updateSpeed || FPSLimit == false)
            {
                //process sfml events
                gameWindow.DispatchEvents();

                //clears the sfml the window
                gameWindow.Clear(background);

                gameWindow.Draw(vertexArray, state);

                for (int i = 0; i < circlesDictionary.Count; i++)
                {
                    gameWindow.Draw(circlesDictionary.ElementAt(i).Value);
                }
                for (int i = 0; i < textDictionary.Count; i++)
                {
                    gameWindow.Draw(textDictionary.ElementAt(i).Value);
                }
                for (int i = 0; i < animationDictionary.Count; i++)
                {
                    playAnimation(animationDictionary.ElementAt(i).Key);
                }

                gameWindow.Display();

                gameTimer.restartWatch();
            }
        }

        //adds a new view to the view list
        public void addView(string name, Vector2f position, Vector2f size)
        {
            viewDictionary.Add(name, new View(position, size));
        }

        //sets the current view
        public View getView(string name)
        {
            return viewDictionary[name];
        }

        //moves the view
        public void moveView(string name, Vector2f position)
        {
            viewDictionary[name].Move(position);
        }

        //rotates a view
        public void rotateView(string name, float rotation)
        {
            viewDictionary[name].Rotate(rotation);
        }

        //zooms the view
        public void zoomView(string name, float zoom)
        {
            viewDictionary[name].Zoom(zoom);
        }

        //clears all views
        public void clearViews()
        {
            viewDictionary.Clear();
        }

        //sets the games speed
        public void setSpeed(int newSpeed)
        {
            updateSpeed = 1000 / newSpeed;
        }

        //returns the FPS limit currently set
        public double getFPSCap()
        {
            return fps;
        }

        //returns if the FPS is limited or not
        public bool getFPSLimited()
        {
            return FPSLimit;
        }

        //returns the current window resolution
        public Vector2f getResolution()
        {
            tempVertex.Position.X = resolutionX;
            tempVertex.Position.Y = resolutionY;
            return tempVertex.Position;
        }
    }

    //holds four vertexes, the first point in the vertex array
    //where the quad is stored, the scale of the entity, and
    //the rotation.
    class Quad
    {
        public Vertex vertex1;
        public Vertex vertex2;
        public Vertex vertex3;
        public Vertex vertex4;
        public Color color1;
        public Color color2;
        public Color color3;
        public Color color4;
        public uint firstPosition = 0;
        public Vector2f scale;
        public double rotation;

        public Quad()
        {
            vertex1 = new Vertex();
            vertex2 = new Vertex();
            vertex3 = new Vertex();
            vertex4 = new Vertex();
            rotation = 0;
            scale = new Vector2f(1, 1);
        }

        public Quad(Vertex vert1, Vertex vert2, Vertex vert3, Vertex vert4)
        {
            vertex1 = vert1;
            vertex2 = vert2;
            vertex3 = vert3;
            vertex4 = vert4;
            rotation = 0;
            scale = new Vector2f(1, 1);
            color1 = vert1.Color;
            color2 = vert2.Color;
            color3 = vert3.Color;
            color4 = vert4.Color;
        }

        public Quad(Vertex vert1, Vertex vert2, Vertex vert3, Vertex vert4, double rotate, Vector2f scaler)
        {
            vertex1 = vert1;
            vertex2 = vert2;
            vertex3 = vert3;
            vertex4 = vert4;
            rotation = rotate;
            scale = scaler;
            color1 = vert1.Color;
            color2 = vert2.Color;
            color3 = vert3.Color;
            color4 = vert4.Color;
        }

        public Quad(Quad newQuad)
        {
            vertex1 = newQuad.vertex1;
            vertex2 = newQuad.vertex2;
            vertex3 = newQuad.vertex3;
            vertex4 = newQuad.vertex4;
            rotation = newQuad.rotation;
            scale = newQuad.scale;
            color1 = newQuad.vertex1.Color;
            color2 = newQuad.vertex2.Color;
            color3 = newQuad.vertex3.Color;
            color4 = newQuad.vertex4.Color;
        }

        public override string ToString()
        {
            return vertex1.Position.ToString() + "\n" + vertex2.Position.ToString() + "\n" +
                vertex3.Position.ToString() + "\n" + vertex2.Position.ToString() + "\n" +
                rotation + "\n" + scale;
        }
    }

    //holds all information on an animation, including all frames, speed, position, if it loops, and if its paused;
    class Animation
    {
        public int speed;
        public int currentFrame = 0;
        public string[] frames;
        public Quad positions;
        public bool loops = true;
        public bool paused = false;
        public int frameCounter = 0;

        public Animation(string[] animationFrames)
        {
            frames = animationFrames;
            speed = 0;
            Quad positions = new Quad();
        }

        public Animation(string[] animationFrames, Quad animationPositions, int animationSpeed)
        {
            speed = animationSpeed;
            frames = animationFrames;
            positions = animationPositions;
        }
    }
}