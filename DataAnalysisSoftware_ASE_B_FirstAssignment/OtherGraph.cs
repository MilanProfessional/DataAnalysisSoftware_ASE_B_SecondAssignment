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
    public partial class OtherGraph : Form
    {
        public static Dictionary<string, List<string>> _hrData;
        private List<int> _smode;

        public OtherGraph(List<int> smode)
        {
            _smode = smode;
            InitializeComponent();
            plotGraph();
        }

        private void plotGraph()
        {
            GraphPane speedPane = zedGraphControl1.GraphPane;
            GraphPane heartRatePane = zedGraphControl2.GraphPane;
            GraphPane cadencePane = zedGraphControl3.GraphPane;
            GraphPane powerPane = zedGraphControl4.GraphPane;
            GraphPane altitudePane = zedGraphControl5.GraphPane;


            // Set the Titles
            altitudePane.Title = "Overview";
            altitudePane.XAxis.Title = "Time in second";
            altitudePane.YAxis.Title = "Data";
            

            heartRatePane.Title = "Overview";
            heartRatePane.XAxis.Title = "Time in second";
            heartRatePane.YAxis.Title = "Data";

            cadencePane.Title = "Overview";
            cadencePane.XAxis.Title = "Time in second";
            cadencePane.YAxis.Title = "Data";

            powerPane.Title = "Overview";
            powerPane.XAxis.Title = "Time in second";
            powerPane.YAxis.Title = "Data";

            speedPane.Title = "Overview";
            speedPane.XAxis.Title = "Time in second";
            speedPane.YAxis.Title = "Data";



            PointPairList cadencePairList = new PointPairList();
            PointPairList altitudePairList = new PointPairList();
            PointPairList heartPairList = new PointPairList();
            PointPairList powerPairList = new PointPairList();
            PointPairList speedPairList = new PointPairList();


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

            for (int i = 0; i < _hrData["speed"].Count; i++)
            {
                speedPairList.Add(i, Convert.ToDouble(_hrData["speed"][i]));
            }

            LineItem cadence = cadencePane.AddCurve("Cadence",
                    cadencePairList, Color.Red, SymbolType.None);
            //cadence.Symbol.Fill = new Fill(new Color[] { Color.Blue, Color.Green, Color.Red });

            LineItem altitude = altitudePane.AddCurve("Altitude",
                  altitudePairList, Color.Cyan, SymbolType.None);

            LineItem heart = heartRatePane.AddCurve("Heart",
                   heartPairList, Color.Black, SymbolType.None);

            LineItem power = powerPane.AddCurve("Power",
                  powerPairList, Color.DarkGreen, SymbolType.None);

            LineItem speed = speedPane.AddCurve("Speed",
                  speedPairList, Color.DarkOrange, SymbolType.None);

            zedGraphControl1.AxisChange();
            zedGraphControl2.AxisChange();
            zedGraphControl3.AxisChange();
            zedGraphControl4.AxisChange();
            zedGraphControl5.AxisChange();

            if (_smode[0] == 0)
            {
                //cadence
                zedGraphControl3.Visible = false;
            }
            else if (_smode[1] == 0)
            {
                //altitude
                zedGraphControl1.Visible = false;
            }
            else if (_smode[2] == 0)
            {
                //heart rate
                zedGraphControl2.Visible = false;
            }
            else if (_smode[3] == 0)
            {
                //pwer
                zedGraphControl4.Visible = false;
            }
            else if (_smode[4] == 0)
            {
                //sped
                zedGraphControl5.Visible = false;
            }
        }

        private void OtherGraph_Load(object sender, EventArgs e)
        {

        }

        private void SetSize()
        {
            zedGraphControl1.Location = new Point(0, 0);
            zedGraphControl1.IsShowPointValues = true;
            zedGraphControl1.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);

            zedGraphControl2.Location = new Point(0, 0);
            zedGraphControl2.IsShowPointValues = true;
            zedGraphControl2.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);

            zedGraphControl3.Location = new Point(0, 0);
            zedGraphControl3.IsShowPointValues = true;
            zedGraphControl3.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);

            zedGraphControl4.Location = new Point(0, 0);
            zedGraphControl4.IsShowPointValues = true;
            zedGraphControl4.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);

            zedGraphControl5.Location = new Point(0, 0);
            zedGraphControl4.IsShowPointValues = true;
            zedGraphControl5.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);
        }

        private void OtherGraph_Resize(object sender, EventArgs e)
        {
           // SetSize();
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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

        //private void homeToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    this.Hide();
        //    new HomePage().Show();
        //}

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
                ViewGraph._hrData = _hrData;
                new ViewGraph().Show();
            }
        }
    }
}
