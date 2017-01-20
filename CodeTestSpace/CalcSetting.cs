namespace CalcXmlFile
{
    /// <summary> 特殊計算を指定する際に、計算対象と計算方法を格納する容器としてのクラス </summary>
    public class CalcSetting
    {
        /// <summary> 計算対象1 </summary>
        /// <remarks>例: *, * など</remarks>
        public string InsName1 { set; get; }

        /// <summary> 計算対象2 </summary>
        /// <remarks>例: *, * など</remarks>
        public string InsName2 { set; get; }

        /// <summary> 計算内容 </summary>
        /// <remarks>例: *, * など</remarks>
        public string Operator { set; get; }

    }
}