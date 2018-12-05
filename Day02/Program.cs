using System;
using System.Linq;
using System.IO;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var counts = 
                from line in File.ReadLines("input.txt")
                let groups = 
                    from chr in line.OrderBy(x => x)
                    group chr by chr into g
                    select g.Count()

                let two = groups.Any(x => x== 2)?1:0
                let three = groups.Any(x => x == 3)?1:0
                select new { two, three };

            var result = counts.Aggregate( (acum, item) => new { two = acum.two + item.two, three = acum.three + item.three});

            Console.WriteLine($"Result: {result.two * result.three}");
        }
    }
}
