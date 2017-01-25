using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CalcXmlFile
{
    /// <summary> ファイル取り扱いに関するユーティリティークラス </summary>
    public static class FileUtil
    {
        /// <summary> 結果をファイルに保存する </summary>
        public static void SaveDatas(string fName, List<CalcValue> values, List<SpCalcMeanDev> spValues)
        {
            //CSVファイルに書き込むときに使うEncoding
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");

            // 出力用のファイルを開く
            using (var sw = new System.IO.StreamWriter(fName, false, enc))
            {
                sw.WriteLine("注目測定点,項目,平均,標準偏差");
                foreach (var value in values)
                {
                    sw.WriteLine(
                        $"{value.Inspect},{value.Item},{Math.Round(value.MeanValue,6)},{value.DevValue:E}");
                }

                sw.WriteLine();

                sw.WriteLine($"特殊計算内容,{spValues[0].Operator}");

                sw.WriteLine("計算対象,計算対象,平均,標準偏差");
                for (var i = 0; i < 3; i++)
                {
                    sw.WriteLine($"{spValues[i].Inspect1},{spValues[i].Inspect2},{Math.Round(spValues[i].SpMeanValue,6)},{spValues[i].SpDevValue:E},");
                }
            }
        }

        /// <summary> 指定フォルダ以下のファイル名のリストを取得する </summary>
        public static List<string> GetXmlFiles(string basePath)
        {
            //引数で渡されたフォルダ以下の全てのxmlファイルを取得
            var fnames = Directory.GetFiles(basePath, "*.xml", SearchOption.AllDirectories).ToList();
            return fnames;
        }
    }
}


