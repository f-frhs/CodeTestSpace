using System.Collections.Generic;
using System.Linq;

namespace CalcXmlFile
{
    /// <summary> 計測データ（注目測定点名・注目計測名・項目名）を格納する容器としてのクラス </summary>
    public class InspectItem
    {
        /// <summary> 注目測定点名 ST1_SF01など </summary>
        public List<string> InsNames { get; set; }

        /// <summary> 注目計測名 CubeHole1など </summary>
        public List<string> Inspects { get; set; }

        /// <summary> 項目 Xなど </summary>
        public List<string> Items { get; set; }
    }
}