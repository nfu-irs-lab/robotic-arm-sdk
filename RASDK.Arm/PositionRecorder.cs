using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RASDK.Basic;

namespace RASDK.Arm
{
    public class PositionRecorder
    {
        public static void Write(string name, double[] position, string type, string remark = "")
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
            Csv.Write("test.csv", row, colName);
        }
    }
}
