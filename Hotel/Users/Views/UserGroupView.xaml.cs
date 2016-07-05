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
    /// Interaction logic for UserGroupView.xaml
    /// </summary>
    public partial class UserGroupView : UserControl
    {
        List<UserGroup> UserGroups = new List<UserGroup>();

        public UserGroupView()
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
               UserGroups = context.UserGroups.ToList();
                dgUserGroup.ItemsSource = null;
                dgUserGroup.ItemsSource = UserGroups;
                viewUserGroup.BestFitColumns();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new Windows.UserGroupWindow();
            MethodsClass.ShowPropertyWindow(window);
            Refresh();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) 
      {
          if (dgUserGroup.SelectedItem != null)
          {
              using (var context = new DatabaseContext())
              {
                  var selected = dgUserGroup.SelectedItem as User;
                  var users = context.UserGroups.FirstOrDefault(c => c.UserGroupId == selected.UserGroupId);
                  var items = context.Users.Where(c => c.UserGroupId == users.UserGroupId);
                  if (items.Count() > 0)
                  {
                      MethodsClass.ShowNotification("Related records will be affected.\nDeletion cancelled.");
                  }
                  else if (MethodsClass.ShowConfirmation("Do you really want to delete this record?") == true)
                  {
                      context.UserGroups.Remove(users);
                      context.SaveChanges();
                      Refresh();
                  }
              }
          }
          else
          {
              MethodsClass.ShowNotification("Empty");
          }
       }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgUserGroup.SelectedItem != null)
            {
                var selected = dgUserGroup.SelectedItem as UserGroup;
               var window = new Windows.UserGroupWindow(selected.UserGroupId);
               MethodsClass.ShowPropertyWindow(window);
                Refresh();
            }
            else {
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

        private void dgItemGroup_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            if (dgUserGroup.SelectedItem != null)
            {
                var item = dgUserGroup.SelectedItem as UserGroup;
                txtUserGroup.Text = item.UserGroupName;
            }
            else
            {
                txtUserGroup.Text = "";
            }
        }
    }
}
