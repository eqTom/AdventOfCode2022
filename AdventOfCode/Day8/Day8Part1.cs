public class Day8Part1 : Challenge
{
    int[,] map;
    int[,] score;

    int yMax = 0;
    int xMax = 0;

    public override string Run()
    {
        LoadMap();
        int answer = 0;
        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                score[y, x] = left(x, y) + right(x, y) + up(x, y) + down(x, y);
                answer += score[y, x] >= 1  ? 1 : 0;
            }
        }

        // display();

        return answer.ToString();
    }

    private void display()
    {
        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                if(score[y,x] == 0) Console.BackgroundColor = ConsoleColor.Black;
                if(score[y,x] == 1) Console.BackgroundColor = ConsoleColor.DarkGreen;
                if(score[y,x] == 2) Console.BackgroundColor = ConsoleColor.Magenta;
                if(score[y,x] == 4) Console.BackgroundColor = ConsoleColor.Red;
                if(score[y,x] == 5) Console.BackgroundColor = ConsoleColor.Red;
                Console.Write(map[y, x]);
            }
            Console.ResetColor();
            Console.WriteLine("");
        }
    }

    private int left(int x, int y)
    {
        if (x == 0) return 1;

        for (int s = x - 1; s >= 0; s--)
            if (map[y,s] >= map[y,x]) return 0;
        return 1;
    }

    private int right(int x, int y)
    {
        if(x == xMax) return 1;

        for (int s = x + 1; s <= xMax - 1; s++)
            if (map[y,s] >= map[y,x]) return 0;
        return 1;
    }

    private int up(int x, int y)
    {
        if (y == 0) return 1;

        for (int s = y - 1; s >= 0; s--)
            if (map[s,x] >= map[y,x]) return 0;
        return 1;
    }

    private int down(int x, int y)
    {
        if (y == yMax) return 1;

        for (int s = y + 1 ; s <= yMax - 1; s++)
            if (map[s,x] >= map[y,x]) return 0;
        return 1;
    }

    private void LoadMap()
    {
        List<string> lines = new List<string>();

        foreach (string nextLine in File.ReadLines(@"./data/day8.txt"))
            lines.Add(nextLine);

        yMax = lines.Count;
        xMax = lines[0].Length;
        map = new int[yMax, xMax];
        score = new int[yMax, xMax];

        for (int y = 0; y < yMax; y++)
        {
            var chars = lines[y].ToCharArray();
            for (int x = 0; x < xMax; x++)
            {
                map[y, x] = int.Parse(chars[x].ToString());
            }
        }
    }

}