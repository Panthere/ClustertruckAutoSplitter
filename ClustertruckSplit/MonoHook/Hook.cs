using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading;

namespace ClustertruckSplit
{
    public static class Hook
    {
        public static GameObject mainObj;
        public static Controller controlObj;

        public static void Initialize()
        {
            if (!Main.IsLive)
                Main.IsLive = true;


            new Thread(delegate()
            {
                while (Main.IsLive)
                {
                    try
                    {
                        if (mainObj == null)
                        {
                            mainObj = new GameObject();
                            controlObj = mainObj.AddComponent<Controller>();
                            UnityEngine.Object.DontDestroyOnLoad(mainObj);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Hook exception: {0}", ex);
                    }
                    Thread.Sleep(5000);
                }
            }).Start();

            Main.LoadSplitter();

        }
    }

    public class Controller : MonoBehaviour
    {

        public static InfoRetriever retriever;

        public GameObject system;

        public void Start()
        {
            try
            {
                system = new GameObject();

                UnityEngine.Object.DontDestroyOnLoad(system);

                retriever = system.AddComponent<InfoRetriever>();
            }
            catch (Exception ex)
            {
                Logger.Log("Controller exception: {0}", ex);
            }
        }
    }

    public class InfoRetriever : MonoBehaviour
    {
        public void Update()
        {
            try
            {
                if (Variables.CurrentPlayer == null || Variables.CurrentPlayer.gameObject == null)
                {
                    Variables.CurrentPlayer = UnityEngine.Object.FindObjectOfType<player>();
                }
            }
            catch (Exception ex)
            {
                Logger.Log("InfoRet exception: {0}", ex);
            }
        }
    }
}
