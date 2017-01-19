// collectDataFroXmlFiles.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Office.Interop.Excel;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;
using AutoAssyModules.Perceptron;
using CalcXmlFile;
using System.Text;
using CalcXmlFile.DataHangar;

namespace CalcXmlFile
{
    class FileUtility
    {
        //結果をファイルに保存する
        public void SaveDatas(string fName, List<InspectItem> items, List<double> mean, List<double> dev,
        List<CalcAnswer> funcs)
        {
            throw new NotImplementedException();
        }

        //指定フォルダ以下のファイルを取得する
        public List<string> GetXmlFiles(string basePath)
        {
            //引数で渡されたフォルダ以下の全てのxmlファイルを取得
            List<string> fnames = Directory.GetFiles(basePath, "*.xml", SearchOption.AllDirectories).ToList();
            return fnames;
        }
    }
}


