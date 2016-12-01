using System;
using System.IO;
//using AutoAssyModules.Perceptron;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;

namespace OutputHoleDataToCSV
{
    public class Program
    {
        static void Main(string[] args)
        {
            //---cmdから読み取る場合----
            //string[] arguments = System.Environment.GetCommandLineArgs();
            //var watchFile = arguments[1];
            //OutputHoleDataToCSV(watchFile);
            //---cmdから読み取る場合----

            foreach (string file in GetFiles(@"C:\Users\hayashi\Desktop\test\"))
            {
                OutputHoleDataToCSV(file);
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
        }

        private struct MeasureData
        {
            internal string x, y, z, i, j, k, d;
        }

        private static MeasureData GetValues(CPerceptronData data, string inspecName)
        {
            CInspectionCharacteristic ichara = null;

            var p = new MeasureData();
            p.x = CPerceptronData.IsContains(data, inspecName, "X", out ichara) ? ichara.Measurement.abusolute : string.Empty;
            p.y = CPerceptronData.IsContains(data, inspecName, "Y", out ichara) ? ichara.Measurement.abusolute : string.Empty;
            p.z = CPerceptronData.IsContains(data, inspecName, "Z", out ichara) ? ichara.Measurement.abusolute : string.Empty;
            p.i = CPerceptronData.IsContains(data, inspecName, "Orientation I", out ichara) ? ichara.Measurement.abusolute : string.Empty;
            p.j = CPerceptronData.IsContains(data, inspecName, "Orientation J", out ichara) ? ichara.Measurement.abusolute : string.Empty;
            p.k = CPerceptronData.IsContains(data, inspecName, "Orientation K", out ichara) ? ichara.Measurement.abusolute : string.Empty;
            p.d = CPerceptronData.IsContains(data, inspecName, "Diameter", out ichara) ? ichara.Measurement.abusolute : string.Empty;

            return p;
        }

        static void OutputHoleDataToCSV(string watchFile)
        {
            var fileFulPath = Path.GetFullPath(watchFile);
            var fileCurrentDirectoryName = Path.GetFileName(Path.GetDirectoryName(fileFulPath));   //対象xmlファイルが存在する(フルパスではない)ディレクトリ名
            var xmlFileName = Path.GetFileName(fileFulPath);

            var data = CPerceptronData.LoadFromFile(fileFulPath, true);

            var c1Y = GetValues(data, inspecName: "CubeHole1");
            var c2Y = GetValues(data, inspecName: "CubeHole2");
            var c3Y = GetValues(data, inspecName: "CubeHole3");
            var f1Y = GetValues(data, inspecName: "FrangeHole1");
            var f2Y = GetValues(data, inspecName: "FrangeHole2");
            var f3Y = GetValues(data, inspecName: "FrangeHole3");
            var n1Y = GetValues(data, inspecName: "HoleNut1");
            var n2Y = GetValues(data, inspecName: "HoleNut2");
            var c1N = GetValues(data, inspecName: "CubeHole1_No");
            var c2N = GetValues(data, inspecName: "CubeHole2_No");
            var c3N = GetValues(data, inspecName: "CubeHole3_No");
            var f1N = GetValues(data, inspecName: "FrangeHole1_No");
            var f2N = GetValues(data, inspecName: "FrangeHole2_No");
            var f3N = GetValues(data, inspecName: "FrangeHole3_No");
            var n1N = GetValues(data, inspecName: "HoleNut1_No");
            var n2N = GetValues(data, inspecName: "HoleNut2_No");

            Console.WriteLine($"{fileCurrentDirectoryName}, {xmlFileName}," +

                              $"{c1Y.x},{c1Y.y},{c1Y.z},{c1Y.i},{c1Y.j},{c1Y.k},{c1Y.d}," +
                              $"{c2Y.x},{c2Y.y},{c2Y.z},{c2Y.i},{c2Y.j},{c2Y.k},{c2Y.d}," +
                              $"{c3Y.x},{c3Y.y},{c3Y.z},{c3Y.i},{c3Y.j},{c3Y.k},{c3Y.d}," +
                              $"{f1Y.x},{f1Y.y},{f1Y.z},{f1Y.i},{f1Y.j},{f1Y.k},{f1Y.d}," +
                              $"{f2Y.x},{f2Y.y},{f2Y.z},{f2Y.i},{f2Y.j},{f2Y.k},{f2Y.d}," +
                              $"{f3Y.x},{f3Y.y},{f3Y.z},{f3Y.i},{f3Y.j},{f3Y.k},{f3Y.d}," +
                              $"{n1Y.x},{n1Y.y},{n1Y.z},{n1Y.i},{n1Y.j},{n1Y.k},{n1Y.d}," +
                              $"{n2Y.x},{n2Y.y},{n2Y.z},{n2Y.i},{n2Y.j},{n2Y.k},{n2Y.d}," +

                              $"{c1N.x},{c1N.y},{c1N.z},{c1N.i},{c1N.j},{c1N.k},{c1N.d}," +
                              $"{c2N.x},{c2N.y},{c2N.z},{c2N.i},{c2N.j},{c2N.k},{c2N.d}," +
                              $"{c3N.x},{c3N.y},{c3N.z},{c3N.i},{c3N.j},{c3N.k},{c3N.d}," +
                              $"{f1N.x},{f1N.y},{f1N.z},{f1N.i},{f1N.j},{f1N.k},{f1N.d}," +
                              $"{f2N.x},{f2N.y},{f2N.z},{f2N.i},{f2N.j},{f2N.k},{f2N.d}," +
                              $"{f3N.x},{f3N.y},{f3N.z},{f3N.i},{f3N.j},{f3N.k},{f3N.d}," +
                              $"{n1N.x},{n1N.y},{n1N.z},{n1N.i},{n1N.j},{n1N.k},{n1N.d}," +
                              $"{n2N.x},{n2N.y},{n2N.z},{n2N.i},{n2N.j},{n2N.k},{n2N.d},"

            );
            Console.ReadLine();
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

                //Trace.WriteLine(TConst + System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            //データをファイルに保存する
            //シリアライズして保存
            public void SaveToFile(string fname)
            {
                //Trace.WriteLine(TConst + System.Reflection.MethodBase.GetCurrentMethod().Name);

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
                if (dInspec.CInspectionCharacteristics.Any(d => d.defaultName == element))
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


        /////////////////////////
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
    }
}