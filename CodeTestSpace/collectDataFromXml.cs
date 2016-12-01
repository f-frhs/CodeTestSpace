// collectDataFroXmlFiles.cs <= mgetFileName.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

//-----------------------------------------------------------------------
//class Program
//{
//    static void Main(string[] args)
//    {
//        StoresXmlRead();
//        //Console.ReadKey();
//    }

//    static private void StoresXmlRead()
//    {
//        using (StreamReader reader = new StreamReader(@"C:\Users\hayashi\Desktop\test\aaa.xml"))
//        {
//            XmlSerializer serializer = new XmlSerializer(typeof(Stores));
//            var value = (Stores)serializer.Deserialize(reader);
//            Console.WriteLine(string.Format("NumOfStore = {0}.", value.store.Length));
//            foreach (Store s in value.store)
//            {
//                Console.WriteLine(string.Format("Name = {0}", s.Name));
//                foreach (Area a in s.area)
//                {
//                    Console.WriteLine(string.Format("description = {0}", a.description));
//                }
//            }
//        }
//    }
//}

//[XmlRootAttribute(Namespace = "", IsNullable = false)]
//public class Stores
//{
//    [System.Xml.Serialization.XmlElementAttribute("Store")]
//    public Store[] store { get; set; }
//}

//[XmlRootAttribute(Namespace = "", IsNullable = false)]
//public class Store
//{
//    public string Name { get; set; }

//    [XmlElementAttribute("Area")]
//    public Area[] area { get; set; }
//}

//[XmlRoot(Namespace = "", IsNullable = false)]
//public class Area
//{
//    public string description { get; set; }
//}
//-----------------------------------------------------------------------
//                  ↑参考　↓改造
//-----------------------------------------------------------------------

class Program
{
    static void Main(string[] args)
    {
        foreach (string file in GetFiles(@"C:\Users\hayashi\Desktop\csvtesrt"))
        {
            StoresXmlRead(file);
        }
        Console.ReadLine();
    }

    static private void StoresXmlRead(string file)
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

//-----------------------------------------------------------------------