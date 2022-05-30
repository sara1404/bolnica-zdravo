using Controller;
using hospital.Controller;
using hospital.View;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace hospital.ViewModel
{
    public class MedicinePageViewModel
    {
        public ObservableCollection<Medicine> Medicines { get; set; }
        public Medicine SelectedItem { get; set; }
        public ICommand EditMedicineCommand => new EditWindowCommand();
        public ICommand AddMedicineCommand => new AddMedicineCommand();
        public ICommand AddIngridientCommand => new AddIngridientCommand();
        private App app;
        private MedicineController medicineController;
        private IngridientsController ingridientsController;
        public MedicinePageViewModel()
        {
            app = Application.Current as App;
            medicineController = app.medicineController;
            ingridientsController = app.ingridientsController;

            Medicines = app.medicineController.FindAll();
        }

        //private void FillForm(EditMedicineWindow editWindow, Medicine medicine)
        //{
        //    editWindow.nameField.Text = medicine.Name;
        //    editWindow.codeField.Text = medicine.Id;
        //    editWindow.quanityField.Text = medicine.quantity.ToString();
        //    editWindow.alternativesField.ItemsSource = medicineController.FindAll();
        //    editWindow.ingridientsField.ItemsSource = ingridientsController.FindAll();
        //    SetAllSelectedIngridients(editWindow, medicine.Ingridients);
        //    SetAllSelectedAlternatives(editWindow, medicine.Alternatives);
        //}

        //private void SetAllSelectedIngridients(EditMedicineWindow editWindow, List<string> igridients)
        //{
        //    editWindow.ingridientsField.SelectedItems.Clear();
        //    foreach (string ingridient in igridients)
        //    {
        //        editWindow.ingridientsField.SelectedItems.Add(ingridient);
        //    }
        //}

        //private void SetAllSelectedAlternatives(EditMedicineWindow editWindow, List<Medicine> alternatives)
        //{
        //    editWindow.alternativesField.SelectedItems.Clear();
        //    foreach (Medicine alternative in alternatives)
        //    {
        //        editWindow.alternativesField.SelectedItems.Add(alternative);
        //    }
        //}

    }

    public class EditWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Medicine selected = parameter as Medicine;
            if (selected != null)
            {
                EditMedicineWindow editWindow = new EditMedicineWindow()
                {
                    DataContext = selected
                };
                editWindow.FillForm();
                editWindow.Show();
            }
        }
    }

    public class AddMedicineCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            new AddMedicineWindow().Show();
        }
    }

    public class AddIngridientCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            new AddIngridientsWindow().Show();
        }
    }

}
