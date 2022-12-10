public class Day7Part2 : Challenge
{
    Path current;
    Path root = new Path() { Name = "/", PathType = PathTypeEnum.Directory };
    long totalSpace = 70000000;
    long requiredSpace = 30000000;

    public override string Run()
    {
        current = root;
        foreach (string nextLine in File.ReadLines(@"./data/day7.txt"))
        {
            if (nextLine.StartsWith("$")) cliInput(nextLine.Substring(2));
            else cliOutput(nextLine);
        }
        updateDirSizes(root);
        //drawTree(root, 0);
        List<long> directorySizes = new List<long>();
        getDirSizes(root, ref directorySizes);
        long toFree = requiredSpace - (totalSpace - root.Size);
        return directorySizes.Where(s => s >= toFree).OrderBy(s => s).FirstOrDefault().ToString();
    }

    private long updateDirSizes(Path nextPath)
    {
        nextPath.Size = 0;

        foreach(var nextSubDir in nextPath.SubPath.Where(s => s.PathType == PathTypeEnum.Directory))
            nextPath.Size += updateDirSizes(nextSubDir);

        nextPath.Size += nextPath.SubPath
            .Where(s => s.PathType == PathTypeEnum.File)
            .Sum(s => s.Size);

        return nextPath.Size;
    }

    private void getDirSizes(Path nextPath, ref List<long> dirSizes)
    {
        dirSizes.Add(nextPath.Size);
        foreach (var nextSubPath in nextPath.SubPath.Where(s => s.PathType == PathTypeEnum.Directory))
            getDirSizes(nextSubPath, ref dirSizes);
    }

    private void drawTree(Path nextPath, int lvl)
    {
        var pad = new string(' ', lvl * 4);
        Console.WriteLine($"{pad}{nextPath.Name} ({nextPath.Size})");
        lvl++;
        foreach(var nextSubPath in nextPath.SubPath)
            drawTree(nextSubPath, lvl);
    }

    private void cliInput(string data)
    {
        if (data.StartsWith("cd"))
        {
            string param = data.Substring(3);
            if (param == "/") current = root;
            else if (param == "..") current = current.ParentPath;
            else current = current.SubPath.First(s => s.Name == param);
        }
    }

    private void cliOutput(string data)
    {
        if(data.StartsWith("dir"))
        {
            current.AddDir(data.Substring(4));
        }
        else
        {
            var fileInfo = data.Split(' ');
            current.AddFile(fileInfo[1], long.Parse(fileInfo[0]));
        }
    }

    public class Path
    {
        public Path()
        {
            SubPath = new List<Path>();
        }

        public void AddDir(string name)
        {
            SubPath.Add(new Path
            {
                Name = name,
                PathType = PathTypeEnum.Directory,
                ParentPath = this
            });
        }

        public void AddFile(string name, long size)
        {
            SubPath.Add(new Path
            {
                Name = name,
                PathType = PathTypeEnum.File,
                ParentPath = this,
                Size = size,
            });
        }

        public string Name { get; set; }
        public PathTypeEnum PathType { get; set; }
        public Path ParentPath { get; set; }
        public List<Path> SubPath { get; set; }
        public long Size { get; set; }
    }

    public enum PathTypeEnum
    {
        File = 0,
        Directory = 1
    }
}