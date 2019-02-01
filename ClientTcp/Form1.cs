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
using System.IO;
using System.Threading;

namespace ClientTcp
{
    public partial class Form1 : Form
    {
        const int PORT_NO = 5000;
        const string SERVER_IP = "192.168.0.5";
        string textToSend;
        TcpClient client;
        NetworkStream nwStream;
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {





             client = new TcpClient(SERVER_IP, PORT_NO);
             nwStream = client.GetStream();
           
            textToSend = "hye";
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

            //---send the text---
            Console.WriteLine("Sending : " + textToSend);
          
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            //---read back the text---
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
            label1.Text = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
            Console.ReadLine();
            client.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client = new TcpClient(SERVER_IP, PORT_NO);
            nwStream = client.GetStream();
            textToSend = textBox1.Text;
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

           
            Console.WriteLine("Sending : " + textToSend);

            nwStream.Write(bytesToSend, 0, bytesToSend.Length);


        }
    }
}