using System;
using System.Collections.Generic;
using System.Linq;

namespace CalcXmlFile
{
    /// <summary> Main関数 </summary>
    public class Program
    {
        private static void Main(string[] args)
        {
            //注目測定点名と注目計測名と項目が書かれたファイルのアドレス
            var csvFilePath = @"C:\Users\hayashi\Documents\Visual Studio 2015\Projects\CodeTestSpace\insepectionData\settingData.CSV";

            //特殊計算と対象が書かれたファイルのアドレス
            var csvCalcPath = @"C:\Users\hayashi\Documents\Visual Studio 2015\Projects\CodeTestSpace\insepectionData\calcSettingData.CSV";

            //処理対象のフォルダのアドレス
            var basePath = @"C:\Users\hayashi\Documents\Visual Studio 2015\Projects\CodeTestSpace\testdata\";

            //結果ファイル保存先のアドレス
            var saveDataPath = @"C:\Users\hayashi\Documents\Visual Studio 2015\Projects\CodeTestSpace\output\result.csv";

            //注目測定点名と注目計測名と項目をファイルから読み込む
            //例
            //ST1_SF01　（注目測定点名）
            //CubeHole1,CubeHole2 (注目計測名)
            //X Y ...等（項目）
            //注目計測名、項目は可変数
            var insSetting = InspectItem.LoadConfiguration(csvFilePath);

            //特殊計算内容をファイルから読み込む
            //例：
            //distance,CubeHole1,CubeHole2
            //特殊計算内容は可変とする
            var calcSetting = CalcSetting.LoadConfiguration(csvCalcPath);

            //指定フォルダ以下のファイルを取得する
            //注目測定点名と合致するファイルを更にコレクトする
            //コレクトしたファイルから、注目計測名と注目測定名のデータを収集する
            var collectData = MeasuredValue.CollectInspectedValues(insSetting[0], basePath);

            //各注目測定点名ごとの平均と標準偏差を求める
            var calcMeanDev = CalcValue.CalcMeanDev(insSetting[0], collectData);

            //特殊計算を求める
            var spCalc = SpCalcValue.SellectSpCalc(calcSetting, collectData);


            //-------------制作中---------------
            //特殊計算の平均と標準偏差を求める
            var spCalcMeanDev = SpCalcMeanDev.CalcMeanDev(spCalc);
            //----------------------------------

            //結果をファイルに保存する
            FileUtil.SaveDatas(saveDataPath, calcMeanDev);

        }

    }
}

