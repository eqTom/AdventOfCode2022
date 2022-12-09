public class Day1Part1 : Challenge
{
    public override int Run()
    {
        int answer = 0, runningTotal = 0;
        foreach (string line in File.ReadLines(@"./Data/day1.txt"))
        {
            if (line.Length > 0 && int.TryParse(line, out int x))
                runningTotal += x;
            else
            {
                if (runningTotal > answer) answer = runningTotal;
                runningTotal = 0;
            }
        }
        return answer;
    }
}
