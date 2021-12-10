using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace day10
{
    class Program
    {
        private static readonly char[] openings = {'{', '(', '<', '['};
        private static readonly Dictionary<char, char> closings = new() 
        {
            [')'] = '(',
            ['}'] = '{',
            [']'] = '[',
            ['>'] = '<'
        };
        private static readonly Dictionary<char, int> weights = new() 
        {
            [')'] = 3,
            ['}'] = 1197,
            [']'] = 57,
            ['>'] = 25137,
            [' '] = 0,
        };
        private static readonly Dictionary<char, int> otherWeights = new() 
        {
            ['('] = 1,
            ['['] = 2,
            ['{'] = 3,
            ['<'] = 4
        };
        
        static void Main(string[] args)
        {
            FirstPart();
            SecondPart();
        }

        static void FirstPart()
        {
            var input = GetInput();

            var sum = 0;
            foreach (var s in input)
            {
                var wrongSymbol = FindWrongSymbol(s);
                sum += weights[wrongSymbol];
            }
            
            Console.WriteLine(sum);
        }

        static void SecondPart()
        {
            var input = GetInput();

            var sums = new List<BigInteger>();
            foreach (var s in input)
            {
                sums.Add(CompleteString(s));
            }

            var sorted = sums.Where(i => i != 0).OrderBy(i => i).ToArray();
            var middleIndex = sorted.Length / 2;
            
            Console.WriteLine(sorted[middleIndex]);
        }

        static char FindWrongSymbol(string row)
        {
            var openedStack = new Stack<char>();
            
            foreach (var symbol in row)
            {
                if (openings.Contains(symbol))
                {
                    openedStack.Push(symbol);
                    continue;
                }

                var openForThisClosing = closings[symbol];

                if (openedStack.First() == openForThisClosing)
                {
                    openedStack.Pop();
                }
                else
                {
                    return symbol;
                }
            }

            return ' ';
        }

        static BigInteger CompleteString(string row)
        {
            var openedStack = new Stack<char>();
            
            foreach (var symbol in row)
            {
                if (openings.Contains(symbol))
                {
                    openedStack.Push(symbol);
                    continue;
                }

                var openForThisClosing = closings[symbol];

                if (openedStack.First() == openForThisClosing)
                {
                    openedStack.Pop();
                }
                else
                {
                    return 0;
                }
            }

            var sum = new BigInteger(0);
            while (openedStack.Count > 0)
            {
                var c = openedStack.Pop();
                sum *= 5;
                sum += otherWeights[c];
            }

            return sum;
        }
        
        static string[] GetInput()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;            
            var path = System.IO.Path.Combine(currentDirectory, @"../../../input.txt");
            return System.IO.File.ReadAllLines(path);
        }
    }
}