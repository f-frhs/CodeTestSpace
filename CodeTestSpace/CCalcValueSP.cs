using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using Microsoft.Office.Interop.Excel;

namespace CalcXmlFile
{
    /// <summary> 計算結果(対象ファイル・注目計測名・計算内容・計算結果)を格納するクラス</summary>
    public class SpCalcValue
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



        //計算内容から実行するメソッドを選択
        public static List<SpCalcValue> SellectSpCalc(List<CalcSetting> settingDatas, List<MeasuredValue> collectDatas)
        {
            var result = new List<SpCalcValue>();
            foreach (var settingData in settingDatas)
            {
                switch (settingData.Operator)
                {
                    case "distance":
                        result = SpCalc(settingDatas, collectDatas);
                        break;
                }
            }
            return result;
        }

        //特殊計算の実行：距離算出
        public static List<SpCalcValue> SpCalc(List<CalcSetting> settingDatas, List<MeasuredValue> collectDatas)
        {
            //容器を作成
            var answers = new List<SpCalcValue>();

            //collectDatasの各要素ごとに距離算出を行い、リストに保存する
            foreach (var settingData in settingDatas)
            {
                //下記収集・計算をファイル毎に行う
                foreach (var fname in collectDatas.Select(d => d.Fname).Distinct())
                {
                    //測定点1のデータ収集
                    var target1 = SpCalcValue.ExtractXyz(settingData.Inspec1, fname, collectDatas);

                    //測定点2のデータ収集  
                    var target2 = SpCalcValue.ExtractXyz(settingData.Inspec2, fname, collectDatas);

                    //距離計算
                    var distance = MathLibrary.CalcDistance(target1, target2);

                    //リストに格納
                    var answer = new SpCalcValue
                    {
                        FileName = fname,
                        Inspect1 = settingData.Inspec1,
                        Inspect2 = settingData.Inspec2,
                        Operator = "distance",
                        Value = distance
                    };
                    answers.Add(answer);
                }
            }
            return answers;
        }

        /// <summary> allDataから対応するデータ(inspecString 及び fname)の抽出 </summary>
        private static double[] ExtractXyz(string inspecString, string fname, List<MeasuredValue> allData )
        {
            //fname及びinspecStringに一致するデータを抽出
            var extractedDataSet = allData
                .Where(d => d.Fname == fname)
                .Where(d => d.Inspect == inspecString)
                .ToList();

            //extractedDataSetから、X,Y,Zのデータを抽出
            var x = extractedDataSet.Where(d => d.Item == "X").Select(d => d.Value).First();
            var y = extractedDataSet.Where(d => d.Item == "Y").Select(d => d.Value).First();
            var z = extractedDataSet.Where(d => d.Item == "Z").Select(d => d.Value).First();
            
            //データの格納
            return new double[] {x, y, z};
        }
    }
}
