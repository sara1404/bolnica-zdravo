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
    /// Interaction logic for MedicinePage.xaml
    /// </summary>
    public partial class MedicinePage : Page
    {
        private MedicineController medicineController;
        public MedicinePage()
        {
            InitializeComponent();
            App app = Application.Current as App;
            medicineController = app.medicineController;
            medicineDataGrid.ItemsSource = medicineController.FindAll();
        }

        private void Add_Medicine_Click(object sender, RoutedEventArgs e)
        {
            new AddMedicineWindow().Show();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            int selectedRow = medicineDataGrid.SelectedIndex;
            if (e.Key == Key.Enter && selectedRow != -1)
            {
                Medicine medicine = (Medicine)medicineDataGrid.SelectedItem;
                EditMedicineWindow editWindow = new EditMedicineWindow();
                editWindow.nameField.Text = medicine.Name;
                editWindow.codeField.Text = medicine.Id;
                editWindow.quanityField.Text = medicine.quantity.ToString();
                editWindow.alternativesField.ItemsSource = medicine.Alternatives;
                editWindow.ingridientsField.ItemsSource = medicine.Ingridients;
                editWindow.Show();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
