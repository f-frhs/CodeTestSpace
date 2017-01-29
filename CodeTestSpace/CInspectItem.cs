using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CalcXmlFile
{
    /// <summary> 計測データ（注目測定点名・注目計測名・項目名）を格納するクラス </summary>
    public class InspectItem
    {
        //定数の作成
        /// <summary> リストから読み込とる行数 </summary>
        public static int NumOfLines = 3;

        //public string FolderName { set; get; }

        /// <summary> 注目測定点名  </summary>
        /// <remarks> 例:ST1_SF01　等 </remarks>
        public List<string> InsNames { set; get; }

        /// <summary> 注目計測名 </summary>
        /// <remarks> 例:CubeHole1　等 </remarks>
        public List<string> Inspects { set; get; }

        /// <summary> 項目 </summary>
        /// <remarks> 例:X　等 </remarks>
        public List<string> Items { set; get; }

        /// <summary> fNameからInspectItem型のリストを返す </summary>
        public List<InspectItem> LoadConfiguration(string fName)
        {
            //CSVファイルを読み出し、List<string>に格納
            var inspectionItems = File.ReadAllLines(fName).ToList();

            //リストから空白行("")の削除
            inspectionItems.RemoveAll(d => d == "");

            //それぞれの配列をInspectItem型に格納
            var listInspection = CreateListInspection(inspectionItems);
            return listInspection;
        }


        /// <summary> リストの各配列をそれぞれ inspectItem に格納し、それをList<inspectItem>に格納  </summary>
        public List<InspectItem> CreateListInspection(List<string> configLines)
        {
            //容器作成
            var answers = new List<InspectItem>();

            //configLinesを NumOfLines 行ずつにまとめる
            var configSets = configLines
                .Select((item, index) => new {item, index})
                .GroupBy(elem => (int) (elem.index / NumOfLines), elem => elem.item)
                .ToList();

            //dataListsの各項をそれぞれinspectItemに格納
            //inspectionItemsの値をそれぞれのリストに格納
            foreach (var configSet in configSets)
            {
                var answer = new InspectItem();
                var insNameList = new List<string>();
                var inspectsList = new List<string>();
                var itemsList = new List<string>();

                for (int i = 0; i < NumOfLines; i++)
                {

                    //カンマを区切りにリスト作成
                    var result = configSet.ElementAt(i).Split(',').ToList();

                    //iの値で格納先変更
                    switch (i)
                    {
                        //InsNameListに保存
                        case 0:
                            insNameList.AddRange(result);
                            break;
                        //InspectsListに保存
                        case 1:
                            inspectsList.AddRange(result);
                            break;
                        //ItemsListに保存
                        case 2:
                            itemsList.AddRange(result);
                            break;
                        //スキップ
                        //case 3:
                        //このケースには到達できない。 numOfLines = 3 なので。
                        //    break;
                        default:
                            break;
                    }
                }

                //List<inspectItem>に値を格納
                answer.InsNames = insNameList;
                answer.Inspects = inspectsList;
                answer.Items = itemsList;
                answers.Add(answer);

            }
            return answers;
        }
    }
}
