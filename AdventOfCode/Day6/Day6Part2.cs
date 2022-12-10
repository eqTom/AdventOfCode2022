public class Day6Part2 : Challenge
{
    public override string Run()
    {
        Queue<char> byteQueue = new Queue<char>();
        var bytes = File.ReadLines(@"./data/day6.txt").FirstOrDefault().ToCharArray();
        for (int i = 0; i < bytes.Length; i++)
        {
            if (i < 14) byteQueue.Enqueue(bytes[i]);
            else
            {
                if (byteQueue.Distinct().Count() == 14) return i.ToString();
                else
                {
                    byteQueue.Enqueue(bytes[i]);
                    _ = byteQueue.Dequeue();
                }
            }
        }
        return "err";
    }
}