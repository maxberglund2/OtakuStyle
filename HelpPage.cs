using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYA___Max
{
    public partial class HelpPage : Form
    {
        public HelpPage()
        {
            InitializeComponent();
        }

        private void HelpPage_Load(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;

        }

        private void CloseHelpPage_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form Menu = new Menu();
            Menu.Show();
        }

        private void CloseHelpForm_Click(object sender, EventArgs e)
        {
            this.Close();
            Form Menu = new Menu();
            Menu.Show();
        }
    }
}
