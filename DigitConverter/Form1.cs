using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void descriptionHeader_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        protected string ToHex(int target)
        {
            return target.ToString("X");
        }

        protected string ToBinary(int target)
        {
            return Convert.ToString(target,2);
        }

        protected DataTable GenerateConversionChart()
        {
            //create template for conversion chart
            DataTable chart = new DataTable();
            chart.Columns.Add("Digit", typeof(int));
            chart.Columns.Add("Hexadecimal", typeof(string));
            chart.Columns.Add("Binary", typeof(string));
            return chart;
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            //holds the lower and upper bounds for the conversions
            int floor, ceiling;

            //check input for correct input
            if ((int.TryParse(lowerBound.Text.ToString(), out floor) == true) && (int.TryParse(upperBound.Text.ToString(), out ceiling) == true))
            {
                //check to make sure that bounds are within range
                if ((floor >= 0) && (ceiling > floor))
                {
                    //create chart and generate data
                    DataTable conversionChart = GenerateConversionChart();
                    for (int x = floor; x <= ceiling; x++)
                    {
                        conversionChart.Rows.Add(new object[] {x,ToHex(x),ToBinary(x)});
                    }

                    //bind the data
                    dataGridView1.DataSource = conversionChart;
                }
            }
        }
    }
}
