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
        public ViewGraph()
        {
            InitializeComponent();
        }

        private void ViewGraph_Load(object sender, EventArgs e)
        {
            plotGraph();
            SetSize();
        }

        private int[] buildTeamAData()
        {
            int[] goalsScored = new int[10];
            for (int i = 0; i < 10; i++)
            {
                goalsScored[i] = (i + 1) * 10;
            }
            return goalsScored;
        }

        private int[] buildTeamBData()
        {
            int[] goalsScored = new int[10];
            for (int i = 0; i < 10; i++)
            {
                goalsScored[i] = (i + 10) * 11;
            }
            return goalsScored;
        }

        private void plotGraph()
        {
            GraphPane myPane = zedGraphControl1.GraphPane;
            PointPairList teamAPairList = new PointPairList();
            PointPairList teamBPairList = new PointPairList();
            int[] teamAData = buildTeamAData();
            int[] teamBData = buildTeamBData();
            for (int i = 0; i < 10; i++)
            {
                teamAPairList.Add(i, teamAData[i]);
                teamBPairList.Add(i, teamBData[i]);
            }

            LineItem teamACurve = myPane.AddCurve("Team A",
                   teamAPairList, Color.Red, SymbolType.Diamond);

            LineItem teamBCurve = myPane.AddCurve("Team B",
                  teamBPairList, Color.Blue, SymbolType.Circle);

            zedGraphControl1.AxisChange();
        }

        private void SetSize()
        {
            zedGraphControl1.Location = new Point(0, 0);
            zedGraphControl1.IsShowPointValues = true;
            zedGraphControl1.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);

        }
    }
}
