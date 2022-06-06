using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace hospital.View.Manager
{
    public class FillTxtFieldCommand : IDemoCommand
    {
        private TextBox box;
        private string text;
        public FillTxtFieldCommand(TextBox box, string text)
        {
            this.box = box;
            this.text = text;
        }
        public void execute()
        {
            App.Current.Dispatcher.Invoke((Action)delegate {
                box.Text = text;
            });
        }
    }
}
