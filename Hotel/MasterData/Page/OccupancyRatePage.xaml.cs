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

namespace Hotel.MasterData.Page
{
    /// <summary>
    /// Interaction logic for OccupancyRatePage.xaml
    /// </summary>
    public partial class OccupancyRatePage : UserControl
    {
        int selectedId = 0;

        List<OccupancyRateView> OccupancyRates = new List<OccupancyRateView>();
        public OccupancyRatePage(int id)
        {
            InitializeComponent();
            this.selectedId = id;
        }

        public OccupancyRatePage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {

            using (var context = new DatabaseContext())
            {

                var OccupancyRates = context.OccupancyRates.ToList();

                if (txtSearch.Text != "")
                {
                    OccupancyRates = OccupancyRates.Where(c => c.OccupancyRateName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();

                }

                dgOccupancyRate.ItemsSource = null;
                dgOccupancyRate.ItemsSource = OccupancyRates;
                viewOccupancyRates.BestFitColumns();

                var total = context.OccupancyRates.Select(c=> c.OccupancyRateId);
                blkTotal.Text = total.Count().ToString();
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new Windows.OccupancyRateWindow();
            MethodsClass.ShowPropertyWindow(window);
            Refresh();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgOccupancyRate.SelectedItem != null)
            {
                if (MethodsClass.ShowConfirmation("Do you really want to delete this record?") == true)
                {
                    var selected = dgOccupancyRate.SelectedItem as OccupancyRate;
                    using (var context = new DatabaseContext())
                    {
                        var occupancy= context.OccupancyRates.FirstOrDefault(c => c.OccupancyRateId == selected.OccupancyRateId);
                        context.OccupancyRates.Remove(occupancy);
                        context.SaveChanges();
                        MethodsClass.ShowNotification("Record successfully removed.");
                        Refresh();
                    }
                }
            }
            else
            {
                MethodsClass.ShowNotification("Please select a row.");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgOccupancyRate.SelectedItem != null)
            {
                var selected = dgOccupancyRate.SelectedItem as OccupancyRate;
                var occupancy= new Windows.OccupancyRateWindow(selected.OccupancyRateId);
                MethodsClass.ShowPropertyWindow(occupancy);
                Refresh();
            }
            else if (dgOccupancyRate.SelectedItem != null)
            {
                MethodsClass.ShowNotification("Please select a row.");
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Variables.collapseUserContent = true;
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            frame.BackNavigationMode = BackNavigationMode.Root;
            frame.GoBack();
        }

       
        

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void dgOccupancyRate_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            if (dgOccupancyRate.SelectedItem != null)
            {
                var occupancy = dgOccupancyRate.SelectedItem as OccupancyRate;
             
                txtOccupancyRateName.Text = occupancy.OccupancyRateName;
                txtOccupancyRateType.Text = occupancy.OccupancyRateType;
                txtOccupancyRateAmount.Text = occupancy.OccupancyRateAmount.ToString();
            }
            else
            {
              
                txtOccupancyRateName.Text = "";
                txtOccupancyRateType.Text = "";
                txtOccupancyRateAmount.Text = "";
            }
        }

    
    
    }
}

