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
        public static void SaveDatas(string fName, List<CalcValue> values)
        {
            try
            {
                var append = false;
                // 出力用のファイルを開く
                using (var sw = new System.IO.StreamWriter(fName, append))
                {
                    foreach (var value in values)
                    {
                        sw.WriteLine($"{value.Inspect},{value.Item},{value.MeanValue},{value.DevValue}");
                    }

                }
            }
            catch (System.Exception e)
            {
                // ファイルを開くのに失敗したときエラーメッセージを表示
                System.Console.WriteLine(e.Message);
            }
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


