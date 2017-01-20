namespace CalcXmlFile
{
    /// <summary> 注目計測名・項目・absoluteの値を格納</summary>
    public class CalcAnswer
    {
        ///<summary>注目計測名</summary>
        public string InsName { set; get; }

        ///<summary>項目名</summary>
        public string Inspect { set; get; }

        /// <summary>absolute</summary>
        public double Ans { set; get; }
    }
}
