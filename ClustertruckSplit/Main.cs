using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using UnityEngine;

namespace ClustertruckSplit
{
    public static class Main
    {
        public static ClientPipe LiveSplit;
        public static Thread UpdateThread;
        public static bool IsLive;

        private static bool OldPlaying;
        private static bool OldDead;
        private static int OldLevel;


        private static bool HasWon;

        public static void LoadSplitter()
        {
            try
            {
                LiveSplit = new ClientPipe(Settings.PipeName);
                LiveSplit.Connect();


                IsLive = true;

            }
            catch (Exception ex)
            {
                Logger.Log("Error on loading: {0}", ex);
                return;
            }

            UpdateThread = new Thread(delegate() { while (IsLive) { Update(); } });
            UpdateThread.Start();
        }

        private static void Update()
        {

            if (!HasWon && (info.currentLevel == 90 && PlayerPrefs.GetInt("BeatGame", 0) == 1))
            {
                HasWon = true;

                // Split as it is the end of the game
                LiveSplit.Split();

                return;
            }


            if (Settings.Pause)
            {
                if (Variables.CurrentPlayer != null && LiveSplit.HasStarted)
                {
                    // Hack to get private property value
                    if (Variables.CurrentPlayer.GetPrivateFieldValue<Manager>("man").IsMenuActive)
                    {
                        // pause timer?
                        LiveSplit.PauseGameTime();

                    }

                    else if (!Variables.CurrentPlayer.GetPrivateFieldValue<Manager>("man").IsMenuActive)
                    {
                        LiveSplit.ResumeGameTime();
                    }
                }
            }


            // TODO: Fix bug with dying and splitting...
            if ((info.playing && !OldPlaying) && (info.currentLevel == 1))
            {

                if (Variables.CurrentPlayer != null)
                {
                    // if we're not dead and were were not dead
                    if (!Variables.CurrentPlayer.dead && !OldDead)
                    {
                        // Reset game
                        PlayerPrefs.SetInt("BeatGame", 0);
                        HasWon = false;

                        LiveSplit.Start();
                    }


                }
                else
                {
                    PlayerPrefs.SetInt("BeatGame", 0);
                    HasWon = false;

                    LiveSplit.Start();
                }
            }

            if (Settings.Reset)
            {
                if ((info.currentLevel == 1 && OldLevel != 1) && (!info.playing))
                {
                    LiveSplit.Reset();
                }
            }

            if (Settings.IsLevelMode)
            {
                // per level split
                if (info.currentLevel > OldLevel)
                {
                    LiveSplit.Split();
                }
            }
            else
            {  
                // per world split
                if (((info.currentLevel - 1) / 10 > (OldLevel - 1) / 10))
                {
                    LiveSplit.Split();
                }
            }

            // end game boss 
            //	private sealed class <Boom>c__IteratorA : IDisposable, IEnumerator, IEnumerator<object>
            // Inside LastBoss class

            /*if (info.currentLevel == 90 && info.completedLevels == 90)
            {
                // since we set the completed levels to 0 on start this should be triggered when the game ends :s
                LiveSplit.Split();
            }*/


            // Assign the 'Old' variables
            OldLevel = info.currentLevel;
            OldPlaying = info.playing;

            if (Variables.CurrentPlayer != null)
            {
                OldDead = Variables.CurrentPlayer.dead;
            }

            Thread.Sleep(Settings.SleepTime);
        }
    }
}
