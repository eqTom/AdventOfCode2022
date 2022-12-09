public class Day3Part1 : Challenge
{
    public override string Run()
    {
        int score = 0;
        foreach (string line in File.ReadLines(@"./data/day3.txt"))
        {
            char[] firstHalf = line.Substring(0, line.Length / 2).ToCharArray();
            char[] secondHalf = line.Substring(line.Length / 2, line.Length / 2).ToCharArray();
            foreach (var nextChar in firstHalf)
            {
                if (secondHalf.Contains(nextChar))
                {
                    score += "-abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(nextChar);
                    break;
                }
            }
        }
        return score.ToString();
    }
}