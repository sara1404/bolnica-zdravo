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
using Controller;
using Model;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for EditMedicineWindow.xaml
    /// </summary>
    public partial class EditMedicineWindow : Window
    {
        private MedicineController medicineController;

        public EditMedicineWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            medicineController = app.medicineController;
            codeField.IsEnabled = false;
        }

        private void Cancel_Medicine_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Close_Window(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void Edit_Medicine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Validate();
                EditMedicine();
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void EditMedicine()
        {
            Medicine newMedicine = new Medicine(codeField.Text, nameField.Text, GetIngridients(), nameField.Text,
                Int32.Parse(quanityField.Text));
            newMedicine.Alternatives = GetAlternatives();
            medicineController.UpdateById(codeField.Text, newMedicine);
        }

        private void Validate()
        {
            CheckIfEditable();
            ValidateQuantity();
            ValidateIngridients();
        }

        private void CheckIfEditable()
        {
            if (medicineController.FindById(codeField.Text).Status.Equals("approved"))
                throw new Exception("Editing an approved medicine is not allowed!");
        }

        private void ValidateQuantity() {
            int value;
            bool isValid = Int32.TryParse(quanityField.Text, out value);

            if (!isValid)
                throw new Exception("Quantity should be a number!");

            if (value < 0)
                throw new Exception("Quantity should be positive!");
        }
        
        private void ValidateIngridients() {
            if (GetIngridients().Count == 0)
                throw new Exception("There should be at least one ingredient!");
        }

        private List<string> GetIngridients()
        {
            List<string> ingridients = new List<string>();
            for (int i = 0; i < ingridientsField.SelectedItems.Count; i++)
            {
                ingridients.Add((string)ingridientsField.SelectedItems[i]);
            }

            return ingridients;
        }

        private List<Medicine> GetAlternatives()
        {
            List<Medicine> alternatives = new List<Medicine>();
            for (int i = 0; i < alternativesField.SelectedItems.Count; i++)
            {
                alternatives.Add((Medicine)alternativesField.SelectedItems[i]);
            }
            return alternatives;
        }

        private void FormFilled(object sender, TextChangedEventArgs e)
        {
            FormFilled();
        }

        private void FormFilled(object sender, SelectionChangedEventArgs e)
        {
            FormFilled();
        }

        private void FormFilled()
        {
            if (nameField.Text != "" && ingridientsField.SelectedItems.Count != 0 || quanityField.Text != "")
            {
                confirmBtn.IsEnabled = true;
                return;
            }
            confirmBtn.IsEnabled = false;
        }
    }
}
