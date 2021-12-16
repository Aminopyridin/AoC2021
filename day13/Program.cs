using System;
using System.Collections.Generic;
using System.Text;

namespace day13
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
            var (dots, instructions) = ParseInput(input);

            var newDots = MakeFold(instructions[0], dots);
            
            Console.WriteLine(newDots.Count);
        }

        static void SecondPart()
        {
            var input = GetInput();
            var (dots, instructions) = ParseInput(input);

            foreach (var instruction in instructions)
            {
                dots = MakeFold(instruction, dots);
            }

            var maxX = 0;
            var maxY = 0;
            foreach (var dot in dots)
            {
                var dotParts = dot.Split(",");
                var x = int.Parse(dotParts[0]);
                var y = int.Parse(dotParts[1]);

                if (x > maxX) maxX = x;
                if (y > maxY) maxY = y;
            }

            
            for (int y = 0; y <= maxY; y++)
            {
                var row = new StringBuilder(maxX);
                for (int x = 0; x <= maxX; x++)
                {
                    if (dots.Contains($"{x},{y}"))
                        row.Append("🀫");
                    else
                        row.Append("🀆");
                }
                Console.WriteLine(row);
            }
            
        }

        static HashSet<string> MakeFold(string instruction, HashSet<string> dots)
        {
            var cleared = instruction.Replace("fold along ", "");
            var parts = cleared.Split('=');
            var axe = parts[0];
            var line = int.Parse(parts[1]);

            var newDots = new HashSet<string>();
            foreach (var dot in dots)
            {
                var dotParts = dot.Split(",");
                var x = int.Parse(dotParts[0]);
                var y = int.Parse(dotParts[1]);
                
                if (axe == "y")
                {
                    if (y <= line)
                    {
                        newDots.Add(dot);
                        continue;
                    }
                    y = line - (y - line);
                    newDots.Add($"{x},{y}");
                    continue;
                }

                if (x <= line)
                {
                    newDots.Add(dot);
                    continue;
                }
                x = line - (x - line);
                newDots.Add($"{x},{y}");
            }

            return newDots;
        }

        static Tuple<HashSet<string>, List<string>> ParseInput(string[] input)
        {
            var instructions = new List<string>();
            var dots = new HashSet<string>();
            
            foreach (var row in input)
            {
                if (row.Trim().Length == 0) continue;
                
                if (row.StartsWith("fold along"))
                {
                    instructions.Add(row);
                    continue;
                }
                
                dots.Add(row);
            }

            return new Tuple<HashSet<string>, List<string>>(dots, instructions);
        }
        
        static string[] GetInput()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;            
            var path = System.IO.Path.Combine(currentDirectory, @"../../../input.txt");
            return System.IO.File.ReadAllLines(path);
        }
    }
}