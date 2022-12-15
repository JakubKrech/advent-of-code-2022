using AdventOfCode2022.Tools;

namespace AdventOfCode2022.Solutions
{
    internal static class Day_13
    {
        public static void Solve()
        {
            var input = InputData.ReadTestInput("Day_13.txt");
            // var input = InputData.ReadInput("Day_13.txt");

            Console.WriteLine("=== Day 13 ===");
            Console.WriteLine($"Part 1: {Part1(input)}");
            Console.WriteLine($"Part 2: {Part2(input)}");
        }

        private static int Part1(IEnumerable<string> input)
        {
            List<string> lines = input.ToList();

            int ammountOfPacketPairs = (input.Count() / 3) + 1;
            int currentPacketPair = 0;

            int correctlyOrderedBracketPairs = 0;

            Console.WriteLine($"There are {ammountOfPacketPairs} packet pairs to check!");

            while (currentPacketPair < ammountOfPacketPairs)
            {
                string firstPacket = lines[0 + currentPacketPair * 3];
                string secondPacket = lines[1 + currentPacketPair * 3];

                Console.WriteLine($"Comparing packets: {firstPacket} vs {secondPacket}");

                correctlyOrderedBracketPairs += CompareTwoPackets(firstPacket, secondPacket) ? 1 : 0; 

                currentPacketPair++;
            }

            return correctlyOrderedBracketPairs;
        }

        public static bool CompareTwoPackets(string firstPacket, string secondPacket)
        {
            SplitIntoSublists(firstPacket.Substring(1, firstPacket.Length - 2));
            SplitIntoSublists(secondPacket.Substring(1, secondPacket.Length - 2));

            return true;
        }

        private static List<string> SplitIntoSublists(string value)
        {
            int unclosedBrackets = 0;
            int lastCommaIndex = 0;

            List<string> sublists = new();

            // search for first comma that splits strings containing equal number of [ and ]
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == '[') unclosedBrackets++;
                else if (value[i] == ']') unclosedBrackets--;
                else if (value[i] == ',')
                {
                    if (unclosedBrackets == 0)
                    {
                        sublists.Add(value.Substring(lastCommaIndex, i - lastCommaIndex));
                        lastCommaIndex = i + 1;
                    }
                }
            }
            sublists.Add(value.Substring(lastCommaIndex, value.Length - lastCommaIndex));

            foreach (string sublist in sublists)
            {
                Console.Write(sublist);
                Console.Write("     ");
            }
            Console.WriteLine();

