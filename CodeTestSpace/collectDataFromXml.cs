// collectDataFroXmlFiles.cs <= mgetFileName.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Office.Interop.Excel;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;

class Program
{
    private static List<Tuple<string, string, string, double>> list;

    static void Main(string[] args)
    {
        foreach (string file in GetFiles(@"C:\Users\hayashi\Desktop\csvtesrt"))
        {
            XmlRead(file);

            //個々の出力
            //oDo: list全体をエクセルに吐き出す
            //

            foreach (var tuple in list)
            {
                var Name = tuple.Item1;
                var IPName = tuple.Item2;
                var DefaltName = tuple.Item3;
                var Absolute = tuple.Item4;
                Console.WriteLine(string.Format($"{Name}, {IPName}, {DefaltName}, {Absolute}"));
            }

            ////計算結果の出力
            //var sum = (list.Select(d => d.Item4).Sum());
            //var oddList = (list.FindAll(IsSmaller));


        }
        Console.ReadLine();
    }

    private static bool IsSmaller(int num)
    {
        return num < 5;
    }

    ////渡されたパスのxmlをデシリアライズcmdに書き出し
    //static private void XmlRead(string file)
    //{
    //    using (StreamReader reader = new StreamReader(file))
    //    {
    //        DirectoryInfo dirInfo = Directory.GetParent(file);

    //        XmlSerializer serializer = new XmlSerializer(typeof(dot));
    //        var value = (dot)serializer.Deserialize(reader);

    //        foreach (inspectionpoint IP in value.Inspectionpoint)
    //        {
    //            foreach (characteristic CH in IP.Characteristic)
    //            {
    //                foreach(measurement ME in CH.Measurement)
    //                {
    //                    Console.WriteLine(string.Format($"{dirInfo.Name}, {IP.Name}, {CH.Defaultname}, {ME.Absolute}"));

    //                }
    //            }
    //        }
    //    }
    //}

    // 標準偏差計算
    private Double CalcStandardDeviation(Double[] p_Values)
    {
        //平均を取得
        Double l_Average = p_Values.Average();

        //「σの二乗×データ数」まで計算
        Double l_StandardDeviation = 0;
        foreach (Double f_Value in p_Values)
        {
            l_StandardDeviation += (f_Value - l_Average) * (f_Value - l_Average);
        }

        //σを算出して返却
        return Math.Sqrt(l_StandardDeviation / p_Values.Length);
    }

    //渡されたパスのxmlをデシリアライズ配列に書き出し　
    private static void XmlRead(string file)
    {
        using (StreamReader reader = new StreamReader(file))
        {
            DirectoryInfo dirInfo = Directory.GetParent(file);

            XmlSerializer serializer = new XmlSerializer(typeof(dot));
            var value = (dot)serializer.Deserialize(reader);
            list = new List<Tuple<string, string, string, double>>();

            foreach (inspectionpoint IP in value.Inspectionpoint)
            {
                foreach (characteristic CH in IP.Characteristic)
                {
                    foreach (measurement ME in CH.Measurement)
                    {
                        list.Add(Tuple.Create(dirInfo.Name, IP.Name, CH.Defaultname, double.Parse(ME.Absolute)));
                    }
                }
            }
        }
    }

    //指定階層下のフォルダからxmlのパスを書き出し返す
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
                files = Directory.GetFiles(path, "*.xml");
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
    }
}

//以下xmlのデシリアライズ
[XmlRootAttribute(Namespace = "", IsNullable = false)]
public class dot
{
    //[XmlElement("cell")]
    //public cell[] Cell { get; set; }

    //[XmlElement("parttype")]
    //public parttype[] Parttype { get; set; }

    [XmlElement("cycle")]
    public cycle[] Cycle { get; set; }

    public string STORED { get; set; }

    [XmlElement("inspectionpoint")]
    public inspectionpoint[] Inspectionpoint { get; set; }
}

//[XmlRoot(Namespace = "", IsNullable = false)]
//public class cell
//{
//    [XmlElement("name")]
//    public string Name { get; set; }
//}

//[XmlRoot(Namespace = "", IsNullable = false)]
//public class parttype
//{
//    [XmlElement("name")]
//    public string Name { get; set; }
//}

[XmlRoot(Namespace = "", IsNullable = false)]
public class cycle
{
    [XmlElement("date")]
    public date[] Date { get; set; }
}

[XmlRoot(Namespace = "", IsNullable = false)]
public class date
{
    [XmlElement("month")]
    public string Month { get; set; }
}

[XmlRoot(Namespace = "", IsNullable = false)]
public class inspectionpoint
{
    [XmlElement("name")]
    public string Name { get; set; }

    [XmlElement("characteristic")]
    public characteristic[] Characteristic { get; set; }
}

[XmlRoot(Namespace = "", IsNullable = false)]
public class characteristic
{
    [XmlElement("defaultname")]
    public string Defaultname { get; set; }

    [XmlElement("measurement")]
    public measurement[] Measurement { get; set; }
}

[XmlRoot(Namespace = "", IsNullable = false)]
public class measurement
{
    [XmlElement("absolute")]
    public string Absolute { get; set; }
}
