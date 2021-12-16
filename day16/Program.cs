using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;

namespace day16
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
            var input = GetInput()[0];
            var binaryString = ConvertToBinary(input);
            
            var res = GetPacketResult(binaryString, 0);
            Console.WriteLine($"First part: {res.Item3}");
        }

        static void SecondPart()
        {
            var input = GetInput()[0];
            var binaryString = ConvertToBinary(input);
            
            var res = GetPacketResult(binaryString, 0);
            Console.WriteLine($"Second part: {res.Item1}");
        }

        static Tuple<long, int, int> GetPacketResult(string rawPacket, int startIndex)
        {
            var index = startIndex;
            var versionBites = rawPacket.Substring(index, 3);
            var version = Convert.ToInt32(versionBites, 2);
            index += 3;

            var typeIdBites = rawPacket.Substring(index, 3);
            var typeId = Convert.ToInt32(typeIdBites, 2);
            index += 3;

            Tuple<List<long>, int, int> res;
            long result;
            switch (typeId)
            {
                case 0:
                    res = GetSubPackets(rawPacket, index);
                    result = res.Item1.Sum();
                    break;
                case 1:
                    res = GetSubPackets(rawPacket, index);
                    result = res.Item1.Aggregate(1L, (acc, i) => acc * i);
                    break;
                case 2:
                    res = GetSubPackets(rawPacket, index);
                    result = res.Item1.Min();
                    break;
                case 3:
                    res = GetSubPackets(rawPacket, index);
                    result = res.Item1.Max();
                    break;
                case 4:
                    res = GetNumber(rawPacket, index);
                    result = res.Item1[0];
                    break;
                case 5:
                    res = GetSubPackets(rawPacket, index);
                    result = res.Item1[0] > res.Item1[1] ? 1L : 0L;
                    break;
                case 6:
                    res = GetSubPackets(rawPacket, index);
                    result = res.Item1[0] < res.Item1[1] ? 1L : 0L;
                    break;
                case 7:
                    res = GetSubPackets(rawPacket, index);
                    result = res.Item1[0] == res.Item1[1] ? 1L : 0L;
                    break;
                default:
                    res = GetSubPackets(rawPacket, index);
                    result = res.Item1[0];
                    break;
            }
            
            return new Tuple<long, int, int>(result, res.Item2, version + res.Item3);
        }

        static Tuple<List<long>, int, int> GetSubPackets(string rawPacket, int startIndex)
        {
            var lengthTypeId = rawPacket[startIndex];
            var index = startIndex + 1;
            var counterLength = lengthTypeId == '0' ? 15 : 11;
            var lengthOfSubpacketsBinary = rawPacket.Substring(index, counterLength);
            var lengthOfSubpackets = Convert.ToInt32(lengthOfSubpacketsBinary, 2);
            index += counterLength;
            
            var numbers = new List<long>();
            var count = 0;
            var versionsSum = 0;
            while (count < lengthOfSubpackets)
            {
                var packet = GetPacketResult(rawPacket, index);
                if (lengthTypeId == '0')
                    count += packet.Item2 - index;
                else
                    count++;
                
                index = packet.Item2;
                numbers.Add(packet.Item1);
                versionsSum += packet.Item3;
            }
            
            return new Tuple<List<long>, int, int>(numbers, index, versionsSum);
        }

        static Tuple<List<long>, int, int> GetNumber(string rawPacket, int startIndex)
        {
            var result = new StringBuilder();
            var index = startIndex;
            for (var i = startIndex; i < rawPacket.Length; i += 5)
            {
                var readBites = rawPacket.Substring(i + 1, 4);
                result.Append(readBites);
                index += 5;
                var firstSymbol = rawPacket[i];
                if (firstSymbol == '0') break;
            }

            var resultNum = Convert.ToInt64(result.ToString(), 2);
            return new Tuple<List<long>, int, int>(new List<long>{resultNum}, index, 0);
        }

        static string ConvertToBinary(string input)
        {
            var bytes = input.ToCharArray();
            // Create a StringBuilder having appropriate capacity.
            var base2 = new StringBuilder(bytes.Length * 4);

            // Convert remaining bytes adding leading zeros.
            foreach (var s in bytes)
            {
                var num = Convert.ToInt32(s.ToString(), 16);
                base2.Append(Convert.ToString(num, 2).PadLeft(4, '0'));
            }

            return base2.ToString();
        }

        static string[] GetInput()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;            
            var path = System.IO.Path.Combine(currentDirectory, @"../../../input.txt");
            return System.IO.File.ReadAllLines(path);
        }
    }
}