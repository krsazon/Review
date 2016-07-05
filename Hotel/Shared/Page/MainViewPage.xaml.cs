using DevExpress.Xpf.WindowsUI;
using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel.Page
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
 
        public partial class MainViewPage : UserControl
        {
            String selectedusertype = "";
            double screenLeftEdge = Application.Current.MainWindow.Left;
            double screenTopEdge = Application.Current.MainWindow.Top;
            double screenHeight = Application.Current.MainWindow.Height;
            double screenWidth = Application.Current.MainWindow.Width;

            public static System.Windows.Threading.DispatcherTimer updateDateTimer;
            public static System.Windows.Threading.DispatcherTimer updateTimer;
            public static int selectedCategoryId;

            public MainViewPage()
            {
                InitializeComponent();
            }

            public MainViewPage(string usertype)
            {
                InitializeComponent();
                this.selectedusertype = usertype;
            }

            #region Measure StackPanel
            private double GetStackPanelHeight(StackPanel stackPanel)
            {
                double stackPanelHeight = 0;

                var buttonChildren = stackPanel.Children.OfType<ToggleButton>().ToList();
                if (buttonChildren.Count() > 0)
                {
                    foreach (var child in buttonChildren)
                    {
                        double childUpperMargin = child.Margin.Top;
                        double childHeight = child.Height;
                        double childLowerMargin = child.Margin.Bottom;

                        double totalChildHeight = childUpperMargin + childHeight + childLowerMargin;
                        stackPanelHeight += totalChildHeight;
                    }
                }

                var stackPanelChildren = stackPanel.Children.OfType<StackPanel>().ToList();
                if (stackPanelChildren.Count() > 0)
                {
                    foreach (var stackPanelChild in stackPanelChildren)
                    {
                        var buttons = stackPanelChild.Children.OfType<ToggleButton>().ToList();
                        if (buttons.Count() > 0)
                        {
                            foreach (var child in buttons)
                            {
                                double childUpperMargin = child.Margin.Top;
                                double childHeight = child.Height;
                                double childLowerMargin = child.Margin.Bottom;

                                double totalChildHeight = childUpperMargin + childHeight + childLowerMargin;
                                stackPanelHeight += totalChildHeight;
                            }
                        }
                    }
                }

                return stackPanelHeight;
            }

            private double GetStackPanelHeightButton(StackPanel stackPanel)
            {
                double stackPanelHeight = 0;

                var buttonChildren = stackPanel.Children.OfType<Button>().ToList();
                if (buttonChildren.Count() > 0)
                {
                    foreach (var child in buttonChildren)
                    {
                        double childUpperMargin = child.Margin.Top;
                        double childHeight = child.Height;
                        double childLowerMargin = child.Margin.Bottom;

                        double totalChildHeight = childUpperMargin + childHeight + childLowerMargin;
                        stackPanelHeight += totalChildHeight;
                    }
                }

                var stackPanelChildren = stackPanel.Children.OfType<StackPanel>().ToList();
                if (stackPanelChildren.Count() > 0)
                {
                    foreach (var stackPanelChild in stackPanelChildren)
                    {
                        var buttons = stackPanelChild.Children.OfType<Button>().ToList();
                        if (buttons.Count() > 0)
                        {
                            foreach (var child in buttons)
                            {
                                double childUpperMargin = child.Margin.Top;
                                double childHeight = child.Height;
                                double childLowerMargin = child.Margin.Bottom;

                                double totalChildHeight = childUpperMargin + childHeight + childLowerMargin;
                                stackPanelHeight += totalChildHeight;
                            }
                        }
                    }
                }

                return stackPanelHeight;
            }

            private double GetStackPanelWidth(StackPanel stackPanel)
            {
                double stackPanelWidth = 0;

                var stackPanelChildren = stackPanel.Children.OfType<ToggleButton>().ToList();
                if (stackPanelChildren.Count() > 0)
                {
                    double childLeftMargin = stackPanelChildren.FirstOrDefault().Margin.Left;
                    double childWidth = stackPanelChildren.FirstOrDefault().Width;
                    double childRightMargin = stackPanelChildren.FirstOrDefault().Margin.Right;

                    double totalChildWidth = childLeftMargin + childWidth + childRightMargin;
                    stackPanelWidth = totalChildWidth;
                }

                return stackPanelWidth;
            }
            #endregion

            #region Animate StackPanel
            private void FoldStackPanelUpward(StackPanel stackPanel)
            {
                double stackPanelHeight = GetStackPanelHeight(stackPanel);
                if (stackPanel.Height > 0)
                {
                    DoubleAnimation animation = new DoubleAnimation() { From = stackPanelHeight, To = 0, Duration = TimeSpan.Parse("0:0:0.35") };
                    stackPanel.BeginAnimation(StackPanel.HeightProperty, animation);
                }
                else
                {
                    DoubleAnimation animation = new DoubleAnimation() { From = 0, To = stackPanelHeight, Duration = TimeSpan.Parse("0:0:0.35") };
                    stackPanel.BeginAnimation(StackPanel.HeightProperty, animation);
                }
            }

            private void FoldStackPanelUpwardButton(StackPanel stackPanel)
            {
                double stackPanelHeight = GetStackPanelHeightButton(stackPanel);
                if (stackPanel.Height > 0)
                {
                    DoubleAnimation animation = new DoubleAnimation() { From = stackPanelHeight, To = 0, Duration = TimeSpan.Parse("0:0:0.35") };
                    stackPanel.BeginAnimation(StackPanel.HeightProperty, animation);
                }
                else
                {
                    DoubleAnimation animation = new DoubleAnimation() { From = 0, To = stackPanelHeight, Duration = TimeSpan.Parse("0:0:0.35") };
                    stackPanel.BeginAnimation(StackPanel.HeightProperty, animation);
                }
            }

            private void FoldStackPanelSideward(StackPanel stackPanel)
            {
                double stackPanelWidth = GetStackPanelWidth(stackPanel);
                if (stackPanel.Width > 0)
                {
                    DoubleAnimation animation = new DoubleAnimation() { From = stackPanelWidth, To = 0, Duration = TimeSpan.Parse("0:0:0.35") };
                    stackPanel.BeginAnimation(StackPanel.WidthProperty, animation);
                }
                else
                {
                    DoubleAnimation animation = new DoubleAnimation() { From = 0, To = stackPanelWidth, Duration = TimeSpan.Parse("0:0:0.35") };
                    stackPanel.BeginAnimation(StackPanel.WidthProperty, animation);
                }
            }

            private void FoldAfterAnotherSingleMode(StackPanel openedStackPanel, StackPanel closedStackPanel)
            {
                double openedStackPanelHeight = GetStackPanelHeight(openedStackPanel);
                double closedStackPanelHeight = GetStackPanelHeight(closedStackPanel);

                DoubleAnimation closingAnimation = new DoubleAnimation() { From = openedStackPanelHeight, To = 0, Duration = TimeSpan.Parse("0:0:0.35") };
                DoubleAnimation openingAnimation = new DoubleAnimation() { From = 0, To = closedStackPanelHeight, Duration = TimeSpan.Parse("0:0:0.35") };
                closingAnimation.Completed += (s, e) => closedStackPanel.BeginAnimation(StackPanel.HeightProperty, openingAnimation);
                openedStackPanel.BeginAnimation(StackPanel.HeightProperty, closingAnimation);
            }

            private void CloseStackPanel(StackPanel stackPanel)
            {
                if (stackPanel.Height > 0)
                {
                    DoubleAnimation animation = new DoubleAnimation() { From = stackPanel.Height, To = 0, Duration = TimeSpan.Parse("0:0:0.35") };
                    stackPanel.BeginAnimation(StackPanel.HeightProperty, animation);
                }
            }
            #endregion

            private void UpdateUserProfileEvent(object source, EventArgs e)
            {
                //for detecting the user profile for collapsing
                if (Variables.collapseUserContent == true)
                {
                    CloseStackPanel(stackUserContent);
                    Variables.collapseUserContent = false;
                }
            }

            private void UpdateTimeEvent(object source, EventArgs e)
            {
                blkDate.Text = DateTime.Now.ToString("MMMM d, yyyy");
                blkTime.Text = DateTime.Now.ToString("T");
                Refresh();
            }

           public void UserControl_Loaded(object sender, RoutedEventArgs e)
            {
               
               stackUserContent.Height = 0;

               var page = new Page.MainMenuPage();
               frameMainView.Source = page;
               if (selectedusertype == "Master Data")
               {
                   page.btnFunctions.Visibility = Visibility.Hidden;
                   page.btnReports.Visibility = Visibility.Hidden;
                   page.btnSystem.Visibility = Visibility.Hidden;
               }
               else
               {

               }
               updateTimer = new System.Windows.Threading.DispatcherTimer();
               updateTimer.Tick += new EventHandler(UpdateUserProfileEvent);
               updateTimer.Interval = TimeSpan.FromSeconds(0.1);
               updateTimer.Start();

               updateDateTimer = new System.Windows.Threading.DispatcherTimer();
               updateDateTimer.Tick += new EventHandler(UpdateTimeEvent);
               updateDateTimer.Interval = TimeSpan.FromSeconds(1);
               updateDateTimer.Start();
               
               using (var context = new DatabaseContext())
               {
                   var parameters = context.Parameters.FirstOrDefault(c => c.ParameterId == c.ParameterId);

                   lblName.Text = parameters.HotelName;
                   lblAdd.Text = parameters.HotelAddress;
                   blkDisplayName.Text = Page.LoginPage.tx;
                   blkUserType.Text = Page.LoginPage.ty;

               }     
            }

           public void Refresh()
           {
               using (var context = new DatabaseContext())
               {
                   var parameters = context.Parameters.FirstOrDefault(c => c.ParameterId == c.ParameterId);

                   lblName.Text = parameters.HotelName;
                   lblAdd.Text = parameters.HotelAddress;
                   blkDisplayName.Text = Page.LoginPage.tx;
                   blkUserType.Text = Page.LoginPage.ty;

               }
           }


            private void btnSystemUser_Click(object sender, RoutedEventArgs e)
            {
                Variables.collapseMenu = true;
                FoldStackPanelUpwardButton(stackUserContent);
            }

            private void btnUpdateProfile_Click(object sender, RoutedEventArgs e)
            {
               
            }

            private void btnLogOut_Click(object sender, RoutedEventArgs e)
            {
                CloseStackPanel(stackUserContent);
                var window = new Shared.Windows.MessageBoxYesNo("Do you really want to logout?");
                double screenWidth = Application.Current.MainWindow.Width;
                window.Height = 0;
                window.Top = Application.Current.MainWindow.Top + 8;
                window.Left = (screenWidth / 2) - (window.Width / 2);
                if (screenLeftEdge > 0 || screenLeftEdge < -8)
                {
                    window.Left += screenLeftEdge;
                }
                window.ShowDialog();
                if (Variables.yesClicked == true)
                {
                    var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
                    frame.BackNavigationMode = BackNavigationMode.Root;
                    frame.GoBack();
                    updateTimer.Stop();
                    updateDateTimer.Stop();
                    Variables.yesClicked = false;
                }
            }

            private void btnExit_Click(object sender, RoutedEventArgs e)
            {
                CloseStackPanel(stackUserContent);
                var window = new Shared.Windows.MessageBoxYesNo("Do you really want to exit?");
                double screenWidth = Application.Current.MainWindow.Width;
                window.Height = 0;
                window.Top = Application.Current.MainWindow.Top + 8;
                window.Left = (screenWidth / 2) - (window.Width / 2);
                if (screenLeftEdge > 0 || screenLeftEdge < -8)
                {
                    window.Left += screenLeftEdge;
                }
                window.ShowDialog();
                if (Variables.yesClicked == true)
                {
                    Application.Current.MainWindow.Close();
                    Variables.yesClicked = false;
                }
            }
        }
  
    }

