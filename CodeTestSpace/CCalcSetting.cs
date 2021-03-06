﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CalcXmlFile
{
    /// <summary> 特殊計算の設定（計算対象1,2・計算内容）を格納するクラス </summary>
    public class CalcSetting
    {
        /// <summary> 計算対象1 </summary>
        /// <remarks>例: CubeHole1 など</remarks>
        public string Inspect1 { set; get; }

        /// <summary> 計算対象2 </summary>
        /// <remarks>例: CubeHole2 など</remarks>
        public string Inspect2 { set; get; }

        /// <summary> 計算内容 </summary>
        /// <remarks>例: distance など</remarks>
        public string Operator { set; get; }

        /// <summary> fNameからCalcSetting型のリストを返す </summary>
        public List<CalcSetting> LoadConfiguration(string fName)
        {
            //CSVファイルから各行取り込み
            var clcSettings = System.IO.File.ReadAllLines(fName);

            //計算内容を取り出したのちリストから削除
            var strOperator = clcSettings[0];
            var sSettings = clcSettings.Skip(1);

            //容器(answers)を作成
            var answers = new List<CalcSetting>();

            //リストから計算対象を取り出す
            foreach (var sSetting in sSettings)
            {
                var sp = sSetting.Split(new[] { ',' });
                if (sp.Length < 2) continue;

                var tmpSetting = new CalcSetting
                {
                    Operator = strOperator,
                    Inspect1 = sp[0],
                    Inspect2 = sp[1]
                };
                answers.Add(tmpSetting);
            }
            return answers;
        }

    }
}