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
using AutoAssyModules.Perceptron;
using System.Text;

public class InspectItem
{
    //CSV内容を格納：注目測定点名・注目計測名・項目
    //注目測定点名 ST1_SF01など
    public List<string> InsNames { get; set; }

    //注目計測名 CubeHole1など
    public List<string> Inspects { get; set; }

    //項目 Xなど
    public List<string> Items {get; set; }
}

public class CalcSetting
{
    /// <summary>CSV内容を格納：計算を行う対象の測定名と計算方法</summary>
    public string InsName1 { set; get; }
    public string InsName2 { set; get; }
    public string Operator { set; get; }
}

public class CalcAnswer
{
    /// <summary>収集されたデータを格納</summary>
    public string InsName { set; get; }
    public string Inspect { set; get; }
    public double Ans { set; get; }
}

class Program
{
    //指定フォルダ以下のファイルを取得する
    public static List<string> GetXmlFiles(string basePath)
    {
        //引数で渡されたフォルダ以下の全てのxmlファイルを取得：フォルダは各測定(2mmFront等)ごととする
        List<string> fnames = Directory.GetFiles(basePath, "*.xml", SearchOption.AllDirectories).ToList();
        return fnames;
    }

    //注目測定点名と注目計測と項目をファイルから読み込む
    public static List<InspectItem> GetInspectionItems(string fName)
    {
        //CSVから測定点名・注目計測・項目を読み出す
        StreamReader reader = new StreamReader(fName, Encoding.GetEncoding("Shift_JIS"));
        var result = reader.ReadLine().Split(',').ToList();


        //各リストに文字列を追加
        var InsNameList = new List<string>();
        InsNameList.Add(result[0]);
        var InspectsList = new List<string>();
        InspectsList.Add(result[1]);
        InspectsList.Add(result[2]);
        var ItemsList = new List<string>();
        ItemsList.Add(result[3]);
        ItemsList.Add(result[4]);
        ItemsList.Add(result[5]);
        ItemsList.Add(result[6]);
        ItemsList.Add(result[7]);
        ItemsList.Add(result[8]);
        ItemsList.Add(result[9]);

        //一つの InspectItem を作る
        var answer = new InspectItem();
        answer.InsNames = InsNameList;
        answer.Inspects = InspectsList;
        answer.Items = ItemsList;

        //作成した　answer を リストの answers に追加する
        var answers = new List<InspectItem>();
        answers.Add(answer);

        return answers;
    }

    //特殊計算内容をファイルから読み込む
    public static List<CalcSetting> GetClcSettings(string fName)
    {
        //CSVから特殊計算を読み込む
        StreamReader reader = new StreamReader(fName, Encoding.GetEncoding("Shift_JIS"));
        var result = reader.ReadLine().Split(',').ToList();

        //一つの InspectItem を作る
        var answer = new CalcSetting();
        answer.InsName1 = result[1];
        answer.InsName2 = result[2];
        answer.Operator = result[0];

        //作成した　answer を リストの answers に追加する
        var answers = new List<CalcSetting>();
        answers.Add(answer);

        return answers;
    }

    //注目計測名と注目測定名のデータを収集する
    public List<CalcAnswer> CollectAnswers(InspectItem inspect)
    {
        //InspectItemを元に計測名・測定名・項目からAbsoluteを返す
        //var data = CPerceptronData.LoadFromFile(fname, true); CInspectionCharacteristic outInspect;
        //if (CPerceptronData.IsContains(data, "CubeHole1", "X", out outInspect))
        //{
        //    Console.WriteLine($"X={outInspect.Measurement.abusolute}");
        //}

        throw new NotImplementedException();
    }

    //平均と分散を求める
    public double[] CalcMeanDev(List<CalcAnswer> collectDatas)
    {
        //平均の式

        //分散の式

        throw new NotImplementedException();
    }

    //特殊計算を実施する
    public double CalcFunction(CalcSetting calSetting)
    {
        //GetClcSettingsで選択された計算を用いて結果を返す

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
        //注目測定点名と注目計測名と項目が書かれたファイルのアドレス
        string csvFilePath = @"C:\Users\hayashi\Documents\Visual Studio 2015\Projects\CodeTestSpace\insepectionData\settingData.CSV";

        //注目測定点名と注目計測名と項目をファイルから読み込む
        //例
        //ST1_SF01　（注目測定点名）
        //CubeHole1,CubeHole2 (注目計測名)
        //X Y ...等（項目）
        //注目計測名、項目は可変数
        var insSetting = GetInspectionItems(csvFilePath);

        //特殊計算と対象が書かれたファイルのアドレス
        string csvCalcPath = @"C:\Users\hayashi\Documents\Visual Studio 2015\Projects\CodeTestSpace\insepectionData\calcSettingData.CSV";

        //特殊計算内容をファイルから読み込む
        //例：
        //distance,CubeHole1,CubeHole2
        //特殊計算内容は可変とする
        var calcSetting = GetClcSettings(csvCalcPath);

        //処理対象のフォルダを指定
        string basePath = @"C:\Users\hayashi\Documents\Visual Studio 2015\Projects\CodeTestSpace\testdata\";

        //指定フォルダ以下のファイルを取得する (フォルダ内のxmlファイルをリストに格納)
        var fnames = GetXmlFiles(basePath);

        //注目測定点名と合致するファイルを更にコレクトする
        //CSVファイルからデータの読み込み
        //読み込んだデータと合致するファイルを選択(方法：xml内の<parttype>の<name>で判別)


        //コレクトしたファイルから、注目計測名と注目測定名のデータを収集する
        //

        //収集したデータから、各注目測定点名ごとの平均と分散を求める

        //結果をファイルに保存する


        Console.ReadLine();
    }


    //---以下旧版：削除予定--------

    //計算内容(現在)
    //出力：各試行(Front2mm...ごと)、全試行(各試行の合算)
    //---RB1フランジ、RB2フランジ
    //穴間距離：　CH、FH各試行のAvgとSD
    //穴座標：　CH全試行 x,y,z のAvgとSD、各試行 x,y,z,Dia のAvgとSD
    //          FH各試行 x,y,z,Dia のAvgとSD
    //各穴法線ベクトル：　CH、FH各試行 i,j,k のAvgとSD
    //---RB1フランジ
    //穴間距離：　CH各試行のAvgとSD
    //穴座標：　CH各試行 x,y,z,Dia のAvgとSD
    //各穴法線ベクトル：　CH各試行 i,j,k のAvgとSD
    //---ナットランナー
    //穴間距離：　CH各試行のAvgとSD
    //穴座標：　NH各試行 x,y,z,Dia のAvgとSD
    //          CH各試行 x,y,z,Dia のAvgとSD
    //---マテハン
    //穴間距離：　MH1-2　各試行の　AvgとSD
    //穴座標：　MH　各試行 x,y,z,Dia のAvgとSD
    //各穴法線ベクトル：　MH　各試行 i,j,k のAvgとSD
    //マテハン設置誤差：　x,y,z,Roll,Pitch,Yaw のAvgとSD


    private static List<Tuple<string, string, string, double>> list;
    private static Dictionary<string, List<double>> names;

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
