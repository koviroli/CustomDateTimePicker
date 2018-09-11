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
        ToolStripDropDown tdd;
        MonthCalendar mcalendar;
        Panel p1;
        public Form1()
        {
            InitializeComponent();

            myDateTimePicker2.SetSeparator('/');
            myDateTimePicker2.DateFormat = ECultureDateFormat.yyyymmdd;

            mcalendar = new MonthCalendar();
            mcalendar.Margin = new Padding(0);
            mcalendar.Location = new Point(-5, -5);

            p1 = new Panel();
            p1.Margin = Padding.Empty;
            p1.Padding = Padding.Empty;
            p1.TabStop = false;
            p1.BorderStyle = BorderStyle.None;
            p1.BackColor = Color.Transparent;
            p1.Location = new Point(0, 0);
            p1.MinimumSize = new Size(220, 100);
            p1.Size = mcalendar.Size;
            p1.Controls.Add(mcalendar);
         
            tdd = new ToolStripDropDown();
            var tsp = new ToolStripControlHost(p1);
            tdd.Items.Add(tsp);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tdd.Show(new Point(300,300));
        }
    }
}
