using System;
using System.Linq;
using System.Numerics;

namespace day6
{
    class Program
    {
        private const int DaysCountFirstPart = 80;
        private const int DaysCountSecondPart = 256;
        static void Main(string[] args)
        {
            FirstPart();
            SecondPart();
        }

        static void FirstPart()
        {
            var numbers = GetInput()[0].Split(',');
            var counters = CountInput(numbers);

            MakeRounds(counters, DaysCountFirstPart);
            

            var fishCount = counters.Aggregate((BigInteger)0, (acc, el) => acc + el);
            Console.WriteLine(fishCount);
        }

        static void SecondPart()
        {
            var numbers = GetInput()[0].Split(',');
            var counters = CountInput(numbers);

            MakeRounds(counters, DaysCountSecondPart);
            

            var fishCount = counters.Aggregate((BigInteger)0, (acc, el) => acc + el);
            Console.WriteLine(fishCount);
        }

        static void MakeRounds(BigInteger[] counters, int daysCount)
        {
            for (int i = 0; i < daysCount; i++)
            {
                var temp = counters[0];
                for (int j = 0; j < counters.Length; j++)
                {
                    if (j == 0) continue;
                    counters[j - 1] = counters[j];

                    if (j == counters.Length - 1)
                    {
                        counters[j] = temp;
                        counters[6] += temp;
                    }
                }
            }

        }

        static BigInteger[] CountInput(string[] inputNums)
        {
            var counters = new BigInteger[9];

            foreach (var el in inputNums)
            {
                var num = Int32.Parse(el);
                counters[num]++;
            }

            return counters;
        }
        
        static string[] GetInput()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;            
            var path = System.IO.Path.Combine(currentDirectory, @"../../../input.txt");
            return System.IO.File.ReadAllLines(path);
        }
    }
}