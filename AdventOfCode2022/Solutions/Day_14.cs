using AdventOfCode2022.Tools;

namespace AdventOfCode2022.Solutions
{
    internal static class Day_14
    {
        public static void Solve()
        {
            AoCTools<int>.RunMeasureTimeAndLog(Part1, Part2, dayId: "14", testInput: false);
        }

        private static int Part1(IEnumerable<string> input)
        {
            int min_x = int.MaxValue; // just for logging
            int min_y = int.MaxValue; // just for logging

            int max_x = 0;
            int max_y = 0;

            List<List<Tuple<int, int>>> rockLines = new List<List<Tuple<int, int>>>();

            // find max x and y value
            foreach(var line in input) 
            {
                var points = line.Split(" -> ");
                List<Tuple<int, int>> rockPoints = new List<Tuple<int, int>>();

                foreach(var p in points)
                {
                    var xy = p.Split(',');

                    int x = int.Parse(xy[0]);
                    int y = int.Parse(xy[1]);

                    min_x = Math.Min(min_x, x); // just for logging
                    min_y = Math.Min(min_y, y); // just for logging

                    max_x = Math.Max(max_x, x);
                    max_y = Math.Max(max_y, y);

                    rockPoints.Add(new Tuple<int, int>(x, y));
                }

                rockLines.Add(rockPoints);
            }

            // initialize tab with enough space and fill with rocks marked as 1
            int[,] tab = new int[max_x + 5, max_y + 5];
            Console.WriteLine($"max_x: {max_x}, max_y: {max_y}");

            foreach(var rockPoints in rockLines)
            {
                for (int i = 0; i < rockPoints.Count - 1; i++)
                {
                    var point1 = rockPoints[i];
                    var point2 = rockPoints[i + 1];

                    if (point1.Item1 == point2.Item1) // x coord is the same, iterate over y
                    {
                        var start = point1.Item2 < point2.Item2 ? point1.Item2 : point2.Item2;
                        var end =   point1.Item2 < point2.Item2 ? point2.Item2 : point1.Item2;

                        int x = point1.Item1;

                        for (int y = start; y <= end; y++)
                        {
                            tab[x, y] = (int)StructureType.Rock;
                        }
                    }
                    else // x coord is different, iterate over it
                    {
                        var start = point1.Item1 < point2.Item1 ? point1.Item1 : point2.Item1;
                        var end =   point1.Item1 < point2.Item1 ? point2.Item1 : point1.Item1;

                        int y = point1.Item2;

                        for (int x = start; x <= end; x++)
                        {
                            tab[x, y] = (int)StructureType.Rock;
                        }
                    }
                }
            }

            Console.WriteLine(); Console.WriteLine();

            min_x = Math.Max(min_x - 5, 0);
            min_y = Math.Max(min_y - 5, 0);

            for (int y = min_y; y < max_y + 5; y++)
            {
                for (int x = min_x; x < max_x + 5; x++)
                {
                    if(tab[x, y] == (int)StructureType.Air)
                    {
                        Console.Write(" ");
                    }
                    else if(tab[x, y] == (int)StructureType.Rock)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write("o");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine(); Console.WriteLine();

            return -1;
        }

        enum StructureType
        {
            Air = 0,
            Rock = 1,
            Sand = 2
        }

        private static int Part2(IEnumerable<string> input)
        {


            return -1;
        }
    }
}
