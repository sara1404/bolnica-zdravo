using Controller;
using Model;
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

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for EquipmentPage.xaml
    /// </summary>
    public partial class EquipmentPage : Page
    {
        private UserController uc;
        public EquipmentPage()
        {
            InitializeComponent();
            App app = Application.Current as App;
            uc = app.userController;
            FillDataGrid();
        }

        private void Show_Relocation_Window_Click(object sender, RoutedEventArgs e)
        {
            new EquipmentRelocationWindow().Show();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            uc.CurentLoggedUser = null;
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void FillDataGrid() {
            List<DamiEquipment> equipment = new List<DamiEquipment>();
            equipment.Add(new DamiEquipment("DentCo", "Dentist chair", 50));
            equipment.Add(new DamiEquipment("DentCo", "Dentist chair", 50));
            equipment.Add(new DamiEquipment("DentCo", "Dentist chair", 50));
            equipment.Add(new DamiEquipment("DentCo", "Dentist chair", 50));
            equipment.Add(new DamiEquipment("DentCo", "Dentist chair", 50));
            equipment.Add(new DamiEquipment("DentCo", "Dentist chair", 50));
            equipment.Add(new DamiEquipment("DentCo", "Dentist chair", 50));
            equipment.Add(new DamiEquipment("DentCo", "Dentist chair", 50));
            equipmentDataGrid.ItemsSource = equipment;
        }
    }

    public class DamiEquipment{
        public string manufacturer { get; set; }
        public string type { get; set; }
        public int quantity { get; set; }

        public DamiEquipment(string name, string type, int quantity)
        {
            this.manufacturer = name;
            this.type = type;
            this.quantity = quantity;
        }
    }
}
