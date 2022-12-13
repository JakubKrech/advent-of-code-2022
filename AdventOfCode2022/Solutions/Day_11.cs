using AdventOfCode2022.Tools;

namespace AdventOfCode2022.Solutions
{
    internal static class Day_12
    {
        public static void Solve()
        {
            //var input = InputData.ReadTestInput("Day_12.txt");
            var input = InputData.ReadInput("Day_12.txt");

            Console.WriteLine("=== Day 12 ===");
            Console.WriteLine($"Part 1: {Part1(input)}");
            Console.WriteLine($"Part 2: {Part2(input)}");
        }

        private static int Part1(IEnumerable<string> input)
        {
            int height = input.Count();
            int width = input.First().Length;

            Point[,] table2d = new Point[height, width];

            int start_x = -1;
            int start_y = -1;

            int target_x = -1;
            int target_y = -1;

            char destinationCharValue = 'a';

            for (int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    char c = input.ElementAt(i)[j];

                    if(destinationCharValue < c)
                    {
                        destinationCharValue = c;
                    }

                    if(c == 'S')
                    {
                        start_x = i;
                        start_y = j;
                        table2d[i, j] = new Point('a', 'S');
                    }
                    else if (c == 'E')
                    {
                        target_x = i;
                        target_y = j;
                    }
                    else
                    {
                        table2d[i, j] = new Point(c);
                    }
                }
            }

            destinationCharValue++;
            table2d[target_x, target_y] = new Point(destinationCharValue, 'E');

            Console.WriteLine($"Starting point: {start_x},{start_y}");
            Console.WriteLine($"Destination point: {destinationCharValue} {target_x},{target_y}");

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write((char)table2d[i, j].value);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Explore(table2d, start_x, start_y, -1, destinationCharValue);
            ColorPrintFinal(table2d, start_x, start_y, destinationCharValue);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    //if (table2d[i,j].minimumSteps == int.MaxValue)
                    //{
                    //    Console.Write($"  .");
                    //}
                    //else
                    //{
                    //    Console.Write($"{table2d[i, j].minimumSteps,2} ");

                    //}
                    Console.Write(table2d[i,j].printChar);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            return table2d[target_x, target_y].minimumSteps;
        }

        public class Point
        {
            public int value;
            public bool visited = false;
            public int minimumSteps = int.MaxValue;
            public char printChar = '.';

            public Point(char c)
            {
                value = c;
            }

            public Point(char c, char pc)
            {
                value = c;
                printChar = pc;
            }
        }

        public static void ColorPrintFinal(Point[,] table2d, int x, int y, char destinationCharValue)
        {
            int curr_x = x;
            int curr_y = y;

            int height = table2d.GetLength(0);
            int width = table2d.GetLength(1);

            int curr_minimumSteps = table2d[curr_x, curr_y].minimumSteps;

            while (table2d[curr_x, curr_y].value != destinationCharValue)
            {
                if (curr_y - 1 >= 0 && table2d[curr_x, curr_y - 1].minimumSteps == curr_minimumSteps + 1)
                {
                    table2d[curr_x, curr_y].printChar = '<';
                    curr_y--;
                    curr_minimumSteps = table2d[curr_x, curr_y].minimumSteps;
                }
                else if (curr_y + 1 < width && table2d[curr_x, curr_y + 1].minimumSteps == curr_minimumSteps + 1)
                {
                    table2d[curr_x, curr_y].printChar = '>';
                    curr_y++;
                    curr_minimumSteps = table2d[curr_x, curr_y].minimumSteps;
                }
                else if (curr_x - 1 >= 0 && table2d[curr_x - 1, curr_y].minimumSteps == curr_minimumSteps + 1)
                {
                    table2d[curr_x, curr_y].printChar = '^';
                    curr_x--;
                    curr_minimumSteps = table2d[curr_x, curr_y].minimumSteps;
                }
                else if (curr_x + 1 < height && table2d[curr_x + 1, curr_y].minimumSteps == curr_minimumSteps + 1)
                {
                    table2d[curr_x, curr_y].printChar = 'V';
                    curr_x++;
                    curr_minimumSteps = table2d[curr_x, curr_y].minimumSteps;
                }
            }
        }

        private static void Explore(Point[,] table2d, int x, int y, int steps, char destinationCharValue)
        {
            steps += 1;

           // Console.WriteLine($"Visiting ({x},{y}) with {steps} steps");

            int height = table2d.GetLength(0); 
            int width = table2d.GetLength(1);

            table2d[x, y].minimumSteps = Math.Min(table2d[x, y].minimumSteps, steps);
            table2d[x, y].visited = true;

            if (table2d[x, y].value == destinationCharValue)
                return;

            int currValue = table2d[x, y].value;

            // go left
            if (y - 1 >= 0)
            {
                int leftValue = table2d[x, y - 1].value;

                if (leftValue <= currValue + 1)
                {
                    if (table2d[x, y - 1].visited == false || table2d[x, y - 1].minimumSteps > steps + 1)
                    {
                        Explore(table2d, x, y - 1, steps, destinationCharValue);
                    }
                }
            }

            // go right
            if (y + 1 < width)
            {
                int leftValue = table2d[x, y + 1].value;

                if (leftValue <= currValue + 1)
                {
                    if (table2d[x, y + 1].visited == false || table2d[x, y + 1].minimumSteps > steps + 1)
                    {
                        Explore(table2d, x, y + 1, steps, destinationCharValue);
                    }
                }
            }

            // go up
            if (x - 1 >= 0)
            {
                int upValue = table2d[x - 1, y].value;

                if (upValue <= currValue + 1)
                {
                    if(table2d[x - 1, y].visited == false || table2d[x - 1, y].minimumSteps > steps + 1)
                    {
                        Explore(table2d, x - 1, y, steps, destinationCharValue);
                    }
                }
            }

            // go down
            if (x + 1 < height)
            {
                int downValue = table2d[x + 1, y].value;

                if (downValue <= currValue + 1)
                {
                    if (table2d[x + 1, y].visited == false || table2d[x + 1, y].minimumSteps > steps + 1)
                    {
                        Explore(table2d, x + 1, y, steps, destinationCharValue);
                    }
                }
            }

            
        }

        private static int Part2(IEnumerable<string> input)
        {


            return -1;
        }
    }
}
