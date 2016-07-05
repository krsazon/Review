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

namespace Hotel.MasterData.Windows
{
    /// <summary>
    /// Interaction logic for RoomEquipmentWindow.xaml
    /// </summary>
    public partial class RoomEquipmentWindow : Window
    {

          int selectedid = 0;

        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;

        public RoomEquipmentWindow()
        {
            InitializeComponent();
        }

        public RoomEquipmentWindow(int id)
        {
            InitializeComponent();
            this.selectedid = id;
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

            if (selectedid > 0)
            {
                using (var context = new DatabaseContext())
                {
                    var equipment = context.Equipments.FirstOrDefault(c => c.EquipmentId == selectedid);
                    txtEquipmentName.Text = equipment.EquipmentName;
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
                if (selectedid > 0)
                {
                    if (txtEquipmentName.Text != "")
                    {
                        var duplicates = context.Equipments.Where(c => c.EquipmentName == txtEquipmentName.Text).ToList();
                        if (duplicates.Count() > 0)
                        {
                            MethodsClass.ShowNotification("Cannot add duplicate records.");
                        }
                        else
                        {
                            var equipment = context.Equipments.FirstOrDefault(c => c.EquipmentId == selectedid);
                            equipment.EquipmentName = txtEquipmentName.Text;
                            MethodsClass.ShowNotification("Record successfully updated!");
                            context.SaveChanges();
                            this.Close();
                        }
                    }
                    else
                    {
                        MethodsClass.ShowNotification("Please fill up the fields correctly.");
                    }
                }
                else
                {
                        var equipment = new Equipment();

                        if (txtEquipmentName.Text == "")
                        {
                            MethodsClass.ShowNotification("Please fill up the fields correctly.");
                        }
                        else
                        {
                            var duplicates = context.Equipments.Where(c => c.EquipmentName == txtEquipmentName.Text).ToList();
                            if (duplicates.Count() > 0)
                            {
                                MethodsClass.ShowNotification("Cannot add duplicate records.");
                            }
                            else
                            {
                            equipment.EquipmentName = txtEquipmentName.Text;
                            context.Equipments.Add(equipment);
                            context.SaveChanges();
                            MethodsClass.ShowNotification("New record successfully saved!");
                            this.Close();
                        }
                    }
                }
            }
        }
    }
}
