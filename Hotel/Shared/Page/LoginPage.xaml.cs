using DevExpress.Xpf.WindowsUI;
using Hotel.Models;
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

namespace Hotel.Page
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {

        public static string tx = "";
        public static string ty = "";
        public LoginPage()
        {
            InitializeComponent();
        }

       public void btnOK_Click(object sender, RoutedEventArgs e)
        {
             //using (var context = new DatabaseContext())
             //{
             //  var user = context.Users.Where(c => c.Username == this.txtUsername.Text.ToLower() && c.Password == this.txtPassword.Text).SingleOrDefault();
             //  if (user != null)
             //  {
             //      var users = context.Users.FirstOrDefault(c => c.Username == txtUsername.Text);
                 MethodsClass.ShowNotification("Welcome, you have successfully logged in.");
                 var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
                 MainViewPage page = new MainViewPage();
                 frame.Navigate(page);
               //  tx = users.Name;
               //  ty = users.UserType;
               //}
               //else
               //{
               //MethodsClass.ShowNotification("Logged in failed.");
               //}
            //}
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
