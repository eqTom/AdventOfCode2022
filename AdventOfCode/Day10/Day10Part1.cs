public class Day10Part1 : Challenge
{
    int currentCycle = 1;
    Dictionary<int, int> adjustments = new Dictionary<int, int>();
    List<int> signalStrength = new List<int>();

    // 11700 is too low

    public override string Run()
    {
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

        int register = 1;
        int nextSignalCalc = 20;
        for (int i = 1; i < currentCycle; i++)
        {
            if (adjustments.TryGetValue(i, out int adjustment))
                register += adjustment;

            if (i == nextSignalCalc)
            {
                int strenght = register * i;
                // Console.WriteLine($"{i}th cycle, {strenght} ({i} * {register})");
                signalStrength.Add(strenght);
                nextSignalCalc += 40;
            }
        }

        return signalStrength.Sum().ToString();
    }

    public void addx(int cycle, int i)
    {
        Console.WriteLine($"{cycle}, addx, {i}");
        adjustments.Add(cycle + 2, i);
    }
}