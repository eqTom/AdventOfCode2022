public class Day9Part2 : Challenge
{
    public End Head = new End();
    public Dictionary<int, End> Tails = new Dictionary<int, End>();
    int numberOfTails = 9;

    public Dictionary<string, int> LastTailVisit = new Dictionary<string, int>();

    public override string Run()
    {
        // Start the tails off at home
        for (int i = 1; i <= numberOfTails; i++)
            Tails.Add(i, new End());

        LastTailVisit.Add(cacheKey(0,0), 1);

        foreach (string nextLine in File.ReadLines(@"./data/day9.txt"))
        {
            var instruction = nextLine.Split(" ");
            MoveHead(instruction[0], int.Parse(instruction[1]));
        }

        return LastTailVisit.Where(s => s.Value >= 1).Count().ToString();
    }

    public void MoveHead(string direction, int moves)
    {
        for (int i = 0; i < moves; i++)
        {
            if (direction == "U") Head.Y++;
            else if (direction == "D") Head.Y--;
            else if (direction == "L") Head.X--;
            else if (direction == "R") Head.X++;
            MoveTailsTowardHead();
        }
    }

    public void MoveTailsTowardHead()
    {
        End lastEnd = Head;
        End nextTail;

        for (int i = 1; i <= numberOfTails; i++)
        {
            Tails.TryGetValue(i, out nextTail);

            // Already touching, so no move needed
            if (nextTail.X >= lastEnd.X - 1 && nextTail.X <= lastEnd.X + 1
                && nextTail.Y >= lastEnd.Y - 1 && nextTail.Y <= lastEnd.Y + 1)
            {
                return;
            }

            if(nextTail.X == lastEnd.X)
            {
                // Right col, move up/down
                if(nextTail.Y < lastEnd.Y) nextTail.Y++;
                else nextTail.Y--;
            }
            else if(nextTail.Y == lastEnd.Y)
            {
                // Right row, move left/right
                if(nextTail.X < lastEnd.X) nextTail.X++;
                else nextTail.X--;
            }
            else
            {
                if (nextTail.X < lastEnd.X) nextTail.X++;
                else nextTail.X--;

                if (nextTail.Y < lastEnd.Y) nextTail.Y++;
                else nextTail.Y--;
            }

            lastEnd = nextTail;

            if(i == numberOfTails)
                RecordTailTrail(nextTail);
        }
    }

    private void RecordTailTrail(End endTail)
    {
        string key = cacheKey(endTail.X, endTail.Y);
        if (LastTailVisit.TryGetValue(key, out var value))
        {
            value++;
            LastTailVisit.Remove(key);
            LastTailVisit.Add(key, value);
        }
        else
            LastTailVisit.Add(key, 1);
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