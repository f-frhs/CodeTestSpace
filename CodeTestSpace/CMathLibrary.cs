using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CalcXmlFile
{
    /// <summary> 各種計算メソッドを提供するクラス </summary>
    public static class MathLibrary
    {
        /// <summary> 平均と分散を求める </summary>
        public static List<double[]> CalcMeanDev(InspectItem inspect, List<MeasuredValue> collectDatas)
        {
            //容器を作成
            var insArray = new double[2];
            var insList  = new List<double[]>();

            //注目測定点名・項目が同じものを取り出し、それぞれ平均分散を求める
            foreach (var sInspection in inspect.Inspects)
            {
                foreach (var sItem in inspect.Items)
                {
                    //リストから同系のものを取り出す
                    var dList = collectDatas
                        .Where(d => d.Inspect == sInspection)
                        .Where(d => d.Item == sItem)
                        .Select(d => d.Value)
                        .ToList();

                    //平均
                    insArray[0] = CalcMean(dList);

                    //分散
                    insArray[1] = CalvDev(CalcMean(dList), dList);

                    //リストに格納
                    insList.Add(insArray);
                }
            }
            return  insList;
        }

        //平均の式
        public static double CalcMean(List<double> valuses)
        {
            //平均の計算
            double mean = valuses.Average();
            return mean;
        }

        //分散の式
        public static double CalvDev(double meanData, List<double> valuses)
        {
            //平均との差の2乗を加算する
            var difSqur = new double();
            foreach (var value in valuses)
            {
                difSqur += (value - meanData) * (value - meanData);
            }

            //標準偏差の導出
            var sd = Math.Sqrt(difSqur / (valuses.Count - 1));

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
