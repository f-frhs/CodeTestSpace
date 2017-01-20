using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CalcXmlFile
{
    /// <summary> ファイル取り扱いに関するユーティリティークラス </summary>
    public static class FileUtil
    {
        /// <summary> 結果をファイルに保存する </summary>
        public static void SaveDatas(string fName, List<InspectItem> items, List<double> mean, List<double> dev,
        List<MeasuredValue> funcs)
        {
            throw new NotImplementedException();
        }

        /// <summary> 指定フォルダ以下のファイル名のリストを取得する </summary>
        public static List<string> GetXmlFiles(string basePath)
        {
            //引数で渡されたフォルダ以下の全てのxmlファイルを取得
            var fnames = Directory.GetFiles(basePath, "*.xml", SearchOption.AllDirectories).ToList();
            return fnames;
        }
    }
}


