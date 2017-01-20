﻿// collectDataFroXmlFiles.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CalcXmlFile
{
    /// <summary> ファイルへのデータの保存や取得 </summary>
    public static class FileUtility
    {
        /// <summary> 結果をファイルに保存する </summary>
        public static void SaveDatas(string fName, List<InspectItem> items, List<double> mean, List<double> dev,
        List<CalcAnswer> funcs)
        {
            throw new NotImplementedException();
        }

        /// <summary> 指定フォルダ以下のファイルを取得する </summary>
        public static List<string> GetXmlFiles(string basePath)
        {
            //引数で渡されたフォルダ以下の全てのxmlファイルを取得
            List<string> fnames = Directory.GetFiles(basePath, "*.xml", SearchOption.AllDirectories).ToList();
            return fnames;
        }
    }
}


