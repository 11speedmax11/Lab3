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
        public List<double[]>[] calculetGraph(double leftBorder, double rightBorder, double a, double c, ref List<double> points, ref List<double> dot)
        {
            List<double[]> firstGraph = new List<double[]>();
            List<double[]> secondGraph = new List<double[]>();
            List<double[]> thirdGraph = new List<double[]>();
            List<double[]> fourthGraph = new List<double[]>();
            double dotStart = 0;
            double pointsStart = 0;
            for (int i = 0; i < dot.Count(); i++)
            {
                if (dot[i] < 0 || a >= c)
                {
                    firstGraph.Add(new double[] { dot[i], points[i] });
                    dotStart = dot[i];
                    pointsStart = points[i];
                }
                else
                {
                    secondGraph.Add(new double[] { dot[i], points[i] });
                }
            }
            if (rightBorder - dotStart > 0.001)
            {
                thirdGraph.Add(new double[] { dotStart, pointsStart });
            }

            double dotEnd = 0;
            double pointsEnd = 0;

            if (rightBorder - dot[dot.Count - 1] > 0.001 && a < c)
            {
                fourthGraph.Add(new double[] { dot[dot.Count - 1], points[dot.Count - 1] });
            }
            else if (rightBorder - dot[dot.Count - 1] > 0.001)
            {
                secondGraph.Add(new double[] { dot[dot.Count - 1], points[dot.Count - 1] });
            }

            for (int i = dot.Count(); i > 0; i--)
            {
                if (dot[i - 1] < 0 || a >= c)
                {
                    thirdGraph.Add(new double[] { dot[i - 1], -points[i - 1] });
                }
                else
                {
                    fourthGraph.Add(new double[] { dot[i - 1], -points[i - 1] });
                    dotEnd = dot[i - 1];
                    pointsEnd = points[i - 1];
                }
            }
            if (leftBorder < dot[0])
            {
                thirdGraph.Add(new double[] { dot[0], points[0] });
            }
            if (a < c)
            {
                fourthGraph.Add(new double[] { dotEnd, pointsEnd });
            }
            return new List<double[]>[] { firstGraph, secondGraph, thirdGraph, fourthGraph };
        }
        public bool GetPoints(double a, double c, ref List<double> points, ref List<double> dot)
        {
            int k = dot.Count();
            int num = 0;
            for (int i = 0; i < k; i++)
            {
                if (GetY(dot[num], a, c, out double y))
                {
                    points.Add(Math.Round(y, 3));
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
