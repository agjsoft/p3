using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PvPPokerClient
{
    public partial class FormLobby : Form
    {
        public FormLobby()
        {
            InitializeComponent();
        }

        private void FormLobby_Load(object sender, EventArgs e)
        {
            Text = Client.mApplicationTitle;
        }
    }
}