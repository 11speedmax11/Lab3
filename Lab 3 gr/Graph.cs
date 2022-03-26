using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3_gr
{
    class Graph
    {
        double a { get { return a; } set { a = Math.Round(Math.Abs(value), 3); } }
        double c { get { return c; } set { c = Math.Round(Math.Abs(value), 3); } }
        double leftBorder { get; set; }
        double rightBorder { get; set; }
        double step { get; set; }
        List<double> points { get; set; }
        List<double> dot { get; set; }
    }
}
