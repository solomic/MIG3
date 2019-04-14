using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mig.datetimepicker
{
    public partial class myDatePicker : UserControl
    {
        private CalendarForm _calendarForm = null;
        public myDatePicker()
        {
            InitializeComponent();

            _calendarForm = new CalendarForm();
           // _wrongDateBox = new MessageForm();

            //_wrongDateBox.Message = "Дата введена неправильно! Введите правильную или пустую дату!";
          
            _calendarForm.Calendar.DateSelected += new DateRangeEventHandler(Calendar_DateSelected);
           // _calendarButton.BackgroundImage = _arrowsImageList.Images["arrow-01-down.png"];

            _calendarForm.VisibleChanged += new EventHandler(_calendarForm_VisibleChanged);
        }
        void _calendarForm_VisibleChanged(object sender, EventArgs e)
        {
            _calendarForm.Font = new Font(_calendarForm.Font.Name, this.Font.Size);
            //if (_calendarForm.Visible)
            //{
            //    _calendarButton.BackgroundImage = _arrowsImageList.Images["arrow-01-up.png"];
            //}
            //else
            //{
            //    _calendarButton.BackgroundImage = _arrowsImageList.Images["arrow-01-down.png"];
            //}
        }


        // при выборе даты в календаре ставим строку в MaskedTextBox
        void Calendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.maskedTextBox1.Text = _calendarForm.Calendar.SelectionEnd.ToShortDateString();
        }
        // bool vis = true;
        public String SelectedDate
        {
            get
            {
                string rs = maskedTextBox1.Text.Replace(',', '.').Replace(" ", string.Empty);

                try
                {
                    DateTime dt = DateTime.ParseExact(rs, "dd.MM.yyyy", null);
                }
                catch
                {
                    rs = "";
                }
                return rs;
            }
            set { maskedTextBox1.Text = value; }

        }
        public bool ValidateDate()
        {
            //валидация даты
            bool res = false;
            string rs = maskedTextBox1.Text.Replace(',', '.').Replace(" ", string.Empty);
            if (rs == "..")
                return true;
            try
            {
                DateTime dt = DateTime.ParseExact(rs, "dd.MM.yyyy", null);
                res = true;
            }
            catch
            {
                this.maskedTextBox1.Focus();
                this.maskedTextBox1.SelectAll();
                MessageBox.Show("Некорректная дата", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
            return res;
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
           

            Point parentScreenPoint = this.Parent.PointToScreen(new Point(this.Location.X, this.Location.Y + this.Height));
            _calendarForm.Location = parentScreenPoint;

            if (_calendarForm.Visible)
            {
                _calendarForm.Hide();
            }
            else
            {
                _calendarForm.Show();
            }
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            maskedTextBox1.Text = e.End.Date.ToString();
        }

       

        private static object EventValueChanged = new object();

        public event EventHandler ValueChanged
        {
            add { Events.AddHandler(EventValueChanged, value); }
            remove { Events.RemoveHandler(EventValueChanged, value); }
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[EventValueChanged];
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
