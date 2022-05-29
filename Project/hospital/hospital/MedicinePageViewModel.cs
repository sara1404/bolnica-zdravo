using Controller;
using hospital.Controller;
using hospital.View;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace hospital.ViewModel
{
    public class MedicinePageViewModel
    {
        public List<Medicine> Medicines { get; set; }
        public Medicine SelectedItem { get; set; }
        public ICommand EditWindowCommand { get { return new EditWindowCommand(SelectedItem)} }
        private App app;
        private MedicineController medicineController;
        private IngridientsController ingridientsController;
        public MedicinePageViewModel()
        {
            app = Application.Current as App;
            medicineController = app.medicineController;
            ingridientsController = app.ingridientsController;

            Medicines = app.medicineController.FindAll().ToList();
        }


        public void Edit_Command_Execute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Edit_Command_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(SelectedItem != null)
            {
                EditMedicineWindow editWindow = new EditMedicineWindow();
                FillForm(editWindow, SelectedItem);
                editWindow.Show();
            }

        }

        private void FillForm(EditMedicineWindow editWindow, Medicine medicine)
        {
            editWindow.nameField.Text = medicine.Name;
            editWindow.codeField.Text = medicine.Id;
            editWindow.quanityField.Text = medicine.quantity.ToString();
            editWindow.alternativesField.ItemsSource = medicineController.FindAll();
            editWindow.ingridientsField.ItemsSource = ingridientsController.FindAll();
            SetAllSelectedIngridients(editWindow, medicine.Ingridients);
            SetAllSelectedAlternatives(editWindow, medicine.Alternatives);
        }

        private void SetAllSelectedIngridients(EditMedicineWindow editWindow, List<string> igridients)
        {
            editWindow.ingridientsField.SelectedItems.Clear();
            foreach (string ingridient in igridients)
            {
                editWindow.ingridientsField.SelectedItems.Add(ingridient);
            }
        }

        private void SetAllSelectedAlternatives(EditMedicineWindow editWindow, List<Medicine> alternatives)
        {
            editWindow.alternativesField.SelectedItems.Clear();
            foreach (Medicine alternative in alternatives)
            {
                editWindow.alternativesField.SelectedItems.Add(alternative);
            }
        }

    }

    public class EditWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Medicine selected;
        public EditWindowCommand(Medicine selected)
        {
            this.selected = selected;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Medicine selected = parameter as Medicine;
            Console.WriteLine(selected + " je selektovani red");

        }
    }
}
