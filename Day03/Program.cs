using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var fabric = new byte[1000,1000];

            var lines = new[] {
"#1 @ 1,3: 4x4",
"#2 @ 3,1: 4x4",
"#3 @ 5,5: 2x2"
            };

            var claims = 
                from line in File.ReadLines("input.txt")
                let parts = Regex.Match(line, @"#([0-9]*) @ ([0-9]*),([0-9]*): ([0-9]*)x([0-9]*)")
                let id = Convert.ToInt32(parts.Groups[1].Value)
                let x = Convert.ToInt32(parts.Groups[2].Value)
                let y = Convert.ToInt32(parts.Groups[3].Value)
                let width = Convert.ToInt32(parts.Groups[4].Value)
                let height = Convert.ToInt32(parts.Groups[5].Value)
                select new {id, x, y, width, height};

            var result = 0;
            foreach(var group in claims)
            {
                for(var y = group.y;y < group.y + group.height;y++)
                    for(var x = group.x;x < group.x + group.width;x++)
                    {
                        switch(fabric[x,y]) {
                            case 0:
                                fabric[x,y] = 1;
                                break;
                            case 1:
                                fabric[x,y] = 2;
                                result++;
                                break;
                        }
                    }
            }

            Console.WriteLine($"Part 1: {result}");

            foreach(var claim in claims)
            {
                var coords =
                    from y in Enumerable.Range(claim.y, claim.height)
                    from x in Enumerable.Range(claim.x, claim.width)
                    select fabric[x,y];
                if (coords.All(v => v == 1))
                {
                    Console.WriteLine($"Part 2: {claim.id}");
                    
                } 
            }
        }
    }
}
