using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RASDK.Basic;

namespace RASDK.Arm
{
    public class PositionRecorder
    {
        private readonly string _path;
        private string _filename = "";

        public PositionRecorder(string Path = "/")
        {
            _path = Path;
            CreateFile();
        }

        public void Write(string name, double[] position, string type, string remark = "")
        {
            var col = new List<string>();

            DateTime localDate = DateTime.Now;
            col.Add(localDate.ToString("yyyy-MM-dd_HH:mm:ss"));
            col.Add(name);

            for (int i = 0; i < position.Length; i++)
            {
                col.Add(position[i].ToString());
            }
            col.Add(type);
            col.Add(remark);
            var row = new List<List<string>>();
            row.Add(col);

            var colName = new List<string>() { "time", "name", "x_j1", "y_j2", "z_j3", "a_j4", "b_j5", "c_j6", "type", "remark" };
            Csv.Write(_path + _filename, row, colName);
        }
        private void CreateFile()
        {
            // 取得目前的時間。
            var dateTimeNow = DateTime.Now;
            var num = 1;

            // Update filename.
            while (true)
            {
                // 設定目標檔案名稱。
                var targetFilename = $"{dateTimeNow:MMMdd-HH}_{num}.csv";

                // 判斷目前檔案是否已經存在。
                if (System.IO.File.Exists(_path + targetFilename))
                {
                    // 若目標檔案已經存在，遞增序號，使檔案名稱不重複 。
                    num++;
                }
                else
                {
                    // 若目標檔案不存在，使用此檔案名稱。
                    _filename = targetFilename;
                    break;
                }
            }

            var sw = MakeStreamWriter();
            sw.Close();
        }

        /// <summary>
        /// Factory pattern。
        /// </summary>
        /// <returns></returns>
        private StreamWriter MakeStreamWriter()
        {
            try
            {
                return System.IO.File.AppendText(_path + _filename);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(_path);
                return System.IO.File.AppendText(_path + _filename);
            }
        }
    }
}
