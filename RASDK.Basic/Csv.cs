using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RASDK.Basic
{
    public class Csv
    {
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

        public static void Write(string path,
                                 List<List<string>> rowColumnData,
                                 List<string> columnName = null,
                                 char symbolSeparated = ',',
                                 char symbolStringDelimiter = '\"')
        {
            var file = MakeStreamWriter(path);

            // Write column name.
            if (!columnName.Equals(null) && !File.Exists(path))
            {
                // string rowData = "";
                // foreach (var cn in columnName)
                // {
                //     rowData += $"{cn}{symbolSeparated}";
                // }
                // rowData = rowData.TrimEnd(symbolSeparated).Trim();
                // file.WriteLine(rowData);
                //
                rowColumnData.Insert(0, columnName);
            }

            // Write data.
            foreach (var row in rowColumnData)
            {
                string rowData = "";
                foreach (var colData in row)
                {
                    if (Regex.IsMatch(colData, @"[,\s]"))
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
            return System.IO.File.AppendText(path);
        }
    }
}