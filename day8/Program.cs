using System;
using System.Linq;

namespace day8
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

            var count = 0;
            foreach (var row in input)
            {
                var parts = row.Split(" | ");
                var outputs = parts[1].Split(' ');
                count += outputs.Count(el => el.Length is 2 or 3 or 4 or 7);
            }
            
            Console.WriteLine(count);
        }

        static void SecondPart()
        {
            var input = GetInput();

            var sum = 0;
            foreach (var row in input)
            {
                var parts = row.Split(" | ").Select(s => s.Split(' ')).ToArray();
                var inputNums = parts[0];
                var nums = GuessingNumbers(inputNums);

                var output = "";
                foreach (var s in parts[1])
                {
                    var finding = s.ToCharArray();
                    for (var i = 0; i < nums.Length; i++)
                    {
                        var num = nums[i];
                        if (num.Length != s.Length) continue;
                        var diff = num.ToCharArray().Except(finding).ToArray();
                        if (diff.Length == 0) output += i;
                    }
                }

                sum += int.Parse(output); 
            }
            
            Console.WriteLine(sum);
            
        }

        static string[] GuessingNumbers(string[] input)
        {
            var nums = new string[10];
            foreach (var s in input)
            {
                if (s.Length == 2)
                    nums[1] = s;
                if (s.Length == 4)
                    nums[4] = s;
                if (s.Length == 3)
                    nums[7] = s;
                if (s.Length == 7)
                    nums[8] = s;
            }

            // find 3
            nums[3] = string.Join("", input
                .Where(s => s.Length == 5)
                .Select(s => s.ToCharArray())
                .First(s => s.Contains(nums[1][0]) && s.Contains(nums[1][1]))
            );
            
            // find 6 
            nums[6] = string.Join("", input
                .Where(s => s.Length == 6)
                .Select(s => s.ToCharArray())
                .First(s => !(s.Contains(nums[1][0]) && s.Contains(nums[1][1])))
            );
            
            // find 5
            nums[5] = input
                .Where(s => s.Length == 5 && s != nums[3])
                .First(s =>
                {
                    var six = nums[6].ToCharArray();
                    var possible = s.ToCharArray();
                    var diff = six.Except(possible).ToArray();
                    return diff.Length == 1;
                }
            );
            
            // find 2
            nums[2] = input.First(s => s.Length == 5 && s != nums[3] && s != nums[5]);
            
            // find 9
            nums[9] = input
                .Where(s => s.Length == 6 && s != nums[6])
                .First(s =>
                    {
                        var five = nums[5].ToCharArray();
                        var possible = s.ToCharArray();
                        var diff = possible.Except(five).ToArray();
                        return diff.Length == 1;
                    }
                );
            
            //find 0
            nums[0] = input.First(s => s.Length == 6 && s != nums[9] && s != nums[6]);

            return nums;
        }
        
        static string[] GetInput()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;            
            var path = System.IO.Path.Combine(currentDirectory, @"../../../input.txt");
            return System.IO.File.ReadAllLines(path);
        }
    }
}