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

        /// <summary> 距離計算 </summary>
        public static double CalcDistance(double[] p1, double[] p2)
        {
            //p1とp2の各項目の差を計算
            var dx = p1[0] - p2[0];
            var dy = p1[1] - p2[1];
            var dz = p1[2] - p2[2];

            //距離計算：dx,dy,dzの各二乗の和の平方根
            var answer = Math.Sqrt(dx * dx + dy * dy + dz * dz);

            return answer;
        }
    }
}
