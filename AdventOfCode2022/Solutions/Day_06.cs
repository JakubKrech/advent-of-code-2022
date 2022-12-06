using AdventOfCode2022.Tools;

namespace AdventOfCode2022.Solutions
{
    internal static class Day_06
    {
        public static void Solve()
        {
            // var input = InputData.ReadTestInput("Day_06.txt");
            var input = InputData.ReadInput("Day_06.txt");

            Console.WriteLine("=== Day 06 ===");
            Console.WriteLine($"Part 1: {FindEncodedPackets(input, 4)}");
            Console.WriteLine($"Part 2: {FindEncodedPackets(input, 14)}");
        }

        private static int FindEncodedPacket(string input, int lengthOfPacket)
        {
            Queue<char> queue = new();

            int currentChar = 1;

            foreach (char c in input)
            {
                while (queue.Contains(c))
                {
                    queue.Dequeue();
                }

                queue.Enqueue(c);

                if (queue.Count == lengthOfPacket)
                {
                    break;
                }

                currentChar++;
            }

            return currentChar;
        }


        // Function created to allow testing with multiple inputs
        private static string FindEncodedPackets(IEnumerable<string> input, int lengthOfPacket)
        {
            List<int> results = new();

            foreach (string line in input)
            {
                results.Add(FindEncodedPacket(line, lengthOfPacket));
            }

            return string.Join(",", results);
        }
    }
}
