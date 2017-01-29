using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using AutoAssyModules.Perceptron;

namespace CalcXmlFile
{
    /// <summary> ファイル取り扱いに関するユーティリティークラス </summary>
    public class FileUtil
    {
        /// <summary> 指定フォルダ以下のファイル名のリストを取得 </summary>
        public List<string> GetXmlFiles(string basePath)
        {
            //引数で渡されたフォルダ以下の全てのxmlファイルを取得
            var fnames = Directory.GetFiles(basePath, "*.xml", SearchOption.AllDirectories).ToList();
            return fnames;
        }

        /// <summary> 結果をファイルに書き出し保存 </summary>
        public void SaveDatas(string fName, List<CalcValue> values, List<SpCalcValue> spValues)
        {
            //保存・書き出しをする対象を決定
            var targetXyz = new string[] {"X", "Y", "Z", "Diameter"};
            var targetIjk = new string[] { "Orientation I", "Orientation J", "Orientation K" };
            var targetInspect = new string[] {"CubeHole1", "CubeHole2", "CubeHole3"};

            //CSVファイルに書き込むときに使うEncoding
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");

            // 出力用のファイルを開く
            // outputフォルダの有無：なければフォルダ追加
            // ここは、ファイルの有無をチェックしている。フォルダ有無のチェックを期待していた。
            if (!File.Exists(fName))
            {
                //TODO: 変数化
                Directory.CreateDirectory(@"C:\Users\hayashi\Documents\Visual Studio 2015\Projects\CodeTestSpace\output");
            }

            using (var sw = new StreamWriter(fName, false, enc))
            {
                //（X,Y,Z,Dia）用の見出し行を出力
                OutputFieldHeadingsForXyzDia(sw);

                //（X,Y,Z,Dia）の平均と標準偏差の書き出す
                // TODO: メソッド名で、「平均と標準偏差」を書き出すと分かるように
                // TODO: values の変数名をリネーム。何の値か分かるように。
                OutputResultOfCalc(values, sw, targetXyz);

                //空白行挿入
                OutputBlankLine(sw);

                //（I,J,K）用の見出し行を出力
                OutputFieldHeadingsForIjk(sw);

                //（I,J,K）平均と標準偏差の書き出し
                // TODO: メソッド名で、「平均と標準偏差」を書き出すと分かるように
                // TODO: values の変数名をリネーム。何の値か分かるように。
                OutputResultOfCalc(values, sw, targetIjk);

                //空白行挿入
                OutputBlankLine(sw);

                //特殊計算の表の見出し行を出力
                OutputFieldHeadingsForSpecialCalc(spValues, sw);

                //特殊計算の平均と標準偏差書き出し
                // TODO: メソッド名で、「平均と標準偏差」を書き出すと分かるように
                // TODO: spvalues の変数名をリネーム。何の値か分かるように。
                OutputResultOfSpecialCalc(spValues, sw);
            }
        }

        /// <summary> 空白行を出力する </summary>
        private void OutputBlankLine(StreamWriter sw)
        {
            sw.WriteLine();
        }

        //TODO: summary を書き直す
        //以下同様に。
        /// <summary> FiekdHeadings書き出し: X,Y,Z,Diaの平均・標準偏差 </summary>
        private void OutputFieldHeadingsForXyzDia(StreamWriter sw)
        {
            sw.WriteLine("注目計測名,フォルダ名,X Avg,X SD,Y Avg,Y SD,Z Avg,Z SD,Dia Avg,Dia SD");
        }

        /// <summary> FiekdHeadings書き出し: 計測名毎のX,Y,Z,Diaの平均・標準偏差 </summary>
        private void OutputFieldHeadingsForAllDatas(StreamWriter sw)
        {
            sw.WriteLine("注目計測名,X Avg,X SD,Y Avg,Y SD,Z Avg,Z SD");
        }

        /// <summary> FiekdHeadings書き出し: I,J,Kの平均・標準偏差 </summary>
        private void OutputFieldHeadingsForIjk(StreamWriter sw)
        {
            sw.WriteLine("注目計測名,フォルダ名,I Avg,I SD,J Avg,J SD,K Avg,K SD");
        }

        /// <summary> FiekdHeadings書き出し: 特殊計算の平均・標準偏差 </summary>
        private void OutputFieldHeadingsForSpecialCalc(List<SpCalcValue> spValues, StreamWriter sw)
        {
            sw.WriteLine($"特殊計算内容,{spValues[0].Operator}");
            sw.WriteLine("計算対象,フォルダ名,計算対象,平均,標準偏差");
        }

        /// <summary> 注目計測名毎に平均と標準偏差を書き出す </summary>
        private void OutputResultOfCalc(List<CalcValue> values, StreamWriter sw, string[] target)
        {
            //values で指定した注目計測名に対応するデータをリストから取り出す
            var inspectName = values.Select(d => d.Inspect).Distinct().ToList();

            //リストからフォルダ名取得→重複を削除リスト化
            var folderNames = values.Select(d => d.FolderName).Distinct().ToList();

            //注目計測名等データがなかった場合の空容器
            //メソッドの度に nanList を生成する必要は無い?
            var nanList = new CalcValue();
            {
                nanList.Inspect = "NaN";
                nanList.Item = "NaN";
                nanList.MeanValue = double.NaN;
                nanList.DevValue = double.NaN;
            }

            //注目計測名毎に結果を出力
            foreach (var inspectname in inspectName)
            {
                //フォルダ名毎に下記処理を行う
                foreach (var folderName in folderNames)
                {
                    //容器作成
                    var lineToOutput = new List<CalcValue>();

                    //注目計測名と項目名をもとにデータを収集
                    foreach (var item in target)
                    {
                        var value = values
                            .Where(d => d.FolderName == folderName)
                            .Where(d => d.Inspect == inspectname)
                            .Where(d => d.Item == item)
                            .ToList()
                            .FirstOrDefault();

                        lineToOutput.Add(value ?? nanList);
                    }

                    //平均を有効数字6桁、標準偏差を精度指定子6
                    var valuesString = string.Join(", ", lineToOutput
                        .Select(d => new string[] {d.MeanValue.ToString("F6"), d.DevValue.ToString("E6")})
                        .SelectMany(i => i));

                    //データの書き出し
                    sw.Write($"{inspectname},{folderName}, {valuesString}");
                    sw.WriteLine();
                }
            }
        }

        /// <summary> 注目計測名毎に特殊計算値の平均と標準偏差を書き出す </summary>
        private void OutputResultOfSpecialCalc(List<SpCalcValue> spValues, StreamWriter sw)
        {
            //リストからフォルダ名取得→重複を削除リスト化
            var folderNames = spValues.Select(d => d.FolderName).Distinct().ToList();

            //フォルダ毎に下記作業を行う
            foreach (var folderName in folderNames)
            {
                for (var i = 0; i < 3; i++)
                {
                    //平均を有効数字6桁、標準偏差を精度指定子6
                    sw.WriteLine(
                        $"{spValues[i].Inspect1},{spValues[i].Inspect2},{folderName},{spValues[i].SpMeanValue:F6}, {spValues[i].SpDevValue:E6},");
                }
            }
        }
    }
}


