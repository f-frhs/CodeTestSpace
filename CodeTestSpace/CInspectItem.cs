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

        /// <summary> 注目測定点名  </summary>
        /// <remarks> 例:ST1_SF01　等 </remarks>
        public List<string> InsNames { get; set; }

        /// <summary> 注目計測名 </summary>
        /// <remarks> 例:CubeHole1　等 </remarks> 
        public List<string> Inspects { get; set; }

        /// <summary> 項目 </summary> 
        /// <remarks> 例:X　等 </remarks>
        public List<string> Items { get; set; }

        /// <summary> fNameからInspectItem型のリストを返す </summary>
        public List<InspectItem> LoadConfiguration(string fName)
        {
            //CSVファイルを読み出し、配列に格納
            var inspectionItems = File.ReadAllLines(fName);

            //配列を3行(測定点名・注目計測・項目)毎に、リストに格納
            var itemList = CreateListFromArray(inspectionItems);

            //それぞれの配列をInspectItem型に格納
            var listInspection = CreateListInspection(itemList);
            return listInspection;
        }

        /// <summary> arrayDataを先頭から順番に３つずつそれぞれのリストに格納 </summary>
        public List<string[]> CreateListFromArray(string[] arrayData)
        {
            //容器作成
            var listOfItems = new List<string[]>();

            //先頭から順に3つずつList<string[]>に格納
            for (var i = 0; i < arrayData.Length / 3; i++)
            {
                var cluster = new string[] {arrayData[i * 3], arrayData[1 + i * 3], arrayData[2 + i * 3]};
                listOfItems.Add(cluster);
            }

            return listOfItems;
        }

        /// <summary> リストの各配列をそれぞれ inspectItem に格納し、それをList<inspectItem>に格納  </summary>
        public List<InspectItem> CreateListInspection(List<string[]> dataLists)
        {
            //容器作成
            var answers = new List<InspectItem>();

            //dataListsの各項をそれぞれinspectItemに格納
            foreach (var dataList in dataLists)
            {
                //容器の作成
                var answer = new InspectItem();
                var insNameList = new List<string>();
                var inspectsList = new List<string>();
                var itemsList = new List<string>();

                //inspectionItemsの値をそれぞれのリストに格納
                for (var i = 0; i < NumOfLines; i++)
                {
                    //カンマを区切りにリスト作成
                    var result = dataList[i].Split(',').ToList();

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
