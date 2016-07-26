﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;

namespace Chat
{
    public partial class Form1 : Form
    {
        enum Status {OFFLINE, ONLINE};
        string serIp;
        string locIp;
        string name;
        int port;
        Socket client;
        Socket reciever;
        Thread recieveThread;
        Status status;
        List<Chater> chaters;
        public Form1()
        {
            InitializeComponent();
            this.Width = 200;
            this.Height = 360;
            ChatViewer.Visible = false;
            MesBox.Visible = false;
            SendBtn.Visible = false;
            MinimizeBtn.Visible = false;
            this.Top = Screen.PrimaryScreen.Bounds.Height - this.Height-40;
            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width;
            chaters = new List<Chater>();
            string[] sets;
            if (!File.Exists("settings.txt"))
            {
                Settings set = new Settings();
                set.ShowDialog();
                set.Close();
                if (!File.Exists("settings.txt"))
                {
                    MessageBox.Show("Настройки подключения не найдены", "Ошибка");
                }
                else
                {
                    sets = File.ReadAllLines("settings.txt");
                    serIp = sets[0];
                    locIp = sets[1];
                    port = int.Parse(sets[2]);
                    name = sets[3];
                    client = new Socket(AddressFamily.InterNetwork,
                        SocketType.Stream, ProtocolType.Tcp);
                    client.Connect(new IPEndPoint(IPAddress.Parse(serIp), port));
                    client.Send(Encoding.ASCII.GetBytes("{REGISTRATION}"+MesId()+
                        "{TEXT}" + name + "{FINAL}"));
                    Listen();
                }
            }
            else
            {
                sets = File.ReadAllLines("settings.txt");
                serIp = sets[0];
                locIp = sets[1];
                port = int.Parse(sets[2]);
                name = sets[3];
            }
            Text = "Chat: " + name + " - " + status.ToString();
            reciever = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            recieveThread = new Thread(DataRecieve);
        }

        public void Listen()
        {
            client = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            client.Bind(new IPEndPoint(IPAddress.Parse(locIp), port));
            client.Listen(1);
            while (true)
            {
                Socket handler = client.Accept();

                //Входящее соединение необходимо обработать
                ChatViewer.Text+=String.Format("Принято соединение от {0}", handler.RemoteEndPoint);

                ChatViewer.Text += String.Format("Отправляем сообщениею..");
                handler.Send(Encoding.ASCII.GetBytes("{OK}"));

                // Соединение необходимо закрыть
                ChatViewer.Text += String.Format("Закрытие соединение");
                handler.Close();
            }
        }
        public void NewMessage(string mes)
        {
            ChatViewer.Text += mes;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Settings set = new Settings();
            set.ShowDialog();
            set.Close();
            if (!File.Exists("settings.txt"))
            {
                MessageBox.Show("Настройки подключения не найдены", "Ошибка");
            }
            else
            {
                string[] sets = File.ReadAllLines("settings.txt");
                serIp = sets[0];
                port = int.Parse(sets[1]);
            }
            /*
            if (!ChatViewer.Visible)
            {
                this.Width += 300;
                this.Left -= 300;
                ChatViewer.Visible = true;
                MinimizeBtn.Visible = true;
                MesBox.Visible = true;
                SendBtn.Visible = true;
            }
            else
            {
                this.Width = 200;
                this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width;
                MesBox.Visible = false;
                SendBtn.Visible = false;
                ChatViewer.Visible = false;
                MinimizeBtn.Visible = false;
            }
            */
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            TcpClient tcpClient= new TcpClient("172.20.53.7", 1990);
            NetworkStream ns = tcpClient.GetStream();
            string mes = MesBox.Text+"{FINAL}";
            ns.Write(Encoding.ASCII.GetBytes(mes), 0, mes.Length);
            ns.Close();
            tcpClient.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (status == Status.ONLINE)
            {
                SendInfo("{STATUS}OFFLINE{FINAL}");
                status = Status.OFFLINE;
            }
        }
        private IPEndPoint ServerOptions(string ip, int port)
        {
            return new IPEndPoint(IPAddress.Parse(ip), port);
        }
        private void SendInfo(string info)
        {
            client = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(ServerOptions("127.20.53.7", 1990));
                client.Send(Encoding.ASCII.GetBytes(info));
                byte[] byteAnswer = new byte[1024];
                int numBytes = client.Receive(byteAnswer);
                string answer = Encoding.ASCII.GetString(byteAnswer, 0, numBytes);
                if (answer == "{OK}")
                {
                    ChatViewer.Text += "OK!/n";
                    status = Status.ONLINE;
                }
                else
                    ChatViewer.Text += "Error!!!/n";
            }
            catch (SocketException)
            {
                MessageBox.Show("Can not connect to Chat Server. Check your connection or try it later.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                client.Close();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

        }

        private void AddGroup_Click(object sender, EventArgs e)
        {
            //UsersTree.Nodes.Add()
            //UsersTree.LabelEdit = true;
            //UsersTree.Nodes["New Group"].BeginEdit();
            //UsersTree.LabelEdit = false;
        }

        private void onlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendInfo("{STATUS}" + Status.ONLINE.ToString() + "{FINAL}");
        }
        private void offlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendInfo("{STATUS}" + Status.OFFLINE.ToString() + "{FINAL}");
        }

        private void addFriend_Click(object sender, EventArgs e)
        {
            SearchFriend searchFriend = new SearchFriend();
            searchFriend.ShowDialog();
        }

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            this.Width = 200;
            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width;
            MesBox.Visible = false;
            SendBtn.Visible = false;
            ChatViewer.Visible = false;
            MinimizeBtn.Visible = false;
        }

        private void DataRecieve()
        {
            
        }
        private string MesId()
        {
            StringBuilder text = new StringBuilder(17);
            text = text.Append(DateTime.Now.Year.ToString());
            text = text.Append(DateTime.Now.Month.ToString());
            text = text.Append(DateTime.Now.Day.ToString());
            text = text.Append(DateTime.Now.Hour.ToString());
            text = text.Append(DateTime.Now.Minute.ToString());
            text = text.Append(DateTime.Now.Second.ToString());
            text = text.Append(DateTime.Now.Millisecond.ToString());
            return text.ToString();
        }
    }
    class Chater
    {
        public enum Status { Online, Offline };
        public string ip;
        public string name;
        public Status status;


        public Chater(string chater)
        {
            string[] temp = chater.Split('\t');
            this.ip = temp[0];
            this.name = temp[1];
            status = Status.Offline;
        }
        public bool IsOnline()
        {
            if (status == Status.Offline) return false;
            else return true;
        }
    }
}
