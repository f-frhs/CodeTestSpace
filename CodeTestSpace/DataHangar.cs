// collectDataFroXmlFiles.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Office.Interop.Excel;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;
using AutoAssyModules.Perceptron;
using System.Text;

namespace CalcXmlFile
{
    class DataHangar
    {
        public class InspectItem
        {
            //CSV内容を格納：注目測定点名・注目計測名・項目
            //注目測定点名 ST1_SF01など
            public List<string> InsNames { get; set; }

            //注目計測名 CubeHole1など
            public List<string> Inspects { get; set; }

            //項目 Xなど
            public List<string> Items { get; set; }
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
    }
}