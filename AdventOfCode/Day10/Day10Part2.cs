public class Day10Part2 : Challenge
{
    int currentCycle = 1;
    Dictionary<int, int> adjustments = new Dictionary<int, int>();

    public override string Run()
    {
        // Load in the commands and work out when they take effect
        foreach (string nextLine in File.ReadLines(@"./data/day10.txt"))
        {
            if (nextLine.StartsWith("addx"))
            {
                adjustments.Add(currentCycle + 2, int.Parse(nextLine.Substring(5)));
                currentCycle += 2;
            }
            else
            {
                currentCycle += 1;
            }
        }

        // Work out what the registers are then for each cycle
        int[] reigsterHistory = new int[currentCycle + 1];
        reigsterHistory[0] = 0;
        int register = 1;
        for (int cycle = 1; cycle <= currentCycle; cycle++)
        {
            if (adjustments.TryGetValue(cycle, out int adjustment))
                register += adjustment;
            reigsterHistory[cycle] = register;
        }

        // Now draw the text
        string ctr = "\n";
        int nextCycle = 1;
        for (int row = 1; row <= 6; row++)
        {
            for (int column = 0; column <= 39; column++)
            {
                int spriteMiddle = reigsterHistory[nextCycle];
                if (spriteMiddle - 1 <= column && column <= spriteMiddle + 1)
                    ctr += "#";
                else
                    ctr += ".";
                nextCycle++;
            }
            ctr += "\n";
        }

        return ctr;
    }
}