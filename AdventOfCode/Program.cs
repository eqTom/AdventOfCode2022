List<Challenge> challenges = new List<Challenge>();

challenges.Add(new Day1Part1());
challenges.Add(new Day1Part2());

challenges.Add(new Day2Part1());
challenges.Add(new Day2Part2());

challenges.Add(new Day3Part1());
challenges.Add(new Day3Part2());

challenges.Add(new Day4Part1());
challenges.Add(new Day4Part2());

challenges.Add(new Day5Part1());
challenges.Add(new Day5Part2());

challenges.Add(new Day6Part1());
challenges.Add(new Day6Part2());

challenges.Add(new Day7Part1());
challenges.Add(new Day7Part2());

// Run the Answers

Console.WriteLine(@"  ___      _                 _   _____  __ _____           _      
 / _ \    | |               | | |  _  |/ _/  __ \         | |     
/ /_\ \ __| |_   _____ _ __ | |_| | | | |_| /  \/ ___   __| | ___ 
|  _  |/ _` \ \ / / _ \ '_ \| __| | | |  _| |    / _ \ / _` |/ _ \
| | | | (_| |\ V /  __/ | | | |_\ \_/ / | | \__/\ (_) | (_| |  __/
\_| |_/\__,_| \_/ \___|_| |_|\__|\___/|_|  \____/\___/ \__,_|\___|
                                                                  ");
Console.WriteLine("# Tom S - 2022");
Console.WriteLine("");
foreach (var challenge in challenges)
    Console.WriteLine($"{challenge.GetType()} - Answer: {challenge.Run()}");
Console.WriteLine("");
