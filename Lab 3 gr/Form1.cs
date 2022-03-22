using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab_3_gr
{
    public partial class Form1: Form
    {
        Table table;
        JobFile jodFile = new JobFile();
        Calculation calculation = new Calculation();
        about aboutProgr = new about(false);
        private readonly string error = "Wrong data";
        private readonly string borderError = "Wrong border edges";
        private readonly string errorOfConst = "Graph is dot";
        public Form1()
        {
            InitializeComponent();
            if (jodFile.AgainAbout())
            {
                aboutProgr.Show();
            }
        }

        bool GetDouble(TextBox text, out double num)
        {
           return (double.TryParse(text.Text, out num) && !text.Text.Contains(','));
        }
        bool CheckData() 
        {
            errorProvider1.Clear();
            bool flag = true;
            if (!GetDouble(textBox1, out double a)) 
            {
                errorProvider1.SetError(textBox1, error);
                flag = false;
            }
            if (!GetDouble(textBox2, out double c))
            {
                errorProvider1.SetError(textBox2, error);
                flag = false;
            }
            if (!GetDouble(textBox3, out double leftBorder))
            {
                errorProvider1.SetError(textBox3, error);
                flag = false;
            }
            if (!GetDouble(textBox4, out double rightBorder))
            {
                errorProvider1.SetError(textBox4, error);
                flag = false;
            }
            if (!GetDouble(textBox5, out double step))
            {
                errorProvider1.SetError(textBox5, error);
                flag = false;

            }
            else
            if (step <= 0.001)
            {
                errorProvider1.SetError(textBox5, error);
                flag = false;
            }
            else
            if ((rightBorder - leftBorder) - step <= 0) 
            {
                errorProvider1.SetError(textBox5, errorOfConst);
                flag = false;
            }
            if (leftBorder >= rightBorder) 
            {
                errorProvider1.SetError(textBox3, borderError);
                errorProvider1.SetError(textBox4, borderError);
                flag = false;
            }
            if (a == 0 && c == 0) 
            {
                errorProvider1.SetError(textBox1, error);
                errorProvider1.SetError(textBox2, error);
                flag = false;
            }
            if (a <= 0 && c != 0)
            {
                errorProvider1.SetError(textBox2, errorOfConst);
                flag = false;
            }

            return flag;
        }
        private void calculetGraph(double leftBorder, double rightBorder, double a, double c, ref List<double> points, ref List<double> dot) 
        {
            double dotStart = 0;
            double pointsStart = 0;
            for (int i = 0; i < dot.Count(); i++)
            {
                if (dot[i] < 0 || a >= c)
                {
                    chart1.Series[0].Points.AddXY(dot[i], points[i]);
                    dotStart = dot[i];
                    pointsStart = points[i];
                }
                else
                    chart1.Series[1].Points.AddXY(dot[i], points[i]);
            }
            if (rightBorder - dotStart > 0.001 && a <= c)
                chart1.Series[2].Points.AddXY(dotStart, pointsStart);

            double dotEnd = 0;
            double pointsEnd = 0;

            if (rightBorder - dot[dot.Count - 1] > 0.001 && a < c)
                chart1.Series[3].Points.AddXY(dot[dot.Count - 1], points[dot.Count - 1]);
            else if (rightBorder - dot[dot.Count - 1] > 0.001)
                chart1.Series[2].Points.AddXY(dot[dot.Count - 1], points[dot.Count - 1]);

            for (int i = dot.Count(); i > 0; i--)
            {
                if (dot[i - 1] < 0 || a >= c)
                    chart1.Series[2].Points.AddXY(dot[i - 1], -points[i - 1]);
                else
                {
                    chart1.Series[3].Points.AddXY(dot[i - 1], -points[i - 1]);
                    dotEnd = dot[i - 1];
                    pointsEnd = points[i - 1];
                }
            }
            if (leftBorder < dot[0] )
                chart1.Series[2].Points.AddXY(dot[0], points[0]);
            if (a < c)
                chart1.Series[3].Points.AddXY(dotEnd, pointsEnd);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "F2";
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
            if (CheckData())
            {
                double a, c, leftBorder, rightBorder, step;
                double.TryParse(textBox1.Text, out a);
                double.TryParse(textBox2.Text, out c);
                double.TryParse(textBox3.Text, out leftBorder);
                double.TryParse(textBox4.Text, out rightBorder);
                double.TryParse(textBox5.Text, out step);
                a = Math.Abs(a);
                c = Math.Abs(c);
                List<double> points = new List<double>();
                List<double> dot = new List<double>();
                double left = leftBorder;
                while (left <= rightBorder + 0.00001)
                {
                    dot.Add(left);
                    left += step;
                }
                if (calculation.GetPoints(a, c, ref points, ref dot))
                {
                    calculetGraph(leftBorder, rightBorder, a, c, ref points, ref dot);
                }
                else
                    errorProvider1.SetError(textBox2, errorOfConst);
            }
        }
        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!aboutProgr.Visible)
            {
                about aboutProgram;
                if (jodFile.AgainAbout())
                    aboutProgram = new about(false);
                else
                    aboutProgram = new about(true);
                aboutProgram.ShowDialog();
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                GetDouble(textBox1, out double a);
                GetDouble(textBox2, out double c);
                GetDouble(textBox3, out double leftBorder);
                GetDouble(textBox4, out double rightBorder);
                GetDouble(textBox5, out double step);
                a = Math.Abs(a);
                c = Math.Abs(c);
                List<double> points = new List<double>();
                List<double> dot = new List<double>();
                double left = leftBorder;
                while (left <= rightBorder + 0.00001)
                {
                    dot.Add(left);
                    left += step;
                }
                if (calculation.GetPoints(a, c, ref points, ref dot))
                {
                    table = new Table();
                    table.createTable(dot, points);
                    table.ShowDialog();
                }
                else
                    errorProvider1.SetError(textBox2, errorOfConst);
            }
        }
    }
}
