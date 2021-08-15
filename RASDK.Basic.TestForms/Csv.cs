using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RASDK.Basic.TestForms
{
    public partial class Csv : Form
    {
        public Csv()
        {
            InitializeComponent();
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            var csv = Basic.Csv.Read(textBoxPath.Text);

            labelReadedFile.Text = string.Empty;
            foreach (var r in csv)
            {
                string row = "";
                foreach (var c in r)
                {
                    row += c;
                }
                labelReadedFile.Text += row + "\r\n";
            }
        }

        private void buttonWriteFile_Click(object sender, EventArgs e)
        {
            var data = new List<List<string>>
            {
                new List<string> { "11", "12", "13" },
                new List<string> { "21", "22", "32" },
                new List<string> { "3  1", "3,2", "3\r\n2" }
            };
            Basic.Csv.Write(textBoxPath.Text, data);
        }
    }
}