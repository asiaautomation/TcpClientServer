using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

using System.Collections; 



namespace TcpClientServer
{
    public partial class Server : Form
    {







        const int PORT_NO = 5000;
        const string SERVER_IP = "192.168.0.5";
        IPAddress localAdd;
        TcpListener listener;
        TcpClient client;
        NetworkStream nwStream;
        public Server()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {




            localAdd = IPAddress.Parse(SERVER_IP);
             listener = new TcpListener(localAdd, PORT_NO);
            Console.WriteLine("Listening...");
            listener.Start();

            //---incoming client connected---
            client = listener.AcceptTcpClient();

            //---get the incoming data through a network stream---
            NetworkStream nwStream = client.GetStream();

            byte[] buffer = new byte[client.ReceiveBufferSize];
        
            //---read incoming stream---
            int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

            //---convert the data received into a string---
            string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received : " + dataReceived);
            label2.Text = dataReceived;
            //---write back the text to the client---
            Console.WriteLine("Sending back : " + dataReceived);
            nwStream.Write(buffer, 0, bytesRead);
            client.Close();
            listener.Stop();
            Console.ReadLine();




        }


        private void button1_Click(object sender, EventArgs e)
        {
            String str = textBox1.Text;
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(str);
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
        }
    }
}
