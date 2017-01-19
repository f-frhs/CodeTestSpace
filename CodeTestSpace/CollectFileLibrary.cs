// collectDataFroXmlFiles.cs

using System.Collections.Generic;
using System.Linq;
using AutoAssyModules.Perceptron;
using static CalcXmlFile.DataHangar;

namespace CalcXmlFile
{
    class CollectFileLibrary
    {
        //注目測定点名と注目計測と項目をファイルから読み込む
        public static List<InspectItem> GetInspectionItems(string fName)
        {
            //CSVから測定点名・注目計測・項目を読み出す
            var inspectionItems = System.IO.File.ReadAllLines(fName);

            //answerとanswersの生成
            var answer = new InspectItem();
            var answers = new List<InspectItem>();

            //それぞれの値を格納するリストの作成
            var InsNameList = new List<string>();
            var InspectsList = new List<string>();
            var ItemsList = new List<string>();

            //リストに格納
            for (int i = 0; i < listLinage; i++)
            {
                //カンマを区切りにリスト作成
                var result = inspectionItems[i].Split(',').ToList();

                //iの値で格納先変更
                switch (i)
                {
                    case 0:
                        InsNameList.AddRange(result);
                        break;
                    case 1:
                        InspectsList.AddRange(result);
                        break;
                    case 2:
                        ItemsList.AddRange(result);
                        break;
                    default:
                        break;
                }
            }

            //anserに値を格納
            answer.InsNames = InsNameList;
            answer.Inspects = InspectsList;
            answer.Items = ItemsList;

            //answerをanswersに追加
            answers.Add(answer);

            return answers;
        }

        //特殊計算内容をファイルから読み込む
        public static List<CalcSetting> GetClcSettings(string fName)
        {
            //CSVから特殊計算を読み込む
            //ファイルから各行取り込み
            var clcSettings = System.IO.File.ReadAllLines(fName);

            //計算内容が記述されている行を指定
            var strOperator = clcSettings[0];
            var sSettings = clcSettings.Skip(1);

            //answersを作成
            var answers = new List<CalcSetting>();

            foreach (var sSetting in sSettings)
            {
                var sp = sSetting.Split(new[] { ',' });
                if (sp.Length < 2) continue;

                var tmpSetting = new CalcSetting();
                tmpSetting.Operator = strOperator;
                tmpSetting.InsName1 = sp[0];
                tmpSetting.InsName2 = sp[1];

                answers.Add(tmpSetting);

            }
            return answers;
        }

        //注目計測名と注目測定名のデータを収集する
        public static List<CalcAnswer> CollectAnswers(InspectItem inspect, string targetDir)
        {
            //指定フォルダ以下のファイルを取得する (フォルダ内のxmlファイルをリストに格納)
            var fnames = GetXmlFiles(targetDir);

            //answersの作成
            var answers = new List<CalcAnswer>();

            //注目測定点名と合致するファイルを更にコレクトする
            foreach (var fname in fnames)
            {
                var data = CPerceptronData.LoadFromFile(fname, true);
                CInspectionCharacteristic outInspect;

                //double nan = Double.NaN;

                foreach (var element in inspect.Inspects)
                {
                    foreach (var item in inspect.Items)
                    {
                        if (CPerceptronData.IsContains(data, element, item, out outInspect))
                        {
                            //anserを作成
                            var answer = new CalcAnswer();

                            //指定した測定点・項目を元にabsoluteを返す
                            var itemAbsolute = outInspect.Measurement.abusolute;

                            //anserに値を格納
                            answer.InsName = element;
                            answer.Inspect = item;
                            answer.Ans = itemAbsolute == null ? double.Parse(itemAbsolute) : double.NaN;

                            answers.Add(answer);
                        }
                    }
                }
            }
            return answers;
        }
    }
}
