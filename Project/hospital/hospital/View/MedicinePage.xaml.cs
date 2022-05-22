using Controller;
using hospital.Controller;
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
        private UserController userController;
        private IngridientsController ingridientsController;
        private ManagerMainWindow mainWindow;

        public MedicinePage()
        {
            InitializeComponent();
            mainWindow = SetMainWindow();
            App app = Application.Current as App;
            medicineController = app.medicineController;
            ingridientsController = app.ingridientsController;
            medicineDataGrid.ItemsSource = medicineController.FindAll();
            userController = app.userController;
        }

        private ManagerMainWindow SetMainWindow()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win is ManagerMainWindow)
                {
                    mainWindow = (ManagerMainWindow)win;
                }
            }
            return mainWindow;
        }

        private void Add_Medicine_Click(object sender, RoutedEventArgs e)
        {
            new AddMedicineWindow().Show();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            userController.CurentLoggedUser = null;
            MainWindow mw = new MainWindow();
            mw.Show();
            mainWindow.Close();
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            int selectedRow = medicineDataGrid.SelectedIndex;
            if (e.Key == Key.Enter && selectedRow != -1)
            {
                Medicine medicine = (Medicine)medicineDataGrid.SelectedItem;
                EditMedicineWindow editWindow = new EditMedicineWindow();
                FillForm(editWindow, medicine);
                editWindow.Show();
            }
        }

        private void FillForm(EditMedicineWindow editWindow, Medicine medicine)
        {
            editWindow.nameField.Text = medicine.Name;
            editWindow.codeField.Text = medicine.Id;
            editWindow.quanityField.Text = medicine.quantity.ToString();
            editWindow.alternativesField.ItemsSource = medicine.Alternatives;
            editWindow.ingridientsField.ItemsSource = ingridientsController.FindAll();
            Console.WriteLine("Broj selektovanih ingridienta je " + medicine.Ingridients.Count);
            SetAllSelected(editWindow, medicine.Ingridients);
        }

        private void SetAllSelected(EditMedicineWindow editWindow, List<string> igridients)
        {
            editWindow.ingridientsField.SelectedItems.Clear();
            foreach (string ingridient in igridients)
            {
                editWindow.ingridientsField.SelectedItems.Add(ingridient);
            }
        }




        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Add_Ingridients_Click(object sender, RoutedEventArgs e)
        {
            new AddIngridientsWindow().Show();
        }
    }
}
