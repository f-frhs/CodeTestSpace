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
        var lengthX1_2 = new List<double>();
        var lengthX1_3 = new List<double>();
        var lengthX2_3 = new List<double>();

        var placeNames = new List<string> { "CubeHole1", "CubeHole2", "CubeHole3", "FrangeHole1", "FrangeHole2", "FrangeHole3", "HoleNut1", "HoleNut2" };
        var pointNames = new List<string> { "X", "Y", "Z", "Orientation I", "Orientation J", "Orientation K", "Diamater" };

        var names = new Dictionary<string, List<double>>();


        foreach (var placeName in placeNames)
        {
            foreach (var pointName in pointNames)
            {
                foreach (string file in GetFiles(@"C:\Users\hayashi\Desktop\csvtesrt"))
                {
                    XmlRead(file);
                    names[$"{placeName}_{pointName}"] = new List<double>();
                    foreach (var l in list.FindAll(c => c.Item2 == placeName).FindAll(c => c.Item3 == pointName))
                    {
                        names[$"{placeName}_{pointName}"].Add(l.Item4);
                    };
                }
            }
        }


            //var resoletCH1_2 = CalcSquare(CHY1_X - CHY2_X, CHY1_Y - CHY2_Y, CHY1_Z - CHY2_Z);
            //var resoletCH1_3 = CalcSquare(CHY1_X - CHY3_X, CHY1_Y - CHY3_Y, CHY1_Z - CHY3_Z);
            //var resoletCH2_3 = CalcSquare(CHY2_X - CHY3_X, CHY2_Y - CHY3_Y, CHY2_Z - CHY3_Z);

            //lengthX1_2.Add(resoletCH1_2);

            //}

            //Console.WriteLine("----------結果---------------");
            //foreach (var item in lengthX1_2)
            //{
            //    Console.WriteLine($"{item}");
            //}
            //foreach (var item in lengthX1_3)
            //{
            //    Console.WriteLine($"{item}");
            //}
            //foreach (var item in lengthX2_3)
            //{
            //    Console.WriteLine($"{item}");
            //}

            foreach (var e in names["CubeHole1_X"])
        {
            Console.WriteLine(e);
        }

        //Console.WriteLine(names["CubeHole1_X"].Sum());

        Console.ReadLine();

    }

    //ToDo: list全体をエクセルに吐き出す

    //ToDo: Listから任意の要素を抜き出す部分の関数化

    //引数の総和の平方根を返す
    private static double CalcSquare(double x, double y, double z)
    {
        double length3D = Math.Sqrt(x * x + y * y + z * z );
        return length3D;
    }

    // List<doubel>を受取り、その平均と標準偏差計算を返す
    private static dynamic CalcSD(double[] x, double[] y, double[] z)
    {
        //距離： x y z の和の平方根
        double[] length3D = (((x.Zip(y, (a, b) => (a + b)).ToArray()).Zip(z, (a, b) => (a + b))).Select(i => Math.Sqrt(i)).ToArray());

        //距離： 平均
        Double l_Avg = length3D.Average();

        //σの二乗×データ数
        Double l_calcSD = 0;
        foreach (Double data in length3D)
        {
            l_calcSD += (data - l_Avg) * (data - l_Avg);
        }

        //σを算出して返却
        var l_SD = Math.Sqrt(l_calcSD / (length3D.Length - 1));

        return new
        {
            avg = l_Avg,
            sd = l_SD
        };
    }

    //xmlのパス受取り、Absoluteの値をListに書き出す
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
                foreach (parttype PT in value.Parttype)
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

    //指定階層下のフォルダにあるxmlのパスを返す
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

    [XmlElement("parttype")]
    public parttype[] Parttype { get; set; }

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

[XmlRoot(Namespace = "", IsNullable = false)]
public class parttype
{
    [XmlElement("name")]
    public string Name { get; set; }
}

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
