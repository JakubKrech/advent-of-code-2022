using AdventOfCode2022.Tools;
using System;

namespace AdventOfCode2022.Solutions
{
    internal static class Day_02
    {
        const int rockShown = 1;
        const int paperShown = 2;
        const int scissorsShown = 3;

        const int win = 6;
        const int draw = 3;
        const int lose = 0;

        public static void Solve()
        {
            // var input = InputData.ReadTestInput("Day_02.txt");
            var input = InputData.ReadInput("Day_02.txt");

            Console.WriteLine("=== Day 02 ===");
            Console.WriteLine($"Part 1: {Part1(input)}");
            Console.WriteLine($"Part 2: {Part2(input)}");
        }

        private static int Part1(IEnumerable<string> input)
        {
            // rock = 1p, paper = 2p, scissors = 3p
            // win = 6p, draw = 3p, lose = 0p
            int totalPoints = 0;

            foreach (var pair in input)
            {
                string enemy = pair.Split(' ')[0];
                string me = pair.Split(' ')[1];

                switch (me)
                {
                    case "X": // rock
                        totalPoints += rockShown + SolveDuel(enemy, me);
                        break;
                    case "Y": // paper
                        totalPoints += paperShown + SolveDuel(enemy, me);
                        break;
                    case "Z": // scissors
                        totalPoints += scissorsShown + SolveDuel(enemy, me);
                        break;
                }
            }

            return totalPoints;
        }

        private static int Part2(IEnumerable<string> input)
        {
            // rock = 1p, paper = 2p, scissors = 3p
            // win = 6p, draw = 3p, lose = 0p
            int totalPoints = 0;

            foreach (var pair in input)
            {
                string enemy = pair.Split(' ')[0];
                string resolution = pair.Split(' ')[1];
                string me = FindCorrectMove(enemy, resolution);

                switch (me)
                {
                    case "X": // rock
                        totalPoints += rockShown + SolveDuel(enemy, me);
                        break;
                    case "Y": // paper
                        totalPoints += paperShown + SolveDuel(enemy, me);
                        break;
                    case "Z": // scissors
                        totalPoints += scissorsShown + SolveDuel(enemy, me);
                        break;
                }
            }

            return totalPoints;
        }

        private static int SolveDuel(string enemy, string me)
        {
            switch (enemy)
            {
                case "A": // rock
                    return me == "X" ? 3 : me == "Y" ? 6 : 0;
                case "B": // paper
                    return me == "Y" ? 3 : me == "Z" ? 6 : 0;
                default:  // case "C": // scissors
                    return me == "Z" ? 3 : me == "X" ? 6 : 0;
            }
        }

        private static string FindCorrectMove(string enemy, string resolution)
        {
            switch (resolution)
            {
                case "X": // need to lose
                    return enemy == "A" ? "Z" : enemy == "B" ? "X" : "Y";
                case "Y": // need to draw
                    return enemy == "A" ? "X" : enemy == "B" ? "Y" : "Z";
                default:  // case "Z": // need to win
                    return enemy == "A" ? "Y" : enemy == "B" ? "Z" : "X";
            }
        }
    }
}
