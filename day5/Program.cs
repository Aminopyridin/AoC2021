using System;
using System.Collections.Generic;
using System.Linq;

namespace day5
{
    class Program
    {
        static void Main(string[] args)
        {
            FirstPart();
            SecondPart();
        }

        static void FirstPart()
        {
            var input = GetInput();
            var vectors = input.Select(s => s.Split(" -> ").Select(s =>
            {
                var splitted = s.Split(',');
                return new Point(Int32.Parse(splitted[0]), Int32.Parse(splitted[1]));
            }).ToArray()).ToArray();

            var points = CalcEveryPoint(vectors, true);

            var usedMultipleTimes = points.Where(i => i.Value > 1).ToArray();
            Console.WriteLine(usedMultipleTimes.Length);
        }

        static void SecondPart()
        {
            var input = GetInput();
            var vectors = input.Select(s => s.Split(" -> ").Select(s =>
            {
                var splitted = s.Split(',');
                return new Point(Int32.Parse(splitted[0]), Int32.Parse(splitted[1]));
            }).ToArray()).ToArray();

            var points = CalcEveryPoint(vectors, false);
            var usedMultipleTimes = points.Where(i => i.Value > 1).ToArray();
            Console.WriteLine(usedMultipleTimes.Length);
        }

        static Dictionary<string, int> CalcEveryPoint(Point[][] vectors, bool needClean) 
        {
            var points = new Dictionary<string, int>();
            if (needClean)
            {
                vectors = vectors.Where(item => item[0].x == item[1].x || item[0].y == item[1].y).ToArray();
            }
            
            foreach (var vector in vectors)
            {
                var point1 = vector[0];
                var point2 = vector[1];
                var deltaX = point1.x == point2.x ? 0 : point1.x > point2.x ? -1 : 1;
                var deltaY = point1.y == point2.y ? 0 : point1.y > point2.y ? -1 : 1;
                AddPoint(points, point1);
                AddPoint(points, point2);
                

                for (int ix = point1.x + deltaX, iy = point1.y + deltaY; ix != point2.x || iy != point2.y; ix += deltaX, iy += deltaY)
                {
                    var point = new Point(ix, iy);
                    AddPoint(points, point);
                }
            }
            
            return points;
        }

        static void AddPoint(Dictionary<string, int> points, Point point)
        {
            if (!points.ContainsKey(point.ToString()))
            {
                points[point.ToString()] = 0;
            }

            points[point.ToString()]++;
            
        }
        
        static string[] GetInput()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;            
            var path = System.IO.Path.Combine(currentDirectory, @"../../../input.txt");
            return System.IO.File.ReadAllLines(path);
        }
    }

    class Point
    {
        public int x;
        public int y;
        
        public Point(int x0, int y0)
        {
            x = x0;
            y = y0;
        }

        public override string ToString()
        {
            return $"{x}_{y}";
        }
    }
}