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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
       
  double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        int SelectedId = 0;
        
       public UserWindow()
        {
            InitializeComponent();
        }
       public UserWindow(int id)
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
                    var user = context.Users.FirstOrDefault(c => c.UserId == SelectedId);
                    txtUsername.Text = user.Username;
                    List<String> list = context.UserGroups.Select(c => c.UserGroupName).ToList();
                    cmbUserType.ItemsSource = list;
                    var usertype = context.UserGroups.FirstOrDefault(c => c.UserGroupId == user.UserGroupId);
                    cmbUserType.Text = usertype.UserGroupName;
                    txtName.Text = user.Name;
                    txtPassword.Text = user.Password;
                }
            }
            else
            {
                using (var context = new DatabaseContext())
                {
                    List<String> list = context.UserGroups.Select(c => c.UserGroupName).ToList();
                    cmbUserType.ItemsSource = list;
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if (SelectedId > 0)
                {
                    var duplicates = context.Users.Where(c => c.Username == txtUsername.Text).ToList();
                    if (txtUsername.Text == "")
                    {
                        MethodsClass.ShowNotification("Please input iusername");
                    }
                    else if (duplicates.Count() > 0)
                    {
                        MethodsClass.ShowNotification("The username is already exist");
                    }

                    else
                    {
                        if (txtUsername.Text != "" && txtName.Text != "" && cmbUserType.Text != "" && txtPassword.Text != "")
                        {
                            var user = context.Users.FirstOrDefault(c => c.UserId == SelectedId);
                            var usertype = context.UserGroups.FirstOrDefault(c => c.UserGroupName == cmbUserType.Text);
                            
                           
                            user.UserGroupId = usertype.UserGroupId;
                            user.Username = txtUsername.Text;
                            user.Name = txtName.Text;
                            user.Password = txtPassword.Text;
                            context.SaveChanges();
                            MethodsClass.ShowNotification("Successfully Updated");
                            DialogResult = true;
                        }

                        else if (txtUsername.Text != "" && txtName.Text != "" && cmbUserType.Text != "" && txtPassword.Text != "")
                        {
                            MethodsClass.ShowNotification("Please input data in the necessary fields");
                        }
                    }
                }
                else
                {
                    var duplicates = context.Users.Where(c => c.Username == txtUsername.Text ).ToList();
                    if (duplicates.Count() > 0)
                    {
                        MethodsClass.ShowNotification("The item is already exist");
                    }
                    else
                    {
                        if (txtUsername.Text != "" && txtName.Text != "" && cmbUserType.Text != "" && txtPassword.Text != "")
                        {
                            var user = new User();
                            var usertype = context.UserGroups.FirstOrDefault(c => c.UserGroupName == cmbUserType.Text);
                           
                           user.UserGroupId = usertype.UserGroupId;
                           user.Username = txtUsername.Text;
                           user.Name = txtName.Text;
                           user.Password = txtPassword.Text;
                           user.UserType = cmbUserType.Text;


                            context.Users.Add(user);
                            context.SaveChanges();
                            MethodsClass.ShowNotification("Successfully Added");
                            DialogResult = true;
                        }
                       
                        else if (txtUsername.Text != "" && txtName.Text != "" && cmbUserType.Text != "" && txtPassword.Text != "")
                        {
                            MethodsClass.ShowNotification("Please input data in the necessary fields");
                        }
                    }
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
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
    }
}
