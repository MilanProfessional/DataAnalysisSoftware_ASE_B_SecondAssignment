using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DataAnalysisSoftware_ASE_B_FirstAssignment
{
    public partial class HomePage : Form
    {

        private void Player_MediaError(object pMediaObject)
        {
            MessageBox.Show("Cannot play media file.");
            this.Close();
        }


        public HomePage()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Data Analysis Software By:" + Environment.NewLine +
                "Name: Milan Babu Adhikari" + Environment.NewLine +
                "Email: milanadhikari09@live.com" + Environment.NewLine +
                "Contact No.: +9779803818797" + Environment.NewLine +
                "Developed in: Microsoft Visual Studio 2017 Community" + Environment.NewLine,
                "Version 1.0.8 Freeware"
                );
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do You want to quit ?", "Quit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Hide();
            new Startup().Show();
        }
    }
}
