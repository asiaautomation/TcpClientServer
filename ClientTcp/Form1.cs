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
        delegate void SetTextCallback(string text);


        Boolean connected = false;
        TcpClient client;

        NetworkStream ns;
        
        Thread t = null;

       // IPAddress ipAdd;
      //  int port = 4545;

 
       
        public Form1()
        {
            InitializeComponent();
            
            //client = new TcpClient("192.168.0.5",4545 );

           // ns = client.GetStream();

           // String s = "Connected";

         //   byte[] byteTime = Encoding.ASCII.GetBytes(s);

         //   ns.Write(byteTime, 0, byteTime.Length);


       //    t = new Thread(DoWork);

         //   t.Start();
        }
        public void connect()
        {
            try
            {
                client = new TcpClient(textBox2.Text, 4545);
                connected = true;
            }
            catch(SocketException SE)
            {
                MessageBox.Show("enter correct Address");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String s = "\n"+"Client : "+textBox1.Text;
            textBox1.Text = " ";
            richTextBox1.Text = richTextBox1.Text + s;
            byte[] byteTime = Encoding.ASCII.GetBytes(s);

            ns.Write(byteTime, 0, byteTime.Length);
              
        }
        public void DoWork()
        {

            byte[] bytes = new byte[1024];
            try
            {
                while (true)
                {

                    int bytesRead = ns.Read(bytes, 0, bytes.Length);

                    this.SetText(Encoding.ASCII.GetString(bytes, 0, bytesRead));



                }
            }
            catch(System.IO.IOException e)
            {
                ns.Close();
                client.Close();
                this.SetText("\n Server Disconnected");
            }
            
      
            

        }
        private void SetText(string text)
        {

           

                if (this.richTextBox1.InvokeRequired)
                {

                    SetTextCallback d = new SetTextCallback(SetText);

                    this.Invoke(d, new object[] { text });

                }

                else
                {
                    this.richTextBox1.Text = this.richTextBox1.Text + "  ";
                    this.richTextBox1.Text = this.richTextBox1.Text + text;

                }
     
      
           

        }

        private void button2_Click(object sender, EventArgs e)
        {

            //   ipAdd = IPAddress.Parse(textBox2.Text);

            if (textBox2.Text == "")
            {
                MessageBox.Show("Enter Ip Address Of SERver");
            }
            {

                connect();
                if (connected == true)
                {
                    ns = client.GetStream();






                    String s = "Client Connected";

                    byte[] byteTime = Encoding.ASCII.GetBytes(s);

                    ns.Write(byteTime, 0, byteTime.Length);

                    textBox2.Text = " ";
                    t = new Thread(DoWork);

                    t.Start();

                    richTextBox1.Text = richTextBox1.Text + "\n Server Connected";
                }
            }

        }
    }
}