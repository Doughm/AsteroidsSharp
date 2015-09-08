//plays and manages audio files

using SFML.Audio;
using System.Collections.Generic;

namespace Asteroids
{
    class Audio
    {
        Dictionary<string, Sound> soundDictionary = new Dictionary<string, Sound>();
        Dictionary<string, Music> musicDictionary = new Dictionary<string, Music>();
        bool soundOn = true;

        //sets sound on or off
        public void setSoundOn(bool active)
        {
            soundOn = active;
        }

        //returns if the sound is on or off
        public bool getSoundOn()
        {
            return soundOn;
        }

        //turns sound on or off
        public void soundToggle()
        {
            if (soundOn == true)
            {
                soundOn = false;
            }
            else
            {
                soundOn = true;
            }
        }

        //adds a new sound into memory
        public void addSample(string name, string file)
        {
            if (soundOn)
            {
                soundDictionary.Add(name, new Sound());
                soundDictionary[name].SoundBuffer = new SoundBuffer(file);
            }
        }

        //plays a sample
        public void samplePlay(string name)
        {
            if (soundOn && soundDictionary.ContainsKey(name))
            {
                soundDictionary[name].Play();
            }
        }

        //pauses a sample
        public void samplePause(string name)
        {
            if (soundOn && soundDictionary.ContainsKey(name))
            {
                soundDictionary[name].Pause();
            }
        }

        //sets if a sample loops
        public void sampleLoop(string name, bool loop)
        {
            if (soundOn && soundDictionary.ContainsKey(name))
            {
                soundDictionary[name].Loop = loop;
            }
        }

        //stops a sample playing
        public void sampleStop(string name)
        {
            if (soundOn && soundDictionary.ContainsKey(name))
            {
                soundDictionary[name].Stop();

            }
        }

        //sets the volume of the sample [0 -100]
        public void sampleVolume(string name, int volume)
        {
            if (soundOn && soundDictionary.ContainsKey(name))
            {
                soundDictionary[name].Volume = volume;

            }
        }

        //adds a file to play from the hard drive
        public void addMusic(string name, string file)
        {
            if (soundOn)
            {
                musicDictionary.Add(name, new Music(file));
            }
        }

        //plays a music file
        public void musicPlay(string name)
        {
            if (soundOn && soundDictionary.ContainsKey(name))
            {
                musicDictionary[name].Play();
            }
        }

        //pauses a music file
        public void musicPause(string name)
        {
            if (soundOn && soundDictionary.ContainsKey(name))
            {
                musicDictionary[name].Pause();
            }
        }

        //sets if a music file loops
        public void musicLoop(string name, bool loop)
        {
            if (soundOn && soundDictionary.ContainsKey(name))
            {
                musicDictionary[name].Loop = loop;
            }
        }

        //stops a music file from playing
        public void musicStop(string name)
        {
            if (soundOn && soundDictionary.ContainsKey(name))
            {
                musicDictionary[name].Stop();
            }
        }

        //sets the volume of the music [0 -100]
        public void musicVolume(string name, int volume)
        {
            if (soundOn && soundDictionary.ContainsKey(name))
            {
                musicDictionary[name].Volume = volume;
            }
        }

        //clears all samples from this object
        public void clearSamples()
        {
            if (soundOn)
            {
                soundDictionary.Clear();
            }
        }

        //clears all music from this object
        public void clearMusic()
        {
            if (soundOn)
            {
                musicDictionary.Clear();
            }
        }

        //clears all sounds from this object
        public void clearAll()
        {
            if (soundOn)
            {
                soundDictionary.Clear();
                musicDictionary.Clear();
            }
        }
    }
}