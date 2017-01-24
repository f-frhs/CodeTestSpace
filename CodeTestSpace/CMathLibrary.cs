using System;
using System.Collections.Generic;
using System.Linq;


namespace CalcXmlFile
{
    /// <summary> 各種計算メソッドを提供するクラス </summary>
    public static class MathLibrary
    {        
        //平均の式
        public static double CalcMean(List<double> values)
        {
            //平均の計算
            var mean = values.Average();
            return mean;
        }

        //標準偏差
        public static double CalvDev(List<double> values)
        {
            //平均の計算
            var mean = values.Average();

            //平均との差の2乗を加算する
            var sumSqur = 0.0d;
            foreach (var value in values)
            {
                sumSqur += (value - mean) * (value - mean);
            }

            //標準偏差を計算
            var sd = Math.Sqrt(sumSqur / (values.Count - 1));

            return sd;
        }

        /// <summary> 特殊計算を求める </summary>
        public static double CalcFunction(CalcSetting calSetting)
        {
            //GetClcSettingsで選択された計算を用いて結果を返す


            throw new NotImplementedException();
        }
    }
}
