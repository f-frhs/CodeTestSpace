namespace CalcXmlFile
{
    /// <summary>特殊計算を行う計算対象と計算内容を格納</summary>
    public class CalcSetting
    {
        /// <summary>計算対象1</summary>
        public string InsName1 { set; get; }

        /// <summary>計算対象2</summary>
        public string InsName2 { set; get; }

        /// <summary>計算内容</summary>
        public string Operator { set; get; }
    }
}