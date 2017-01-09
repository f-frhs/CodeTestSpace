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
        throw new NotImplementedException();
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

    public void SaveDatas(string fName, List<InspectItem> items, List<double> mean, List<double> dev,
        List<CalcAnswer> funcs)
    {
        throw new NotImplementedException();
    }

    static void Main(string[] args)
    {
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


        var lengthX1_2 = new List<double>();
        var lengthX1_3 = new List<double>();
        var lengthX2_3 = new List<double>();

        var placeNames = new List<string> { "CubeHole1", "CubeHole2", "CubeHole3", "FrangeHole1", "FrangeHole2", "FrangeHole3", "HoleNut1", "HoleNut2", "CubeHole1_No", "CubeHole2_No", "CubeHole3_No", "FrangeHole1_No", "FrangeHole2_No", "FrangeHole3_No", "HoleNut1_No", "HoleNut2_No" };
        var pointNames = new List<string> { "X", "Y", "Z", "Orientation I", "Orientation J", "Orientation K", "Diameter" };

        names = new Dictionary<string, List<double>>();

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

        //Console.WriteLine($"{list}");
        //Console.WriteLine($"Count: {names["CubeHole1_X"].Count}");
        //Console.WriteLine($"Sum: {names["CubeHole1_X"].Sum()}");
        //Console.WriteLine($"Average: {names["CubeHole1_X"].Average()}");
        //Console.WriteLine($"SD: {names["CubeHole1_X"].Sd()}");


        //ToDo：----------------以下を簡略化----------------------
        //出力：各試行(Front2mm...ごと)、全試行(各試行の合算)
        //---RB1フランジ、RB2フランジ
        //穴間距離：　CH、FH各試行のAvgとSD
        //穴座標：　CH全試行 x,y,z のAvgとSD、各試行 x,y,z,Dia のAvgとSD
        //          FH各試行 x,y,z,Dia のAvgとSD
        //各穴法線ベクトル：　CH、FH各試行 i,j,k のAvgとSD
        //ｰｰｰRB1フランジ
        //穴間距離：　CH各試行のAvgとSD
        //穴座標：　CH各試行 x,y,z,Dia のAvgとSD
        //各穴法線ベクトル：　CH各試行 i,j,k のAvgとSD
        //---ナットランナー
        //穴間距離：　CH各試行のAvgとSD
        //穴座標：　NH各試行 x,y,z,Dia のAvgとSD
        //          CH各試行 x,y,z,Dia のAvgとSD
        //ｰｰｰマテハン
        //穴間距離：　MH1-2　各試行の　AvgとSD
        //穴座標：　MH　各試行 x,y,z,Dia のAvgとSD
        //各穴法線ベクトル：　MH　各試行 i,j,k のAvgとSD
        //マテハン設置誤差：　x,y,z,Roll,Pitch,Yaw のAvgとSD

        //CH各穴間各対応座標の差の２乗
        var CH_X12 = names["CubeHole1_X"].ToArray().Zip(names["CubeHole2_X"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var CH_X13 = names["CubeHole1_X"].ToArray().Zip(names["CubeHole3_X"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var CH_X23 = names["CubeHole2_X"].ToArray().Zip(names["CubeHole3_X"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();

        var CH_Y12 = names["CubeHole1_Y"].ToArray().Zip(names["CubeHole2_Y"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var CH_Y13 = names["CubeHole1_Y"].ToArray().Zip(names["CubeHole3_Y"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var CH_Y23 = names["CubeHole2_Y"].ToArray().Zip(names["CubeHole3_Y"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();

        var CH_Z12 = names["CubeHole1_Z"].ToArray().Zip(names["CubeHole2_Z"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var CH_Z13 = names["CubeHole1_Z"].ToArray().Zip(names["CubeHole3_Z"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var CH_Z23 = names["CubeHole2_Z"].ToArray().Zip(names["CubeHole3_Z"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();

        //FH各穴間各対応座標の差の２乗
        var FH_X12 = names["FrangeHole1_X"].ToArray().Zip(names["FrangeHole2_X"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var FH_X13 = names["FrangeHole1_X"].ToArray().Zip(names["FrangeHole3_X"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var FH_X23 = names["FrangeHole2_X"].ToArray().Zip(names["FrangeHole3_X"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();

        var FH_Y12 = names["FrangeHole1_Y"].ToArray().Zip(names["FrangeHole2_Y"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var FH_Y13 = names["FrangeHole1_Y"].ToArray().Zip(names["FrangeHole3_Y"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var FH_Y23 = names["FrangeHole2_Y"].ToArray().Zip(names["FrangeHole3_Y"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();

        var FH_Z12 = names["FrangeHole1_Z"].ToArray().Zip(names["FrangeHole2_Z"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var FH_Z13 = names["FrangeHole1_Z"].ToArray().Zip(names["FrangeHole3_Z"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var FH_Z23 = names["FrangeHole2_Z"].ToArray().Zip(names["FrangeHole3_Z"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();

        //CHN各穴間各対応座標の差の２乗
        var CHN_X12 = names["CubeHole1_No_X"].ToArray().Zip(names["CubeHole2_No_X"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var CHN_X13 = names["CubeHole1_No_X"].ToArray().Zip(names["CubeHole3_No_X"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var CHN_X23 = names["CubeHole2_No_X"].ToArray().Zip(names["CubeHole3_No_X"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();

        var CHN_Y12 = names["CubeHole1_No_Y"].ToArray().Zip(names["CubeHole2_No_Y"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var CHN_Y13 = names["CubeHole1_No_Y"].ToArray().Zip(names["CubeHole3_No_Y"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var CHN_Y23 = names["CubeHole2_No_Y"].ToArray().Zip(names["CubeHole3_No_Y"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();

        var CHN_Z12 = names["CubeHole1_No_Z"].ToArray().Zip(names["CubeHole2_No_Z"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var CHN_Z13 = names["CubeHole1_No_Z"].ToArray().Zip(names["CubeHole3_No_Z"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var CHN_Z23 = names["CubeHole2_No_Z"].ToArray().Zip(names["CubeHole3_No_Z"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();

        //FHN各穴間各対応座標の差の２乗
        var FHN_X12 = names["FrangeHole1_No_X"].ToArray().Zip(names["FrangeHole2_No_X"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var FHN_X13 = names["FrangeHole1_No_X"].ToArray().Zip(names["FrangeHole3_No_X"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var FHN_X23 = names["FrangeHole2_No_X"].ToArray().Zip(names["FrangeHole3_No_X"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();

        var FHN_Y12 = names["FrangeHole1_No_Y"].ToArray().Zip(names["FrangeHole2_No_Y"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var FHN_Y13 = names["FrangeHole1_No_Y"].ToArray().Zip(names["FrangeHole3_No_Y"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var FHN_Y23 = names["FrangeHole2_No_Y"].ToArray().Zip(names["FrangeHole3_No_Y"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();

        var FHN_Z12 = names["FrangeHole1_No_Z"].ToArray().Zip(names["FrangeHole2_No_Z"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var FHN_Z13 = names["FrangeHole1_No_Z"].ToArray().Zip(names["FrangeHole3_No_Z"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
        var FHN_Z23 = names["FrangeHole2_No_Z"].ToArray().Zip(names["FrangeHole3_No_Z"].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();

        //CH穴間距離
        var chLength12 = CalcSD(CH_X12, CH_Y12, CH_Z12);
        var chLength13 = CalcSD(CH_X13, CH_Y13, CH_Z13);
        var chLength23 = CalcSD(CH_X23, CH_Y23, CH_Z23);

        //FH穴間距離
        var fhLength12 = CalcSD(FH_X12, FH_Y12, FH_Z12);
        var fhLength13 = CalcSD(FH_X13, FH_Y13, FH_Z13);
        var fhLength23 = CalcSD(FH_X23, FH_Y23, FH_Z23);

        //CHN穴間距離
        var chNLength12 = CalcSD(CHN_X12, CHN_Y12, CHN_Z12);
        var chNLength13 = CalcSD(CHN_X13, CHN_Y13, CHN_Z13);
        var chNLength23 = CalcSD(CHN_X23, CHN_Y23, CHN_Z23);

        //FHN穴間距離
        var fhNLength12 = CalcSD(FHN_X12, FHN_Y12, FHN_Z12);
        var fhNLength13 = CalcSD(FHN_X13, FHN_Y13, FHN_Z13);
        var fhNLength23 = CalcSD(FHN_X23, FHN_Y23, FHN_Z23);

        //FH穴座標・直径
        //Console.WriteLine($"Svg: {names["CubeHole1_X"].Average()},SD: {names["CubeHole1_X"].Sd()}");
        //Console.WriteLine($"Svg: {names["CubeHole1_Y"].Average()},SD: {names["CubeHole1_Y"].Sd()}");
        //Console.WriteLine($"Svg: {names["CubeHole1_Z"].Average()},SD: {names["CubeHole1_Z"].Sd()}");
        //Console.WriteLine($"Svg: {names["CubeHole1_Diameter"].Average()},SD: {names["CubeHole1_Diameter"].Sd()}");
        //Console.WriteLine($"Svg: {names["CubeHole2_X"].Average()},SD: {names["CubeHole2_X"].Sd()}");
        //Console.WriteLine($"Svg: {names["CubeHole2_Y"].Average()},SD: {names["CubeHole2_Y"].Sd()}");
        //Console.WriteLine($"Svg: {names["CubeHole2_Z"].Average()},SD: {names["CubeHole2_Z"].Sd()}");
        //Console.WriteLine($"Svg: {names["CubeHole2_Diameter"].Average()},SD: {names["CubeHole2_Diameter"].Sd()}");
        //Console.WriteLine($"Svg: {names["CubeHole3_X"].Average()},SD: {names["CubeHole3_X"].Sd()}");
        //Console.WriteLine($"Svg: {names["CubeHole3_Y"].Average()},SD: {names["CubeHole3_Y"].Sd()}");
        //Console.WriteLine($"Svg: {names["CubeHole3_Z"].Average()},SD: {names["CubeHole3_Z"].Sd()}");
        //Console.WriteLine($"Svg: {names["CubeHole3_Diameter"].Average()},SD: {names["CubeHole3_Diameter"].Sd()}");

        //各穴間距離の平均と標準偏差
        //Console.WriteLine(chLength13);
        //Console.WriteLine(chLength23);
        //Console.WriteLine(fhLength12);
        //Console.WriteLine(fhLength13);
        //Console.WriteLine(fhLength23);
        //Console.WriteLine("--------------------------------");
        //Console.WriteLine(chNLength12);
        //Console.WriteLine(chNLength13);
        //Console.WriteLine(chNLength23);
        //Console.WriteLine(fhNLength12);
        //Console.WriteLine(fhNLength13);
        //Console.WriteLine(fhNLength23);

        foreach (var serultsCH_X12 in CHN_X12)
        {
            Console.WriteLine(serultsCH_X12);
        }
        Console.WriteLine("--------------------");

        var rE = CalcEachMinusSquare("CubeHole1_X", "CubeHole2_X");
        foreach(var resultsRE in rE)
        {
            Console.WriteLine(resultsRE);
        }

        Console.ReadLine();
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
