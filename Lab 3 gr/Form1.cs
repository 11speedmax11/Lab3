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
        Graph graph = new Graph();
        static double a, c, leftBorder, rightBorder, step;
        double[] parameters = new[] { a, c, leftBorder, rightBorder, step };
        JobFile jodFile = new JobFile();
        Calculation calculation = new Calculation();
        about aboutProgr = new about(false);
        List<double> points = new List<double>();
        List<double> dot = new List<double>();
        const int numberGraphs = 4;
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
            saveResultToolStripMenuItem.Enabled = false;
            saveInputToolStripMenuItem.Enabled = false;
        }
        bool GetDouble(string text, out double num)
        {
           return (double.TryParse(text, out num) && !text.Contains(','));
        }
        bool CheckData(string strA, string strC, string strLeftBorder, string strRightBorder, string strStep) 
        {
            errorProvider1.Clear();
            bool flag = true;
            if (!GetDouble(strA, out a)) 
            {
                errorProvider1.SetError(textBox1, error);
                flag = false;
            }
            if (!GetDouble(strC, out c))
            {
                errorProvider1.SetError(textBox2, error);
                flag = false;
            }
            if (!GetDouble(strLeftBorder, out leftBorder))
            {
                errorProvider1.SetError(textBox3, error);
                flag = false;
            }
            if (!GetDouble(strRightBorder, out rightBorder))
            {
                errorProvider1.SetError(textBox4, error);
                flag = false;
            }
            if (!GetDouble(strStep, out step))
            {
                errorProvider1.SetError(textBox5, error);
                flag = false;
            }
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
        private void button1_Click(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "F2";
            for(int i = 0; i < numberGraphs; i++)
                chart1.Series[i].Points.Clear();

            if (CheckData(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text))
            {
                a = Math.Round(Math.Abs(a), 3);
                c = Math.Round(Math.Abs(c), 3);
                points = new List<double>();
                dot = new List<double>();

                for(double i = leftBorder; i <= rightBorder + 0.00001; i += step)
                    dot.Add(Math.Round(i, 3));

                if (calculation.GetPoints(a, c, ref points, ref dot))
                {
                    saveResultToolStripMenuItem.Enabled = true;
                    saveInputToolStripMenuItem.Enabled = true;

                    List<double[]>[] graphs =calculation.calculetGraph(leftBorder, rightBorder, a, c, ref points, ref dot);

                    for (int i = 0; i < graphs.Length; i++) 
                        for (int j = 0; j < graphs[i].Count; j++)
                            chart1.Series[i].Points.AddXY(graphs[i][j][0], graphs[i][j][1]);
                }
                else
                    errorProvider1.SetError(textBox2, errorOfConst);
            }
        }
        private void readDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog fileTable = new OpenFileDialog();
            fileTable.Filter = "Text files(*.txt)|*.txt";
            fileTable.ShowDialog();
            string filename = fileTable.FileName;
            try
            {
                string[] readText = System.IO.File.ReadAllLines(filename);
                if (readText.Length >= parameters.Length)
                {
                    if (CheckData(readText[0], readText[1], readText[2], readText[3], readText[4]))
                    {
                        textBox1.Text = readText[0];
                        textBox2.Text = readText[1];
                        textBox3.Text = readText[2];
                        textBox4.Text = readText[3];
                        textBox5.Text = readText[4];
                    }
                    else 
                    {
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                    }
                }
                else
                    MessageBox.Show("invalid data format", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch 
            {
                MessageBox.Show("file was not selected, data was not read", "warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void saveResultInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.xlsx)|*.xlsx";
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.ShowDialog();
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
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "F2";
            for(int i = 0; i < numberGraphs; i++)
                chart1.Series[i].Points.Clear();

            if (CheckData(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text))
            {
                a = Math.Abs(a);
                c = Math.Abs(c);
                points = new List<double>();
                dot = new List<double>();

                for (double i = leftBorder; i <= rightBorder + 0.00001; i += step)
                    dot.Add(Math.Round(i, 3));

                if (calculation.GetPoints(a, c, ref points, ref dot))
                {
                    saveResultToolStripMenuItem.Enabled = true;
                    saveInputToolStripMenuItem.Enabled = true;
                    List<double[]>[] graphs = calculation.calculetGraph(leftBorder, rightBorder, a, c, ref points, ref dot);
                    for (int i = 0; i < graphs.Length; i++)
                        for (int j = 0; j < graphs[i].Count; j++)
                            chart1.Series[i].Points.AddXY(graphs[i][j][0], graphs[i][j][1]);

                    Table table = new Table();
                    table.createTable(dot, points);
                    table.ShowDialog();
                }
                else
                    errorProvider1.SetError(textBox2, errorOfConst);
            }
        }

        private void saveResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileTable = new SaveFileDialog();
            fileTable.Filter = "Text files(*.txt)|*.txt";
            fileTable.CreatePrompt = true;
            fileTable.OverwritePrompt = true;
            fileTable.ShowDialog();
            string filename = fileTable.FileName;
            List<string> table = new List<string>();
            table.Add("X".PadLeft(4, ' ').PadRight(13, ' ') + "Y".PadLeft(4, ' ').PadRight(13, ' ') + "-Y".PadLeft(4, ' ').PadRight(13, ' '));
            for (int i = 0; i < dot.Count(); i++)
            {
                table.Add(dot[i].ToString().PadRight(13, ' ') + points[i].ToString().PadRight(13, ' ') + (-points[i]).ToString().PadRight(13, ' '));
            }
            try
            {
                System.IO.File.WriteAllLines(filename, table);
            }
            catch
            {
                MessageBox.Show("file was not selected, data was not saved", "warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void saveInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileTable = new SaveFileDialog();
            fileTable.Filter = "Text files(*.txt)|*.txt";
            fileTable.CreatePrompt = true;
            fileTable.OverwritePrompt = true;
            fileTable.ShowDialog();
            string filename = fileTable.FileName;
            List<string> table = new List<string>
            {
                a.ToString(),
                c.ToString(),
                leftBorder.ToString(),
                rightBorder.ToString(),
                step.ToString()
            };
            try
            {
                System.IO.File.WriteAllLines(filename, table);
            }
            catch
            {
                MessageBox.Show("file was not selected, data was not saved", "warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
