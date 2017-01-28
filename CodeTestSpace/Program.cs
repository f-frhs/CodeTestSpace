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

            //結果保持用
            var resultCalcMeanDev = new List<List<CalcValue>>();
            var resultSpCalcMeanDev = new List< List<SpCalcMeanDev>>();

            //注目測定点名と注目計測名と項目をファイルから読み込む
            //例
            //ST1_SF01　（注目測定点名）
            //CubeHole1,CubeHole2 (注目計測名)
            //X Y ...等（項目）
            //注目計測名、項目は可変数
            var institem = new InspectItem();
            var inspectItems = institem.LoadConfiguration(csvFilePath);

            //特殊計算内容をファイルから読み込む
            //例：
            //distance,CubeHole1,CubeHole2
            //特殊計算内容は可変とする
            var instSetting = new CalcSetting();
            var calcSetting = instSetting.LoadConfiguration(csvCalcPath);

            foreach (var inspectItem in inspectItems)
            {
                //指定フォルダ以下のファイルを取得する
                //注目測定点名と合致するファイルを更にコレクトする
                //コレクトしたファイルから、注目計測名と注目測定名のデータを収集する
                var instData = new MeasuredValue();
                var measuredValues = instData.CollectInspectedValues(inspectItem, basePath);

                //各注目測定点名ごとの平均と標準偏差を求める
                var instDataMeanDev = new CalcValue();
                var calcMeanDev = instDataMeanDev.CalcMeanDev(inspectItem, measuredValues);
                resultCalcMeanDev.Add(calcMeanDev);

                //特殊計算を求める
                var instSpCalc = new SpCalcValue();
                var spCalc = instSpCalc.SellectSpCalc(calcSetting, measuredValues);

                //特殊計算の平均と標準偏差を求める
                var instDataSpCalcMeanDev = new SpCalcMeanDev();
                var spCalcMeanDev = instDataSpCalcMeanDev.CalcMeanDev(calcSetting, spCalc);
                resultSpCalcMeanDev.Add(spCalcMeanDev);

                //結果をファイルに保存する
                var saveDatas = new FileUtil();
                saveDatas.SaveDatas(saveDataPath, calcMeanDev, spCalcMeanDev);
            }



        }

    }
}

