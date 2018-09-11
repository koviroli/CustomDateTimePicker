using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public enum ECultureDateFormat
    {
        yyyymmdd = 0,
        ddmmyyyy = 1,
        mmddyyyy = 2
    }
    /// <summary>
    /// DatetimePicker control, for handling continuous date typing and placeholder text
    /// </summary>
    public partial class CustomDateTimePicker : UserControl
    {
        /// <summary>
        /// Popup window for Calendar
        /// </summary>
        private PopupCalendar m_popup;

        /// <summary>
        /// Determines if popup calendar is shown or not
        /// </summary>
        private bool _shown = false;

        #region Properties
        /// <summary>
        /// DateFormat 
        /// </summary>
        public ECultureDateFormat DateFormat { get; set; }

        /// <summary>
        /// Separator of Date
        /// </summary>
        private char Separator { get; set; }

        /// <summary>
        /// Value of TextBox parsed to DateTime
        /// </summary>
        public DateTime Value
        {
            get
            {
                if (DateTime.TryParse(tbDate.Text, out DateTime temp))
                    return temp;
                else
                    return DateTime.Now;
            }
        }

        public int MaximumYear { get; set; } = 2600;

        public int MinimumYear { get; set; } = 1;
        
        /// <summary>
        /// Determine if is there a valid DateTime value in the TextBox
        /// </summary>
        public bool Valid
        {
            get
            {
                try
                {
                    DateTime.Parse(tbDate.Text);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Format used to parsing DateTime from
        /// </summary>
        public string DateFormatText
        {
            get
            {
                switch (DateFormat)
                {
                    case ECultureDateFormat.ddmmyyyy:
                        return $"dd{Separator}MM{Separator}yyyy";
                    case ECultureDateFormat.mmddyyyy:
                        return $"MM{Separator}dd{Separator}yyyy";
                    case ECultureDateFormat.yyyymmdd:
                        return $"yyyy{Separator}MM{Separator}dd";
                    default:
                        return $"DD{Separator}MM{Separator}yyyy";
                }
            }
        }
        /// <summary>
        /// Placeholder text house in empty TextBox if it is not focused yet
        /// </summary>
        public string PlaceHolderText
        {
            get
            {
                switch (DateFormat)
                {
                    case ECultureDateFormat.ddmmyyyy:
                        return $"DD{Separator}MM{Separator}YYYY";
                    case ECultureDateFormat.mmddyyyy:
                        return $"MM{Separator}DD{Separator}YYYY";
                    case ECultureDateFormat.yyyymmdd:
                        return $"YYYY{Separator}MM{Separator}DD";
                    default:
                        return $"DD{Separator}MM{Separator}YYYY";
                }
            }
        }

        /// <summary>
        /// The first separator index in the TextBox
        /// </summary>
        private int FirstSeparatorIndex
        {
            get
            {
                return tbDate.Text.IndexOf(Separator);
            }
        }

        /// <summary>
        /// The seconds separator index in the TextBox
        /// </summary>
        private int SecondSeparatorIndex
        {
            get
            {
                try
                {
                    return tbDate.Text.IndexOf(Separator, FirstSeparatorIndex+1);
                }
                catch(ArgumentOutOfRangeException)
                {
                    return -1;
                }
            }
        }
        #endregion

        public CustomDateTimePicker()
        {
            InitializeComponent();
            m_popup = new PopupCalendar();
            m_popup.Location = new Point(2, 24);
            m_popup.MCalendar.DateSelected += _mc_DateSelected;
            m_popup.m_tsdd.Closing += M_tsdd_Closing;
        }

        public void SetSeparator(char separator)
        {
            if (separator != '\0')
                Separator = separator;
        }

        #region Calendar handle
        private void M_tsdd_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            btnCalendar.BackColor = SystemColors.Window;
            _shown = false;
        }

        private void _mc_DateSelected(object sender, DateRangeEventArgs e)
        {
            tbDate.Text = m_popup.MCalendar.SelectionStart.ToString(DateFormatText).Replace('/', Separator);
            tbDate.ForeColor = Color.Black;
            CloseCalendar();
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            if (!_shown)
            {
                OpenCalendar();
                btnCalendar.BackColor = Color.AliceBlue;
            }
            else
            {
                CloseCalendar();
                btnCalendar.BackColor = SystemColors.Control;
            }
        }

        private void CloseCalendar()
        {
            m_popup.Close();
            _shown = false;
        }

        private void OpenCalendar()
        {
            m_popup.Show(this);
            _shown = true;
        }
        #endregion
        
        private void tbDate_Click(object sender, EventArgs e)
        {
            if(tbDate.Text.Equals(PlaceHolderText))
            {
                tbDate.Text = DateTime.Now.ToString(DateFormatText);
                tbDate.ForeColor = Color.Black;
            }

            CheckDateValidity();
            if (tbDate.SelectionStart <= FirstSeparatorIndex)
            {
                tbDate.SelectionStart = 0;
                tbDate.SelectionLength = FirstSeparatorIndex;
            }
            else if(tbDate.SelectionStart > FirstSeparatorIndex && tbDate.SelectionStart <= SecondSeparatorIndex)
            {
                tbDate.SelectionStart = FirstSeparatorIndex + 1;
                tbDate.SelectionLength = SecondSeparatorIndex - FirstSeparatorIndex - 1;
            }
            else
            {
                tbDate.SelectionStart = SecondSeparatorIndex + 1;
                tbDate.SelectionLength = tbDate.TextLength - SecondSeparatorIndex - 1;
            }
        }

        private void SetPlaceholderText()
        {
            tbDate.Text = PlaceHolderText;
            tbDate.ForeColor = Color.LightGray;
        }

        private void tbDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(tbDate.SelectionLength == 10)
            {
                tbDate.SelectionStart = 0;
                tbDate.SelectionLength = FirstSeparatorIndex;

            }
            if (e.KeyChar == 8)
            {
                e.Handled = true;
                return;
            }

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        
        private string GetFirstSeparatorBefore()
        {
            if (FirstSeparatorIndex < 0)
                return tbDate.Text;
            return tbDate.Text.Substring(0, FirstSeparatorIndex);
        }

        private string GetFirstSeparatorAfter()
        {
            return tbDate.Text.Substring(FirstSeparatorIndex + 1, SecondSeparatorIndex - 1 - FirstSeparatorIndex);
        }

        private string GetSecondSeparatorAfter()
        {
            if (SecondSeparatorIndex < 0) return "";
            return tbDate.Text.Substring(SecondSeparatorIndex+1, tbDate.Text.Length - 1 - SecondSeparatorIndex);
        }

        private int? Day
        {
            get
            {
                try
                {
                    var res = -1;
                    switch (DateFormat)
                    {
                        case ECultureDateFormat.ddmmyyyy:
                            if (GetFirstSeparatorBefore().Length != 2) return null;
                            res = Convert.ToInt32(GetFirstSeparatorBefore());
                            break;
                        case ECultureDateFormat.mmddyyyy:
                            if (GetFirstSeparatorAfter().Length != 2) return null;
                            res = Convert.ToInt32(GetFirstSeparatorAfter());
                            break;
                        case ECultureDateFormat.yyyymmdd:
                            if (GetSecondSeparatorAfter().Length != 2) return null;
                            res = Convert.ToInt32(GetSecondSeparatorAfter());
                            break;
                    }
                    
                    return res;
                }
                catch
                {
                    return null;
                }
            }
        }

        private int? Month
        {
            get
            {
                try
                {
                    var res = -1;
                    switch (DateFormat)
                    {
                        case ECultureDateFormat.ddmmyyyy:
                            if (GetFirstSeparatorAfter().Length != 2) return null;
                            res = Convert.ToInt32(GetFirstSeparatorAfter());
                            break;
                        case ECultureDateFormat.mmddyyyy:
                            if (GetFirstSeparatorBefore().Length != 2) return null;
                            res = Convert.ToInt32(GetFirstSeparatorBefore());
                            break;
                        case ECultureDateFormat.yyyymmdd:
                            if (GetFirstSeparatorAfter().Length != 2) return null;
                            res = Convert.ToInt32(GetFirstSeparatorAfter());
                            break;
                    }

                    return res;
                }
                catch
                {
                    return null;
                }
            }
        }

        private int? Year
        {
            get
            {
                try
                {
                    var res = 0;
                    switch (DateFormat)
                    {
                        case ECultureDateFormat.ddmmyyyy:
                        case ECultureDateFormat.mmddyyyy:
                            if (GetSecondSeparatorAfter().Length != 4) return null;
                            res = Convert.ToInt32(GetSecondSeparatorAfter());
                            break;
                        case ECultureDateFormat.yyyymmdd:
                            if (GetFirstSeparatorBefore().Length != 4) return null;
                            res = Convert.ToInt32(GetFirstSeparatorBefore());
                            break;
                    }
                    return res;
                }
                catch
                {
                    return null;
                }
            }
        }

        private void SetDay(string day)
        {
            string dateWithoutDay;
            switch (DateFormat)
            {
                case ECultureDateFormat.ddmmyyyy:
                        tbDate.Text = $"{day}{Separator}{GetFirstSeparatorAfter()}{Separator}{GetSecondSeparatorAfter()}";
                    break;
                case ECultureDateFormat.mmddyyyy:
                    if(SecondSeparatorIndex < 0)
                        tbDate.Text = $"{GetFirstSeparatorBefore()}{Separator}{day}";
                    else
                        tbDate.Text = $"{GetFirstSeparatorBefore()}{Separator}{day}{Separator}{GetSecondSeparatorAfter()}";
                    break;
                case ECultureDateFormat.yyyymmdd:
                    dateWithoutDay = tbDate.Text.Substring(0, SecondSeparatorIndex+1);
                    tbDate.Text = $"{dateWithoutDay}{day}";
                    break;
            }
        }

        private void SetMonth(string month)
        {
            switch (DateFormat)
            {
                case ECultureDateFormat.ddmmyyyy:
                    if (SecondSeparatorIndex < 0)
                        tbDate.Text = $"{GetFirstSeparatorBefore()}{Separator}{month}";
                    else
                        tbDate.Text = $"{GetFirstSeparatorBefore()}{Separator}{month}{Separator}{GetSecondSeparatorAfter()}";
                    break;
                case ECultureDateFormat.mmddyyyy:
                    if (tbDate.Text.Length < 4)
                        tbDate.Text = $"{month}";
                    else
                        tbDate.Text = $"{month}{Separator}{GetFirstSeparatorAfter()}{Separator}{GetSecondSeparatorAfter()}";
                    break;
                case ECultureDateFormat.yyyymmdd:
                        tbDate.Text = $"{GetFirstSeparatorBefore()}{Separator}{month}{Separator}{GetSecondSeparatorAfter()}";
                    break;
            }
        }

        private void SetYear(string year)
        {
            switch (DateFormat)
            {
                case ECultureDateFormat.ddmmyyyy:
                    tbDate.Text = $"{GetFirstSeparatorBefore()}{Separator}{GetFirstSeparatorAfter()}{Separator}{year}";
                    break;
                case ECultureDateFormat.mmddyyyy:
                    tbDate.Text = $"{GetFirstSeparatorBefore()}{Separator}{GetFirstSeparatorAfter()}{Separator}{year}";
                    break;
                case ECultureDateFormat.yyyymmdd:
                    tbDate.Text = $"{year}{Separator}{GetFirstSeparatorAfter()}{Separator}{GetSecondSeparatorAfter()}";
                    break;
            }
        }

        private void tbDate_TextChanged(object sender, EventArgs e)
        {
            CheckDateValidity();
            JumpToNextCaret();
        }

        private void JumpToNextCaret()
        {
            switch (DateFormat)
            {
                case ECultureDateFormat.ddmmyyyy:
                    if (tbDate.SelectionStart == 2 && GetFirstSeparatorBefore().Length == 2)
                    {
                        tbDate.SelectionStart++;
                        tbDate.SelectionStart = FirstSeparatorIndex + 1;
                        tbDate.SelectionLength = SecondSeparatorIndex - FirstSeparatorIndex - 1;
                    }
                    else if (tbDate.SelectionStart == 5 && GetFirstSeparatorAfter().Length == 2)
                    {
                        tbDate.SelectionStart++;
                        tbDate.SelectionStart = SecondSeparatorIndex + 1;
                        tbDate.SelectionLength = tbDate.TextLength - SecondSeparatorIndex - 1;
                    }
                    break;
                case ECultureDateFormat.mmddyyyy:
                    if (tbDate.SelectionStart == 2 && GetFirstSeparatorBefore().Length == 2)
                    {
                        tbDate.SelectionStart++;
                        tbDate.SelectionStart = FirstSeparatorIndex + 1;
                        tbDate.SelectionLength = SecondSeparatorIndex - FirstSeparatorIndex - 1;
                    }
                    else if (tbDate.SelectionStart == 5 && GetFirstSeparatorAfter().Length == 2)
                    {
                        tbDate.SelectionStart++;
                        tbDate.SelectionStart = SecondSeparatorIndex + 1;
                        tbDate.SelectionLength = tbDate.TextLength - SecondSeparatorIndex - 1;
                    }
                    break;
                case ECultureDateFormat.yyyymmdd:
                    if (tbDate.SelectionStart == 4 && GetFirstSeparatorBefore().Length == 4)
                    {
                        tbDate.SelectionStart++;
                        tbDate.SelectionStart = FirstSeparatorIndex + 1;
                        tbDate.SelectionLength = SecondSeparatorIndex - FirstSeparatorIndex - 1;
                    }
                    else if (tbDate.SelectionStart == 7 && GetFirstSeparatorAfter().Length == 2)
                    {
                        tbDate.SelectionStart++;
                        tbDate.SelectionStart = SecondSeparatorIndex + 1;
                        tbDate.SelectionLength = tbDate.TextLength - SecondSeparatorIndex - 1;
                    }
                    break;
            }
        }

        private bool CheckDateValidity()
        {
            if (tbDate.Text.Equals(PlaceHolderText)) return true;
            bool result = true;
            switch (DateFormat)
            {
                case ECultureDateFormat.ddmmyyyy:
                    if (Day != null )
                    {
                        if (Day > DateTime.DaysInMonth(Value.Year, Value.Month) || Day < 1)
                        {
                            result = false;
                            SetDay("01");
                            tbDate.SelectionStart = 2;
                        }
                    }
                    if (Month != null)
                    {
                        if(Month > 12 || Month < 1)
                        {
                            result = false;
                            SetMonth("01");
                            tbDate.SelectionStart = 5;
                        }
                    }
                    if(Year != null)
                    {
                        if (Year > MaximumYear || Year < MinimumYear)
                        {
                            result = false;
                            SetYear("2018");
                            tbDate.SelectionStart = tbDate.Text.Length;
                        }
                    }
                    break;
                case ECultureDateFormat.mmddyyyy:
                    if (Day != null )
                    {
                        if(Day > DateTime.DaysInMonth(Value.Year, Value.Month) || Day < 1)
                        {
                            result = false;
                            SetDay("01");
                            tbDate.SelectionStart = 5;
                        }
                    }
                    if (Month != null )
                    {
                        if(Month > 12 || Month < 1)
                        {
                            result = false;
                            SetMonth("01");
                            tbDate.SelectionStart = 2;
                        }
                    }
                    if (Year != null)
                    {
                        if (Year > MaximumYear || Year < MinimumYear)
                        {
                            result = false;
                            SetYear("2018");
                            tbDate.SelectionStart = tbDate.Text.Length;
                        }
                    }
                    break;
                case ECultureDateFormat.yyyymmdd:
                    if (Month != null)
                    {
                        if (Month > 12 || Month < 1)
                        {
                            result = false;
                            SetMonth("01");
                            tbDate.SelectionStart = 7;
                        }
                    }
                    if (Day != null && Month != null && Year != null)
                    {
                        if (Day > DateTime.DaysInMonth(Year.Value, Month.Value) || Day < 1)
                        {
                            result = false;
                            SetDay("01");
                            tbDate.SelectionStart = tbDate.TextLength;
                        }
                    }
                    if (Year != null )
                    {
                        if(Year > MaximumYear || Year < MinimumYear)
                        {
                            result = false;
                            SetYear(DateTime.Now.Year.ToString());
                            tbDate.SelectionStart = 4;
                        }
                    }
                    break;
            }
            return result;
        }

        private void tbDate_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbDate.Text))
            {
                SetPlaceholderText();
            }
        }

        private void tbDate_MouseHover(object sender, EventArgs e)
        {
            TextBox TB = (TextBox)sender;
            int visibleTime = 1000;
            ToolTip tt = new ToolTip();
            tt.Show(PlaceHolderText, TB, 0, 21, visibleTime);
        }

        private void CustomDateTimePicker_Load(object sender, EventArgs e)
        {
            SetPlaceholderText();
        }
    }
    #region Popup Calendar
    public sealed class PopupCalendar : IDisposable
    {
        public readonly ToolStripDropDown m_tsdd;
        private readonly Panel m_hostPanel;
        public MonthCalendar MCalendar { get; set; }
        public Point Location { get; set; }

        public PopupCalendar()
        {
            MCalendar = new MonthCalendar();
            MCalendar.Margin = new Padding(0);
            MCalendar.Location = new Point(-5, -5);

            m_hostPanel = new Panel();
            m_hostPanel.Padding = Padding.Empty;
            m_hostPanel.Margin = Padding.Empty;
            m_hostPanel.TabStop = false;
            m_hostPanel.BorderStyle = BorderStyle.None;
            m_hostPanel.BackColor = Color.Transparent;
            m_hostPanel.MinimumSize = new Size(220, 100);
            m_hostPanel.Controls.Add(MCalendar);

            m_tsdd = new ToolStripDropDown();
            m_tsdd.Items.Add(new ToolStripControlHost(m_hostPanel));
        }

        public void Show(Control pParentControl)
        {
            if (pParentControl == null) return;

            var loc = pParentControl.PointToScreen(Location);
            m_tsdd.Show(loc);

        }

        public void Close()
        {
            m_tsdd.Close();
        }

        public void Dispose()
        {

            m_tsdd.Dispose();
            m_hostPanel.Dispose();
        }
    }
    #endregion
}
