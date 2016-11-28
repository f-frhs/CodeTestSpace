// collectDataFroXmlFiles.cs <= mgetFileName.cs

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

class Mainclass
{
    static void Main(string[] arg)
    {
        foreach (string file in GetFiles(@"C:\Users\hayashi\Desktop\test"))
        //foreach (string file in GetFiles(@"C:\Users\hayashi\Desktop\csvtesrt\1"))
        {
            //Console.WriteLine(file);
            //XDocument doc = XDocument.Load(file);

            //var authors = doc.Descendants("absolute");

            //foreach (var author in authors)
            //{
            //    Console.WriteLine(author.Value);
            //}
            //Console.ReadLine();

            //  ---MDM系---
            //FileStream inputStream = new FileStream(file, FileMode.Open); // ファイルストリームのインスタンスを作る。
            //XmlSerializer serializer = new XmlSerializer(typeof(Company)); // シリアライザーのインスタンスを作る。
            //Company model = (Company)serializer.Deserialize(inputStream); // 逆シリアライズしてオブジェクトに格納する。
            //// 読み込んだデータの表示
            //foreach (Person person in model.Person)
            //{
            //    Console.WriteLine(String.Format("ID={0}, Name={1}, NickName={2}, Color={3}", person.ID, person.Name, person.NickName, person.Color));
            //}
            //  ---MDM系---

            //var q = from parent in XElement.Parse(rsp).Element("root").Elements("parent")
            //        from child in parent.Elements("child")
            //        from item in child.Elements("item")
            //        select new { Parent = parent, Child = child, Item = item };
            //foreach (var x in q)
            //{
            //    Console.WriteLine("parent:{0}, child:{1}, item:{2}",
            //        x.Parent.Attribute("id").Value,
            //        x.Child.Attribute("id").Value,
            //        x.Item.Attribute("id").Value
            //        );
            //}
            //http://blogs.wankuma.com/masaru/archive/2011/08/08/201490.aspx
            //

            System.IO.FileStream fs = new System.IO.FileStream(file, System.IO.FileMode.Open);
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Cycleresult));
            Cycleresult result = (Cycleresult)serializer.Deserialize(fs);

            //Console.WriteLine(String.Format("SYSTEM={0}, Version={1}", configModel.SystemName, configModel.Version));
            foreach (Date dataTime in result.Cycle)
            {
                Console.WriteLine(String.Format("Month={0}, Day={1}, Year={2}", dataTime.Month, dataTime.Day, dataTime.Year));
            }
            fs.Close();

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
                //foreach (string subDir in Directory.EnumerateFiles(@"C:\Users\hayashi\Desktop\2016_1115_data", "*.xml")), System.IO.SearchOption.AllDirectories))
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

//  ---MDM系---
//[System.Xml.Serialization.XmlRoot("company")]
//public class Company
//{
//    // XMLファイル内にcompany要素直下のperson要素が複数あるので、プロパティをコレクションにする。
//    [System.Xml.Serialization.XmlElement("person")]
//    public System.Collections.Generic.List<Person> Person { get; set; }
//}
//public class Person
//{
//    [System.Xml.Serialization.XmlAttribute("id")]
//    public String ID { get; set; }
//    [System.Xml.Serialization.XmlElement("name")]
//    public String Name { get; set; }
//    [System.Xml.Serialization.XmlElement("color")]
//    public String Color { get; set; }
//    [System.Xml.Serialization.XmlElement("nickname")]
//    public String NickName { get; set; }
//}
//  ---MDM系---



[System.Xml.Serialization.XmlRoot("cycleresult")]
public class Cycleresult
{
    [System.Xml.Serialization.XmlArray("cell")]
    [System.Xml.Serialization.XmlArrayItem("name")]
    public string Cell { get; set; }

    [System.Xml.Serialization.XmlArray("parttype")]
    [System.Xml.Serialization.XmlArrayItem("name")]
    public string Parttype { get; set; }

    [System.Xml.Serialization.XmlArray("cycle")]
    [System.Xml.Serialization.XmlArrayItem("date")]
    [System.Xml.Serialization.XmlArrayItem("time")]
    public List<Date> Cycle { get; set; }
}

[Serializable]
public class Date
{
    [System.Xml.Serialization.XmlElement("month")]
    public string Month { get; set; }

    [System.Xml.Serialization.XmlElement("day")]
    public string Day { get; set; }

    [System.Xml.Serialization.XmlElement("year")]
    public string Year { get; set; }
}

[Serializable]
public class Time
{
    [System.Xml.Serialization.XmlElement("hour")]
    public string Month { get; set; }

    [System.Xml.Serialization.XmlElement("minute")]
    public string Day { get; set; }

    [System.Xml.Serialization.XmlElement("second")]
    public string Year { get; set; }
}
