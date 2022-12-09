public class Day2Part1 : Challenge
{
    public override string Run()
    {
        Dictionary<string, Shape> shapeMap = new Dictionary<string, Shape>()
        {
            { "A", Shape.Rock }, { "B", Shape.Paper }, { "C", Shape.Scissors },
            { "X", Shape.Rock }, { "Y", Shape.Paper }, { "Z", Shape.Scissors }
        };

        int score = 0;
        foreach (string line in File.ReadLines(@"./data/day2.txt"))
        {
            var round = line.Split(' ');
            shapeMap.TryGetValue(round[0], out Shape opponent);
            shapeMap.TryGetValue(round[1], out Shape me);
            score += (int)me;
            if (opponent == me) score += 3;
            else if (opponent == Shape.Rock && me == Shape.Paper
                || opponent == Shape.Scissors && me == Shape.Rock
                || opponent == Shape.Paper && me == Shape.Scissors)
            {
                score += 6;
            }
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