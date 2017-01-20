// collectDataFroXmlFiles.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Office.Interop.Excel;
using System.Linq;
using AutoAssyModules.Perceptron;
using CalcXmlFile;
using System.Text;

namespace CalcXmlFile
{
    public static class FileUtility
    {
        //結果をファイルに保存する
        public static void SaveDatas(string fName, List<InspectItem> items, List<double> mean, List<double> dev,
        List<CalcAnswer> funcs)
        {
            throw new NotImplementedException();
        }

        //指定フォルダ以下のファイルを取得する
        public static List<string> GetXmlFiles(string basePath)
        {
            //引数で渡されたフォルダ以下の全てのxmlファイルを取得
            List<string> fnames = Directory.GetFiles(basePath, "*.xml", SearchOption.AllDirectories).ToList();
            return fnames;
        }
    }
}


