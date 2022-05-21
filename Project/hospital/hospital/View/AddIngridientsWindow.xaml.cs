using hospital.Controller;
using System;
using System.Windows;

namespace hospital.View
{
    public partial class AddIngridientsWindow : Window
    {
        private IngridientsController ingridientsController;
        public AddIngridientsWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            ingridientsController = app.ingridientsController;
        }

        private void Add_New_Ingridients(object sender, RoutedEventArgs e)
        {
            string[] ingridients = newIngridients.Text.Split(',');
            foreach (string ingridient in ingridients) {
                ingridientsController.Create(ingridient);
            }
            Close();
        }

        private void Cancel_Add_Ingridients(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}