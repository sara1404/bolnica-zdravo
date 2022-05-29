using hospital.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace hospital.VM
{
    public class GoBackCommand : ICommand
    {
        private Frame Main;

        public GoBackCommand()
        {
            RetrieveMainFrame();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

            try
            {
                if (Main == null)
                {
                    RetrieveMainFrame();
                }
                Main.GoBack();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
           
        }

        public event EventHandler CanExecuteChanged;

        private void RetrieveMainFrame()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win.GetType() == typeof(ManagerMainWindow))
                {
                    Main = ((ManagerMainWindow)win).Main;
                }
            }
        }
    }
}
