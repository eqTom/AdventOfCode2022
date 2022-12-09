public class Day3Part2 : Challenge
{
    public override int Run()
    {
        int score = 0;
        List<string> group = new List<string>();
        foreach (string line in File.ReadLines(@"./data/day3.txt"))
        {
            group.Add(line);
            if (group.Count == 3)
            {
                foreach (var nextChar in group[0].ToCharArray())
                {
                    if (group[1].Contains(nextChar) && group[2].Contains(nextChar))
                    {
                        score += "-abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(nextChar);
                        break;
                    }
                }
                group.Clear();
            }
        }
        return score;
    }
}