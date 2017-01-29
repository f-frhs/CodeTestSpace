using System.Collections.Generic;
using System.Linq;

namespace CalcXmlFile
{
    /// <summary> 特殊計算の計算結果(注目計測名1,2・計算内容・平均・標準偏差)を格納するクラス</summary>
    public class SpCalcValue
    {
        /// <summary> 対象フォルダ名  </summary>
        public string FolderName { set; get; }

        /// <summary> 注目測定点名  </summary>
        /// <remarks>例:CubeHole1　等 </remarks>
        public string Inspect1 { set; get; }

        /// <summary> 注目測定点名  </summary>
        /// <remarks>例:CubeHole1　等 </remarks>
        public string Inspect2 { set; get; }

        /// <summary> 計算内容 </summary>
        public string Operator { set; get; }

        /// <summary> 平均 </summary>
        public double SpMeanValue { set; get; }

        /// <summary> 標準偏差 </summary>
        public double SpDevValue { set; get; }

        /// <summary> 特殊計算の平均と標準偏差を求める </summary>
        public List<SpCalcValue> CalcMeanDev(List<CalcSetting> settingDatas, List<SpMeasuredValue> collectDatas)
        {
            //容器を作成
            var answers = new List<SpCalcValue>();
            var mathLibrary = new MathLibrary();

            //フォルダ名のリスト作成
            var fileUtil = new FileUtil();
            var folderNames = fileUtil.SpGetFolderNameList(collectDatas);

            //フォルダ名毎に下記処理を行う
            foreach (var folderName in folderNames)
            {
                //指定注目測定点2点の平均・標準偏差を（collectDatas全体から）求める
                foreach (var settingData in settingDatas)
                {
                    var data = collectDatas
                        .Where(d => d.FolderName == folderName)
                        .Where(d => d.Inspect1 == settingData.Inspect1)
                        .Where(d => d.Inspect2 == settingData.Inspect2)
                        .Select(d => d.Value)
                        .ToList();

                    //リストに格納
                    var answer = new SpCalcValue
                    {
                        FolderName = folderName,
                        Inspect1 = settingData.Inspect1,
                        Inspect2 = settingData.Inspect2,
                        Operator = settingData.Operator,

                        //平均
                        SpMeanValue = mathLibrary.CalcMean(data),

                        //標準偏差
                        SpDevValue = mathLibrary.CalvDev(data)
                    };
                    answers.Add(answer);
                }
            }
            return answers;
        }
    }
}
