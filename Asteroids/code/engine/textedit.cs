//edits a string with the keyboard
namespace Asteroids
{
    class TextEdit
    {
        private string str;
        private string tempStr;
        private GameTimer timer = new GameTimer();
        private int speed;
        private int repeat;
        private int delay;
        private string lastKey = string.Empty;
        private bool held = false;
        private bool paused = false;

        public TextEdit()
        {
            str = string.Empty;
            speed = 125;
            delay = 350;
            repeat = 50;
            timer.restartWatch();
        }

        public TextEdit(string startingString)
        {
            str = startingString;
            speed = 125;
            delay = 350;
            repeat = 50;
            timer.restartWatch();
        }

        public TextEdit(int keystrokeSpeed, int repeatDelay, int repeatRate)
        {
            str = string.Empty;
            speed = keystrokeSpeed;
            delay = repeatDelay;
            repeat = repeatRate;
            timer.restartWatch();
        }

        public TextEdit(int keystrokeSpeed, int repeatDelay, int repeatRate, string startingString)
        {
            speed = keystrokeSpeed;
            delay = repeatDelay;
            repeat = repeatRate;
            str = startingString;
            timer.restartWatch();
        }

        //takes in keyboard input and changes the held string accordingly, returns the held string
        public string takeInput(string keyboard)
        {
            if (keyboard != "")
            {
                //checks to see if a key is held
                if (lastKey == keyboard)
                {
                    held = true;
                    if (timer.getTimeMilliseconds() >= delay)
                    {
                        paused = true;
                    }
                }
                else
                {
                    held = false;
                    paused = false;
                }

                //keystroke not held
                if (timer.getTimeMilliseconds() >= speed && held == false)
                {
                    tempStr = string.Empty;
                    if (keyboard.Contains("Lshift") || keyboard.Contains("Rshift"))
                    {
                        for (int i = 0; i < keyboard.Length; i++)
                        {
                            tempStr += keyboard[i].ToString();
                            if (changeStringShift(tempStr) == true)
                            {
                                tempStr = string.Empty;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < keyboard.Length; i++)
                        {
                            tempStr += keyboard[i].ToString();
                            if (changeString(tempStr) == true)
                            {
                                tempStr = string.Empty;
                            }
                        }
                    }
                    held = false;
                    timer.restartWatch();
                }
                //keystroke held
                else if (timer.getTimeMilliseconds() >= repeat && paused == true)
                {
                    tempStr = string.Empty;
                    if (keyboard.Contains("Lshift") || keyboard.Contains("Rshift"))
                    {
                        for (int i = 0; i < keyboard.Length; i++)
                        {
                            tempStr += keyboard[i].ToString();
                            if (changeStringShift(tempStr) == true)
                            {
                                tempStr = string.Empty;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < keyboard.Length; i++)
                        {
                            tempStr += keyboard[i].ToString();
                            if (changeString(tempStr) == true)
                            {
                                tempStr = string.Empty;
                            }
                        }
                    }
                    changeString(keyboard);
                    timer.restartWatch();
                }
            }
            lastKey = keyboard;
            return str;
        }

        //adds to the string to whatever key is pressed
        private bool changeString(string keyboardIn)
        {
            if (keyboardIn == "Backspace")
            {
                if (str.Length != 0)
                {
                    str = str.Remove(str.Length - 1);
                }
                return true;
            }
            else if (keyboardIn == "Space")
            {
                str += " ";
                return true;
            }

            if (keyboardIn == "LetrA" || keyboardIn == "LetrB" || keyboardIn == "LetrC" ||
                keyboardIn == "LetrD" || keyboardIn == "LetrE" || keyboardIn == "LetrF" ||
                keyboardIn == "LetrG" || keyboardIn == "LetrH" || keyboardIn == "LetrI" ||
                keyboardIn == "LetrJ" || keyboardIn == "LetrK" || keyboardIn == "LetrL" ||
                keyboardIn == "LetrM" || keyboardIn == "LetrN" || keyboardIn == "LetrO" ||
                keyboardIn == "LetrP" || keyboardIn == "LetrQ" || keyboardIn == "LetrR" ||
                keyboardIn == "LetrS" || keyboardIn == "LetrT" || keyboardIn == "LetrU" ||
                keyboardIn == "LetrV" || keyboardIn == "LetrW" || keyboardIn == "LetrX" ||
                keyboardIn == "LetrY" || keyboardIn == "LetrZ")
            {
                str += keyboardIn[4].ToString().ToLower();
                return true;
            }
            else if (keyboardIn == "Num0" || keyboardIn == "Numpad0")
            {
                str += "0";
                return true;
            }
            else if (keyboardIn == "Num1" || keyboardIn == "Numpad1")
            {
                str += "1";
                return true;
            }
            else if (keyboardIn == "Num2" || keyboardIn == "Numpad2")
            {
                str += "2";
                return true;
            }
            else if (keyboardIn == "Num3" || keyboardIn == "Numpad3")
            {
                str += "3";
                return true;
            }
            else if (keyboardIn == "Num4" || keyboardIn == "Numpad4")
            {
                str += "4";
                return true;
            }
            else if (keyboardIn == "Num5" || keyboardIn == "Numpad5")
            {
                str += "5";
                return true;
            }
            else if (keyboardIn == "Num6" || keyboardIn == "Numpad6")
            {
                str += "6";
                return true;
            }
            else if (keyboardIn == "Num7" || keyboardIn == "Numpad7")
            {
                str += "7";
                return true;
            }
            else if (keyboardIn == "Num8" || keyboardIn == "Numpad8")
            {
                str += "8";
                return true;
            }
            else if (keyboardIn == "Num9" || keyboardIn == "Numpad9")
            {
                str += "9";
                return true;
            }
            else if (keyboardIn == "Lbracket")
            {
                str += "[";
                return true;
            }
            else if (keyboardIn == "Rbracket")
            {
                str += "]";
                return true;
            }
            else if (keyboardIn == "SemiColon")
            {
                str += ";";
                return true;
            }
            else if (keyboardIn == "Quote")
            {
                str += "'";
                return true;
            }
            else if (keyboardIn == "Comma")
            {
                str += ",";
                return true;
            }
            else if (keyboardIn == "Period")
            {
                str += ".";
                return true;
            }
            else if (keyboardIn == "Slash")
            {
                str += "/";
                return true;
            }
            else if (keyboardIn == "Backslash")
            {
                str += "\\";
                return true;
            }
            else if (keyboardIn == "Dash")
            {
                str += "-";
                return true;
            }
            else if (keyboardIn == "Equal")
            {
                str += "=";
                return true;
            }
            else if (keyboardIn == "Tilde")
            {
                str += "`";
                return true;
            }
            else if (keyboardIn == "Plus")
            {
                str += "+";
                return true;
            }
            else if (keyboardIn == "Minus")
            {
                str += "-";
                return true;
            }
            else if (keyboardIn == "Star")
            {
                str += "*";
                return true;
            }
            else if (keyboardIn == "Slash")
            {
                str += "/";
                return true;
            }

            return false;
        }

        //adds to the string to whatever key is pressed + shift
        private bool changeStringShift(string keyboardIn)
        {
            if (keyboardIn == "LShift" || keyboardIn == "RShift")
            {
                return true;
            }
            else if (keyboardIn == "LetrA" || keyboardIn == "LetrB" || keyboardIn == "LetrC" ||
                     keyboardIn == "LetrD" || keyboardIn == "LetrE" || keyboardIn == "LetrF" ||
                     keyboardIn == "LetrG" || keyboardIn == "LetrH" || keyboardIn == "LetrI" ||
                     keyboardIn == "LetrJ" || keyboardIn == "LetrK" || keyboardIn == "LetrL" ||
                     keyboardIn == "LetrM" || keyboardIn == "LetrN" || keyboardIn == "LetrO" ||
                     keyboardIn == "LetrP" || keyboardIn == "LetrQ" || keyboardIn == "LetrR" ||
                     keyboardIn == "LetrS" || keyboardIn == "LetrT" || keyboardIn == "LetrU" ||
                     keyboardIn == "LetrV" || keyboardIn == "LetrW" || keyboardIn == "LetrX" ||
                     keyboardIn == "LetrY" || keyboardIn == "LetrZ")
            {
                str += keyboardIn[4].ToString();
                return true;
            }
            else if (keyboardIn == "Num1")
            {
                str += "!";
                return true;
            }
            else if (keyboardIn == "Num2")
            {
                str += "@";
                return true;
            }
            else if (keyboardIn == "Num3")
            {
                str += "#";
                return true;
            }
            else if (keyboardIn == "Num4")
            {
                str += "$";
                return true;
            }
            else if (keyboardIn == "Num5")
            {
                str += "%";
                return true;
            }
            else if (keyboardIn == "Num6")
            {
                str += "^";
                return true;
            }
            else if (keyboardIn == "Num7")
            {
                str += "&";
                return true;
            }
            else if (keyboardIn == "Num8")
            {
                str += "*";
                return true;
            }
            else if (keyboardIn == "Num9")
            {
                str += "(";
                return true;
            }
            else if (keyboardIn == "Num0")
            {
                str += ")";
                return true;
            }
            else if (keyboardIn == "Lbracket")
            {
                str += "{";
                return true;
            }
            else if (keyboardIn == "Rbracket")
            {
                str += "}";
                return true;
            }
            else if (keyboardIn == "SemiColon")
            {
                str += ":";
                return true;
            }
            else if (keyboardIn == "Quote")
            {
                str += "\"";
                return true;
            }
            else if (keyboardIn == "Comma")
            {
                str += "<";
                return true;
            }
            else if (keyboardIn == "Period")
            {
                str += ">";
                return true;
            }
            else if (keyboardIn == "Slash")
            {
                str += "?";
                return true;
            }
            else if (keyboardIn == "Backslash")
            {
                str += "|";
                return true;
            }
            else if (keyboardIn == "Dash")
            {
                str += "_";
                return true;
            }
            else if (keyboardIn == "Equal")
            {
                str += "+";
                return true;
            }
            else if (keyboardIn == "Tilde")
            {
                str += "~";
                return true;
            }
            else if (keyboardIn == "Plus")
            {
                str += "+";
                return true;
            }
            else if (keyboardIn == "Minus")
            {
                str += "-";
                return true;
            }
            else if (keyboardIn == "Star")
            {
                str += "*";
                return true;
            }
            else if (keyboardIn == "Slash")
            {
                str += "/";
                return true;
            }

            return false;
        }

        //returns the held string
        public string getString()
        {
            return str;
        }
    }
}