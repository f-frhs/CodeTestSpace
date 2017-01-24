using System.Collections.Generic;

namespace CalcXmlFile
{
    /// <summary> 特殊計算の計算結果(対象ファイル・注目計測名・計算内容・平均・標準偏差)を格納するクラス</summary>
    public class SpCalcMeanDev
    {
        /// <summary> 対象ファイル名 </summary>
        public string FileName { set; get; }

        /// <summary> 注目測定点名  </summary>
        /// <remarks>例:CubeHole1　等 </remarks>
        public string Inspect1 { set; get; }

        /// <summary> 注目測定点名  </summary>
        /// <remarks>例:CubeHole1　等 </remarks>
        public string Inspect2 { set; get; }

        /// <summary> 平均 </summary>
        public string SpMeanValue { set; get; }

        /// <summary> 標準偏差 </summary>
        public double SpDevValue { set; get; }

        public static List<SpCalcMeanDev> CalcMeanDev(List<SpCalcValue> collectDatas)
        {
            //容器を作成
            var answers = new List<SpCalcValue>();

            

            return new List<SpCalcMeanDev>();
        }

    }
}