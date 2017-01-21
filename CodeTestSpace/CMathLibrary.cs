using System;
using System.Collections.Generic;
using System.Linq;

namespace CalcXmlFile
{
    /// <summary> 各種計算メソッドを提供するクラス </summary>
    public static class MathLibrary
    {
        /// <summary> 平均と分散を求める </summary>
        public static double[] CalcMeanDev(List<MeasuredValue> collectDatas)
        {
            //目的のデータをフィルタリング
            var valsList = collectDatas
                .FindAll(d => d.InsName == "CubeHole2" && d.Inspect == "X")
                .Select(d => d.Value)
                .ToList();

            //平均の式
            var ave = valsList.Average();

            //分散の式
            var sd = CalcDeviation(valsList);

            return new double[] {ave, sd};
        }

        //リスト dsWithNan の標準偏差を計算する
        //要素にNaNがある場合は、除去してから計算を開始する。
        private static double CalcDeviation(List<double> dsWithNaN)
        {
            //NaNが含まれているときは除去する
            var nanCount = dsWithNaN.FindAll(double.IsNaN).Count;
            var ds = dsWithNaN.Where(d => !double.IsNaN(d));
            var n = ds.Count();
            if (dsWithNaN.Contains(double.NaN))
            {
                Console.WriteLine(
                    $"dsWithNaN has {nanCount} double.NaN. After removing, the remaining list has {n} elements.");
            }

            //計算で使うデータ数の確認。データ数は1よりも多い必要がある。
            if (n <= 1) throw new ArgumentException("Not enough data.");

            var m = dsWithNaN.Average();
            var sumSqared = ds.Sum(d => (d - m) * (d - m));
            var sd = Math.Sqrt(sumSqared) / (n - 1);

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
