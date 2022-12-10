public class Day9Part1 : Challenge
{
    public End Head = new End();
    public End Tail = new End();

    public Dictionary<string, int> TailVisits = new Dictionary<string, int>();

    // 1932 was too low

    public override string Run()
    {
        TailVisits.Add(cacheKey(0,0), 1);

        foreach (string nextLine in File.ReadLines(@"./data/day9.txt"))
        {
            var instruction = nextLine.Split(" ");
            MoveHead(instruction[0], int.Parse(instruction[1]));
        }

        return TailVisits.Where(s => s.Value >= 1).Count().ToString();
    }

    public void MoveHead(string direction, int moves)
    {
        for (int i = 0; i < moves; i++)
        {
            if (direction == "U") Head.Y++;
            else if (direction == "D") Head.Y--;
            else if (direction == "L") Head.X--;
            else if (direction == "R") Head.X++;
            MoveTailTowardHead();
        }
    }

    private void display(int turn)
    {
        Console.WriteLine($"Turn: {turn}");
        int buffer = 4;

        Console.Write("       ");
        for (int i = Head.X - buffer; i < Head.X + buffer; i++)
        {
            string header = $"{i} ";
            if (header.Length < 3) header = " " + header;
            Console.Write(header);
        }

        Console.WriteLine();

        for (int y = (Head.Y + buffer); y > (Head.Y - buffer); y--)
        {
            string margin = y.ToString();
            margin += new string(' ', 5 - margin.Length);
            Console.Write($"{margin}: ");
            for (int x = (Head.X - buffer); x < (Head.X + buffer); x++)
            {
                if(Head.X == x && Head.Y == y && Tail.X == x && Tail.Y == y)
                    Console.Write(" H ");
                else if (Head.X == x && Head.Y == y)
                    Console.Write(" h ");
                else if (Tail.X == x && Tail.Y == y)
                    Console.Write(" t ");
                else
                    if(x == 0 && y == 0) Console.Write(" S ");
                    else Console.Write(" - ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public void MoveTailTowardHead()
    {
        // Already touching, so no move needed
        if (Tail.X >= Head.X - 1 && Tail.X <= Head.X + 1
            && Tail.Y >= Head.Y - 1 && Tail.Y <= Head.Y + 1)
        {
            return;
        }

        if(Tail.X == Head.X)
        {
            // Right col, move up/down
            if(Tail.Y < Head.Y) Tail.Y++;
            else Tail.Y--;
        }
        else if(Tail.Y == Head.Y)
        {
            // Right row, move left/right
            if(Tail.X < Head.X) Tail.X++;
            else Tail.X--;
        }
        else
        {
            if (Tail.X < Head.X) Tail.X++;
            else Tail.X--;

            if (Tail.Y < Head.Y) Tail.Y++;
            else Tail.Y--;
        }

        RecordTailTrail();
    }

    private void RecordTailTrail()
    {
        string key = cacheKey(Tail.X, Tail.Y);
        if (TailVisits.TryGetValue(key, out var value))
        {
            value++;
            TailVisits.Remove(key);
            TailVisits.Add(key, value);
        }
        else
            TailVisits.Add(key, 1);
    }

    private string cacheKey(int x, int y) => $"{x}-{y}";

    public class End
    {
        public End()
        {
            X = 0;
            Y = 0;
        }

        public int X { get; set; } // Column
        public int Y { get; set; } // Row
    }
}