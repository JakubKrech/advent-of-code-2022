using AdventOfCode2022.Tools;

namespace AdventOfCode2022.Solutions
{
    internal static class Day_08
    {
        public static void Solve()
        {
            var input = InputData.ReadTestInput("Day_08.txt");
            // var input = InputData.ReadInput("Day_08.txt");

            Console.WriteLine("=== Day 08 ===");
            Console.WriteLine($"Part 1: {Part1(input)}");
            Console.WriteLine($"Part 2: {Part2(input)}");
        }

        private static int Part1(IEnumerable<string> input)
        {
            List<List<int>> list2d = new List<List<int>>();

            int lineAmmount = input.Count();
            int lineLength = input.First().Length;

            for(int i = 0; i < lineAmmount; i++)
            {
                list2d.Add(new List<int>());
            }

            for(int i = 0; i < input.Count(); i++)
            {
                for (int j = 0; j < input.First<string>().Length; j++)
                {
                    string line = input.ElementAt(i);
                    char x = line[j];
                    list2d[i].Add(int.Parse(x.ToString()));
                }
            }

            foreach(var line in list2d)
            {
                foreach(var ch in line)
                {
                    Console.Write(ch);
                }
                Console.WriteLine();
            }

            return -1;
        }

        private static int Part2(IEnumerable<string> input)
        {


            return -1;
        }
    }
}