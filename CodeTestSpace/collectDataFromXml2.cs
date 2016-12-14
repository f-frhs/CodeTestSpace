// collectDataFroXmlFiles.cs <= mgetFileName.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Office.Interop.Excel;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;
using CodeTestSpace;

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
                names[$"{placeName}_{pointName}"] = new List<double>();
                foreach (string file in GetFiles(@"C:\Users\hayashi\Desktop\csvtesrt"))
                {
                    XmlRead(file);
                    DirectoryInfo dirInfo = Directory.GetParent(file);

                    foreach (var l in list.FindAll(c => c.Item2 == placeName).FindAll(c => c.Item3 == pointName))
                    {
                        names[$"{placeName}_{pointName}"].Add(l.Item4);
                    };
                }
            }
        }

        //Console.WriteLine($"Count: {names["CubeHole1_X"].Count}");
        //Console.WriteLine($"Sum: {names["CubeHole1_X"].Sum()}");
        //Console.WriteLine($"Average: {names["CubeHole1_X"].Average()}");
        //Console.WriteLine($"SD: {names["CubeHole1_X"].Sd()}");

        var CH_X12 = names["CubeHole1_X"].ToArray().Zip(names["CubeHole2_X"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var CH_X13 = names["CubeHole1_X"].ToArray().Zip(names["CubeHole3_X"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var CH_X23 = names["CubeHole2_X"].ToArray().Zip(names["CubeHole3_X"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();

        var CH_Y12 = names["CubeHole1_Y"].ToArray().Zip(names["CubeHole2_Y"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var CH_Y13 = names["CubeHole1_Y"].ToArray().Zip(names["CubeHole3_Y"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var CH_Y23 = names["CubeHole2_Y"].ToArray().Zip(names["CubeHole3_Y"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();

        var CH_Z12 = names["CubeHole1_Z"].ToArray().Zip(names["CubeHole2_Z"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var CH_Z13 = names["CubeHole1_Z"].ToArray().Zip(names["CubeHole3_Z"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var CH_Z23 = names["CubeHole2_Z"].ToArray().Zip(names["CubeHole3_Z"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();

        var chLength12 = CalcSD(CH_X12, CH_Y12, CH_Z12);
        var chLength13 = CalcSD(CH_X13, CH_Y13, CH_Z13);
        var chLength23 = CalcSD(CH_X23, CH_Y23, CH_Z23);

        Console.WriteLine(chLength12);
        Console.WriteLine(chLength13);
        Console.WriteLine(chLength23);

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
