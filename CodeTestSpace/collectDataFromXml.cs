// collectDataFroXmlFiles.cs <= mgetFileName.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Office.Interop.Excel;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static void Main(string[] args)
    {
        foreach (string file in GetFiles(@"C:\Users\hayashi\Desktop\csvtesrt"))
        {
            XmlRead(file);
        }
        Console.ReadLine();
    }

    static private void XmlRead(string file)
    {
        using (StreamReader reader = new StreamReader(file))
        {
            DirectoryInfo dirInfo = Directory.GetParent(file);

            XmlSerializer serializer = new XmlSerializer(typeof(dot));
            var value = (dot)serializer.Deserialize(reader);

            foreach (inspectionpoint IP in value.Inspectionpoint)
            {
                foreach (characteristic CH in IP.Characteristic)
                {
                    foreach(measurement ME in CH.Measurement)
                    {
                        Console.WriteLine(string.Format($"{dirInfo.Name}, {IP.Name}, {CH.Defaultname}, {ME.Absolute}"));
                    }
                }
            }
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

    //static private void ExcelOutPut()
    //{
    //    //Excelのパス
    //    string fileName = @"C:\c_sharp\test.xlsx";
    //    Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

    //    //Excelが開かないようにする
    //    xlApp.Visible = false;

    //    //指定したパスのExcelを起動
    //    Workbook wb = xlApp.Workbooks.Open(Filename: fileName);

    //    try
    //    {
    //        //Sheetを指定
    //        ((Worksheet)wb.Sheets[1]).Select();
    //    }
    //    catch (Exception ex)
    //    {
    //        //Sheetがなかった場合のエラー処理

    //        //Appを閉じる
    //        wb.Close(false);
    //        xlApp.Quit();

    //        //Errorメッセージ
    //        Console.WriteLine("指定したSheetは存在しません．");
    //        Console.ReadLine();

    //        //実行を終了
    //        System.Environment.Exit(0);
    //    }

    //    //変数宣言
    //    Range CellRange;

    //    foreach (string file in GetFiles(@"C:\Users\hayashi\Desktop\csvtesrt"))
    //    {
    //        //XmlRead(file);

    //        //書き込む場所を指定
    //        CellRange = xlApp.Cells[] as Range;

    //        //書き込む内容
    //        CellRange.Value2 = XmlRead(); ;
    //    }

    //    //Appを閉じる
    //    wb.Close(true);
    //    xlApp.Quit();
    //}
}

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
