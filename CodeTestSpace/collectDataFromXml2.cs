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

public class InspectItem
{
    //注目測定点名 CubeHole1など
    public List<string> InsNames { set; get; }

    //項目 Xなど
    public List<string> Inspects { set; get; }

}

public class CalcSetting
{
    public string InsName1 { set; get; }
    public string InsName2 { set; get; }
    public string Operator { set; get; }
}

public class CalcAnswer
{
    public string InsName { set; get; }
    public string Inspect { set; get; }
    public double Ans { set; get; }
}

class Program
{
    private static List<Tuple<string, string, string, double>> list;
    private static Dictionary<string, List<double>> names;


    //注目測定点名と注目計測名と項目をファイルから読み込む
    public List<InspectItem> GetInspectionItems(string fName)
    {
        throw new NotImplementedException();
    }

    //特殊計算内容をファイルから読み込む
    public List<CalcSetting> GetClcSettings(string fName)
    {
        throw new NotImplementedException();
    }

    //指定フォルダ以下のファイルを取得する
    public List<string> GetXmlFiles(string basePath)
    {
        List < string > fnames = Directory.GetFiles(basePath, "*.xml", SearchOption.AllDirectories).ToList();
        return fnames;
    }

    //注目計測名と注目測定名のデータを収集する
    public List<CalcAnswer> CollectAnswers(InspectItem inspect)
    {
        throw new NotImplementedException();
    }

    //平均と分散を求める
    public double[] CalcMeanDev(List<CalcAnswer> correctDatas)
    {
        throw new NotImplementedException();
    }

    //特殊計算を実施する
    public double CalcFunction(CalcSetting calSetting)
    {
        throw new NotImplementedException();
    }

    //結果をファイルに保存する
    public void SaveDatas(string fName, List<InspectItem> items, List<double> mean, List<double> dev,
        List<CalcAnswer> funcs)
    {
        throw new NotImplementedException();
    }

    static void Main(string[] args)
    {
        string basePath = @"C:\Users\hayashi\Documents\Visual Studio 2015\Projects\CodeTestSpace\testdata\";
        var fnames = Program.GetFiles(basePath);

        foreach(var fname in fnames)
        {
            Console.WriteLine(fname);
        }

        Console.ReadKey();

        //注目測定点名と注目計測名と項目をファイルから読み込む
        //例
        //ST1_SF01　（注目測定点名）
        //CubeHole1,CubeHole2 (注目計測名)
        //X Y ...等（項目）
        //注目計測名、項目は可変数

        //特殊計算内容をファイルから読み込む
        //例：
        //distance,CubeHole1,CubeHole2
        //特殊計算内容は可変とする

        //指定フォルダ以下のファイルを取得する
        //注目測定点名と合致するファイルを更にコレクトする

        //コレクトしたファイルから、注目計測名と注目測定名のデータを収集する

        //収集したデータから、各注目測定点名ごとの平均と分散を求める

        //結果をファイルに保存する

   
    }

    //ToDo: 全体をエクセルに吐き出す

    //ToDo: Listから任意の要素を抜き出す部分の関数化

    //引数の各項の差の二乗を返す
    //ToDo結果が望むものと違う修正：CubeHole1_X", "CubeHole2_Xのみの予定
    private static double[] CalcEachMinusSquare(string a, string b)
    {
        double[] results  = names[a].ToArray().Zip(names[b].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        return results;
    }

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
