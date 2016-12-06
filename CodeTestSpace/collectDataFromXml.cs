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
    //private static List<Tuple<string, string, string, double>> list;
    private static List<Tuple<string, string, string, double>> list;
    private static List<double> listX;

    static void Main(string[] args)
    {
        listX = new List<double>();

        foreach (string file in GetFiles(@"C:\Users\hayashi\Desktop\csvtesrt"))
        {
            XmlRead(file);

            //個々の出力
            //oDo: list全体をエクセルに吐き出す
            //foreach (var l in list)
            //{
            //    Console.WriteLine(l);
            //}

            var datax = list.FindAll(c => c.Item2 == "CubeHole1").FindAll(c => c.Item3 == "X");

            foreach (var l in datax)
            {
                var Absolute = l.Item4;
                Console.WriteLine(string.Format($"{Absolute}"));
                listX.Add(Absolute);
            }
        }

        //var sum = listX.Average();
        Console.WriteLine("-------------------------");
        var resoluteX = CalcSD(listX);
        Console.WriteLine(string.Format($"{resoluteX.ave}"));
        Console.WriteLine(string.Format($"{resoluteX.sd}"));
        Console.ReadLine();
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

    // 渡されたList<doubel>の標準偏差計算を返す
    private static dynamic CalcSD(List<double> p_Values)
    {
        //平均
        Double l_Ave = p_Values.Average();

        //σの二乗×データ数
        Double l_calcSD = 0;
        foreach (Double f_Value in p_Values)
        {
            l_calcSD += (f_Value - l_Ave) * (f_Value - l_Ave);
        }

        //σを算出して返却
        var l_SD = Math.Sqrt(l_calcSD / (p_Values.Count - 1));

        return new
        {
            ave = l_Ave,
            sd = l_SD
        };
    }

    //渡されたパスのxmlを、配列に書き出す
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
