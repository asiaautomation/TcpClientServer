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
        int disconnect = 0;

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
        public Boolean connect()
        {

            try
            {
                client = new TcpClient(textBox2.Text, 4545);
                connected = true;
                textBox1.Enabled = true;
                button1.Enabled = true;


                return true;

            }
            catch (SocketException SE)
            {
                MessageBox.Show("enter correct Address");
                return false;
            }
            
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            richTextBox1.Enabled = false;
            button1.Enabled = false;
         
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                String s = "\n" + "Client : " + textBox1.Text;
                textBox1.Text = " ";
                richTextBox1.Text = richTextBox1.Text + s;
                byte[] byteTime = Encoding.ASCII.GetBytes(s);

                ns.Write(byteTime, 0, byteTime.Length);
                disconnect = 1;
            }
            catch (Exception sed)
            {
                MessageBox.Show("Server Disconected");
            }

            finally
            {
                if (disconnect == 0)
                {

                    button1.Enabled = false;
                    textBox1.Enabled = false;
                }
            }
              
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
            catch (System.IO.IOException e)
            {
                ns.Close();
                client.Close();


                this.SetText("\n Server Disconnected");


            }
           
                if (disconnect == 1)
                {
                    button1.Enabled = false;
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
            else {

                connect();
                if(connected == true)
                {
                    ns = client.GetStream();






                    String s = "Client Connected";

                    byte[] byteTime = Encoding.ASCII.GetBytes(s);

                    ns.Write(byteTime, 0, byteTime.Length);

                    textBox2.Text = " ";
                    t = new Thread(DoWork);

                    t.Start();
                 
                    richTextBox1.Text = richTextBox1.Text + "\n Server Connected";
                    connected = false;
                   
                }
            }

        }
    }
}