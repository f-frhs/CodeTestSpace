using System.Collections.Generic;
using System.Linq;

namespace CalcXmlFile
    {
        /// <summary> 計算結果(注目計測名・項目・計算結果(平均・標準偏差))を格納するクラス</summary>
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

            /// <summary> absolute </summary>
            public double Value { set; get; }

            /// <summary> 特殊計算を求めるを求める </summary>
            public static List<CalcValue> SpCalc(CalcSetting settingData, List<MeasuredValue> collectDatas)
            {
                //容器を作成
                var answers = new List<CalcValue>();

                //注目測定点名・項目が同じものを取り出し、それぞれ平均標準偏差を求める
                foreach (var data in collectDatas)
                {
                    foreach (var inspec1 in settingData.Inspec1)
                    {
                        foreach (var inspec2 in settingData.Inspec2)
                        {
                            //容器の作成
                            var answer = new CalcValue();

                            //計算を行うファイルを決定

                            //決定したファイルからsettingDataに対応する対象を取り出す
                            //対象がなかった場合は一気に最後にスルー

                            //計算内容を読み出す（内容によって分岐 if else）

                            //計算実行

                            ////リストから同系のものを取り出す
                            //var dList = collectDatas
                            //    .Where(d => d.Inspect == sInspection)
                            //    .Where(d => d.Item == sItem)
                            //    .Select(d => d.Value)
                            //    .ToList();

                            //リストに格納
                            //測定点名・項目
                            answer.Inspect = sInspection;
                            answer.Item = sItem;

                            //平均
                            answer.MeanValue = MathLibrary.CalcMean(dList);

                            //標準偏差
                            answer.DevValue = MathLibrary.CalvDev(dList);

                            answers.Add(answer);
                        }
                    }
                }
                return answers;
            }
        }
    }
}
}