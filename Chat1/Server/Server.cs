using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Server : Form
    {
        Thread listenThread;
        TcpListener listener;
        bool stopChatServer = true;
        Dictionary<string, TcpClient> dict = new Dictionary<string, TcpClient>();
        public Server()
        {
            InitializeComponent();
        }

        private void Server_Load(object sender, EventArgs e)
        {

        }

        void Listen()
        {
            try
            {
                listener = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1210));
                listener.Start();
                while (!stopChatServer)
                {
                    TcpClient client = listener.AcceptTcpClient();

                    StreamReader sr = new StreamReader(client.GetStream());
                    StreamWriter sw = new StreamWriter(client.GetStream());
                    sw.AutoFlush = true;
                    string username = sr.ReadLine();
                    if (string.IsNullOrEmpty(username))
                    {
                        sw.WriteLine("Please pick a username");
                        client.Close();
                    }
                    else
                    {
                        if (!dict.ContainsKey(username))
                        {
                            Thread clientThread = new Thread(() => this.ClientRecv(username, client));
                            dict.Add(username, client);
                            clientThread.Start();
                        }
                        else
                        {
                            sw.WriteLine("Username already exist, pick another one");
                            client.Close();
                        }
                    }
                }
            }
            catch
            {

            }
            }
        public void ClientRecv(string username, TcpClient tcpClient)
        {
            StreamReader sr = new StreamReader(tcpClient.GetStream());
            try
            {
                while (!stopChatServer)
                {
                    Application.DoEvents();
                    string userNameAndMsg = sr.ReadLine();
                    string[] arrPayload = userNameAndMsg.Split(';');
                    string friendUsername = arrPayload[0];
                    string msg = arrPayload[1];
                    string formattedMsg = $" {username}: {msg}\n";
                    TcpClient friendTcpClient;

                    if (dict.TryGetValue(friendUsername, out friendTcpClient))
                    {
                        StreamWriter sw = new StreamWriter(friendTcpClient.GetStream());
                        sw.WriteLine(formattedMsg);
                        sw.AutoFlush = true;
                    }
                    StreamWriter sw2 = new StreamWriter(tcpClient.GetStream());
                    sw2.WriteLine(formattedMsg);
                    sw2.AutoFlush = true;
                    UpdateChatHistoryThreadSafe(formattedMsg);
                }
            }
            catch (SocketException sockEx)  
            {
                tcpClient.Close();
                sr.Close();

            }

        }
        private delegate void SafeCallDelegate(string text);

        private void UpdateChatHistoryThreadSafe(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                var d = new SafeCallDelegate(UpdateChatHistoryThreadSafe);
                richTextBox1.Invoke(d, new object[] { text });
            }
            else
            {
                richTextBox1.Text += text;
            }
        }
        private void button1_Click_1(object sender, EventArgs e)  //Listen
        {
            if (stopChatServer)
            {
                stopChatServer = false;
                listenThread = new Thread(this.Listen);
                listenThread.Start();
                MessageBox.Show(@"Start listening for incoming connections");
                button1.Text = @"Stop";
            }
            else
            {
                stopChatServer = true;
                button1.Text = @"Start listening";
                listener.Stop();
                listenThread = null;

            }
        }

       
    }
    }

