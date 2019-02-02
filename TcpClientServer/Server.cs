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




        delegate void SetTextCallback(string text);

        TcpListener listener;

        TcpClient client;
        IPAddress localAdd;

        NetworkStream ns;

        Thread t = null;


      
        
        public Server()
        {
            InitializeComponent();
             localAdd = IPAddress.Parse("192.168.0.5");
            listener = new TcpListener(localAdd,4545);

            listener.Start();

          client = listener.AcceptTcpClient();

            ns = client.GetStream();

            t = new Thread(DoWork);
           

            t.Start();
            
        }


        private void Server_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                String s = "\n" + "Server : " + textBox1.Text;
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
                catch (System.IO.IOException e)
                {
                    
                    ns.Close();
                    client.Close();
                    this.SetText("\n Client Disconnected....");
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
                
                this.richTextBox1.Text = this.richTextBox1.Text + text;

            }

        }
    }
}
