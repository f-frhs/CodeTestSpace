using System.Collections.Generic;

namespace CalcXmlFile
{
    public class CalcAllMeanData
    {
        /// <summary> 注目測定点名  </summary>
        /// <remarks>例:CubeHole1　等 </remarks>
        public string Inspect { set; get; }

        /// <summary> 項目名 </summary>
        /// <remarks>例:X　等 </remarks>
        public string Item { set; get; }

        /// <summary> 平均 </summary>
        public double MeanValue { set; get; }

        /// <summary> 標準偏差 </summary>
        public double DevValue { set; get; }

        ///平均の平均を求める
        public List<CalcAllMeanData> CalcAllMeanDatas(List<CalcValue> meanDevDatas)
        {
            var answers = new List<CalcAllMeanData>();


            return answers;
        }
    }
}