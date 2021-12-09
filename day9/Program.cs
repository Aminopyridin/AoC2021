using System;
using System.Collections.Generic;
using System.Linq;

namespace day9
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
            var smallestCells = FindSmallest(input);
            
            var sum = smallestCells.Aggregate(0, (acc, el) => acc + int.Parse(input[el.y][el.x].ToString()) + 1);

            Console.WriteLine(sum);
        }

        static void SecondPart()
        {
            var input = GetInput();
            var smallestCells = FindSmallest(input);

            var basinSquares = smallestCells
                .Select(cell => FindBasinSquare(cell, input))
                .OrderByDescending(i => i)
                .Take(3)
                .Aggregate(1, (acc, i) => acc * i);
            Console.WriteLine(basinSquares);
        }

        static List<Cell> FindSmallest(string[] field)
        {
            var smallestCells = new List<Cell>();
            
            for (var i = 0; i < field.Length; i++)
            {
                var row = field[i];
                for (var j = 0; j < row.Length; j++)
                {
                    var cell = row[j];
                    var neighboursCoords = GetNeighboursCoords(j, i, field);

                    var isSmallest = neighboursCoords.All(c => field[c.y][c.x] > cell);
                    if (isSmallest)
                    {
                        smallestCells.Add(new Cell(j, i));
                    }
                }
            }

            return smallestCells;
        }

        static List<Cell> GetNeighboursCoords(int x, int y, string[] field)
        {
            var neighbours = new List<Cell>();

            if (x > 0) neighbours.Add(new Cell(x - 1, y));

            if (x < field[0].Length - 1) neighbours.Add(new Cell(x + 1, y));
            
            if (y > 0) neighbours.Add(new Cell(x, y - 1));

            if (y < field.Length - 1) neighbours.Add(new Cell(x, y + 1));

            return neighbours;
        }

        static int FindBasinSquare(Cell start, string[] field)
        {
            var visited = new HashSet<string>();
            var queue = new Queue<Cell>();
            queue.Enqueue(start);

            while (queue.Count > 0) {
                var cell = queue.Dequeue();

                if (visited.Contains(cell.ToString()))
                    continue;

                visited.Add(cell.ToString());

                var neighbours = GetNeighboursCoords(cell.x, cell.y, field);
                foreach (var neighbor in neighbours)
                {
                    if (field[neighbor.y][neighbor.x] == '9') continue;
                    if (!visited.Contains(neighbor.ToString()))
                        queue.Enqueue(neighbor);
                }
            }

            return visited.Count;
        }
        
        static string[] GetInput()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;            
            var path = System.IO.Path.Combine(currentDirectory, @"../../../input.txt");
            return System.IO.File.ReadAllLines(path);
        }
    }

    class Cell
    {
        public int x;
        public int y;

        public Cell (int x0, int y0)
        {
            x = x0;
            y = y0;
        }

        public override string ToString()
        {
            return $"{y}_{x}";
        }
    }
}