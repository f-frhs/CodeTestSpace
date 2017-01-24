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

            //収集したデータから、各注目測定点名ごとの平均と標準偏差を求める
            var calcMeanDev = CalcValue.CalcMeanDev(insSetting[0], collectData);

            //結果をファイルに保存する
            FileUtil.SaveDatas(saveDataPath, calcMeanDev);

            //Console.ReadLine();
        }

    }
}
    //---以下旧版：削除予定--------

    //計算内容(現在)
    //出力：各試行(Front2mm...ごと)、全試行(各試行の合算)
    //---RB1フランジ、RB2フランジ
    //穴間距離：　CH、FH各試行のAvgとSD
    //穴座標：　CH全試行 x,y,z のAvgとSD、各試行 x,y,z,Dia のAvgとSD
    //          FH各試行 x,y,z,Dia のAvgとSD
    //各穴法線ベクトル：　CH、FH各試行 i,j,k のAvgとSD
    //---RB1フランジ
    //穴間距離：　CH各試行のAvgとSD
    //穴座標：　CH各試行 x,y,z,Dia のAvgとSD
    //各穴法線ベクトル：　CH各試行 i,j,k のAvgとSD
    //---ナットランナー
    //穴間距離：　CH各試行のAvgとSD
    //穴座標：　NH各試行 x,y,z,Dia のAvgとSD
    //          CH各試行 x,y,z,Dia のAvgとSD
    //---マテハン
    //穴間距離：　MH1-2　各試行の　AvgとSD
    //穴座標：　MH　各試行 x,y,z,Dia のAvgとSD
    //各穴法線ベクトル：　MH　各試行 i,j,k のAvgとSD
    //マテハン設置誤差：　x,y,z,Roll,Pitch,Yaw のAvgとSD


//             foreach (var placeName in placeNames)
//         {
//             foreach (var pointName in pointNames)
//             {
//                 names[$"{placeName}_{pointName}"] = new List<double>();
//                 foreach (string file in GetFiles(@"C:\Users\hayashi\Desktop\csvtesrt"))
//                 {
//                     XmlRead(file);
//                     DirectoryInfo dirInfo = Directory.GetParent(file);
// 
//                     foreach (var l in list.FindAll(c => c.Item2 == placeName).FindAll(c => c.Item3 == pointName))
//                     {
//                         names[$"{placeName}_{pointName}"].Add(l.Item4);
//                     };
//                 }
//             }
//         }