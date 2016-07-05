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
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : UserControl
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenHeight = Application.Current.MainWindow.Height;
        double screenWidth = Application.Current.MainWindow.Width;

        public static System.Windows.Threading.DispatcherTimer updateMenuTimer;
        bool functionsClicked, masterdataClicked, reportsClicked, systemClicked;
        bool validationClicked, productionFileClicked;

        public MainMenuPage()
        {
            InitializeComponent();
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
                double stackHeight = GetStackPanelHeight(stackPanel);
                DoubleAnimation animation = new DoubleAnimation() { From = stackHeight, To = 0, Duration = TimeSpan.Parse("0:0:0.35") };
                stackPanel.BeginAnimation(StackPanel.HeightProperty, animation);
            }
        }

        private void CollapseAllStackPanels()
        {
            CloseStackPanel(stackCaptureColumn);
            CloseStackPanel(stackMasterdataColumn);
            CloseStackPanel(stackReportsColumn);
            CloseStackPanel(stackSystemColumn);
            CloseStackPanel(stackValidation);
            CloseStackPanel(stackProductionFile);
        }
        #endregion

        #region Highlight Buttons
        private List<ToggleButton> GetButtonSiblings(ToggleButton button)
        {
            List<ToggleButton> siblings = new List<ToggleButton>();

            StackPanel buttonContainer = button.Parent as StackPanel;
            var containerChildren = buttonContainer.Children.OfType<ToggleButton>().ToList();
            if (containerChildren.Count() > 0)
            {
                foreach (ToggleButton child in containerChildren)
                {
                    if (child != button)
                    {
                        siblings.Add(child);
                    }
                }
            }

            return siblings;
        }

        private void HighlightRelatedButtons(ToggleButton button)
        {
            List<ToggleButton> siblings = GetButtonSiblings(button);
            if (button.IsChecked == true)
            {
                if (siblings.Count() > 0)
                {
                    foreach (ToggleButton sibling in siblings)
                    {
                        sibling.IsChecked = false;
                    }
                }
            }
        }

        private void HighlightHeaders(ToggleButton toggleButton)
        {
            List<ToggleButton> buttons = new List<ToggleButton>();
            buttons.Add(btnFunctions);
            buttons.Add(btnMasterdata);
            buttons.Add(btnReports);
            buttons.Add(btnSystem);
            if (toggleButton.IsChecked == true)
            {
                foreach (var button in buttons)
                {
                    if (button != toggleButton)
                    {
                        button.IsChecked = false;
                    }
                }
            }
            else
            {
                foreach (var button in buttons)
                {
                    button.IsChecked = false;
                }
            }
        }

        private void UncheckAllHeaders()
        {
            List<ToggleButton> buttons = new List<ToggleButton>();
            buttons.Add(btnFunctions);
            buttons.Add(btnMasterdata);
            buttons.Add(btnReports);
            buttons.Add(btnSystem);
            foreach (var button in buttons)
            {
                button.IsChecked = false;
            }
        }

        private void HighlightChildrenButtons(StackPanel stackPanel)
        {
            var buttonChildren = stackPanel.Children.OfType<ToggleButton>().ToList();
            foreach (ToggleButton button in buttonChildren)
            {
                button.IsChecked = true;
            }
        }

        private void UncheckChildrenButtons(StackPanel stackPanel)
        {
            var buttonChildren = stackPanel.Children.OfType<ToggleButton>().ToList();
            if (stackPanel.Height == 0)
            {
                foreach (ToggleButton button in buttonChildren)
                {
                    button.IsChecked = false;
                }
            }
        }
        #endregion

        private void AnimateMethod()
        {
            if (functionsClicked == true)
            {
                FoldStackPanelUpward(stackCaptureColumn);
                UncheckChildrenButtons(stackCaptureColumn);
                HighlightChildrenButtons(stackCaptureColumn);
                CloseStackPanel(stackMasterdataColumn);
                CloseStackPanel(stackReportsColumn);
                CloseStackPanel(stackSystemColumn);

                CloseStackPanel(stackValidation);
                CloseStackPanel(stackProductionFile);
            }
            else if (masterdataClicked == true)
            {
                FoldStackPanelUpward(stackMasterdataColumn);
                UncheckChildrenButtons(stackMasterdataColumn);
                HighlightChildrenButtons(stackMasterdataColumn);
                CloseStackPanel(stackCaptureColumn);
                CloseStackPanel(stackReportsColumn);
                CloseStackPanel(stackSystemColumn);

                CloseStackPanel(stackValidation);
                CloseStackPanel(stackProductionFile);
            }
            else if (reportsClicked == true)
            {
                FoldStackPanelUpward(stackReportsColumn);
                UncheckChildrenButtons(stackReportsColumn);
                //HighlightChildrenButtons(stackReportsColumn);
                CloseStackPanel(stackCaptureColumn);
                CloseStackPanel(stackMasterdataColumn);
                CloseStackPanel(stackSystemColumn);

                CloseStackPanel(stackValidation);
                CloseStackPanel(stackProductionFile);
            }
            else if (systemClicked == true)
            {
                FoldStackPanelUpward(stackSystemColumn);
                UncheckChildrenButtons(stackSystemColumn);
                HighlightChildrenButtons(stackSystemColumn);
                CloseStackPanel(stackCaptureColumn);
                CloseStackPanel(stackMasterdataColumn);
                CloseStackPanel(stackReportsColumn);

                CloseStackPanel(stackValidation);
                CloseStackPanel(stackProductionFile);
            }
        }

        private void AnimateProcessOutputsMethod()
        {
            if (validationClicked == true)
            {
                HighlightChildrenButtons(stackValidation);
                FoldStackPanelUpward(stackValidation);
                CloseStackPanel(stackProductionFile);
            }
            else if (productionFileClicked == true)
            {
                HighlightChildrenButtons(stackProductionFile);
                FoldStackPanelUpward(stackProductionFile);
                CloseStackPanel(stackValidation);
            }
        }

        private void UpdateMenuEvent(object source, EventArgs e)
        {
            //for detecting the menu for collapsing
            if (Variables.collapseMenu == true)
            {
                CollapseAllStackPanels();
                UncheckAllHeaders();
                Variables.collapseMenu = false;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            functionsClicked = false;
            masterdataClicked = false;
            reportsClicked = false;
            systemClicked = false;

            validationClicked = false;
            productionFileClicked = false;

            stackCaptureColumn.Height = 0;
            stackMasterdataColumn.Height = 0;
            stackReportsColumn.Height = 0;
            stackSystemColumn.Height = 0;

            stackValidation.Height = 0;
            stackProductionFile.Height = 0;

            Variables.collapseUserContent = true;
            Variables.hasSearched = false;

            updateMenuTimer = new System.Windows.Threading.DispatcherTimer();
            updateMenuTimer.Tick += new EventHandler(UpdateMenuEvent);
            updateMenuTimer.Interval = TimeSpan.FromSeconds(0.1);
            updateMenuTimer.Start();
        }

        private void btnFunctions_Click(object sender, RoutedEventArgs e)
        {
            functionsClicked = true;
            masterdataClicked = false;
            reportsClicked = false;
            systemClicked = false;

            validationClicked = false;
            productionFileClicked = false;

            #region highlight headers
            ToggleButton button = sender as ToggleButton;
            HighlightHeaders(button);
            #endregion
            AnimateMethod();
            Variables.collapseUserContent = true;

        }

        private void btnLocal_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);

            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }

        private void btnStudentsVerification_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);

            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }

        private void btnMasterdata_Click(object sender, RoutedEventArgs e)
        {
            functionsClicked = false;
            masterdataClicked = true;
            reportsClicked = false;
            systemClicked = false;

            validationClicked = false;
            productionFileClicked = false;

            #region highlight headers
            ToggleButton button = sender as ToggleButton;
            HighlightHeaders(button);
            #endregion
            AnimateMethod();
            Variables.collapseUserContent = true;
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            functionsClicked = false;
            masterdataClicked = false;
            reportsClicked = true;
            systemClicked = false;

            validationClicked = false;
            productionFileClicked = false;

            #region highlight headers
            ToggleButton button = sender as ToggleButton;
            HighlightHeaders(button);
            #endregion
            AnimateMethod();
            Variables.collapseUserContent = true;

        }

        private void btnSystem_Click(object sender, RoutedEventArgs e)
        {
            functionsClicked = false;
            masterdataClicked = false;
            reportsClicked = false;
            systemClicked = true;

            validationClicked = false;
            productionFileClicked = false;

            #region highlight headers
            ToggleButton button = sender as ToggleButton;
            HighlightHeaders(button);
            #endregion
            AnimateMethod();
            Variables.collapseUserContent = true;
        }

        private void btnCaptureStudents_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }


        private void btnStudentsProductionFile_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);

            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }

        private void btnTeachersProductionFile_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);

            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }

        private void btnSystemParametersSettings_Click(object sender, RoutedEventArgs e)
        {
            var window = new Shared.Windows.SystemParameterWindow();
            MethodsClass.ShowPropertyWindow(window);
            CollapseAllStackPanels();
            UncheckAllHeaders();

        }

        private void btnRoomType_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new MasterData.Page.RoomTypePage();
            frame.Navigate(page);
            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();

        }


        private void btnRooms_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new MasterData.Page.RoomPage();
            frame.Navigate(page);
            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }


        private void btnEquipment_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new MasterData.Page.RoomEquipmentPage();
            frame.Navigate(page);
            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }

        private void btnItems_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new MasterData.Page.ItemPage();
            frame.Navigate(page);
            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }

        private void btnItemGroup_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new MasterData.Page.ItemGroupPage();
            frame.Navigate(page);
            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }

        private void btnDiscount_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new MasterData.Page.DiscountPage();
            frame.Navigate(page);
            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }

        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new MasterData.Page.StaffPage();
            frame.Navigate(page);
            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }

        private void btnSystemParametersSettings_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void btnCheckin_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new Hotel.Booking.Page.EntriesPage();
            frame.Navigate(page);
            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new Booking.Page.CheckOut();
            frame.Navigate(page);
            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }

        private void btnReservation_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new Booking.Page.ReservationPage();
            frame.Navigate(page);
            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new Users.Views.UserView();
            frame.Navigate(page);
            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }

        private void btnUserList_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new Users.Views.UserGroupView();
            frame.Navigate(page);
            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }

        private void btnOccupancy_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new MasterData.Page.OccupancyRatePage();
            frame.Navigate(page);
            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }

        private void btnHoliday_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new MasterData.Page.HolidayRatePage();
            frame.Navigate(page);
            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }

        private void btnStaffPosition_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new MasterData.Page.StaffPositionPage();
            frame.Navigate(page);
            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }

        private void btnCancelledAppointments_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllStackPanels();
            UncheckAllHeaders();
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new Booking.Page.CancelledAppointments();
            frame.Navigate(page);
            Variables.collapseUserContent = true;
            updateMenuTimer.Stop();
        }



    }
}
