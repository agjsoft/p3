using System;
using System.Collections.Concurrent;
using System.Windows.Forms;

namespace PvPPokerClient
{
    public partial class Form1 : Form
    {
        public static ConcurrentQueue<string> tbConsoleQueue = new ConcurrentQueue<string>();
        public static ConcurrentQueue<bool> btnLoginQueue = new ConcurrentQueue<bool>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = Client.mApplicationTitle;
            timer1.Start();
            Client.Init();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            Client.Login();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string tbConsoleData;
            if (tbConsoleQueue.TryDequeue(out tbConsoleData))
                tbConsole.AppendText(tbConsoleData + "\r\n");

            bool btnLoginData;
            if (btnLoginQueue.TryDequeue(out btnLoginData))
                btnLogin.Enabled = btnLoginData;
        }
    }
}
