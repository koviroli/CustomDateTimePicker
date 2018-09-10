using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        ECultureDateFormat dateFormat;
        string OldText = string.Empty;

        public Form1()
        {
            InitializeComponent();
          
            dateFormat = ECultureDateFormat.ddmmyyyy;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ToolStripDropDown tsdd = new ToolStripDropDown();
            tsdd.Show(250, 250);
        }
        private List<int> GetIndexesOf(string s, char ch)
        {
            var foundIndexes = new List<int>();

            long t1 = DateTime.Now.Ticks;
            for (int i = s.IndexOf(ch); i > -1; i = s.IndexOf(ch, i + 1))
            {
                // for loop end when i=-1 ('a' not found)
                foundIndexes.Add(i);
            }
            return foundIndexes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }
    }
}
