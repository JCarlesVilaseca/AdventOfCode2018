using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Day4
{
    class Program
    {
        const string pattern_begin = @"Guard #([0-9]*) begins shift";
        const string pattern_sleep = @"\[(.*)\] falls asleep";
        const string pattern_wakes = @"\[(.*)\] wakes up";
     
        static void Main(string[] args)
        {
            var guard_sleeps = new List<(int, int)>();
            var current_guard = 0;
            DateTime? start_sleep = null;

            foreach(var line in File.ReadLines("test.txt").OrderBy(l => l))
            {
                if (Regex.IsMatch(line, pattern_begin))
                {
                    var parts = Regex.Match(line, pattern_begin);
                    current_guard = Convert.ToInt32(parts.Groups[1].Value);
                }
                else if (Regex.IsMatch(line, pattern_sleep))
                {
                    var parts = Regex.Match(line, pattern_sleep);
                    start_sleep = DateTime.ParseExact(parts.Groups[1].Value, "yyyy-MM-dd hh:mm", CultureInfo.InvariantCulture);
                }
                else if (Regex.IsMatch(line, pattern_wakes))
                {
                    var parts = Regex.Match(line, pattern_wakes);
                    var end_sleep = DateTime.ParseExact(parts.Groups[1].Value, "yyyy-MM-dd hh:mm", CultureInfo.InvariantCulture);
                    var guard_sleep = (current_guard, (int)(end_sleep - start_sleep.Value).TotalMinutes);
                    guard_sleeps.Add( guard_sleep );
                }
            }

            var total_sleeps = 
                from gs in guard_sleeps.OrderBy(x => x.Item1)
                group gs by gs.Item1 into g
                select new { Guard = g.Key, Sleep = g.Sum(x => x.Item2)};
        
            var guard_id = 
                total_sleeps
                .OrderByDescending(x => x.Sleep)
                .First().Guard;

            foreach(var i in total_sleeps.OrderByDescending(x => x.Sleep))
                Console.WriteLine(i);

            Console.WriteLine($"Part 1: {guard_id * result.Sleep}");
        }
    }
}
