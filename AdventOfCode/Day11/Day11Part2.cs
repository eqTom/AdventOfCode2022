using System.Text.RegularExpressions;

public class Day11Part2 : Challenge
{
    List<Monkey> monkeys = new List<Monkey>();
    int maxRounds = 10000; // 20

    public override string Run()
    {
        LoadMonkies();

        var allDivisible = monkeys.Select(s => s.TestDivisibleBy).ToList();

        // The worry will get too big, even for a long at 10k rounds
        // Multiply all the numbers together, and then we can mod on this later
        long magic  = 1;
        foreach (var next in allDivisible)
            magic *= next;
       
        for (int round = 1; round <= maxRounds; round++)
        {
            foreach(var nextMonkey in monkeys)
            {
                List<long> nextMonkeyItemsCopy = nextMonkey.Items.ToList();
                foreach (var nextItem in nextMonkeyItemsCopy)
                {
                    nextMonkey.InspectItemCount++;
                    long worryLevel = nextMonkey.getWorryLevel(nextItem);

                    // Stop the worry from getting too big
                    if(worryLevel > int.MaxValue)
                        worryLevel %= magic;

                    bool wasDivisible = worryLevel % nextMonkey.TestDivisibleBy == 0;
                    int throwToMonkeyId = 0;

                    if(wasDivisible)
                        throwToMonkeyId = nextMonkey.IfTrueMonkeyId;
                    else
                        throwToMonkeyId = nextMonkey.IfFalseMonkeyId;

                    nextMonkey.Items.Remove(nextItem);
                    monkeys.First(s => s.Id == throwToMonkeyId).Items.Add(worryLevel);
                }
            }

            if(round ==1 || round == 20 || round % 1000 == 0)
                ListMonkeyInspections(round);
        }

        var topTwo = monkeys.OrderByDescending(s => s.InspectItemCount).Take(2).Select(s => s.InspectItemCount).ToList();
        return (topTwo[0] * topTwo[1]).ToString();
    }

    private void ListMonkeyInspections(int round)
    {
        Console.WriteLine($"\n== After Round {round} ==");
        foreach(var nextMonkey in monkeys)
        {
            Console.WriteLine($"Monkey {nextMonkey.Id} inspected items {nextMonkey.InspectItemCount} times.");
        }
    }

    private void LoadMonkies()
    {
        List<string> monkeyValues = new List<string>();
        foreach (string nextLine in File.ReadLines(@"./data/day11.txt"))
        {
            if (String.IsNullOrWhiteSpace(nextLine))
            {
                monkeys.Add(new Monkey(monkeyValues));
                monkeyValues.Clear();
            }
            else
            {
                monkeyValues.Add(nextLine.Trim());
            }
        }
        monkeys.Add(new Monkey(monkeyValues));
    }

    public class Monkey
    {
        public Monkey(List<string> data)
        {
            Id = int.Parse(data[0].Substring(7, data[0].Length - 7 - 1));
            Items = data[1].Substring(data[1].IndexOf(':') + 1).Split(',').Select(s => long.Parse(s.Trim())).ToList();
            Operator = data[2].Substring(data[2].IndexOf('=') + 1).Trim();
            TestDivisibleBy = int.Parse(Regex.Match(data[3], @"\d+$").Value);
            IfTrueMonkeyId = int.Parse(Regex.Match(data[4], @"\d+$").Value);
            IfFalseMonkeyId = int.Parse(Regex.Match(data[5], @"\d+$").Value);
            InspectItemCount = 0;
        }

        public long getWorryLevel(long item)
        {
            var op = Operator.Split(' ');
            long by = op[2] == "old" ? item : long.Parse(op[2]);
            if (op[1] == "+") return item + by;
            else if(op[1] == "*") return item * by;
            return 0;
        }

        public int Id { get; set; }
        public List<long> Items { get; set; }
        public string Operator { get; set; }
        public int TestDivisibleBy { get; set; }
        public int IfTrueMonkeyId { get; set; }
        public int IfFalseMonkeyId { get; set; }

        public long InspectItemCount { get; set; }
    }
}