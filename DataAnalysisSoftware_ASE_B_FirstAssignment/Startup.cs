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
        private string endTime;
        private Dictionary<string, List<string>> _hrData = new Dictionary<string, List<string>>();
        private Dictionary<string, string> _param = new Dictionary<string, string>();
        private List<int> smode = new List<int>();

        public Startup()
        {
            InitializeComponent();
            InitGrid();
        }
        //private string[] SplitString(string text)
        //{
        //    var splitString = new string[] { "[Params]", "[Note]", "[IntTimes]", "[IntNotes]",
        //        "[ExtraData]", "[LapNames]", "[Summary-123]",
        //        "[Summary-TH]", "[HRZones]", "[SwapTimes]", "[Trip]", "[HRData]"};

        //    var splittedText = text.Split(splitString, StringSplitOptions.RemoveEmptyEntries);

        //    return splittedText;
        //}

        //private string[] SplitStringByEnter(string text)
        //{
        //    return text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        //}

        //private string[] SplitStringBySpace(string text)
        //{
        //    var formattedText = string.Join(" ", text.Split().Where(x => x != ""));
        //    return formattedText.Split(' ');
        //}


            /// <summary>
            /// making open tool working
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                string text = File.ReadAllText(openFileDialog1.FileName);
                Dictionary<string, object> hrData = new TableFiller().FillTable(text, dataGridView1);
                _hrData = hrData.ToDictionary(k => k.Key, k => k.Value as List<string>);
                var param = hrData["params"] as Dictionary<string, string>;
                //header file
                lblStartTime.Text = lblStartTime.Text + "= " + param["StartTime"];
                lblInterval.Text = lblInterval.Text + "= " + Regex.Replace(param["Interval"], @"\t|\n|\r", "") + " Sec ";
                lblMonitor.Text = lblMonitor.Text + "= " + param["Monitor"];
                lblSMode.Text = lblSMode.Text + "= " + param["SMode"];
                lblDate.Text = lblDate.Text + "= " + param["Date"];
                lblLength.Text = lblLength.Text + "= " + param["Length"];
                lblWeight.Text = lblWeight.Text + "= " + Regex.Replace(param["Weight"], @"\t|\n|\r", "") + " KG";

                //Fetching Smode Data from file


                List<string> cadence = new List<string>();
                List<string> altitude = new List<string>();
                List<string> heartRate = new List<string>();
                List<string> watt = new List<string>();
                List<string> speed = new List<string>();

                var metricsCalculation = new AdvancedCalculationOfMatrix();

                //advance mettrics calculation
                double np = metricsCalculation.CalculateNormalizedPower(hrData);
                label4.Text = "Normalized power = " + Summary.RoundUp(np, 2);

                double ftp = metricsCalculation.CalculateFunctionalThresholdPower(hrData);
                label5.Text = "Training Stress Score = " + Summary.RoundUp(ftp, 2);

                double ifa = metricsCalculation.CalculateIntensityFactor(hrData);
                label6.Text = "Intensity Factor = " + Summary.RoundUp(ifa, 2);

                double pb = metricsCalculation.CalculatePowerBalance(hrData);
                label3.Text = "Power balance = " + Summary.RoundUp(pb, 2);

                var sMode = param["SMode"];
                for (int i = 0; i < sMode.Length; i++)
                {
                    smode.Add((int)Char.GetNumericValue(param["SMode"][i]));
                }
                if (smode[0] == 0)
                {
                    dataGridView1.Columns[0].Visible = false;
                }
                else if (smode[1] == 0)
                {
                    dataGridView1.Columns[1].Visible = false;
                }
                else if (smode[2] == 0)
                {
                    dataGridView1.Columns[2].Visible = false;
                }
                else if (smode[3] == 0)
                {
                    dataGridView1.Columns[3].Visible = false;
                }
                else if (smode[4] == 0)
                {
                    dataGridView1.Columns[4].Visible = false;
                }
                dataGridView2.Rows.Clear();
                //dataGridView2.Rows.Add(new TableFiller().FillDataInSumaryTable(hrData, hrData["params"] as Dictionary<string, string>, hrData["endTime"] as string));
                dataGridView2.Rows.Add(new TableFiller().FillDataInSumaryTable(hrData, hrData["endTime"] as string, hrData["params"] as Dictionary<string, string>));
            }

            //DialogResult result = openFileDialog1.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    Cursor.Current = Cursors.WaitCursor;
            //    _param = new Dictionary<string, string>();
            //    _hrData = new Dictionary<string, List<string>>();
            //    string text = File.ReadAllText(openFileDialog1.FileName);
            //    var splittedString = SplitString(text);

            //    var splittedParamsData = SplitStringByEnter(splittedString[0]);

            //    foreach (var data in splittedParamsData)
            //    {
            //        if (data != "\r")
            //        {
            //            string[] parts = data.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
            //            _param.Add(parts[0], parts[1]);
            //        }
            //    }

            //    //To extract file from source and shhowing them
            //    //lblStartTime.AutoSize = false;
            //    //lblStartTime.Size = new Size(2000, 50);

            //    lblStartTime.Text = "Start Time" + "= " + Regex.Replace(_param["StartTime"], @"\t|\n|\r", "") + " ";
            //    lblInterval.Text = "Interval" + "= " + Regex.Replace(_param["Interval"], @"\t|\n|\r", "") + " Sec ";
            //    /*lblStartTimeUnit.Show();*/
            //    lblMonitor.Text = "Monitor" + "= " + Regex.Replace(_param["Monitor"], @"\t|\n|\r", "") + " " ; 
            //    lblSMode.Text = "SMode" + "= " + _param["SMode"];
            //    lblDate.Text = "Date" + "= " + ConvertToDate(_param["Date"]);
            //    lblLength.Text = "Length" + "= " + _param["Length"];
            //    lblWeight.Text = "Weight" + "= " + Regex.Replace(_param["Weight"], @"\t|\n|\r", "") + " KG";

            //    //Fetching Smode Data from file
            //    var sMode = _param["SMode"];
            //    for (int i = 0; i < sMode.Length; i++)
            //    {
            //        smode.Add((int)Char.GetNumericValue(_param["SMode"][i]));
            //    }

            //    List<string> cadence = new List<string>();
            //    List<string> altitude = new List<string>();
            //    List<string> heartRate = new List<string>();
            //    List<string> watt = new List<string>();
            //    List<string> speed = new List<string>();

            //    //adding data for datagrid
            //    var splittedHrData = SplitStringByEnter(splittedString[11]);
            //    DateTime dateTime = DateTime.Parse(_param["StartTime"]);

            //    int temp = 0;
            //    foreach (var data in splittedHrData)
            //    {
            //        temp++;
            //        var value = SplitStringBySpace(data);

            //        if (value.Length >= 5)
            //        {
            //            cadence.Add(value[0]);
            //            altitude.Add(value[1]);
            //            heartRate.Add(value[2]);
            //            watt.Add(value[3]);
            //            speed.Add(value[4]);

            //            if (temp > 2) dateTime = dateTime.AddSeconds(Convert.ToInt32(_param["Interval"]));
            //            endTime = dateTime.TimeOfDay.ToString();
            //            string[] hrData = new string[] { value[0], value[1], value[2], value[3], value[4], dateTime.TimeOfDay.ToString() };
            //            dataGridView1.Rows.Add(hrData);
            //        }
            //    }

            //    _hrData.Add("cadence", cadence);
            //    _hrData.Add("altitude", altitude);
            //    _hrData.Add("heartRate", heartRate);
            //    _hrData.Add("watt", watt);
            //    _hrData.Add("speed", speed);

            //    //
            //    if (smode[0] == 0)
            //    {
            //        dataGridView1.Columns[0].Visible = false;
            //    }
            //    else if (smode[1] == 0)
            //    {
            //        dataGridView1.Columns[1].Visible = false;
            //    }
            //    else if (smode[2] == 0)
            //    {
            //        dataGridView1.Columns[2].Visible = false;
            //    }
            //    else if (smode[3] == 0)
            //    {
            //        dataGridView1.Columns[3].Visible = false;
            //    }
            //    else if (smode[4] == 0)
            //    {
            //        dataGridView1.Columns[4].Visible = false;
            //    }

            //    double startDate = TimeSpan.Parse(_param["StartTime"]).TotalSeconds;
            //    double endDate = TimeSpan.Parse(endTime).TotalSeconds;
            //    double totalTime = endDate - startDate;

            //    //string totalDistanceCovered = Summary.FindSum(_hrData["cadence"]).ToString();

            //    string averageSpeed = Summary.FindAverage(_hrData["speed"]).ToString();
            //    string totalDistanceCovered = ((Convert.ToDouble(averageSpeed) * totalTime) / 360).ToString();
            //    string maxSpeed = Summary.FindMax(_hrData["speed"]).ToString();

            //    string averageHeartRate = Summary.FindAverage(_hrData["heartRate"]).ToString();
            //    string maximumHeartRate = Summary.FindMax(_hrData["heartRate"]).ToString();
            //    string minHeartRate = Summary.FindMin(_hrData["heartRate"]).ToString();

            //    string averagePower = Summary.FindAverage(_hrData["watt"]).ToString();
            //    string maxPower = Summary.FindMax(_hrData["watt"]).ToString();

            //    string averageAltitude = Summary.FindAverage(_hrData["altitude"]).ToString();
            //    string maximumAltitude = Summary.FindAverage(_hrData["altitude"]).ToString();

            //    string[] summarydata = new string[] { totalDistanceCovered, averageSpeed, maxSpeed, averageHeartRate, maximumHeartRate, minHeartRate, averagePower, maxPower, averageAltitude, maximumAltitude };
            //    dataGridView2.Rows.Clear();
            //    dataGridView2.Rows.Add(summarydata);
            //    // radioButton2.Checked = true;
            //    checkBox2.Checked = true;
            //}
        }

        /// <summary>
        /// to convert date in format
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string ConvertToDate(string date)
        {
            string year = "";
            string month = "";
            string day = "";

            for (int i = 0; i < 4; i++)
            {
                year = year + date[i];
            };

            for (int i = 4; i < 6; i++)
            {
                month = month + date[i];
            };

            for (int i = 6; i < 8; i++)
            {
                day = day + date[i];
            };

            string convertedDate = year + "-" + month + "-" + day;

            return convertedDate;
        }

        /// <summary>
        /// to show data in grid view
        /// </summary>
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

        /// <summary>
        /// to calculate speed and convert km into mile and viceversa
        /// </summary>
        /// <param name="type"></param>
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
                else
                {
                    dataGridView1.Columns[4].Name = "Speed(km/hr)";

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
                Program.smode = smode;
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
                Program.smode = smode;
                OtherGraph._hrData = _hrData;
                new OtherGraph(smode).Show();
            }



        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_Click(object sender, EventArgs e)
        {

            checkBox2.Checked = false;
            CalculateSpeed("km");
            checkBox2.Refresh();
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            checkBox1.Refresh();
            checkBox1.Checked = false;

            if (count > 1) CalculateSpeed("Mile");
            checkBox2.Refresh();
            checkBox2.Checked = true;
        }

        private void Startup_Load(object sender, EventArgs e)
        {

        }

        private void compareFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            new FileCompare().Show();
        }

        private Dictionary<string, object> data = new Dictionary<string, object>();
        List<string> listCadence = new List<string>();
        List<string> listAltitude = new List<string>();
        List<string> listHeartRate = new List<string>();
        List<string> listPower = new List<string>();
        List<string> listSpeed = new List<string>();
        List<string> listTime = new List<string>();

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected) return;
            int index = Convert.ToInt32(e.Row.Index.ToString());

            string cadence = dataGridView1.Rows[index].Cells[0].Value.ToString();
            listCadence.Add(cadence);

            //_hrData.Add("cadence", new List());
            string altitude = dataGridView1.Rows[index].Cells[1].Value.ToString();
            listAltitude.Add(altitude);

            string heartRate = dataGridView1.Rows[index].Cells[2].Value.ToString();
            listHeartRate.Add(heartRate);

            string power = dataGridView1.Rows[index].Cells[3].Value.ToString();
            listPower.Add(power);

            string speed = dataGridView1.Rows[index].Cells[4].Value.ToString();
            listSpeed.Add(speed);

            string time = dataGridView1.Rows[index].Cells[5].Value.ToString();
            listTime.Add(time);

            Console.WriteLine(cadence + "/" + altitude + "/" + heartRate + "/" + power + "/" + speed + "/" + time);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                data.Add("cadence", listCadence);
                data.Add("altitude", listAltitude);
                data.Add("heartRate", listHeartRate);
                data.Add("watt", listPower);
                data.Add("speed", listSpeed);
                data.Add("time", listTime);

                var endTime = data["time"] as List<string>;
                int count = endTime.Count();
                Dictionary<string, string> _param = new Dictionary<string, string>();
                _param.Add("StartTime", endTime[0]);

                dataGridView2.Rows.Clear();
                dataGridView2.Rows.Add(new TableFiller().FillDataInSumaryTable(data, endTime[count - 1], _param));
            }

            catch (Exception ex)
            {
                MessageBox.Show("No Data Exist ! ! !" + Environment.NewLine +
                                    "Enter Data First."
                                    );
                this.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            data.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var data = _hrData.ToDictionary(k => k.Key, k => k.Value as object);
                this.Hide();
                new DetectInterval(data).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Data Exist ! ! !" + Environment.NewLine +
                    "Enter Data First."
                    );
                this.Show();
            }


        }
        Dictionary<string, object> list = new Dictionary<string, object>();
        private void button10_Click(object sender, EventArgs e)
        {
            string val = textBox1.Text;
            int value;
            if (int.TryParse(val, out value))
            {
                int count = 0;
                try
                {
                    count = ((List<string>)data["speed"]).Count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please select a row first");
                }

                int portion = count / Convert.ToInt32(val);

                var cadenceData = data["cadence"] as List<string>;
                var altitudeData = data["altitude"] as List<string>;
                var heartRateData = data["heartRate"] as List<string>;
                var wattData = data["watt"] as List<string>;
                var speedData = data["speed"] as List<string>;

                var newCadenceData = new List<string>();
                var newAltitudeData = new List<string>();
                var newHeartRateData = new List<string>();
                var newWattData = new List<string>();
                var newSpeedData = new List<string>();

                int num = 0;
                int portionNumber = 0;

                for (int i = 0; i < count; i++)
                {
                    num++;
                    newCadenceData.Add(cadenceData[i]);
                    newAltitudeData.Add(altitudeData[i]);
                    newHeartRateData.Add(heartRateData[i]);
                    newWattData.Add(wattData[i]);
                    newSpeedData.Add(speedData[i]);

                    if (num == portion)
                    {
                        num = 0;
                        portionNumber++;

                        var listData = new Dictionary<string, List<string>>();
                        listData.Add("cadence", newCadenceData);
                        listData.Add("altitude", newAltitudeData);
                        listData.Add("heartRate", newHeartRateData);
                        listData.Add("watt", newWattData);
                        listData.Add("speed", newSpeedData);

                        list.Add("data" + portionNumber, listData);

                        newCadenceData = new List<string>();
                        newAltitudeData = new List<string>();
                        newHeartRateData = new List<string>();
                        newWattData = new List<string>();
                        newSpeedData = new List<string>();
                    }

                }

                comboBox1.Items.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    comboBox1.Items.Add("Portion " + (i + 1));
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number between 0 - 9");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex + 1;

            dataGridView2.Rows.Clear();

            var a = list["data" + selectedIndex] as Dictionary<string, List<string>>;
            var b = a.ToDictionary(k => k.Key, k => k.Value as object);


            var data = new TableFiller().FillDataInSumaryTable(b, "19:12:15", null);
            dataGridView2.Rows.Add(data);
        }
    }

}
