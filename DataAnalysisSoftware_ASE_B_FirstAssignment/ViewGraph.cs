using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace DataAnalysisSoftware_ASE_B_FirstAssignment
{
    public partial class ViewGraph : Form
    {
        public static Dictionary<string, List<string>> _hrData;
        public ViewGraph()
        {
            InitializeComponent();
        }

        private void ViewGraph_Load(object sender, EventArgs e)
        {
            
            plotGraph();
            initData();
            SetSize();

            zedGraphControl1.Visible = true;
            zedGraphControl2.Visible = false;
            zedGraphControl3.Visible = false;
            zedGraphControl4.Visible = false;
            zedGraphControl5.Visible = false;
        }

        //private int[] buildTeamAData()
        //{
        //    int[] goalsScored = new int[10];
        //    for (int i = 0; i < 10; i++)
        //    {
        //        goalsScored[i] = (i + 1) * 10;
        //    }
        //    return goalsScored;
        //}

        //private int[] buildTeamBData()
        //{
        //    int[] goalsScored = new int[10];
        //    for (int i = 0; i < 10; i++)
        //    {
        //        goalsScored[i] = (i + 10) * 11;
        //    }
        //    return goalsScored;
        //}

        private void plotGraph()
        {
            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.Title = "Overview";
            myPane.XAxis.Title = "Time (second)";
            myPane.YAxis.Title = "Data";
            PointPairList cadencePairList = new PointPairList();
            PointPairList altitudePairList = new PointPairList();
            PointPairList heartPairList = new PointPairList();
            PointPairList powerPairList = new PointPairList();


            for (int i = 0; i < _hrData["cadence"].Count; i++)
            {
                cadencePairList.Add(i, Convert.ToInt16(_hrData["cadence"][i]));
            }

            for (int i = 0; i < _hrData["altitude"].Count; i++)
            {
                altitudePairList.Add(i, Convert.ToInt16(_hrData["altitude"][i]));
            }

            for (int i = 0; i < _hrData["heartRate"].Count; i++)
            {
                heartPairList.Add(i, Convert.ToInt16(_hrData["heartRate"][i]));
            }

            for (int i = 0; i < _hrData["watt"].Count; i++)
            {
                powerPairList.Add(i, Convert.ToInt16(_hrData["watt"][i]));
            }

            

            LineItem cadence = myPane.AddCurve("Cadence",
                   cadencePairList, Color.Red, SymbolType.None);

            LineItem altitude = myPane.AddCurve("Altitude",
                  altitudePairList, Color.Cyan, SymbolType.None);

            LineItem heart = myPane.AddCurve("Heart",
                   heartPairList, Color.Black, SymbolType.None);

            LineItem power = myPane.AddCurve("Power",
                  powerPairList, Color.DarkGreen, SymbolType.None);

            zedGraphControl1.AxisChange();


        }

        private void initData()
        {
            GraphPane speedPane = zedGraphControl2.GraphPane;
            GraphPane heartRatePane = zedGraphControl3.GraphPane;
            GraphPane cadencePane = zedGraphControl4.GraphPane;
            GraphPane powerPane = zedGraphControl5.GraphPane;


            // Set the Titles
            speedPane.Title = "Overview";
            speedPane.XAxis.Title = "Time in second";
            speedPane.YAxis.Title = "Data";

            heartRatePane.Title = "Overview";
            heartRatePane.XAxis.Title = "Time in second";
            heartRatePane.YAxis.Title = "Data";

            cadencePane.Title = "Overview";
            cadencePane.XAxis.Title = "Time in second";
            cadencePane.YAxis.Title = "Data";

            powerPane.Title = "Overview";
            powerPane.XAxis.Title = "Time in second";
            powerPane.YAxis.Title = "Data";

            PointPairList cadencePairList = new PointPairList();
            PointPairList altitudePairList = new PointPairList();
            PointPairList heartPairList = new PointPairList();
            PointPairList powerPairList = new PointPairList();


            for (int i = 0; i < _hrData["cadence"].Count; i++)
            {
                cadencePairList.Add(i, Convert.ToInt16(_hrData["cadence"][i]));
            }

            for (int i = 0; i < _hrData["altitude"].Count; i++)
            {
                altitudePairList.Add(i, Convert.ToInt16(_hrData["altitude"][i]));
            }

            for (int i = 0; i < _hrData["heartRate"].Count; i++)
            {
                heartPairList.Add(i, Convert.ToInt16(_hrData["heartRate"][i]));
            }

            for (int i = 0; i < _hrData["watt"].Count; i++)
            {
                powerPairList.Add(i, Convert.ToInt16(_hrData["watt"][i]));
            }

            LineItem cadence = cadencePane.AddCurve("Cadence",
                    cadencePairList, Color.Red, SymbolType.None);
            //cadence.Symbol.Fill = new Fill(new Color[] { Color.Blue, Color.Green, Color.Red });

            LineItem altitude = speedPane.AddCurve("Altitude",
                  altitudePairList, Color.Cyan, SymbolType.None);

            LineItem heart = heartRatePane.AddCurve("Heart",
                   heartPairList, Color.Black, SymbolType.None);

            LineItem power = powerPane.AddCurve("Power",
                  powerPairList, Color.DarkGreen, SymbolType.None);

            zedGraphControl2.AxisChange();
            zedGraphControl3.AxisChange();
            zedGraphControl4.AxisChange();
            zedGraphControl5.AxisChange();
        }

        private void SetSize()
        {
            zedGraphControl1.Location = new Point(0, 0);
            zedGraphControl1.IsShowPointValues = true;
            zedGraphControl1.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);

        }

        private void ViewGraph_Resize(object sender, EventArgs e)
        {
            SetSize();
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
           // new Startup().Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            new HomePage().Show();
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

        private void graphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_hrData.Count < 1)
            {
                MessageBox.Show("No Data Exist ! ! !" + Environment.NewLine +
                    "Enter Data First."
                    );
            }
            else
            {
                this.Close();
                OtherGraph._hrData = _hrData;
                new OtherGraph().Show();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            zedGraphControl1.Visible = false;
            zedGraphControl2.Visible = true;
            zedGraphControl3.Visible = false;
            zedGraphControl4.Visible = false;
            zedGraphControl5.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            zedGraphControl1.Visible = false;
            zedGraphControl2.Visible = false;
            zedGraphControl3.Visible = true;
            zedGraphControl4.Visible = false;
            zedGraphControl5.Visible = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            zedGraphControl1.Visible = false;
            zedGraphControl2.Visible = false;
            zedGraphControl3.Visible = false;
            zedGraphControl4.Visible = true;
            zedGraphControl5.Visible = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            zedGraphControl1.Visible = false;
            zedGraphControl2.Visible = false;
            zedGraphControl3.Visible = false;
            zedGraphControl4.Visible = false;
            zedGraphControl5.Visible = true;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            zedGraphControl1.Visible = true;
            zedGraphControl2.Visible = false;
            zedGraphControl3.Visible = false;
            zedGraphControl4.Visible = false;
            zedGraphControl5.Visible = false;
        }
    }
}
