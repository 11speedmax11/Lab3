using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3_gr
{
    class Calculation
    {
        public bool GetPoints(double a, double c, ref List<double> points, ref List<double> dot)
        {
            int k = dot.Count();
            int num = 0;
            for (int i = 0; i < k; i++)
            {
                if (GetY(dot[num], a, c, out double y))
                {
                    points.Add(y);
                    num += 1;
                }
                else
                {
                    dot.RemoveAt(num);
                }
            }
            if (points.Count <= 2)
                return false;
            else
                return true;
        }
        private bool GetY(double x, double a, double c, out double y)
        {
            double firstRoot = Math.Sqrt(Math.Pow(a, 4) + 4 * Math.Pow(c, 2) * Math.Pow(x, 2));
            double radicalExpression = firstRoot - Math.Pow(x, 2) - Math.Pow(c, 2);

            if (radicalExpression < 0)
            {
                y = 0;
                return false;
            }
            else
            {
                y = Math.Sqrt(radicalExpression);
                return true;
            }
        }
    }
}
