//Keeps track of game time

using System.Diagnostics;

namespace Asteroids
{
    class GameTimer
    {
        private bool watchSwitch = true;
        private Stopwatch stopWatch = new Stopwatch();
        private double time = 0;

        //starts and stops the stopwatch
        public void toggleStopwatch()
        {
            if (watchSwitch == true)
            {
                stopWatch.Stop();
                watchSwitch = false;
            }
            else if (watchSwitch == false)
            {
                stopWatch.Restart();
                stopWatch.Start();
                watchSwitch = true;
            }
        }

        //restarts the stop watch
        public void restartWatch()
        {
            stopWatch.Stop();
            stopWatch.Restart();
            stopWatch.Start();
        }

        //returns the time value
        public double getTimeMilliseconds()
        {
            time = stopWatch.Elapsed.TotalMilliseconds;
            return time;
        }

        //returns the time value
        public double getTimeSecounds()
        {
            time = stopWatch.Elapsed.TotalSeconds;
            return time;
        }

        //returns the time value
        public double getTimeMinutes()
        {
            time = stopWatch.Elapsed.TotalMinutes;
            return time;
        }

        //returns the amount of time the last cycle was off by
        public double getDeltaTime(int speed)
        {
            time = stopWatch.Elapsed.TotalMilliseconds;
            return speed / time;
        }
    }

    //keeps trak of how many loops have gone by, and when a specific amount has passed
    class Ticker
    {
        private int counter = 0;

        //adds one too the total amount
        public void increment()
        {
            counter++;
        }

        //returns the current amount in the counter
        public int currentAmount()
        {
            return counter;
        }

        //returns if a value is at or greater than the counter
        public bool atAmount(int amount)
        {
            if (amount <= counter)
            {
                return true;
            }
            return false;
        }

        //resets the counter
        public void resetCounter()
        {
            counter = 0;
        }
    }
}   