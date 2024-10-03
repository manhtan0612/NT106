using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
namespace Client
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        IPEndPoint IP;
        Socket client;
        //ket noi
        void Connect()
        {
            IP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10000);  //IPEndPoint(IPAddress.Any, 0);
            client = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.IP);
            try
            {
                client.Connect(IP);
                MessageBox.Show("Connected to server");
            }
            catch {
                MessageBox.Show("Don't connect to servers");
                return; 
            }

            Thread listen = new Thread(Receive);
            listen.IsBackground = true;
            listen.Start();

        }

        //dong ket noi
        void Close()
        {
            client.Close();
        }

        //gui tin
        void Send()
        {
            if (richTextBox1.Text != string.Empty)
            {
                client.Send(Serialize(richTextBox1.Text));
                addMessage(richTextBox1.Text);
            }
        }

        //nhan tin
        void Receive()
        {
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
            catch {
                Close();
            }
        }

        //add message
        void addMessage(string message)
        {
            listView1.Items.Add(new ListViewItem() {Text = message});
            richTextBox1.Clear();
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

        private void Client_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }

        private void btSend_Click(object sender, EventArgs e)
        {
            Send();
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            if (btConnect.Text == "Connect")
            {
                Connect();
                btConnect.Text = "Disconnect";
            }
            else
            {
                Close();
                btConnect.Text = "Connect";
            }
        }
    }
}
