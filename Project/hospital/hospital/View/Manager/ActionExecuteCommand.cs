using hospital.View.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.View
{
    public class ActionExecuteCommand : IDemoCommand
    {
        private Action function;
        public ActionExecuteCommand(Action function)
        {
            this.function = function;
        }
        public void execute()
        {
            App.Current.Dispatcher.Invoke((Action)delegate {
                function();
            });
        }
    }
}
