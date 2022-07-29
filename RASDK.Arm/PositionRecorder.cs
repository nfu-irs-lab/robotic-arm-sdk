using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RASDK.Basic;
using RASDK.Arm.Type;
using System.Windows.Forms;

namespace RASDK.Arm
{
    public class PositionRecorder
    {
        private readonly string _path;
        private string _filename = "";
        private bool _fileCreated = false;

        public PositionRecorder(string Path = "")
        {
            _path = Path;
        }

        #region Write

        /// <summary>
        /// 記錄手臂座標位置。
        /// </summary>
        /// <param name="name">位置名稱。</param>
        /// <param name="position">座標位置。</param>
        /// <param name="type">座標類型。</param>
        /// <param name="remark">備註。</param>
        public void Write(string name, double[] position, string type, string remark = "")
        {
            if (!_fileCreated)
            {
                CreateFile();
            }

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

            var colName = new List<string>()
            { "time", "name", "x_j1", "y_j2", "z_j3", "a_j4", "b_j5", "c_j6", "type", "remark" };
            Csv.Write(_path + _filename, row, colName);
        }

        /// <summary>
        /// 記錄手臂座標位置。
        /// </summary>
        /// <param name="name">位置名稱。</param>
        /// <param name="position">座標位置。</param>
        /// <param name="type">座標類型。</param>
        /// <param name="remark">備註。</param>
        public void Write(string name, double[] position, CoordinateType type, string remark = "")
        {
            Write(name, position, type.ToString(), remark);
        }

        /// <summary>
        /// 記錄手臂座標位置。
        /// </summary>
        /// <param name="name">位置名稱。</param>
        /// <param name="j1X">座標位置j1/X。</param>
        /// <param name="j2Y">座標位置j2/Y。</param>
        /// <param name="j3Z">座標位置j3/Z。</param>
        /// <param name="j4A">座標位置j4/A。</param>
        /// <param name="j5B">座標位置j5/B。</param>
        /// <param name="j6C">座標位置j6/C。</param>
        /// <param name="type">座標類型。</param>
        /// <param name="remark">備註。</param>
        public void Write(string name,
                          double j1X,
                          double j2Y,
                          double j3Z,
                          double j4A,
                          double j5B,
                          double j6C,
                          CoordinateType type,
                          string remark = "")
        {
            Write(name, new double[] { j1X, j2Y, j3Z, j4A, j5B, j6C }, type, remark);
        }

        #endregion Write

        #region Read

        public int Read(out List<string> names,
                        out List<double[]> positions,
                        out List<CoordinateType> types,
                        out List<string> remarks,
                        out List<string> times)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                Multiselect = false
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return Read(dialog.FileName, out names, out positions, out types, out remarks, out times);
            }

            names = null;
            positions = null;
            types = null;
            remarks = null;
            times = null;
            return -1;
        }

        public int Read(string filename,
                        out List<string> names,
                        out List<double[]> positions,
                        out List<CoordinateType> types,
                        out List<string> remarks,
                        out List<string> times)
        {
            names = new List<string>();
            positions = new List<double[]>();
            types = new List<CoordinateType>();
            remarks = new List<string>();
            times = new List<string>();

            var data = Csv.Read(filename);
            foreach (var row in data)
            {
                times.Add(row[0]);
                names.Add(row[1]);
                types.Add(CoordinateTypeParse(row[8]));

                var pos = new double[6];
                for (int i = 0; i < 6; i++)
                {
                    pos[i] = double.Parse(row[i + 2]);
                }
                positions.Add(pos);

                if (row.Count > 9)
                {
                    remarks.Add(row[9]);
                }
                else
                {
                    remarks.Add("");
                }
            }

            return data.Count;
        }

        #endregion Read

        private CoordinateType CoordinateTypeParse(string coordinateType)
        {
            if (coordinateType.Equals(CoordinateType.Descartes.ToString()))
            {
                return CoordinateType.Descartes;
            }
            else if (coordinateType.Equals(CoordinateType.Joint.ToString()))
            {
                return CoordinateType.Joint;
            }
            else if (coordinateType.Equals(CoordinateType.Unknown.ToString()))
            {
                return CoordinateType.Unknown;
            }
            else
            {
                throw new ArgumentException();
            }
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

            _fileCreated = true;
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