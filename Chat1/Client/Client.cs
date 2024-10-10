using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client
{
    public partial class Client : Form
    {
        TcpClient client;
        StreamWriter sWriter;
        Thread clientThread;
        bool stopTcpClient = true;
        public Client()
        {
            InitializeComponent();
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Connect_Click(object sender, EventArgs e) //Connect
        {
            try
            {
                stopTcpClient = false;
                client = new TcpClient();
                client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1210));
                sWriter = new StreamWriter(client.GetStream());
                sWriter.AutoFlush = true;
                sWriter.WriteLine(Username.Text);
                clientThread = new Thread(ClientRecv);
                clientThread.Start();
                MessageBox.Show("Connected");

            }
            catch
            {

            }
        }
        private void Send_Click(object sender, EventArgs e)  //Send
        {
            sWriter.WriteLine($"{Friend.Text};{tbMess.Text}");
            tbMess.Text = "";
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

        private void ClientRecv()
        {
            StreamReader sr = new StreamReader(client.GetStream());
            try
            {
                while(!stopTcpClient && client.Connected)
                {
                    Application.DoEvents();
                    string data = sr.ReadLine();
                    UpdateChatHistoryThreadSafe($"{data}\n");
                }
            }
            catch (SocketException sockEx)
            {
                client.Close();
                sr.Close();

            }
        }

        
    }
}
