using System.Collections.Generic;
using System.Linq;

namespace CalcXmlFile
{
    /// <summary> 計算結果(注目計測名・項目・平均・標準偏差)を格納するクラス</summary>
    public class CalcValue
    {
        /// <summary> 対象フォルダ名  </summary>
        public string FolderName { set; get; }

        /// <summary> 注目測定点名  </summary>
        /// <remarks>例:CubeHole1　等 </remarks>
        public string Inspect { set; get; }

        /// <summary> 項目名 </summary>
        /// <remarks>例:X　等 </remarks>
        public string Item { set; get; }

        /// <summary> 平均 </summary>
        public double MeanValue { set; get; }

        /// <summary> 標準偏差 </summary>
        public double DevValue { set; get; }

        /// <summary> 平均と標準偏差を求める </summary>
        public List<CalcValue> CalcMeanDev(InspectItem inspects, List<MeasuredValue> collectDatas)
        {
            //容器を作成
            var answers = new List<CalcValue>();
            var folderNameList = new List<string>();

            //フォルダごとに下記の処理を繰り返す
            foreach (var collectData in collectDatas)
            {
                var names = collectData.FolderName;
                folderNameList.Add(names);
            }

            var folderNames = folderNameList.Distinct();

            foreach (var folderName in folderNames)
            {
                //注目測定点名・項目が同じものを取り出し、それぞれ平均・標準偏差を求める
                foreach (var sInspection in inspects.Inspects)
                {
                    foreach (var sItem in inspects.Items)
                    {
                        //容器の作成
                        var answer = new CalcValue();

                        //リストから同系のものを取り出す
                        var dList = collectDatas
                            .Where(d => d.FolderName == folderName)
                            .Where(d => d.Inspect == sInspection)
                            .Where(d => d.Item == sItem)
                            .Select(d => d.Value)
                            .ToList();

                        //リストに格納
                        //測定点名・項目
                        answer.Inspect = sInspection;
                        answer.Item = sItem;
                        answer.FolderName = folderName;

                        var mathLibrary = new MathLibrary();

                        //平均
                        answer.MeanValue = mathLibrary.CalcMean(dList);

                        //標準偏差
                        answer.DevValue = mathLibrary.CalvDev(dList);

                        answers.Add(answer);
                    }
                }
            }
            return answers;
        }
    }
}