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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hotel.Users.Windows
{
    /// <summary>
    /// Interaction logic for UserGroupWindow.xaml
    /// </summary>
    public partial class UserGroupWindow : Window
    {
       
   double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        int SelectedId = 0;
        public UserGroupWindow()
        {
            InitializeComponent();
        }

        public UserGroupWindow( int id)
        {
            InitializeComponent();
            this.SelectedId = id;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region animation onLoading
            if (screenLeftEdge > 0 || screenLeftEdge < -8)
            {
                screenWidth += screenLeftEdge;
            }
            DoubleAnimation animation = new DoubleAnimation(0, this.Width, (Duration)TimeSpan.FromSeconds(0.3));
            DoubleAnimation animation2 = new DoubleAnimation(screenWidth, screenWidth - this.Width, (Duration)TimeSpan.FromSeconds(0.3));
            this.BeginAnimation(Window.WidthProperty, animation);
            this.BeginAnimation(Window.LeftProperty, animation2);
            #endregion
            if (SelectedId > 0)
            {
                using (var context = new DatabaseContext())
                {
                    var group = context.UserGroups.FirstOrDefault(c => c.UserGroupId == SelectedId);
                    txtUserGroupName.Text = group.UserGroupName;
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            #region animation onClosing
            if (screenLeftEdge > 0 || screenLeftEdge < -8)
            {
                screenWidth += screenLeftEdge;
            }
            Closing -= Window_Closing;
            e.Cancel = true;
            var anim = new DoubleAnimation(screenWidth, (Duration)TimeSpan.FromSeconds(0.3));
            var anim2 = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.3));
            anim.Completed += (s, _) => this.Close();
            this.BeginAnimation(Window.LeftProperty, anim);
            this.BeginAnimation(Window.WidthProperty, anim2);
            #endregion
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if (SelectedId > 0)
                {
                    var duplicates = context.UserGroups.Where(c => c.UserGroupName == txtUserGroupName.Text).ToList();
                    if (txtUserGroupName.Text == "")
                    {
                        MethodsClass.ShowNotification("Please input username");
                    }
                    else if (duplicates.Count() > 0)
                    {
                        MethodsClass.ShowNotification("The item is already exist");
                    }
                    else
                    { 
                        if (txtUserGroupName.Text != "")
                        {
                            var group = context.UserGroups.FirstOrDefault(c => c.UserGroupId == SelectedId);
                            group.UserGroupName = txtUserGroupName.Text;
                            MethodsClass.ShowNotification("Successfully updated");
                            context.SaveChanges();
                            this.Close();
                        }
                    }
                }
                else
                {
                    var duplicates = context.UserGroups.Where(c => c.UserGroupName == txtUserGroupName.Text).ToList();
                    if (txtUserGroupName.Text == "")
                    {
                        MethodsClass.ShowNotification("Please input name");
                    }
                    else if (duplicates.Count() > 0)
                    {
                        MethodsClass.ShowNotification("The item is already exist");
                    }
                    else
                    {
                        if (txtUserGroupName.Text != "")
                        {
                            var user = new UserGroup();
                            user.UserGroupName = txtUserGroupName.Text;
                            context.UserGroups.Add(user);
                            context.SaveChanges();
                            MethodsClass.ShowNotification("Successfully added");
                            DialogResult = true;
                        }
                    }
                }
            }
        }
    }
}
