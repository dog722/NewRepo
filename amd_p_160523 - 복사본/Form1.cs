using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace amd_p
{
    public partial class Form1 : Form
    {
        private string in_data;
        private int count; 
        private DateTime dateTime;
        private Decimal test;
        private static System.Timers.Timer aTimer;

        public Form1()
        {
            test = 30M;
            count = 0;
            InitializeComponent();
            getAvailablePorts();
            timer.Start();
            stop_bn.Enabled = false;
        }

        void getAvailablePorts()
        {
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
        }

        //start_bn
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Please select port and Baud setting");
            }
            else
            {
                serialPort.PortName = comboBox1.Text;
                serialPort.BaudRate = Convert.ToInt32(comboBox2.Text);
                serialPort.Open();
                start_bn.Enabled = false;
                stop_bn.Enabled = true;
                serialPort.DataReceived += Myport_DataReceived;
            }
        }
        private void Myport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if(serialPort.BytesToRead > 0)
            {
                //in_data = serialPort.ReadLine();
                Invoke(new EventHandler(displaydata_event));
            }
        }

        private void displaydata_event(object sender, EventArgs e)
        {
            try
            {
                //int in_data1;
                //string time = dateTime.Hour + ":" + dateTime.Minute + ":" + dateTime.Second;
                in_data = serialPort.ReadLine();
                if( in_data != "" )//&& in_data.Length == 5)
                {
                    int ddd = serialPort.BytesToRead;
                    textBox1.AppendText(dateTime + "\t" + in_data +"\n");
                    //in_data1 = Convert.ToInt32(in_data);
                    //aGauge1.Value = in_data1 / 10;//////////////////////
                    //progressBar1.Value = in_data1 / 100;//////////////////////
                    //lxLedControl1.Text = in_data;//////////////////////
                    //perfChart.AddValue(in_data1 / 100);//////////////////////
                    //When in_data1 is over 100 is interrept

                    //can_id = ~ ; 
                    // draw_chart(can_id);

                    aTimer = new System.Timers.Timer(1000);
                    aTimer.Elapsed += draw_chart2;
                    aTimer.Enabled = true;
                    draw_chart();
                }
            }
            catch (TimeoutException)
            {
            }
        }

        //stop_bn
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Enabled = false;
                start_bn.Enabled = true;
                stop_bn.Enabled = false;
                serialPort.Close();//serial port Check.
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message, "Error");
            }
        }

        //save_bn
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string save_path = @desktop + "\\" ;
                string filename = "arduinofile.txt";
                System.IO.File.WriteAllText(save_path + filename, textBox1.Text);
                MessageBox.Show("Data has been saved to " + save_path + filename);
            }
            catch(Exception ex3)
            {
                MessageBox.Show(ex3.Message, "Error");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dateTime = DateTime.Now;
            this.time_info.Text = dateTime.ToString();
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void label2_Click(object sender, EventArgs e)
        {
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }
        private void label3_Click(object sender, EventArgs e)
        {
        }
        private void label4_Click(object sender, EventArgs e)
        {
        }
        private void lxLedControl1_Click(object sender, EventArgs e)
        {
        }
        private void aGauge1_ValueInRangeChanged(object sender, ValueInRangeChangedEventArgs e)
        {
        }
        private void perfChart1_Load(object sender, EventArgs e)
        {
        }
        private void perfChart_Load(object sender, EventArgs e)
        {
            {
                try
                {

                    for (int i = 0; i < 100; i++)
                    {

                       // perfChart.AddValue(i * 1M);
                    }

                }
                catch (Exception ex3)
                {
                    MessageBox.Show(ex3.Message, "Error");
                }
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {
        }

        private void init_chart1()
        {
            chart1.Series.Clear();
            string[] xValue = { "data1", "data2", "data3", "data4", "data5", "data6", "data7", "data8" };
            double[] yValue = { 0 };


            for (int i = 0; i < 8; i++)
            {
              System.Windows.Forms.DataVisualization.Charting.Series se = new System.Windows.Forms.DataVisualization.Charting.Series();
              se.Name = xValue[i];
              chart1.Series.Add(se);
              //se.AxisLabel = xValue[i];

            }

            //for (int i = 4; i < 8; i++)
            //{

              //  chart1.Series[xValue[i]].AxisLabel = xValue[i];
           // }

        }

        private void draw_chart()
        {

            init_chart1();

            //can_id에 따라 다른 chart 를 불러오기
            //in_data 를 6개의 데이터로 나누기  data[0] data[1] ... data[7]
            string[] data = { "data1", "data2", "data3", "data4", "data5", "data6", "data7", "data8" };
            for ( int i =0; i < 8; i++)
                {
                    
                    chart1.Series[data[i]].Points.AddY(test);

                if(test>0) test -= 0.01m;
                 }

        }

        private void draw_chart2(Object source, ElapsedEventArgs e)
        {
            try
            {
                perfChart.AddValue(test);
            }
            catch (Exception ex3)
            {
                MessageBox.Show(ex3.Message, "Error");
            }
        }
            
        }
}