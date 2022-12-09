public class Day2Part2 : Challenge
{
    public override string Run()
    {
        Dictionary<string, Shape> shapeMap = new Dictionary<string, Shape>()
        {
            { "A", Shape.Rock }, { "B", Shape.Paper }, { "C", Shape.Scissors }
        };

        int score = 0;
        foreach (string line in File.ReadLines(@"./data/day2.txt"))
        {
            var round = line.Split(' ');
            shapeMap.TryGetValue(round[0], out Shape opponent);
            if (round[1] == "Y")
                score += ((int)opponent + 3);
            else if (opponent == Shape.Rock)
                score += round[1] == "Z" ? ((int)Shape.Paper + 6) : ((int)Shape.Scissors);
            else if (opponent == Shape.Paper)
                score += round[1] == "Z" ? ((int)Shape.Scissors + 6) : ((int)Shape.Rock);
            else if (opponent == Shape.Scissors)
                score += round[1] == "Z" ? ((int)Shape.Rock + 6) : ((int)Shape.Paper);
        }
        return score.ToString();
    }

    enum Shape
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3,
    }
}