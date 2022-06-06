using hospital.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace hospital.Commands
{
    public static class GlobalCommands
    {
        public static ICommand medicine_addWindow = new Medicine_AddWindow();
    }


    public class Medicine_AddWindow : ICommand
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
}
