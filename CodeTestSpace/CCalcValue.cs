﻿using System.Collections.Generic;
using System.Linq;

namespace CalcXmlFile
{
    /// <summary> 計算結果(注目計測名・項目・計算結果(平均・分散))を格納するクラス</summary>
    public class CalcValue
    {
        /// <summary> 注目測定点名  </summary>
        /// <remarks>例:CubeHole1　等 </remarks>
        public string Inspect { set; get; }

        /// <summary> 項目名 </summary>
        /// <remarks>例:X　等 </remarks>
        public string Item { set; get; }

        /// <summary> absolute </summary>
        public double MeanValue { set; get; }

        /// <summary> absolute </summary>
        public double DevValue { set; get; }

        /// <summary> 平均と分散を求める </summary>
        public static List<CalcValue> CalcMeanDev(InspectItem inspect, List<MeasuredValue> collectDatas)
        {
            //容器を作成
            var answers = new List<CalcValue>();

            //注目測定点名・項目が同じものを取り出し、それぞれ平均分散を求める
            foreach (var sInspection in inspect.Inspects)
            {
                foreach (var sItem in inspect.Items)
                {
                    //容器の作成
                    var answer = new CalcValue();

                    //リストから同系のものを取り出す
                    var dList = collectDatas
                        .Where(d => d.Inspect == sInspection)
                        .Where(d => d.Item == sItem)
                        .Select(d => d.Value)
                        .ToList();

                    //リストに格納
                    //測定点名・項目
                    answer.Inspect = sInspection;
                    answer.Item = sItem;

                    //平均
                    answer.MeanValue = MathLibrary.CalcMean(dList);

                    //分散
                    answer.DevValue = MathLibrary.CalvDev(answer.MeanValue, dList);

                    answers.Add(answer);
                }
            }
            return answers;
        }

    }
}