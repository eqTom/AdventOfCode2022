public class Day1Part2 : Challenge
{
    public int Run()
    {
        int runningTotal = 0;
        List<int> answers = new List<int>();
        foreach (string line in File.ReadLines(@"./Data/day1.txt"))
        {
            if (line.Length > 0 && int.TryParse(line, out int x))
                runningTotal += x;
            else
            {
                answers.Add(runningTotal);
                runningTotal = 0;
            }
        }
        return answers.OrderByDescending(o => o).Take(3).Sum();
    }
}