//using System;
//using System.Linq;

//class main
//{
//    public static void Main()
//    {
//        var a = new int[] { 1, 2, 3 };
//        var b = new int[] { 4, 5, 6 };
//        var c = b.Zip(a, (x, y) => x - y);

//        foreach(int i in c)
//        {
//            Console.WriteLine($"{i}");
//        }
//        Console.ReadLine();
//    } 
//}


using System;
using System.Collections.Generic;

namespace SampleTupleListWithLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new List<Tuple<string, string, string, double>>();
            data.Add(Tuple.Create("A", "a", "1", 1d));
            data.Add(Tuple.Create("A", "b", "1", 2d));
            data.Add(Tuple.Create("B", "a", "2", 1d));
            data.Add(Tuple.Create("B", "b", "2", 2d));

            foreach (var l in data)
            {
                Console.WriteLine(l);
            }

            Console.Write("---------------\n");
            Console.WriteLine("Extracted elements are:");

            //ここで抽出している
            var data1 = data.FindAll(c => c.Item2 == "a")
                .FindAll(c => c.Item3 == "1");

            foreach (var l in data1)
            {
                Console.WriteLine(l);
            }

            Console.ReadKey();
        }
    }
}