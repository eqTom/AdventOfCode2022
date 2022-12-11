using System.Text.RegularExpressions;

public class Day11Part1 : Challenge
{
    List<Monkey> monkeys = new List<Monkey>();
    int maxRounds = 20; // 20
    int bored = 3;

    public override string Run()
    {
        LoadMonkies();

        for (int round = 1; round <= maxRounds; round++)
        {
            foreach(var nextMonkey in monkeys)
            {
                Console.WriteLine($"Monkey {nextMonkey.Id}:");

                List<int> nextMonkeyItemsCopy = nextMonkey.Items.ToList();
                foreach (var nextItem in nextMonkeyItemsCopy)
                {
                    nextMonkey.InspectItemCount++;
                    Console.WriteLine($"  Monkey inspects an item with a worry level of {nextItem}.");
                    int worryLevel = nextMonkey.getWorryLevel(nextItem);
                    Console.WriteLine($"    Worry level is {worryLevel}.");
                    worryLevel = (int)(Math.Floor((decimal)worryLevel / bored));
                    Console.WriteLine($"    Monkey gets bored with item. Worry level is divided by {bored} to {worryLevel}.");
                    bool wasDivisible = worryLevel % nextMonkey.TestDivisibleBy == 0;
                    int throwToMonkeyId = 0;
                    if(wasDivisible)
                    {
                        Console.WriteLine($"    Current worry level is divisible by {nextMonkey.TestDivisibleBy}.");
                        throwToMonkeyId = nextMonkey.IfTrueMonkeyId;
                    }
                    else
                    {
                        Console.WriteLine($"    Current worry level is not divisible by {nextMonkey.TestDivisibleBy}.");
                        throwToMonkeyId = nextMonkey.IfFalseMonkeyId;
                    }
                    Console.WriteLine($"    Item with worry level {worryLevel} is thrown to monkey {throwToMonkeyId}.");

                    nextMonkey.Items.Remove(nextItem);
                    monkeys.First(s => s.Id == throwToMonkeyId).Items.Add(worryLevel);
                }
            }

            ListMonkeyItems();
        }

        var topTwo = monkeys.OrderByDescending(s => s.InspectItemCount).Take(2).Select(s => s.InspectItemCount).ToList();
        return (topTwo[0] * topTwo[1]).ToString();
    }

    private void ListMonkeyItems()
    {
        foreach(var nextMonkey in monkeys)
        {
            Console.WriteLine($"Monkey {nextMonkey.Id}: {String.Join(',', nextMonkey.Items)}");
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
            Items = data[1].Substring(data[1].IndexOf(':') + 1).Split(',').Select(s => int.Parse(s.Trim())).ToList();
            Operator = data[2].Substring(data[2].IndexOf('=') + 1).Trim();
            TestDivisibleBy = int.Parse(Regex.Match(data[3], @"\d+$").Value);
            IfTrueMonkeyId = int.Parse(Regex.Match(data[4], @"\d+$").Value);
            IfFalseMonkeyId = int.Parse(Regex.Match(data[5], @"\d+$").Value);
            InspectItemCount = 0;
        }

        public int getWorryLevel(int item)
        {
            var op = Operator.Split(' ');
            int by = op[2] == "old" ? item : int.Parse(op[2]);
            if (op[1] == "+") return item + by;
            else if(op[1] == "*") return item * by;
            return 0;
        }

        public int Id { get; set; }
        public List<int> Items { get; set; }
        public string Operator { get; set; }
        public int TestDivisibleBy { get; set; }
        public int IfTrueMonkeyId { get; set; }
        public int IfFalseMonkeyId { get; set; }

        public int InspectItemCount { get; set; }
    }
}