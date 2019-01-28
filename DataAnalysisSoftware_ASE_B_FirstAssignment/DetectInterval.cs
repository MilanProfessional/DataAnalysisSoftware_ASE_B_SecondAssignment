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
    public partial class DetectInterval : Form
    {
        private Dictionary<string, object> _hrData;
        public DetectInterval(Dictionary<string, object> hrData)
        {
            InitializeComponent();
            _hrData = new IntervalDetection().GetIntervalDetectedData(hrData.ToDictionary(k => k.Key, k => k.Value as object));

            for (int i = 0; i < _hrData.Count; i++)
            {
                comboBox1.Items.Add("Interval " + (i + 1));
            }
        }

        //initializing grid row
        private void InitGrid()
        {
            dataGridView2.ColumnCount = 10;
            dataGridView2.Columns[0].Name = "Total distance covered";
            dataGridView2.Columns[1].Name = "Average speed(km/hr)";
            dataGridView2.Columns[2].Name = "Maximum speed(km/hr)";
            dataGridView2.Columns[3].Name = "Average heart rate(bpm)";
            dataGridView2.Columns[4].Name = "Maximum heart rate(bpm)";
            dataGridView2.Columns[5].Name = "Minimum heart rate(bpm)";
            dataGridView2.Columns[6].Name = "Average power(watt)";
            dataGridView2.Columns[7].Name = "Maximum power(watt)";
            dataGridView2.Columns[8].Name = "Average altitude(RPM)";
            dataGridView2.Columns[9].Name = "Maximum altitude(RPM)";

        }
        private void DetectInterval_Load(object sender, EventArgs e)
        {
            InitGrid();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex + 1;

            dataGridView2.Rows.Clear();

            var a = _hrData["data" + selectedIndex] as Dictionary<string, List<string>>;
            var b = a.ToDictionary(k => k.Key, k => k.Value as object);


            var data = new TableFiller().FillDataInSumaryTable(b, "19:12:15", null);
            dataGridView2.Rows.Add(data);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void backToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
            new Startup().Show();
        }

        private void developerInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Data Analysis Software By:" + Environment.NewLine +
               "Name: Milan Babu Adhikari" + Environment.NewLine +
               "Email: milanadhikari09@live.com" + Environment.NewLine +
               "Contact No.: +9779803818797" + Environment.NewLine +
               "Developed in: Microsoft Visual Studio 2017 Community" + Environment.NewLine,
               "Version 1.0.8 Freeware"
               );
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
