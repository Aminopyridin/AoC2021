using System;
using System.Linq;

namespace day7
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
            var input = GetInput()[0].Split(',').Select(int.Parse);
            var sortedInput = input.OrderBy(v => v).ToArray();

            int smallest = int.MaxValue;
            for (int i = sortedInput[0]; i <= sortedInput[^1]; i++)
            {
                var fuelSum = sortedInput.Aggregate(0, (acc, item) => acc + Math.Abs(item - i));
                if (fuelSum < smallest) smallest = fuelSum;
            }
            
            Console.WriteLine(smallest);
        }

        static void SecondPart()
        {
            var input = GetInput()[0].Split(',').Select(int.Parse);
            var sortedInput = input.OrderBy(v => v).ToArray();

            int smallest = int.MaxValue;
            for (int i = sortedInput[0]; i <= sortedInput[^1]; i++)
            {
                var fuelSum = sortedInput.Aggregate(0, (acc, item) =>
                {
                    var delta = Math.Abs(item - i);
                    var sumOfFuel = (double)(1 + delta) / 2 * delta;
                    return acc + (int)sumOfFuel;
                });
                if (fuelSum < smallest) smallest = fuelSum;
            }
            
            Console.WriteLine(smallest);
        }
        
        static string[] GetInput()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;            
            var path = System.IO.Path.Combine(currentDirectory, @"../../../input.txt");
            return System.IO.File.ReadAllLines(path);
        }
    }
}