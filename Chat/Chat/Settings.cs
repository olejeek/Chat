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
        string serIp;
        string locIp;
        string port;
        string name;
        public Settings()
        {
            InitializeComponent();
            if (File.Exists("settings.txt"))
            {
                string[] sets = File.ReadAllLines("settings.txt");
                serIp = sets[0];
                locIp = sets[1];
                port = sets[2];
                name = sets[3];
                serIpBox.Text = sets[0];
                locIpBox.Text = sets[1];
                portBox.Text = sets[2];
                nameBox.Text = sets[3];
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (serIpBox.Text!=serIp || portBox.Text!=port ||
                locIpBox.Text!=locIp || nameBox.Text!=name)
            {
                if (MessageBox.Show("Вы точно хотите сохранить изменения?", "Сохранение изменений", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    serIp = serIpBox.Text;
                    locIp = locIpBox.Text;
                    port = portBox.Text;
                    name = nameBox.Text;
                    File.WriteAllLines("settings.txt", new string[4] {serIp, locIp ,port, name});
                    Close();
                }
            }
        }
    }
}
