using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Chat
{
    public partial class Settings : Form
    {
        string ip;
        string port;
        public Settings()
        {
            InitializeComponent();
            if (File.Exists("settings.txt"))
            {
                string[] sets = File.ReadAllLines("settings.txt");
                ip = sets[0];
                port = sets[1];
                ipBox.Text = sets[0];
                portBox.Text = sets[1];
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ipBox.Text!=ip || portBox.Text!=port)
            {
                if (MessageBox.Show("Вы точно хотите сохранить изменения?", "Сохранение изменений", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ip = ipBox.Text;
                    port = portBox.Text;
                    File.WriteAllLines("settings.txt", new string[2] {ip, port});
                    Close();
                }
            }
        }
    }
}
