using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAnalysisSoftware_ASE_B_FirstAssignment
{
    public partial class Startup : Form
    {
        private int count = 2;
        private Dictionary<string, List<string>> _hrData = new Dictionary<string, List<string>>();
        private Dictionary<string, string> _param = new Dictionary<string, string>();

        public Startup()
        {
            InitializeComponent();
            InitGrid();
        }
        private string[] SplitString(string text)
        {
            var splitString = new string[] { "[Params]", "[Note]", "[IntTimes]", "[IntNotes]",
                "[ExtraData]", "[LapNames]", "[Summary-123]",
                "[Summary-TH]", "[HRZones]", "[SwapTimes]", "[Trip]", "[HRData]"};

            var splittedText = text.Split(splitString, StringSplitOptions.RemoveEmptyEntries);

            return splittedText;
        }

        private string[] SplitStringByEnter(string text)
        {
            return text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private string[] SplitStringBySpace(string text)
        {
            var formattedText = string.Join(" ", text.Split().Where(x => x != ""));
            return formattedText.Split(' ');
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                _param = new Dictionary<string, string>();
                _hrData = new Dictionary<string, List<string>>();
                string text = File.ReadAllText(openFileDialog1.FileName);
                var splittedString = SplitString(text);

                var splittedParamsData = SplitStringByEnter(splittedString[0]);

                foreach (var data in splittedParamsData)
                {
                    if (data != "\r")
                    {
                        string[] parts = data.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                        _param.Add(parts[0], parts[1]);
                    }
                }

                //To extract file from source and shhowing them
                //lblStartTime.AutoSize = false;
                //lblStartTime.Size = new Size(2000, 50);
                
                lblStartTime.Text = "Start Time" + "= " + Regex.Replace(_param["StartTime"], @"\t|\n|\r", "") + " ";
                lblInterval.Text = "Interval" + "= " + Regex.Replace(_param["Interval"], @"\t|\n|\r", "") + " Sec ";
                /*lblStartTimeUnit.Show();*/
                lblMonitor.Text = "Monitor" + "= " + Regex.Replace(_param["Monitor"], @"\t|\n|\r", "") + " " ; 
                lblSMode.Text = "SMode" + "= " + _param["SMode"];
                lblDate.Text = "Date" + "= " + _param["Date"];
                lblLength.Text = "Length" + "= " + _param["Length"];
                lblWeight.Text = "Weight" + "= " + Regex.Replace(_param["Weight"], @"\t|\n|\r", "") + " KG";

                List<string> cadence = new List<string>();
                List<string> altitude = new List<string>();
                List<string> heartRate = new List<string>();
                List<string> watt = new List<string>();
                List<string> speed = new List<string>();

                //adding data for datagrid
                var splittedHrData = SplitStringByEnter(splittedString[11]);
                DateTime dateTime = DateTime.Parse(_param["StartTime"]);

                int temp = 0;
                foreach (var data in splittedHrData)
                {
                    temp++;
                    var value = SplitStringBySpace(data);

                    if (value.Length >= 5)
                    {
                        cadence.Add(value[0]);
                        altitude.Add(value[1]);
                        heartRate.Add(value[2]);
                        watt.Add(value[3]);
                        speed.Add(value[4]);

                        if (temp > 2) dateTime = dateTime.AddSeconds(Convert.ToInt32(_param["Interval"]));

                        string[] hrData = new string[] { value[0], value[1], value[2], value[3], value[4], dateTime.TimeOfDay.ToString() };
                        dataGridView1.Rows.Add(hrData);
                    }
                }

                _hrData.Add("cadence", cadence);
                _hrData.Add("altitude", altitude);
                _hrData.Add("heartRate", heartRate);
                _hrData.Add("watt", watt);
                _hrData.Add("speed", speed);

                string totalDistanceCovered = Summary.FindSum(_hrData["cadence"]).ToString();
                string averageSpeed = Summary.FindAverage(_hrData["cadence"]).ToString();
                string maxSpeed = Summary.FindMax(_hrData["cadence"]).ToString();

                string averageHeartRate = Summary.FindAverage(_hrData["heartRate"]).ToString();
                string maximumHeartRate = Summary.FindMax(_hrData["heartRate"]).ToString();
                string minHeartRate = Summary.FindMin(_hrData["heartRate"]).ToString();

                string averagePower = Summary.FindAverage(_hrData["watt"]).ToString();
                string maxPower = Summary.FindMax(_hrData["watt"]).ToString();

                string averageAltitude = Summary.FindAverage(_hrData["altitude"]).ToString();
                string maximumAltitude = Summary.FindAverage(_hrData["altitude"]).ToString();

                string[] summarydata = new string[] { totalDistanceCovered, averageSpeed, maxSpeed, averageHeartRate, maximumHeartRate, minHeartRate, averagePower, maxPower, averageAltitude, maximumAltitude };
                dataGridView2.Rows.Clear();
                dataGridView2.Rows.Add(summarydata);
                // radioButton2.Checked = true;
                checkBox2.Checked = true;
            }
        }

        private void InitGrid()
        {

            //Showing data on grid
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "Cadence (RPM)";
            dataGridView1.Columns[1].Name = "Altitude (m/ft)";
            dataGridView1.Columns[2].Name = "Heart rate (BPM)";
            dataGridView1.Columns[3].Name = "Power (Watts)";
            dataGridView1.Columns[4].Name = "Speed (Mile/hr)";
            dataGridView1.Columns[5].Name = "Time";

            dataGridView2.ColumnCount = 10;
            dataGridView2.Columns[0].Name = "Total distance covered (KM)";
            dataGridView2.Columns[1].Name = "Average speed (Mile/Hr)";
            dataGridView2.Columns[2].Name = "Maximum speed (Mile/Hr)";
            dataGridView2.Columns[3].Name = "Average heart rate (BPM)";
            dataGridView2.Columns[4].Name = "Maximum heart rate (BPM)";
            dataGridView2.Columns[5].Name = "Minimum heart rate (BPM)";
            dataGridView2.Columns[6].Name = "Average power (Watts)";
            dataGridView2.Columns[7].Name = "Maximum power (Watts)";
            dataGridView2.Columns[8].Name = "Average altitude(m/ft)";
            dataGridView2.Columns[9].Name = "Maximum altitude(m/ft)";
        }

        private void CalculateSpeed(string type)
        {
            if (_hrData.Count > 0)
            {
                List<string> data = new List<string>();
                if (type == "Mile")
                {
                    dataGridView1.Columns[4].Name = "Speed(Mile/hr)";

                    data.Clear();

                    for (int i = 0; i < _hrData["cadence"].Count; i++)
                    {
                        string temp = (Convert.ToDouble(_hrData["speed"][i]) * 1.60934).ToString();
                        data.Add(temp);
                    }

                    //_hrData["speed"].Clear();
                    _hrData["speed"] = data;

                    dataGridView1.Rows.Clear();
                    DateTime dateTime = DateTime.Parse(_param["StartTime"]);
                    for (int i = 0; i < _hrData["cadence"].Count; i++)
                    {
                        if (i > 0) dateTime = dateTime.AddSeconds(Convert.ToInt32(_param["Interval"]));
                        string[] hrData = new string[] { _hrData["cadence"][i], _hrData["altitude"][i], _hrData["heartRate"][i], _hrData["watt"][i], _hrData["speed"][i], dateTime.TimeOfDay.ToString() };
                        dataGridView1.Rows.Add(hrData);
                    }
                }
                else
                {
                    dataGridView1.Columns[4].Name = "Speed(km/hr)";

                    data.Clear();
                    for (int i = 0; i < _hrData["cadence"].Count; i++)
                    {
                        string temp = (Convert.ToDouble(_hrData["speed"][i]) / 1.60934).ToString();
                        data.Add(temp);
                    }

                    //_hrData["speed"].Clear();
                    _hrData["speed"] = data;

                    dataGridView1.Rows.Clear();
                    DateTime dateTime = DateTime.Parse(_param["StartTime"]);
                    for (int i = 0; i < _hrData["cadence"].Count; i++)
                    {
                        if (i > 0) dateTime = dateTime.AddSeconds(Convert.ToInt32(_param["Interval"]));
                        string[] hrData = new string[] { _hrData["cadence"][i], _hrData["altitude"][i], _hrData["heartRate"][i], _hrData["watt"][i], _hrData["speed"][i], dateTime.TimeOfDay.ToString() };
                        dataGridView1.Rows.Add(hrData);
                    }
                }
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
                
                ViewGraph._hrData = _hrData;
                new ViewGraph().Show();
            }
           
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

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            new Startup().Show();
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

        private void allGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //handling error while viewing graph without opening file.
            if (_hrData.Count < 1)
            {
                MessageBox.Show("No Data Exist ! ! !" + Environment.NewLine +
                    "Enter Data First."
                    );
            }
            else
            {

                OtherGraph._hrData = _hrData;
                new OtherGraph().Show();
            }


            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //checkBox2.Refresh();
            //checkBox2.Checked = false;
            //CalculateSpeed("km");

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //checkBox1.Checked = false;
            //checkBox1.Visible = true;
            //count++;
            //if (count > 1) CalculateSpeed("mile");
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            //checkBox2.Refresh();
            checkBox2.Checked = false;
            CalculateSpeed("km");
            checkBox2.Refresh();
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            checkBox1.Refresh();
           checkBox1.Checked = false;
            //checkBox2.Checked = true;
            //checkBox1.Visible = true;
           // count++;
            if (count > 1) CalculateSpeed("Mile");
            checkBox2.Refresh();
            checkBox2.Checked = true;
        }
    }
    
}
