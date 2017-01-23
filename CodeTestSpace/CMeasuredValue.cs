using System;
using System.Collections.Generic;
using System.Web.Caching;
using AutoAssyModules.Perceptron;

namespace CalcXmlFile
{
    /// <summary> 計測データ(注目計測名・項目・absoluteの値)を格納するクラス</summary>
    public class MeasuredValue
    {
        /// <summary> 対象ファイル名  </summary>

        /// <summary> 注目測定点名  </summary>
        /// <remarks>例:CubeHole1　等 </remarks>
        public string Inspect { set; get; }

        /// <summary> 項目名 </summary>
        /// <remarks>例:X　等 </remarks>
        public string Item { set; get; }

        /// <summary> absolute </summary>
        public double Value { set; get; }

        /// <summary> taegetDir以下のファイルから計測データを抜き出し、MeasuredValue型のリストを返す </summary>
        public static List<MeasuredValue> CollectInspectedValues(InspectItem inspect, string targetDir)
        {
            //指定フォルダ以下のファイルを取得する (フォルダ内のxmlファイルをリストに格納)
            var fnames = FileUtil.GetXmlFiles(targetDir);

            //answersの作成
            var answers = new List<MeasuredValue>();

            //fnameの各ファイルごとに{}内の処理を繰り返しリストの作成
            foreach (var fname in fnames)
            {
                //パーセプトロンの解析結果を得る
                var data = CPerceptronData.LoadFromFile(fname, true);

                //注目測定点名・項目を元にデータを取得し、MeasuredValue型で返す
                foreach (var element in inspect.Inspects)
                {
                    foreach (var item in inspect.Items)
                    {
                        CInspectionCharacteristic outInspect;
                        if (CPerceptronData.IsContains(data, element, item, out outInspect))
                        {
                            //容器(anser)を作成
                            var answer = new MeasuredValue();
                            var absoluteAns = 0d;

                            //指定した測定点・項目を元にabsoluteを返す
                            var itemAbsolute = outInspect.Measurement.abusolute;

                            //リストに値を格納
                            answer.Inspect = element;
                            answer.Item = item;
                            answer.Value = double.TryParse(itemAbsolute, out absoluteAns) ? absoluteAns : double.NaN;
                            answers.Add(answer);
                        }
                    }
                }
            }
            return answers;
        }

    }
}
