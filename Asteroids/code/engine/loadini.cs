//includes all functionality needed to save and load from an ini file

using System.IO;

namespace Asteroids
{
    class LoadINI
    {
        string tempStr;
        string fileName;
        string[] fileContent;

        public LoadINI(string file)
        {
            if (fileExists(file))
            {
                fileName = file;
                fileContent = System.IO.File.ReadAllLines(fileName);
            }
        }

        //returns the whole file
        public string getFile()
        {
            tempStr = string.Empty;
            for (int i = 0; i < fileContent.Length; i++)
            {
                tempStr += fileContent[i] + "\n";
            }
            return tempStr;
        }

        //returns if the given file exists
        public string getFileName()
        {
            return fileName;
        }

        //tests if a given file exists
        public bool fileExists(string file)
        {
            return File.Exists(file);
        }

        //sets a new file to access
        public void setFile(string file)
        {
            if (File.Exists(file))
            {
                fileName = file;
                fileContent = System.IO.File.ReadAllLines(fileName);
            }
        }

        //tests if given key is in the file
        public bool inFile(string key)
        {
            for (int i = 0; i < fileContent.Length; i++)
            {
                if (fileContent[i].Contains(key + " ="))
                {
                    return true;
                }
            }
            return false;
        }

        //returns value of given key from file
        public string getValue(string key)
        {
            for (int i = 0; i < fileContent.Length; i++)
            {
                if (fileContent[i].Contains(key + " ="))
                {
                    return fileContent[i].Replace(key, " ").Replace('=', ' ').Trim();
                }
            }
            return "error";
        }

        //updates a key in the file to a given value
        public void updateFile(string key, string newValue)
        {
            for (int i = 0; i < fileContent.Length; i++)
            {
                if (fileContent[i].Contains(key + " ="))
                {
                    fileContent[i] = key + " = " + newValue;
                    File.WriteAllLines(fileName, fileContent);
                    break;
                }
            }
        }
    }
}