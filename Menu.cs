using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYA___Max
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void SeriesButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form Series = new Series();
            Series.Show();
        }

        private void moviesButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form Movies = new Movies();
            Movies.Show();
        }
        private void HelpButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form HelpPage = new HelpPage();
            HelpPage.Show();
        }
        private void quitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
