using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_3_gr
{
    public partial class Table : Form
    {
       public Table()
        {
            InitializeComponent();
        }

        public void createTable(List<double> dot, List<double> points)
        {

            DataTable tableDot = new DataTable();
            
            tableDot.Columns.Add("Y", typeof(double));
            tableDot.Columns.Add("X", typeof(double));
            tableDot.Columns.Add("-X", typeof(double));
            for (int i = 0; i < dot.Count; i++)
            {
                tableDot.Rows.Add(dot[i], points[i], -points[i]);
            }
            dataGridView1.DataSource = tableDot;
            dataGridView1.Columns[0].DefaultCellStyle.Format = "F3";
            dataGridView1.Columns[1].DefaultCellStyle.Format = "F3";
            dataGridView1.Columns[2].DefaultCellStyle.Format = "F3";
        }

    }
}
