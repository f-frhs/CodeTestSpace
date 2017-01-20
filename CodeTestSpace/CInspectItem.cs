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
        public static List<InspectItem> LoadConfiguration(string fName)
        {
            //CSVファイルから測定点名・注目計測・項目を読み出し、配列に格納
            var inspectionItems = File.ReadAllLines(fName);

            //容器の生成
            var answer = new InspectItem();
            var answers = new List<InspectItem>();
            var InsNameList = new List<string>();
            var InspectsList = new List<string>();
            var ItemsList = new List<string>();

            //inspectionItemsの値をそれぞれのリストに格納
            for (var i = 0; i < NumOfLines; i++)
            {
                //カンマを区切りにリスト作成
                var result = inspectionItems[i].Split(',').ToList();

                //iの値で格納先変更
                switch (i)
                {
                    //InsNameListに保存
                    case 0:
                        InsNameList.AddRange(result);
                        break;
                    //InspectsListに保存
                    case 1:
                        InspectsList.AddRange(result);
                        break;
                    //ItemsListに保存
                    case 2:
                        ItemsList.AddRange(result);
                        break;
                    default:
                        break;
                }
            }

            //リストに値を格納
            answer.InsNames = InsNameList;
            answer.Inspects = InspectsList;
            answer.Items = ItemsList;
            answers.Add(answer);

            return answers;
        }
    }
}