using System;
using System.Collections.Generic;
using System.Web.Caching;
using AutoAssyModules.Perceptron;

namespace CalcXmlFile
{
    /// <summary> 計測データ(注目計測名・項目・absoluteの値)を格納する容器としてのクラス</summary>
    public class MeasuredValue
    {
        /// <summary> 注目測定点名  </summary>
        /// <remarks>例:ST1_SF01　等 </remarks>
        public string InsName { set; get; }

        /// <summary> 項目名 </summary>
        /// <remarks>例:X　等 </remarks>
        public string Inspect { set; get; }

        /// <summary> absolute </summary>
        public double Value { set; get; }

        /// <summary> コレクトしたファイルから、注目計測名と注目測定名とそのabsoluteを返す </summary>
        public static List<MeasuredValue> CollectInspectedValues(InspectItem inspect, string targetDir)
        {
            //指定フォルダ以下のファイルを取得する (フォルダ内のxmlファイルをリストに格納)
            var fnames = FileUtil.GetXmlFiles(targetDir);

            //answersの作成
            var answers = new List<MeasuredValue>();

            //注目測定点名と合致するファイルを更にコレクトする
            foreach (var fname in fnames)
            {
                var data = CPerceptronData.LoadFromFile(fname, true);

                //double nan = Double.NaN;

                foreach (var element in inspect.Inspects)
                {
                    foreach (var item in inspect.Items)
                    {
                        CInspectionCharacteristic outInspect;
                        if (CPerceptronData.IsContains(data, element, item, out outInspect))
                        {
                            //anserを作成
                            var answer = new MeasuredValue();

                            //指定した測定点・項目を元にabsoluteを返す
                            var itemAbsolute = outInspect.Measurement.abusolute;

                            //anserに値を格納
                            double absoluteAns;
                            answer.InsName = element;
                            answer.Inspect = item;
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
