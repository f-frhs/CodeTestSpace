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
            //CSVファイルに書き込むときに使うEncoding
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");

            // 出力用のファイルを開く
            using (var sw = new System.IO.StreamWriter(fName, false, enc))
            {
                //
                OutputFieldHeadings(sw);
                //foreach (var VARIABLE in COLLECTION)
                //{
                    
                //}
                OutputXyzData(values, sw);

                //空白行挿入
                //OutputBlankLine(sw);

                //OutputFieldHeadings(sw);
                //OutputIjkData(values, sw);

                //OutputBlankLine(sw);

                //OutputFieldHeadingsForSpecialCalc(spValues, sw);
                //OutputResultOfSpecialCalc(spValues, sw);
            }
        }

        /// <summary> 空白行の挿入 </summary>
        private static void OutputBlankLine(StreamWriter sw)
        {
            sw.WriteLine();
        }

        /// <summary> FiekdHeadings: X,Y,Z,Diaの平均・標準偏差 </summary>
        private static void OutputFieldHeadings(StreamWriter sw)
        {
            sw.WriteLine("フォルダ名,X Avg,X SD,Y Avg,Y SD,Z Avg,Z SD,Dia Avg,Dia SD");
        }

        /// <summary>  </summary>
        private static void OutputXyzData(List<CalcValue> values, StreamWriter sw)
        {
            //変
            //表成形のため空白マス作成
            //sw.Write($",,");

            foreach (var value in values.Where(d => d.Item == "X" || d.Item == "Y" || d.Item == "Z" || d.Item == "Diameter").ToList())
            {
                sw.Write($",{Math.Round(value.MeanValue, 6)}");//,{value.DevValue:E}");
            }
        }

        /// <summary>  </summary>
        private static void OutputIjkData(List<CalcValue> values, StreamWriter sw)
        {
            foreach (var value in values.Where(d => d.Item == "Orientation I" || d.Item == "Orientation J" || d.Item == "Orientation K").ToList())
            {
                sw.WriteLine(
                    $"{value.Inspect},{value.Item},{Math.Round(value.MeanValue, 6)},{value.DevValue:E}");
            }

        }

        /// <summary>  </summary>
        private static void OutputFieldHeadingsForSpecialCalc(List<SpCalcMeanDev> spValues, StreamWriter sw)
        {
            sw.WriteLine($"特殊計算内容,{spValues[0].Operator}");
            sw.WriteLine("計算対象,計算対象,平均,標準偏差");

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


