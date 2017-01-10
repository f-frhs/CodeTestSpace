using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace AutoAssyModules.Perceptron
{

    [Serializable]
    public class CMeasurement
    {
        [XmlElement("deviation")]
        public double deviation { set; get; }

        [XmlElement("nominal")]
        public double nominal { set; get; }

        [XmlElement("absolute")]
        public double abusolute { set; get; }
    }

    /// <summary>計測データをstringでパースする </summary>
    public class CMeasurementString
    {
        [XmlElement("deviation")]
        public string deviation { set; get; }

        [XmlElement("nominal")]
        public string nominal { set; get; }

        [XmlElement("absolute")]
        public string abusolute { set; get; }
    }

    [Serializable]
    public class CInspectionCharacteristic
    {
        [XmlElement("defaultname")]
        public string defaultName { set; get; }

        [XmlElement("aliasname")]
        public string aliasName { set; get; }

        //[XmlElement("measurement")]
        //public CMeasurement Measurement { set; get; }

        [XmlElement("measurement")]
        public CMeasurementString Measurement { set; get; }
    }

    [Serializable]
    public class CInspectionPoint
    {
        [XmlElement("name")]
        public string name { set; get; }

        [XmlElement("featuretype")]
        public string featuretype { set; get; }

        [XmlElement("characteristic")]
        public List<CInspectionCharacteristic> CInspectionCharacteristics { set; get; }

        
        public static CInspectionPoint LoadFromNode(XmlReader node)
        {
            //Trace.WriteLine(TConst + System.Reflection.MethodBase.GetCurrentMethod().Name);

            var serializer = new XmlSerializer(typeof(CInspectionPoint));
            var ans = (CInspectionPoint)serializer.Deserialize(node);

            return ans;

        }
    }

    [Serializable]
    public class CPerceptronDate
    {
        [XmlElement("month")]
        public int month { set; get; }

        [XmlElement("day")]
        public int day { set; get; }

        [XmlElement("year")]
        public int year { set; get; }
    }

    [Serializable]
    public class CPerceptronTime
    {
        [XmlElement("hour")]
        public int hour { set; get; }

        [XmlElement("minute")]
        public int minute { set; get; }

        [XmlElement("second")]
        public int second { set; get; }
    }

    [Serializable]
    public class CPerceptronPartID
    {
        [XmlElement("name")]
        public string partID { set; get; }

        [XmlElement("value")]
        public string partValue { set; get; }
    }

    [Serializable]
    public class CPerceptronProcessID
    {
        [XmlElement("name")]
        public string ProcessID { set; get; }
        
        [XmlElement("value")]
        public string ProcessValue { set; get; }
    }

    [Serializable]
    public class CPerceptronCycle
    {
        [XmlElement("date")]
        public CPerceptronDate Date { set; get; }

        [XmlElement("time")]
        public CPerceptronTime PTime { set; get; }

        [XmlElement("partid")]
        public CPerceptronPartID PartID { set; get; }

        [XmlElement("processid")]
        public CPerceptronProcessID ProcessID { set; get; }

        [XmlElement("buildquality")]
        public string Quality { set; get; }

        [XmlElement("workshift")]
        public string Workshift { set; get; }

        [XmlElement("cyclemode")]
        public string CycleMode { set; get; }

        [XmlElement("alignment")]
        public string Alignment { set; get; }
    }

    //パーセプトロン結果ファイルを解析するクラス
    //[XmlRoot("dot")]
    [Serializable]
    [XmlRoot("dot")]
    public class CPerceptronData
    {
        [XmlIgnore]
        public string TConst { set; get; }

        [XmlArray("cell")]
        [XmlArrayItem("name")]
        public string[] Cell { set; get; }

        [XmlArray("parttype")]
        [XmlArrayItem("name")]
        public string[] Parttype { set; get; }

        [XmlElement("cycle")]
        public CPerceptronCycle Cycle { set; get; }

        [XmlElement("compensationtype")]
        public string Compensationtype { set; get; }

        [XmlElement("inspectionpoint")]
        public List<CInspectionPoint> Inspections { set; get; }
        
        //コンストラクタ
        public CPerceptronData()
        {
            var className = this.GetType().FullName;
            TConst = "[" + className + "]";

            Trace.WriteLine(TConst + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        //データをファイルに保存する
        //シリアライズして保存
        public void SaveToFile(string fname)
        {
            Trace.WriteLine(TConst + System.Reflection.MethodBase.GetCurrentMethod().Name);

            //XMLファイルに保存する
            var serializer1 = new XmlSerializer(typeof(CPerceptronData));
            var sw = new StreamWriter(fname, false, new UTF8Encoding(false));
            serializer1.Serialize(sw, this);
            sw.Close();
        }

        /// <summary>パーセプトロン結果ファイルを解析し、パース結果を返す </summary>
        /// <param name="fname">結果ファイル（パス文字列を含む）</param>
        /// <param name="flg">XMLパースの指定</param>
        /// <returns>解析結果</returns>
        public static CPerceptronData LoadFromFile(string fname, bool flg)
        {
            //Trace.WriteLine(STConst + System.Reflection.MethodBase.GetCurrentMethod().Name);

            //書き出したファイルを読み込んでクラスを再設定
            var serializer2 = new XmlSerializer(typeof(CPerceptronData));
            using (var sr = new StreamReader(fname, new UTF8Encoding(false)))
            {
                var ans = (CPerceptronData)serializer2.Deserialize(sr);
                sr.Close();

                return ans;
            }
        }

        /// <summary>結果に指定情報が含まれているかチェックする </summary>
        /// <param name="cdata">パーセプトロン結果ファイル</param>
        /// <param name="InspecName">計測名 Nut1,Hole1など</param>
        /// <param name="element">計測要素 X,Y..など</param>
        /// <param name="pCharacteristic">計測要素の結果格納先</param>
        /// <returns>計測要素が存在する場合は true</returns>
        public static bool IsContains(CPerceptronData cdata, string InspecName, string element, out CInspectionCharacteristic pCharacteristic)
        {
            CInspectionCharacteristic chara = null;
            var ans = false;

            //結果にInspecNameがない場合
            if (cdata.Inspections.All(d => d.name != InspecName))
            {
                pCharacteristic = null;
                return false;
            }

            //InspecNameで、elementを検索し、合致すれば返す
            var dInspec = cdata.Inspections.First(d => d.name == InspecName);
            if(dInspec.CInspectionCharacteristics.Any(d =>d.defaultName == element))
            {
                chara = dInspec.CInspectionCharacteristics.First(d => d.defaultName == element);
                ans = true;
            }

            //var ans = cdata.Inspections.Any(d =>
            //{
            //    if (d.CInspectionCharacteristics.Any(dd => dd.defaultName == element))
            //    {
            //        chara = d.CInspectionCharacteristics.First(ddd => ddd.defaultName == element);
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //});

            pCharacteristic = chara;
            return ans;
        }

        //xmlファイルを解析して、データを取得する
        public static CPerceptronData LoadFromFile(string fname)
        {
            //Trace.WriteLine(STConst + System.Reflection.MethodBase.GetCurrentMethod().Name);

            //xmlファイルを読み込む
            var xmlData = XElement.Load(fname);

            var ans = new CPerceptronData();

            ans.Cell = new string[1];
            ans.Cell[0] = xmlData.Element("cell").Element("name").Value;

            ans.Parttype = new string[1];
            ans.Parttype[0] = xmlData.Element("parttype").Element("name").Value;

            var date = new CPerceptronDate();
            date.year = int.Parse(xmlData.Element("cycle").Element("date").Element("year").Value);
            date.month = int.Parse(xmlData.Element("cycle").Element("date").Element("month").Value);
            date.day = int.Parse(xmlData.Element("cycle").Element("date").Element("day").Value);

            var time = new CPerceptronTime();
            time.hour = int.Parse(xmlData.Element("cycle").Element("time").Element("hour").Value);
            time.minute = int.Parse(xmlData.Element("cycle").Element("time").Element("minute").Value);
            time.second = int.Parse(xmlData.Element("cycle").Element("time").Element("second").Value);

            var partid = new CPerceptronPartID();
            partid.partID = xmlData.Element("cycle").Element("partid").Element("name").Value;
            partid.partValue = xmlData.Element("cycle").Element("partid").Element("value").Value;

            var processid = new CPerceptronProcessID();
            processid.ProcessID = xmlData.Element("cycle").Element("processid").Element("name").Value;
            processid.ProcessValue = xmlData.Element("cycle").Element("processid").Element("value").Value;

            ans.Cycle = new CPerceptronCycle();
            ans.Cycle.Date = date;
            ans.Cycle.PTime = time;
            ans.Cycle.PartID = partid;
            ans.Cycle.ProcessID = processid;

            var ins = xmlData.Elements("inspectionpoint").ToArray();

            var insList = new List<CInspectionPoint>();

            foreach (var xElement in ins)
            {
                var inspect = new CInspectionPoint();
                inspect.name = xElement.Element("name").Value;
                inspect.featuretype = xElement.Element("featuretype").Value;
                inspect.CInspectionCharacteristics = new List<CInspectionCharacteristic>();

                foreach (var chara in xElement.Elements("characteristic"))
                {
                    var characteristic = new CInspectionCharacteristic();
                    characteristic.defaultName = chara.Element("defaultname").Value;
                    characteristic.aliasName = chara.Element("aliasname").Value;

                    //characteristic.Measurement = new CMeasurement();
                    //characteristic.Measurement.abusolute =
                    //    double.Parse(chara.Elements("measurement").First().Element("deviation").Value);
                    //characteristic.Measurement.nominal =
                    //    double.Parse(chara.Elements("measurement").First().Element("nominal").Value);
                    //characteristic.Measurement.abusolute =
                    //    double.Parse(chara.Elements("measurement").First().Element("absolute").Value);

                    characteristic.Measurement = new CMeasurementString();
                    characteristic.Measurement.abusolute =
                        chara.Elements("measurement").First().Element("deviation").Value;
                    characteristic.Measurement.nominal =
                        chara.Elements("measurement").First().Element("nominal").Value;
                    characteristic.Measurement.abusolute =
                        chara.Elements("measurement").First().Element("absolute").Value;


                    inspect.CInspectionCharacteristics.Add(characteristic);
                }
                //inspect.CInspectionCharacteristics = clist.ToArray();
                insList.Add(inspect);
            }

            ans.Inspections = insList;

            return ans;
            
        }
        
    }


}