            return sublists;
        }

        private static int Part111(IEnumerable<string> input)
        {
            List<string> lines = input.ToList();

            int ammountOfPacketPairs = (input.Count() / 3) + 1;
            int currentPacketPair = 0;

            int correctlyOrderedBracketPairs = 0;

            Console.WriteLine($"There are {ammountOfPacketPairs} packet pairs to check!");

            while (currentPacketPair < ammountOfPacketPairs)
            {
                string firstPacket = lines[0 + currentPacketPair * 3];
                string secondPacket = lines[1 + currentPacketPair * 3];

                Console.WriteLine($"Comparing packets: {firstPacket} vs {secondPacket}");

                // strip begginning and ending [ ]
                //firstPacket = firstPacket.Substring(1, firstPacket.Length - 2);
                //secondPacket = secondPacket.Substring(1, secondPacket.Length - 2);

                if(CompareValues(firstPacket, secondPacket))
                {
                    correctlyOrderedBracketPairs++;
                    Console.WriteLine($"Pair ordered correctly! Total: {correctlyOrderedBracketPairs}\n");
                }
                else
                {
                    Console.WriteLine($"Pair was NOT ordered correctly!\n");
                }

                currentPacketPair++;
            }

            return correctlyOrderedBracketPairs;
        }

        // [[5,2,0,[10,6,10],9],[],[1,2,8,[],8],[[6,4],0,2,[1,10]],[[],[10,1]]],[10,[3],10,[[5,4,8,5,10],[0],7,7]],[[[1,2],[0],3,[],[1,1,6,2,2]],10,[]]
        public static bool CompareValues(string v1, string v2)
        {
            Console.WriteLine($"Comparing values: {v1} vs {v2}");

            if (v1.Length == 0) return true;
            if (v2.Length == 0) return false;

            if (v1.Contains(',') && !v1.Contains('[')) v1 = '[' + v1 + ']';
            if (v2.Contains(',') && !v2.Contains('[')) v2 = '[' + v2 + ']';

            // Both values are integers
            //if (v1[0] != '[' && v2[0] != ']')
            if (!v1.Contains(',') && !v2.Contains(','))
            {
                v1 = v1.Trim('[').Trim(']');
                v2 = v2.Trim('[').Trim(']');

                if (v1 == "") return true;
                else if (v2 == "") return false;

                if (int.Parse(v1) > int.Parse(v2))
                {
                    return false;
                }
            }
            // Both values are lists
            else if (v1[0] == '[' && v2[0] == '[')
            {
                v1 = v1.Substring(1, v1.Length - 2);
                v2 = v2.Substring(1, v2.Length - 2);

                int unclosedBrackets = 0;
                int commaIndexV1 = -1;
                int commaIndexV2 = -1;

                // search for first comma that splits strings containing equal number of [ and ]
                for (int i = 0; i < v1.Length; i++)
                {
                    if (v1[i] == '[') unclosedBrackets++;
                    else if (v1[i] == ']') unclosedBrackets--;
                    else if (v1[i] == ',')
                    {
                        if (unclosedBrackets == 0)
                        {
                            commaIndexV1 = i;
                            break;
                        }
                    }
                }

                unclosedBrackets = 0;

                for (int i = 0; i < v2.Length; i++)
                {
                    if (v2[i] == '[') unclosedBrackets++;
                    else if (v2[i] == ']') unclosedBrackets--;
                    else if (v2[i] == ',')
                    {
                        if (unclosedBrackets == 0)
                        {
                            commaIndexV2 = i;
                            break;
                        }
                    }
                }

                if(commaIndexV1 != -1 && commaIndexV2 != -1)
                {
                    string v1Part1 = v1.Substring(0, commaIndexV1);
                    string v2Part1 = v2.Substring(0, commaIndexV2);
                    //if (!CompareValues(v1Part1, v2Part1)) return false;
                    if (!CompareValues('[' + v1Part1 + ']', '[' + v2Part1 + ']')) return false;

                    string v1Part2 = v1.Substring(commaIndexV1 + 1);
                    string v2Part2 = v2.Substring(commaIndexV2 + 1);
                    //if (!CompareValues(v1Part2, v2Part2)) return false;
                    if (!CompareValues('[' + v1Part2 + ']', '[' + v2Part2 + ']')) return false;
                }
                else
                {
                    if (!CompareValues(v1, v2)) return false;
                }
            }
            // One of the values is integer and one is list
            else
            {
                Console.WriteLine("One is integer and one is list!!!!!!!!!!!");

                if(v1[0] == '[' || v1.Contains(','))
                {
                    if (int.Parse(v1.Split(',')[0].Trim('[').Trim(']')) < int.Parse(v2.Trim('[').Trim(']'))) return true;
                    else return false;
                    //if (CompareValues(v1.Split(',')[0], v2)) return true; // problem here - if values are the same, then check next values, but i instantly return true (pair 5)
                    //return false;
                }
                else if (v2[0] == '[' || v2.Contains(','))
                {
                    if (!CompareValues(v1, v2.Split(',')[0].Trim('[').Trim(']'))) return false;
                    return true;
                }

                if (!CompareValues(v1, v2)) return false;
            }

            return true;
        }

        private static int Part11(IEnumerable<string> input)
        {
            List<string> lines = input.ToList();

            int ammountOfPacketPairs = (input.Count() / 3) + 1;
            int currentPacketPair = 0;

            int correctlyOrderedBracketPairs = 0;

            Console.WriteLine($"There are {ammountOfPacketPairs} packet pairs to check!");

            while (currentPacketPair < ammountOfPacketPairs)
            {
                string firstPacket = lines[0 + currentPacketPair * 3];
                string secondPacket = lines[1 + currentPacketPair * 3];

                Console.WriteLine($"Comparing packets: {firstPacket} vs {secondPacket}");

                while (firstPacket != string.Empty)
                {
                    if (firstPacket.Length == 0)
                    {
                        correctlyOrderedBracketPairs++;
                        Console.WriteLine("CORRECT PAIR");
                        break;
                    }
                    else if (secondPacket.Length == 0)
                    {
                        break; // right list ran out of items first, incorrect packet pair ordering
                    }

                    // strip side brackets, if they are present
                    //if (firstPacket[0] == '[' && firstPacket.Last() == ']') firstPacket = firstPacket.Substring(1, firstPacket.Length - 2);
                    //if (secondPacket[0] == '[' && secondPacket.Last() == ']') secondPacket = secondPacket.Substring(1, secondPacket.Length - 2);

                    if (firstPacket[0] == '[' && firstPacket.Last() == ']') firstPacket = firstPacket.Trim('[').Trim(']');
                    if (secondPacket[0] == '[' && secondPacket.Last() == ']') secondPacket = secondPacket.Trim('[').Trim(']');

                    if (firstPacket.Length == 0)
                    {
                        correctlyOrderedBracketPairs++;
                        break;
                    }
                    else if (secondPacket.Length == 0)
                    {
                        break; // right list ran out of items first, incorrect packet pair ordering
                    }

                    var leftValue = firstPacket.Split(',')[0];
                    firstPacket = firstPacket.Length > leftValue.Length + 1 ? firstPacket.Substring(leftValue.Length + 1) : "";

                    var rightValue = secondPacket.Split(',')[0];
                    secondPacket = secondPacket.Length > rightValue.Length + 1 ? secondPacket.Substring(rightValue.Length + 1) : "";

                    Console.WriteLine($"Comparing {leftValue} vs {rightValue}");

                    int leftInt = int.Parse(leftValue.Trim('[').Trim(']'));
                    int rightInt = int.Parse(rightValue.Trim('[').Trim(']'));

                    if (leftInt < rightInt)
                    {
                        correctlyOrderedBracketPairs++;
                        Console.WriteLine("CORRECT PAIR");
                        break;
                    }
                    else if (leftInt > rightInt)
                    {
                        break; // incorrect packet pair ordering
                    }
                }

                Console.WriteLine($"{firstPacket} vs {secondPacket}");

                currentPacketPair++;
            }

            return correctlyOrderedBracketPairs;
        }

        private static int Part2(IEnumerable<string> input)
        {


            return -1;
        }
    }
}
