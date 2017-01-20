﻿// collectDataFroXmlFiles.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Office.Interop.Excel;
using System.Linq;
using AutoAssyModules.Perceptron;
using CalcXmlFile;
using System.Text;

namespace CalcXmlFile
{
    public static class MathLibrary
    {
        //平均と分散を求める
        public static double[] CalcMeanDev(List<CalcAnswer> collectDatas)
        {
            //平均の式

            //分散の式

            throw new NotImplementedException();
        }

        //特殊計算を実施する
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
