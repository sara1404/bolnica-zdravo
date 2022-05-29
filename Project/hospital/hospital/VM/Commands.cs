using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace hospital.VM
{
    public class Commands
    {
        public static ICommand GoBackCommand = new GoBackCommand();
        public static ICommand LogoutCommand = new LogoutCommand();
    }
}
