using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Caching;
using AutoAssyModules.Perceptron;

namespace CalcXmlFile
{
    /// <summary> 計測データ(対象フォルダ名・対象ファイル名・注目計測名・項目・absoluteの値)を格納するクラス</summary>
    public class MeasuredValue
    {
        /// <summary> 対象フォルダ名  </summary>
        public string FolderName { set; get; }

        /// <summary> 対象ファイル名  </summary>
        public string FileName { set; get; }

        /// <summary> 注目測定点名  </summary>
        /// <remarks>例:CubeHole1　等 </remarks>
        public string Inspect { set; get; }

        /// <summary> 項目名 </summary>
        /// <remarks>例:X　等 </remarks>
        public string Item { set; get; }

        /// <summary> absolute </summary>
        public double Value { set; get; }

        /// <summary> taegetDir以下のファイルから計測データを抜き出し、MeasuredValue型のリストを返す </summary>
        public List<MeasuredValue> CollectInspectedValues(InspectItem inspects, string targetDir)
        {
            var getTargetNameList = new FileUtil();

            //指定フォルダ以下のフォルダ名を取得する
            var folderNameList = Directory.GetDirectories(targetDir);

            //容器作成
            var answers = new List<MeasuredValue>();

            //フォルダごとに下記の処理を繰り返す
            foreach (var folderName in folderNameList)
            {
                //指定フォルダ以下のファイル名を取得する
                var fileNames = getTargetNameList.GetXmlFiles(folderName);

                //ファイルごとに下記の処理を繰り返しリストの作成
                foreach (var fname in fileNames)
                {
                    //ファイル名の取得
                    var fileName = Path.GetFileName(fname);

                    //パーセプトロンの解析結果を得る
                    var data = CPerceptronData.LoadFromFile(fname, true);

                    //注目測定点名・項目を元にデータを取得し、MeasuredValue型で返す
                    foreach (var element in inspects.Inspects)
                    {
                        foreach (var item in inspects.Items)
                        {
                            CInspectionCharacteristic outInspect;
                            if (CPerceptronData.IsContains(data, element, item, out outInspect))
                            {
                                var absoluteAns = 0d;

                                //指定した測定点・項目を元にabsoluteを返す
                                var itemAbsolute = outInspect.Measurement.abusolute;

                                //リストに値を格納
                                var answer = new MeasuredValue
                                {
                                    FolderName = Path.GetFileName(folderName),
                                    FileName = fileName,
                                    Inspect = element,
                                    Item = item,
                                    Value = double.TryParse(itemAbsolute, out absoluteAns) ? absoluteAns : double.NaN
                                };
                                answers.Add(answer);
                            }
                        }
                    }
                }
            }
            return answers;
        }
    }
}
