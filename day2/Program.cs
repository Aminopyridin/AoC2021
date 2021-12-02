using System;

namespace day2
{
    class Program
    {
        static void Main(string[] args)
        {
            SecondPart();
        }

        static void FirstPart()
        {
            var commands = GetInput();
            var x = 0;
            var depth = 0;
            foreach (var command in commands)
            {
                var parts = command.Split(" ");
                var num = Int32.Parse(parts[1]);
                switch (parts[0])
                {
                    case "forward":
                        x += num;
                        break;
                    case "up":
                        depth -= num;
                        break;
                    case "down":
                        depth += num;
                        break;
                }
            }
            Console.WriteLine(x * depth);
        }
        
        static void SecondPart()
        {
            var commands = GetInput();
            long x = 0;
            long depth = 0;
            long aim = 0;
            foreach (var command in commands)
            {
                var parts = command.Split(" ");
                var num = Int32.Parse(parts[1]);
                switch (parts[0])
                {
                    case "forward":
                        x += num;
                        depth += aim * num;
                        break;
                    case "up":
                        aim -= num;
                        break;
                    case "down":
                        aim += num;
                        break;
                }
            }
            Console.WriteLine($"X is {x}, depth is {depth}, multiplication: {x * depth}");
        }

        static string[] GetInput()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;            
            var path = System.IO.Path.Combine(currentDirectory, @"../../../input.txt");
            return System.IO.File.ReadAllLines(path);
        }
    }
}