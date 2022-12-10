public class Day5Part2 : Challenge
{
    Dictionary<int, List<string>> cargo = new Dictionary<int, List<string>>();

    public override string Run()
    {
        int score = 0;
        bool loaded = false;

        List<string> buffer = new List<string>();
        foreach (string line in File.ReadLines(@"./data/day5.txt"))
        {
            if (string.IsNullOrWhiteSpace(line))
                loaded = loadMap(buffer);
            else if (!loaded)
                buffer.Add(line);
            else
                actOnInstruction(line);
        }

        return answer();
    }

    private string answer()
    {
        string ans = "";
        foreach(var stack in cargo)
            ans += stack.Value.Last();
        return ans;
    }

    private void actOnInstruction(string instruction)
    {
        var commands = instruction.Split(' ');
        
        if(cargo.TryGetValue(int.Parse(commands[3]), out var fromStack)
            && cargo.TryGetValue(int.Parse(commands[5]), out var toStack))
        {
            int toMove = int.Parse(commands[1]);
            toStack.AddRange(fromStack.TakeLast(toMove));
            fromStack.RemoveRange(fromStack.Count - toMove, toMove);
        }
    }

    private bool loadMap(List<string> buffer)
    {
        buffer.Reverse();   // Start at bottom of container
        buffer = buffer.Skip(1).ToList(); // Ignore the index row

        int cols = (buffer[0].Length + 1) / 4;

        for (int i = 1; i <= cols; i++)
            cargo.Add(i, new List<string>());

        for (int row = 0; row < buffer.Count; row++)
        {
            // Check each column
            for (int col = 1; col <= cols; col++)
            {
                int pos = col == 1 ? 1 : (col * 4) - 3;
                string container = buffer[row].Substring(pos, 1);
                if(!string.IsNullOrWhiteSpace(container) && cargo.TryGetValue(col, out List<string> stack))
                {
                    stack.Add(container);
                }
            }
        }
        return true;
    }
}