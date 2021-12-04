using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day4
{
    class Program
    {
        const int BoardSize = 5;
        
        static void Main(string[] args)
        {
            FirstPart();
            SecondPart();
        }

        static void FirstPart()
        {
            var input = GetInput();
            var numbers = input[0].Split(",");
            var boardsLines = ReadBoards(input);

            var winningRes = FindWinLine(numbers, boardsLines);
            var winLine = winningRes.Item1;
            var winNum = Int32.Parse(winningRes.Item2);
            var markedNums = winningRes.Item3; 

            var boardNum = winLine.Split('_')[0];
            var sumOfUnmarked = CalcSumOfUnmarked(boardNum, boardsLines, markedNums);
            Console.WriteLine(sumOfUnmarked * winNum);
        }

        static void SecondPart()
        {
            // Где-то баг, и цикл не доходит до конца (остается две доски в конце), но искать как исправить, я замучалась
            var input = GetInput();
            var numbers = input[0].Split(",");
            var boardsLines = ReadBoards(input);
            var boardsCount = (input.Length - 2) / (BoardSize + 1);
            for (int i = 0; i < boardsCount; i++)
            {
                var winningRes = FindWinLine(numbers, boardsLines);
                var winLine = winningRes.Item1;
                var boardNum = winLine.Split('_')[0];

                if (i < boardsCount - 1)
                {
                    DelWinBoard(boardNum, boardsLines);
                    continue;
                }
                
                var winNum = Int32.Parse(winningRes.Item2);
                var markedNums = winningRes.Item3; 

                var sumOfUnmarked = CalcSumOfUnmarked(boardNum, boardsLines, markedNums);
                Console.WriteLine(winNum);
                Console.WriteLine(sumOfUnmarked);
                Console.WriteLine(sumOfUnmarked * winNum);
            }
        }

        static void DelWinBoard(string boardNum, Dictionary<string, string[]> boardsLines)
        {
            for (int i = 0; i < BoardSize; i++)
            {
                var id = $"{boardNum}_r{i}";
                boardsLines.Remove(id);
            }
            for (int i = 0; i < BoardSize; i++)
            {
                var id = $"{boardNum}_c{i}";
                boardsLines.Remove(id);
            }
        }

        static int CalcSumOfUnmarked(string boardNum, Dictionary<string, string[]> boardsLines, HashSet<string> markedNums)
        {
            var sum = 0;
            for (int i = 0; i < BoardSize; i++)
            {
                var id = $"{boardNum}_r{i}";
                var row = boardsLines[id];
                var rowSum = row.Aggregate(0, (acc, item) =>
                {
                    if (markedNums.Contains(item)) return acc;
                    
                    return Int32.Parse(item) + acc;
                });
                sum += rowSum;
            }

            return sum;
        }

        static Tuple<string, string, HashSet<string>> FindWinLine(string[] numbers, Dictionary<string, string[]> boardsLines)
        {
            var markedNums = new HashSet<string>();
            string winLine = null;
            string winNum = null;
            foreach (var number in numbers)
            {
                markedNums.Add(number);
                
                if (markedNums.Count < BoardSize) continue;

                foreach (var line in boardsLines)
                {
                    if (line.Value.All(s => markedNums.Contains(s)))
                    {
                        winLine = line.Key;
                        winNum = number;
                        break;
                    }
                }

                if (winLine != null) break;
            }

            return new Tuple<string, string, HashSet<string>>(winLine, winNum, markedNums);
        }

        static Dictionary<string, string[]> ReadBoards(string[] rawInput)
        {
            var rowsAndColumns = new Dictionary<string, string[]>();

            for (int i = 2; i < rawInput.Length; i++)
            {
                var row = rawInput[i].Trim();
                if (row.Length == 0) continue;

                var normalizedI = i - 2;
                var rowNumber = normalizedI % (BoardSize + 1);
                var boardNumber = (normalizedI - rowNumber) / (BoardSize + 1);

                var rowSymbols = Regex.Split(row, "\\s+");
                var rowId = $"{boardNumber}_r{rowNumber}";
                rowsAndColumns[rowId] = rowSymbols;

                for (int j = 0; j < rowSymbols.Length; j++)
                {
                    var colId = $"{boardNumber}_c{j}";
                    if (!rowsAndColumns.ContainsKey(colId))
                    {
                        rowsAndColumns[colId] = new string[BoardSize];
                    }

                    rowsAndColumns[colId][rowNumber] = rowSymbols[j];
                }
            }

            return rowsAndColumns;
        }
        
        static string[] GetInput()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;            
            var path = System.IO.Path.Combine(currentDirectory, @"../../../input.txt");
            return System.IO.File.ReadAllLines(path);
        }
    }
}