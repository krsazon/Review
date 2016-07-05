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

namespace Hotel.Users.Views
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {       

        public UserView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            using (var context = new DatabaseContext())
            {
                var UserList = new List<UsersView>();
                var users = context.Users.ToList();
                foreach (var list in users)
                {
                    var usertype = context.UserGroups.FirstOrDefault(c => c.UserGroupId == list.UserGroupId);

                    UserList.Add(new UsersView()
                    {
                      Username = list.Username,
                      Name = list.Name,
                      Password = list.Password,
                      UserGroupId = usertype.UserGroupName,
                      UserType = list.UserType 
                   
                    });

                    dgUser.ItemsSource = null;
                    dgUser.ItemsSource = UserList;
                    viewUser.BestFitColumns();
                }
            }        
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new Users.Windows.UserWindow();
            MethodsClass.ShowPropertyWindow(window);
            Refresh();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgUser.SelectedItem != null)
            {
                var selected = dgUser.SelectedItem as UsersView;
                var window = new Users.Windows.UserWindow(selected.UserId);
                MethodsClass.ShowPropertyWindow(window);
                Refresh();
            }
            else
            {
                MessageBox.Show("Select a row");
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Variables.collapseUserContent = true;
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            frame.BackNavigationMode = BackNavigationMode.Root;
            frame.GoBack();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgUser_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            if (dgUser.SelectedItem != null)
            {
                using (var context = new DatabaseContext())
                {
                    var user = dgUser.SelectedItem as UsersView;
                    txtUsername.Text = user.Username;
                    txtPassword.Text = user.Password;
                    txtUserType.Text = user.UserType;
                    txtName.Text = user.Name;
                }
            }
            else
            {
                txtName.Text = "";
                txtUsername.Text = "";
                txtUserType.Text = "";
                txtPassword.Text = "";
            }
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            if (dgUser.SelectedItem != null)
            {
                var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
                var page = new Booking.SubPage.FolioPage();
                frame.Navigate(page);
                Refresh();
            }
            else
            {
                MessageBox.Show("Select a row");
            }
        }
    }
}