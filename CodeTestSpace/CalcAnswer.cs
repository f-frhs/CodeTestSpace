namespace CalcXmlFile
{
    /// <summary> 計測データ(注目計測名・項目・absoluteの値)を格納する容器としてのクラス</summary>
    public class CalcAnswer
    {
        /// <summary>注目計測名 </summary>
        /// <remarks>例: </remarks>
        public string InsName { set; get; }

        /// <summary>項目名 </summary>
        /// <remarks>例: </remarks>
        public string Inspect { set; get; }

        /// <summary>absolute </summary>
        public double Ans { set; get; }
    }
}
