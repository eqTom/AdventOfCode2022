public class Day4Part1 : Challenge
{
    public override int Run()
    {
        int score = 0;
        foreach (string line in File.ReadLines(@"./data/day4.txt"))
        {
            var assignments = line.Split(',');
            var elf1 = StartEnd(assignments[0]);
            var elf2 = StartEnd(assignments[1]);
            if((elf1.Item1 <= elf2.Item1 && elf1.Item2 >= elf2.Item2)
                || (elf2.Item1 <= elf1.Item1 && elf2.Item2 >= elf1.Item2))
                score++;
        }
        return score;
    }

    private (int, int) StartEnd(string input)
    {
        var s = input.Split('-');
        return (int.Parse(s[0]), int.Parse(s[1]));
    }
}