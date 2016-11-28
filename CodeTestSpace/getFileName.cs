// getFileName.cs
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

class Mainclass
{
    //フォルダ単体から列挙
    //public static void Main()
    //{
    //    //foreach (string file in Directory.EnumerateFiles(@"C:\Users\hayashi\Desktop\2016_1115_data\st1_Nut", "*.xml"))
    //    //{
    //    //    //string contents = File.ReadAllText(file);
    //    //    Console.WriteLine("{0}", file);
    //    //}

    //    IEnumerable<string> files = System.IO.Directory.EnumerateFiles(@"C:\Users\hayashi\Desktop\2016_1115_data\st1_Nut", "*.xml", System.IO.SearchOption.AllDirectories);

    //    //ファイルを列挙する
    //    foreach (string f in files)
    //    {
    //        Console.WriteLine("{0}", f);
    //    }

    //    Console.ReadLine();
    //}

    static void Main()
    {
        foreach (string file in GetFiles(@"C:\Users\hayashi\Desktop\2016_1115_data"))
        {
            Console.WriteLine(file);
        }
    }

    static IEnumerable<string> GetFiles(string path)
    {
        Queue<string> queue = new Queue<string>();
        queue.Enqueue(path);
        while (queue.Count > 0)
        {
            path = queue.Dequeue();
            try
            {
                foreach (string subDir in Directory.GetDirectories(path))
                //foreach (string subDir in Directory.EnumerateFiles(@"C:\Users\hayashi\Desktop\2016_1115_data", "*.xml"))//, System.IO.SearchOption.AllDirectories))
                {
                    queue.Enqueue(subDir);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            string[] files = null;
            try
            {
                files = Directory.GetFiles(path);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            if (files != null)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    yield return files[i];
                }
            }
        }
        Console.ReadLine();
    }
}
