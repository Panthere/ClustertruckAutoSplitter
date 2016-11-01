using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO.Pipes;
using System.IO;

namespace ClustertruckSplit
{
    public class ClientPipe
    {
        NamedPipeClientStream client;
        StreamReader pipeReader;
        StreamWriter pipeWriter;

        public bool IsPausedGameTime;
        public bool IsPaused;
        public bool HasStarted;
        public ClientPipe(string name)
        {
            client = new NamedPipeClientStream(name);
        }

        public void Split()
        {
            SendRaw("split");
        }

        public void Start()
        {
            SendRaw("startorsplit");
            HasStarted = true;
        }

        public void Pause()
        {
            SendRaw("pause");
            IsPaused = true;
        }

        public void Resume()
        {
            SendRaw("resume");
            IsPaused = false;
        }

        public void PauseGameTime()
        {
            SendRaw("pausegametime");
            IsPausedGameTime = true;
        }

        public void ResumeGameTime()
        {
            SendRaw("unpausegametime");
            IsPausedGameTime = false;
        }

        public void Reset()
        {
            SendRaw("reset");
            HasStarted = false;
        }

        private void SendRaw(string data)
        {
            pipeWriter.WriteLine(data);
            pipeWriter.Flush();
        }

        public bool Connect()
        {
            try
            {
                client.Connect();
                if (client.IsConnected)
                {
                    pipeReader = new StreamReader(client);
                    pipeWriter = new StreamWriter(client);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error: {0}", ex);
            }
            return false;
        }
    }
}
