using System;
using System.Linq;
using System.IO;
using System.Text;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var frequencies = from line in File.ReadLines("input.txt")
                select Convert.ToInt32(line);

            var result = frequencies.Aggregate( (accum, next) => accum + next);

            Console.WriteLine($"Part 1: {result}");
        }
    }
}
