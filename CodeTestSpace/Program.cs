using System;
using System.Linq;

namespace CalcXmlFile
{
    /// <summary> Main関数 </summary>
    public class Program
    {
        private static void Main(string[] args)
        {
            //ディレクトリの設定
            //まずは、ソリューションファイルがあるディレクトリのパス
            var solutionDir = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"..\..\..\";

            //注目測定点名と注目計測名と項目が書かれたファイルのアドレス
            var csvFilePath = solutionDir + @"insepectionData\settingData.CSV";

            //特殊計算と対象が書かれたファイルのアドレス
            var csvCalcPath = solutionDir + @"insepectionData\calcSettingData.CSV";

            //処理対象のフォルダのアドレス
            var basePath = solutionDir + @"testdata\";

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


            //-----------
            //収集したデータのリストから、計算したい項目の数値をリストとして抽出する
            var valsList = collectData
                .Where(d => d.Inspect == "CubeHole2" && d.Item == "X")
                .Select(d => d.Value)
                .ToList();

            //収集したデータから、各注目測定点名ごとの平均と分散を求める
            var result = MathLibrary.CalcMeanDev(valsList);
            //-----------

            //距離を計算する
            var result2 = MathLibrary.CalcFunction(calcSetting[0], collectData);


            //結果をファイルに保存する

            Console.ReadLine();
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
