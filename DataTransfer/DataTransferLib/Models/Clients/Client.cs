﻿using DataTransferLib.Models.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DataTransferLib.Models.Clients
{
    public class Client : IClient
    {
        protected TcpClient _client;
        private Thread _listenThread;
        private NetworkStream _networkStream => _client.GetStream();
        
        public bool IsConnected { get; set; }

        public Client(TcpClient client)
        {
            _client = client;
            Start();
        }

        public Client(string ip, int port)
        {
            _client = new TcpClient();

            if (IPAddress.TryParse(ip, out IPAddress clientIP))
            {
                _client.Connect(clientIP, port);
            }
            else
            {
                throw new Exception();
            }
            Start();
        }
        
        public void Start()
        {
            SendMessage("fff");
        }

        public void Stop()
        {
            _client.Close();
        }

        public void Listen()
        {
            byte[] data = new byte[256];
            StringBuilder response = new StringBuilder();
            do
            {
                int bytes = _networkStream.Read(data, 0, data.Length);
                response.Append(Encoding.ASCII.GetString(data, 0, bytes));
            }
            while (_networkStream.DataAvailable);

            var eventArgs = new MessageEventArgs
            {
                Message = response.ToString(),
                MessageTime = DateTime.Now,
                Client = this
            };
        }

        public virtual void SendMessage(object content)
        {
            string message = content.ToString();
            byte[] data = Encoding.ASCII.GetBytes(message);
            _networkStream.Write(data, 0, data.Length);
        }

        public NetworkStream GetStream() => _networkStream;
    }
}