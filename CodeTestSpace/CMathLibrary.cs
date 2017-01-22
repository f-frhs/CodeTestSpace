using System;
using System.Collections.Generic;
using System.Linq;

namespace CalcXmlFile
{
    /// <summary> 各種計算メソッドを提供するクラス </summary>
    public static class MathLibrary
    {
        /// <summary> 平均と分散を求める </summary>
        public static double[] CalcMeanDev(List<double> valsList)
        {
            //平均の式
            var ave = valsList.Average();

            //分散の式
            var sd = CalcSD(valsList);

            return new double[] {ave, sd};
        }

        //リスト ds の標準偏差を計算する
        //要素にNaNがある場合は、除去してから計算を開始する。
        private static double CalcSD(List<double> ds)
        {
            //NaNが含まれているときは除去する

            var nanCount = ds.RemoveAll(double.IsNaN);
            if (0 < nanCount)
            {
                Console.WriteLine(
                    $"ds had {nanCount} double.NaN. Removed NaN-s.");
            }

            //計算で使うデータ数の確認。データ数は1よりも多い必要がある。
            var n = ds.Count;
            if (n <= 1) throw new ArgumentException("Number of data must be greater than 1 to calc its deviation.");

            //計算部分
            var m = ds.Average();
            var sumSqared = ds.Sum(d => (d - m) * (d - m));
            var sd = Math.Sqrt(sumSqared) / (n - 1);

            return sd;
        }

        //２点(p1, p2)間のユークリッド距離を計算する
        // p1 = [x1, y1, z1]
        // p2 = [x2, y2, z2]
        //座標の値に１つでもNaNがあれば、NaNを返す
        public static double CalcDistance(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            if (new List<double> {x1, y1, z1, x2, y2, z2}.Contains(double.NaN))
            {
                return double.NaN;
            }

            var dx = x1 - x2;
            var dy = y1 - y2;
            var dz = z1 - z2;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
    }
}



//    //引数の各項の差の二乗を返す
//    //ToDo結果が望むものと違う修正：CubeHole1_X", "CubeHole2_Xのみの予定
//    private static double[] CalcEachMinusSquare(string a, string b)
//    {
//        double[] results  = names[a].ToArray().Zip(names[b].ToArray(), (x, y) => (x - y) * (x - y)).ToArray();
//        return results;
//    }

//    //引数の総和の平方根を返す
//    private static double CalcSquare(double x, double y, double z)
//    {
//        double length3D = Math.Sqrt(x * x + y * y + z * z );
//        return length3D;
//    }

//    // List<doubel>を受取り、その平均と標準偏差計算を返す
//    private static dynamic CalcSD(double[] x, double[] y, double[] z)
//    {
//        //距離： x y z の和の平方根
//        double[] length3D = (((x.Zip(y, (a, b) => (a + b)).ToArray()).Zip(z, (a, b) => (a + b))).Select(i => Math.Sqrt(i)).ToArray());

//        //距離： 平均
//        Double l_Avg = length3D.Average();

//        //σの二乗×データ数
//        Double l_calcSD = 0;
//        foreach (Double data in length3D)
//        {
//            l_calcSD += (data - l_Avg) * (data - l_Avg);
//        }

//        //σを算出して返却
//        var l_SD = Math.Sqrt(l_calcSD / (length3D.Length - 1));

//        return new
//        {
//            avg = l_Avg,
//            sd = l_SD
//        };
//    }
