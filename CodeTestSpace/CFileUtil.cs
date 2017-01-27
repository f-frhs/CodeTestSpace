using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoAssyModules.Perceptron;

namespace CalcXmlFile
{
    /// <summary> ファイル取り扱いに関するユーティリティークラス </summary>
    public static class FileUtil
    {
        /// <summary> 指定フォルダ以下のファイル名のリストを取得する </summary>
        public static List<string> GetXmlFiles(string basePath)
        {
            //引数で渡されたフォルダ以下の全てのxmlファイルを取得
            var fnames = Directory.GetFiles(basePath, "*.xml", SearchOption.AllDirectories).ToList();
            return fnames;
        }

        /// <summary> 結果をファイルに保存する </summary>
        public static void SaveDatas(string fName, List<CalcValue> values, List<SpCalcMeanDev> spValues)
        {
            var targetXyz = new string[] {"X", "Y", "Z", "Diameter"};
            var targetIjk = new string[] { "Orientation I", "Orientation J", "Orientation K" };


            //CSVファイルに書き込むときに使うEncoding
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");

            // 出力用のファイルを開く
            using (var sw = new System.IO.StreamWriter(fName, false, enc))
            {
                //（X,Y,Z,Dia）フィールドヘッドの書き出し
                OutputFieldHeadingsForXyzDia(sw);

                //（X,Y,Z,Dia）平均と標準偏差の書き出し
                OutputData(values, sw, targetXyz);

                ////空白行挿入
                //OutputBlankLine(sw);

                ////（I,J,K）フィールドヘッド書き出し
                //OutputFieldHeadingsForIjk(sw);

                ////（I,J,K）平均と標準偏差の書き出し
                //OutputData(values, sw, targetIjk);

                ////空白行挿入
                //OutputBlankLine(sw);

                ////特殊計算の表のフィールドヘッド書き出し
                //OutputFieldHeadingsForSpecialCalc(spValues, sw);

                ////特殊計算の平均と標準偏差書き出し
                //OutputResultOfSpecialCalc(spValues, sw);
            }
        }

        /// <summary> 空白行の挿入 </summary>
        private static void OutputBlankLine(StreamWriter sw)
        {
            sw.WriteLine();
        }

        /// <summary> FiekdHeadings書き出し: X,Y,Z,Diaの平均・標準偏差 </summary>
        private static void OutputFieldHeadingsForXyzDia(StreamWriter sw)
        {
            sw.WriteLine("注目計測名,X Avg,X SD,Y Avg,Y SD,Z Avg,Z SD,Dia Avg,Dia SD");
        }

        /// <summary> FiekdHeadings書き出し: I,J,Kの平均・標準偏差 </summary>
        private static void OutputFieldHeadingsForIjk(StreamWriter sw)
        {
            sw.WriteLine("注目計測名,I Avg,I SD,J Avg,J SD,K Avg,K SD");
        }

        /// <summary> FiekdHeadings書き出し: 特殊計算の平均・標準偏差 </summary>
        private static void OutputFieldHeadingsForSpecialCalc(List<SpCalcMeanDev> spValues, StreamWriter sw)
        {
            sw.WriteLine($"特殊計算内容,{spValues[0].Operator}");
            sw.WriteLine("計算対象,計算対象,平均,標準偏差");

        }

        /// <summary> 注目計測名毎に平均と標準偏差を書き出す </summary>
        private static void OutputData(List<CalcValue> values, StreamWriter sw, string[] target)
        {
            //注目計測名をリストから取り出す
            var inspectName = values.Select(d => d.Inspect).Distinct().ToList();


            //注目計測名毎に結果を出力
            foreach (var inspectname in inspectName)
            {
                //容器作成
                var lineToOutput = new List<CalcValue>();

                //注目計測名と項目名をもとにデータを収集
                foreach (var item in target)
                {
                    var value = values
                        .Where(d => d.Inspect == inspectname)
                        .Where(d => d.Item == item)
                        .ToList()
                        .FirstOrDefault();
                    lineToOutput.Add(value);
                }

                //収集したデータを配列に格納
                var valuesString = string.Join(", ", lineToOutput
                    .Select(d => new double[] {d.MeanValue, d.DevValue})
                    .SelectMany(i => i));

                //データの書き出し
                sw.Write(inspectname);
                sw.Write($"{inspectname}, {valuesString}");

                sw.WriteLine();
            }
        }


        /// <summary>  </summary>
        private static void OutputResultOfSpecialCalc(List<SpCalcMeanDev> spValues, StreamWriter sw)
        {
            for (var i = 0; i < 3; i++)
            {
                sw.WriteLine(
                    $"{spValues[i].Inspect1},{spValues[i].Inspect2},{Math.Round(spValues[i].SpMeanValue, 6)},{spValues[i].SpDevValue:E},");
            }
        }
    }
}


