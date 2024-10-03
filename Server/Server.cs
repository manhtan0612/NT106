using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }


        IPEndPoint IP;
        Socket server;
        List<Socket> clientList;
        //ket noi
        void Connect()
        {
            IP = new IPEndPoint(IPAddress.Any, 10000);  //IPEndPoint(IPAddress.Any, 0);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            server.Bind(IP);

            Thread listen = new Thread(() =>
            {
               
                        server.Listen(10); //client in queue
                        Socket client = server.Accept();
                        clientList.Add(client);
                    
                        Thread receive = new Thread(Receive);
                        receive.IsBackground = true;
                        receive.Start(client);
              
            });
            listen.IsBackground = true;
            listen.Start();

        }

        //dong ket noi
        void Close()
        {
            server.Close();
        }

        //gui tin
        void Send(Socket client)
        {
            if (richTextBox1.Text != string.Empty)
            {
                string responseData = richTextBox1.Text.ToString();
                byte[] responseBytes = Encoding.UTF8.GetBytes(responseData);
                client.Send(responseBytes);
            }
        }

        //nhan tin
        void Receive(object obj)
        {
            Socket client = obj as Socket;
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024 * 5000];
                    client.Receive(data);
                    string message = (string)Deserialize(data);
                    addMessage(message);
                }
            }
            catch
            {
                clientList.Remove(client);
                client.Close(); 
            }
        }

        //add message
        void addMessage(string message)
        {
            listView1.Items.Add(new ListViewItem() { Text = message });
        }
        //phan manh file
        byte[] Serialize(object obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        }

        //gom manh file
        object Deserialize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
        }

        private void Server_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }

        private void btSend_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
            foreach (Socket item in clientList)
            {
                Send(item);
            }
            addMessage(richTextBox1.Text);
                richTextBox1.Clear(); 
            }
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            if (btStart.Text=="Start")
            {
                Connect();
                btStart.Text = "Stop";
            }
            else
            {
                Close();
                btStart.Text = "Start";
            }
        }
    }
}
