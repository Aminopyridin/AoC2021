using System;

namespace days
{
    class Program
    {
        static void Main()
        {
            FirstPart();
            SecondPart();
        }

        static void FirstPart()
        {
            var measurements = GetInput();
            var prev = Int32.Parse(measurements[0]);
            var counter = 0;
            foreach (var mes in measurements)
            {
                var num = Int32.Parse(mes);
                if (num > prev)
                {
                    counter++;
                }

                prev = num;
            }
            
            Console.WriteLine(counter);
        }

        static void SecondPart()
        {
            var measurements = GetInput();
            var counter = 0;
            for (int i = 0; i < measurements.Length - 3; i++)
            {
                var num1 = Int32.Parse(measurements[i]);
                var num2 = Int32.Parse(measurements[i + 3]);
                if (num2 - num1 > 0)
                {
                    counter++;
                }
            }
            
            Console.WriteLine(counter);
        }

        static string[] GetInput()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;            
            var path = System.IO.Path.Combine(currentDirectory, @"../../../input.txt");
            return System.IO.File.ReadAllLines(path);
        }
    }
}