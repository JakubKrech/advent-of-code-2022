using AdventOfCode2022.Tools;

namespace AdventOfCode2022.Solutions
{
    internal static class Day_03
    {
        public static void Solve()
        {
            // var input = InputData.ReadTestInput("Day_03.txt");
            var input = InputData.ReadInput("Day_03.txt");

            Console.WriteLine("=== Day 03 ===");
            Console.WriteLine($"Part 1: {Part1(input)}");
            Console.WriteLine($"Part 2: {Part2(input)}");
        }

        private static int Part1(IEnumerable<string> input)
        {
            int sumOfPriorities = 0;

            foreach (var rucksack in input)
            {
                // number of items in both compartments is the same
                int length = rucksack.Length;
                string firstCompartment = rucksack[..(length / 2)];
                string secondCompartment = rucksack.Substring(length / 2, length / 2);

                foreach(char c in firstCompartment)
                {
                    if(secondCompartment.Contains(c))
                    {
                        sumOfPriorities += CalculatePriority(c);
                        break;
                    }
                }
            }

            return sumOfPriorities;
        }

        private static int Part2(IEnumerable<string> input)
        {
            int currentGroup = 0;
            int lineAmmount = input.Count();

            int sumOfPriorities = 0;

            while (currentGroup * 3 < lineAmmount)
            {
                string firstGroup = input.ElementAt(0 + currentGroup * 3);
                string secondGroup = input.ElementAt(1 + currentGroup * 3);
                string thirdGroup = input.ElementAt(2 + currentGroup * 3);

                foreach(var character in firstGroup)
                {
                    if(secondGroup.Contains(character) && thirdGroup.Contains(character))
                    {
                        sumOfPriorities += CalculatePriority(character);
                        break;
                    }
                }

                currentGroup++;
            }

            return sumOfPriorities;
        }

        private static int CalculatePriority(char c)
        {
            if (c >= 'a' && c <= 'z')
            {
                return c - 'a' + 1;
            }
            else // c >= 'A' || c <= 'Z'
            {
                return c - 'A' + 27;
            }
        }
    }
}
