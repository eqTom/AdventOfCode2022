public class Day8Part2 : Challenge
{
    int[,] map;
    int[,] score;
    int maxScore = 0;

    int ansY = 0;
    int ansX = 0;

    int yMax = 0;
    int xMax = 0;

    public override string Run()
    {
        LoadMap();
        for (int y = 0; y <= yMax; y++)
        {
            for (int x = 0; x <= xMax; x++)
            {
                score[y, x] = getleft(x, y) * getRight(x, y) * getUp(x, y) * getDown(x, y);
                if (score[y, x] > maxScore)
                {
                    maxScore = score[y, x];
                    ansX = x;
                    ansY = y;
                }
            }
        }

        // display();

        return maxScore.ToString();
    }

    private void display()
    {
        int left = getleft(ansX, ansY);
        int right = getRight(ansX, ansY);
        int up = getUp(ansX, ansY);
        int down = getDown(ansX, ansY);

        Console.BackgroundColor = ConsoleColor.DarkBlue;
        for (int x = 0; x <= xMax; x++)
        {
            if(x < 10) Console.Write($"{x} ");
            else Console.Write($"{x}");
        }
        Console.ResetColor();
        Console.WriteLine("");

        for (int y = 0; y <= yMax; y++)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            if (y < 10) Console.Write($"{y}   ");
            else Console.Write($"{y}  ");
            Console.ResetColor();

            for (int x = 0; x <= xMax; x++)
            {
                if (x == ansX && y == ansY) Console.BackgroundColor = ConsoleColor.Green;
                else if (y == ansY)
                {
                    // correct row
                    if(x >= (ansX - left) && x <= (ansX + right)) Console.BackgroundColor = ConsoleColor.Red;
                }
                else if(x == ansX)
                {
                    // correct col
                    if (y >= (ansY - up) && x <= (ansY + down)) Console.BackgroundColor = ConsoleColor.Red;
                }

                Console.Write($"{map[y, x]} ");
                Console.ResetColor();
            }
            Console.WriteLine("");
        }
    }

    private int getleft(int column, int row)
    {
        if (column == 0) return 0;

        for (int s = column - 1; s >= 0; s--)
            if (map[row, s] >= map[row, column]) return column - s;

        return column;
    }

    private int getRight(int column, int row)
    {
        if(column == xMax) return 0;

        for (int s = column + 1; s <= xMax; s++)
            if (map[row, s] >= map[row, column]) return s - column;

        return xMax - column;
    }

    private int getUp(int column, int row)
    {
        if (row == 0) return 0;

        for (int s = row - 1; s >= 0; s--)
            if (map[s,column] >= map[row,column]) return row - s;

        return row;
    }

    private int getDown(int column, int row)
    {
        if (row == yMax) return 0;

        for (int s = row + 1 ; s <= yMax; s++)
            if (map[s,column] >= map[row,column]) return s - row;

        return yMax - row;
    }

    private void LoadMap()
    {
        List<string> lines = new List<string>();

        foreach (string nextLine in File.ReadLines(@"./data/day8.txt"))
            lines.Add(nextLine);

        yMax = lines.Count - 1;
        xMax = lines[0].Length - 1;

        map = new int[yMax + 1, xMax + 1];
        score = new int[yMax + 1, xMax + 1];

        for (int y = 0; y <= yMax; y++)
        {
            var chars = lines[y].ToCharArray();
            for (int x = 0; x <= xMax; x++)
            {
                map[y, x] = int.Parse(chars[x].ToString());
            }
        }
    }

}