using System;
using System.Linq;

class main
{
    public static void Main()
    {
        var a = new int[] { 1, 2, 3 };
        var b = new int[] { 4, 5, 6 };
        var c = b.Zip(a, (x, y) => x - y);

        foreach(int i in c)
        {
            Console.WriteLine($"{i}");
        }
        Console.ReadLine();
    } 
}
