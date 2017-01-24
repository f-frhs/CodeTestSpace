using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Excel;

namespace CalcXmlFile
{
    /// <summary> 計算結果(対象ファイル・注目計測名・計算内容・計算結果)を格納するクラス</summary>
    public class CalcValueSP
    {
        /// <summary> 対象ファイル名 </summary>
        public string FileName { set; get; }

        /// <summary> 注目測定点名  </summary>
        /// <remarks>例:CubeHole1　等 </remarks>
        public string Inspect1 { set; get; }

        /// <summary> 注目測定点名  </summary>
        /// <remarks>例:CubeHole1　等 </remarks>
        public string Inspect2 { set; get; }

        /// <summary> 計算内容 </summary>
        public  string Operator { set; get; }

        /// <summary> 計算結果 </summary>
        public double Value { set; get; }

        /// <summary> 特殊計算を求めるを求める </summary>
        public static List<CalcValueSP> SpCalc(List<CalcSetting> settingDatas, List<MeasuredValue> collectDatas)
        {
            //容器を作成
            var answers = new List<CalcValueSP>();

            //ファイル毎に特殊計算結果を求める
            foreach (var collectData in collectDatas)
            {
                //計算を行うファイルを取得
                var targetFile = collectData.Fname;

                //特殊計算実行
                //List<CalcSetting>の各要素毎に　計算内容を取得→二つの測定点の項目の値を取得→値を元に特殊計算実行
                foreach (var settingData in settingDatas)
                {
                    //使用する計算内容を取得
                    var targetOperator = settingData.Operator;

                    if (targetOperator == "distance")
                    {
                        //測定点1のデータ収集
                        var fData = DatasForSpCalc(targetFile, settingData.Inspec1, collectDatas);

                        //測定点2のデータ収集         
                        var sData = DatasForSpCalc(targetFile, settingData.Inspec2, collectDatas);

                        //var calcDistance = 
                    }
                    else
                    {
                        Console.WriteLine("Calculated content is not described");
                    }
                }

            }
            return answers;
        }

        /// <summary>  </summary>
        public static double[] DatasForSpCalc(string fname, string inspec, List<MeasuredValue> value)
        {
            //容器作成
            var answer = new List<double>();

            //x,y,z それぞれのデータをとってくる
            var items = new List<string> {"X", "Y", "Z"};

            //x,y,zに関してそれぞれデータを収集
            foreach (var item in items)
            {
                //List<MeasuredValue>から、引数で指示されたデータを収集
                var spData = value
                .Where(d => d.Fname == fname)
                .Where(d => d.Inspect == inspec)
                .Where(d => d.Item == item)
                .Select(d => d.Value)
                .ToList();

                //容器作成(x,y,zの順番で格納)
                answer.Add(spData[0]);
            }

            return answer.ToArray();
        }
    }
}
