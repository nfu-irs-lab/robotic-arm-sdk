using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RASDK.Basic
{
    /// <summary>
    /// CSV 表格檔案操作。
    /// </summary>
    public class Csv
    {
        /// <summary>
        /// 讀取指定路徑的 CSV 檔案。
        /// </summary>
        /// <param name="path">CSV 檔案路徑，包含副檔名。</param>
        /// <param name="symbolSeparated">分割符號。</param>
        /// <param name="symbolStringDelimiter">字串分隔符號。</param>
        /// <returns>讀取完成的資料。<c>stringData[row][column]</c></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static List<List<string>> Read(string path,
                                              char symbolSeparated = ',',
                                              char symbolStringDelimiter = '\"')
        {
            var csvContent = new List<List<string>>();

            if (File.Exists(path))
            {
                using (var file = new StreamReader(path))
                {
                    while (!file.EndOfStream)
                    {
                        var row = new List<string>();
                        var line = file.ReadLine();
                        var values = line.Split(symbolSeparated);
                        foreach (var t in values)
                        {
                            row.Add(t.Trim().Trim(symbolStringDelimiter).Trim());
                        }
                        csvContent.Add(row);
                    }
                    file.Close();
                }
            }
            else
            {
                throw new FileNotFoundException();
            }

            return csvContent;
        }

        /// <summary>
        /// 建立並寫入指定路徑的 CSV 檔案。
        /// </summary>
        /// <param name="path">CSV 檔案路徑，包含副檔名。</param>
        /// <param name="rowColumnData">要寫入的資料。<c>stringData[row][column]</c></param>
        /// <param name="columnName">欄名。</param>
        /// <param name="symbolSeparated">分割符號。</param>
        /// <param name="symbolStringDelimiter">字串分隔符號。</param>
        public static void Write(string path,
                                 List<List<string>> rowColumnData,
                                 List<string> columnName = null,
                                 char symbolSeparated = ',',
                                 char symbolStringDelimiter = '\"')
        {
            // Insert column name.
            if (columnName != null && !File.Exists(path))
            {
                rowColumnData.Insert(0, columnName);
            }

            var file = MakeStreamWriter(path);

            // Write data.
            foreach (var row in rowColumnData)
            {
                string rowData = "";
                foreach (var colData in row)
                {
                    // 判斷要寫入的資料是否包含分割符號、字串分隔符號及空白字元（\s）。
                    if (Regex.IsMatch(colData, $"[{symbolSeparated}{symbolStringDelimiter}" + @"\s]"))
                    {
                        rowData += $"{symbolStringDelimiter}{colData}{symbolStringDelimiter}{symbolSeparated}";
                    }
                    else
                    {
                        rowData += $"{colData}{symbolSeparated}";
                    }
                }
                rowData = rowData.TrimEnd(symbolSeparated).Trim();
                file.WriteLine(rowData);
            }

            file.Close();
        }

        /// <summary>
        /// Factory pattern。
        /// </summary>
        /// <returns></returns>
        private static StreamWriter MakeStreamWriter(string path)
        {
            return File.AppendText(path);
        }
    }
}