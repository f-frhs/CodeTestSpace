using System.Collections.Generic;
using System.Linq;

namespace CalcXmlFile
{
    //点の情報を保持するクラス
    public class PointData
    {
        public string XmlFname;
        public string InsName;
        public string Inspect;
        public double x = double.NaN;
        public double y = double.NaN;
        public double z = double.NaN;
        public double i = double.NaN;
        public double j = double.NaN;
        public double k = double.NaN;

        //ある1点に関する情報を抽出して PointData 型として返す
        public static PointData ExtractPointData(string fname, string insName, string inspect, List<MeasuredValue> datas)
        {
            var p = new PointData
            {
                XmlFname = fname,
                InsName = insName,
                Inspect = inspect
            };

            var candidates = datas
                .Where(d => d.XmlFname == fname)
                .Where(d => d.InsName == insName)
                .Where(d => d.Inspect == inspect)
                .ToList();

            p.x = candidates.First(d => d.Item == "X").Value;
            p.y = candidates.First(d => d.Item == "Y").Value;
            p.z = candidates.First(d => d.Item == "Z").Value;
            
            //TODO: i,j,kについても値の取得・格納を行う

            return p;
        }

        public static double CalcDistance(PointData p1, PointData p2)
        {
            var d = MathLibrary.CalcDistance(p1.x, p1.y, p1.z, p2.x, p2.y, p2.z);
            return d;
        }

        //ファイル毎の２点間の距離を計算し、collectedData に追加する。
        //実質的には、必要なデータを抽出するコードがほとんど。
        public static List<MeasuredValue> CalcDistances(string inspectName1, string inspectName2,
            List<MeasuredValue> collectedData)
        {
            var allDatas = new List<MeasuredValue>(collectedData);

            var fnames = collectedData.Select(d => d.XmlFname).Distinct();
            foreach (var fname in fnames)
            {
                //対応する insName を取得
                var insName = collectedData
                    .Where(d => d.XmlFname == fname)
                    .Select(d => d.InsName)
                    .Distinct()
                    .First();

                // pointData を取得
                var p1 = ExtractPointData(fname, insName, inspectName1, collectedData);
                var p2 = ExtractPointData(fname, insName, inspectName2, collectedData);

                //距離を算出
                var distance = CalcDistance(p1, p2);

                //データのリストに追加
                allDatas.Add(new MeasuredValue()
                {
                    XmlFname = fname,
                    InsName = insName,
                    Inspect = inspectName1 + inspectName2,
                    Item
                        = inspectName1.Replace("Hole", "H").Replace("Cube", "C").Replace("Frange", "F")
                          + inspectName2.Replace("Hole", "").Replace("Cube", "").Replace("Frange", ""),
                    Value = distance,
                });
            }
            return allDatas;
        }
    }
}