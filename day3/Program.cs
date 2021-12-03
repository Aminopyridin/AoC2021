using System;
using System.Linq;
using System.Text;

namespace day3
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
            var binaryNums = GetInput();
            var len = binaryNums[0].Length;
            var gamma = Enumerable.Range(0, len).Select(i => FindMostCommonChar(binaryNums, i)).ToArray();
            var epsilon = Enumerable.Range(0, len).Select(i => FindMostCommonChar(binaryNums, i) == '1' ? '0' : '1').ToArray();
            var gammaNum = Convert.ToInt32(string.Join("", gamma), 2);
            var epsilonNum = Convert.ToInt32(string.Join("", epsilon), 2);
            Console.WriteLine(gammaNum * epsilonNum);
        }

        static void SecondPart()
        {
            var binaryNums = GetInput();
            var len = binaryNums[0].Length;
            var oxygenNums = binaryNums;
            var carbonDioxideNums = binaryNums;
            
            for (int i = 0; i < len; i++)
            {
                if (oxygenNums.Length == 1) break;
                var popularChar = FindMostCommonChar(oxygenNums, i);
                oxygenNums = Array.FindAll(oxygenNums, s => s[i] == popularChar);
            }
            
            for (int i = 0; i < len; i++)
            {
                if (carbonDioxideNums.Length == 1) break;
                var popularChar = FindMostCommonChar(carbonDioxideNums, i) == '1' ? '0' : '1';
                carbonDioxideNums = Array.FindAll(carbonDioxideNums, s => s[i] == popularChar);
            }
            
            var oxygenNum = Convert.ToInt32(oxygenNums[0], 2);
            var carbonDioxideNum = Convert.ToInt32(carbonDioxideNums[0], 2);
            Console.WriteLine(oxygenNum * carbonDioxideNum);
        }

        static char FindMostCommonChar(string[] elems, int index)
        {
            var counter = 0;
            foreach (var elem in elems)
            {
                if (elem[index] == '1') counter++;
            }
            
            return counter >= (elems.Length - counter) ? '1' : '0';
        }

        static string[] GetInput()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;            
            var path = System.IO.Path.Combine(currentDirectory, @"../../../input.txt");
            return System.IO.File.ReadAllLines(path);
        }
    }
}