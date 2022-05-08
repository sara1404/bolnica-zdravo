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
using System.Windows.Shapes;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for AddMedicineWindow.xaml
    /// </summary>
    public partial class AddMedicineWindow : Window
    {
        private MedicineController medicineController;
        private RoomController roomController;
        private List<string> ingridients = new List<string>();
        public AddMedicineWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            medicineController = app.medicineController;
            roomController = app.roomController;
            ingridients.Add("kikiriki");
            ingridients.Add("silicijum-fosfat");
            ingridients.Add("kalcijum");
            ingridients.Add("natrijum-hlorid");
            ingridients.Add("penicilin");
            ingridientsField.ItemsSource = ingridients;
            alternativesField.ItemsSource = medicineController.FindAll();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Add_Medicine_Click(object sender, RoutedEventArgs e)
        {
            string name = nameField.Text;
            string code = codeField.Text;
            int quantity = Int32.Parse(quanityField.Text);
            Medicine medicine = new Medicine(code, name, GetIngridients(), name, quantity);
            if (medicineController.FindByName(medicine.Name) != null) {
                MessageBox.Show("Medicine with this name already exists");
                return;
            }
            medicine.Alternatives = GetAlternatives();
            medicineController.Create(medicine);
            Equipment equipment = new Equipment(name, quantity);
            roomController.FindRoomByPurpose("warehouse").AddEquipment(equipment);
            this.Close();
        }

        private List<Medicine> GetAlternatives() {
            List<Medicine> alternatives = new List<Medicine>();
            for (int i = 0; i < alternativesField.SelectedItems.Count; i++)
            {
                alternatives.Add((Medicine)alternativesField.SelectedItems[i]);
            }
            return alternatives;
        }

        private List<string> GetIngridients() {
            List<string> ingridients = new List<string>();
            for (int i = 0; i < ingridientsField.SelectedItems.Count; i++) {
                ingridients.Add((string)ingridientsField.SelectedItems[i]);
            }
            return ingridients;
        }

        private void Cancel_Medicine_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
