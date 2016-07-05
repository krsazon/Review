using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void btnButton_Click(object sender, RoutedEventArgs e)
        {
            //string startTime = "2:00 AM";
            //string endTime = "7:00 AM";

            //TimeSpan duration = DateTime.Parse(endTime).Sum(DateTime.Parse(startTime));

            //MessageBox.Show(duration.ToString());




            DateTime myDate = DateTime.Now;

            myDate = myDate.AddHours(1);
            

            MessageBox.Show(myDate.ToString("h:mm tt"));

        }
    }
}
