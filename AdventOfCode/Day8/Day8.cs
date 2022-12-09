
//const string input = @"30373
//25512
//65332
//33549
//35390";


//List<string> lines = new List<string>();
//using (StringReader reader = new StringReader(input))
//{
//    string line;
//    while ((line = reader.ReadLine()) != null)
//    lines.Add(line);
//}

//int rowCount = lines.Count;
//int colCount = lines[0].ToCharArray().Count();

//int[,] map = new int[rowCount, colCount];

//for (int i = 0; i < rowCount; i++)
//{
//    var t = lines[i].ToCharArray();
//    for (int x = 0; x < colCount; x++)
//    {
//        map[i, x] = int.Parse(t[x].ToString());
//    }
//}

//int answer = 0;

//for (int i = 0; i < rowCount; i++)
//{
//    if (i == 0 || i == rowCount - 1)
//    {
//        answer += colCount;
//    }
//    else
//    {
//        answer += 2;

//        int lMax = map[i, 0];
//        int rMax = map[i, colCount - 1];

//        for(int x = i; x < colCount - 2; x++)
//        {
//            if (map[i, x] > lMax)
//            {
//                answer++
//            }
//        }
//    }
//}