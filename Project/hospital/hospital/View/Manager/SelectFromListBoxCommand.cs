using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace hospital.View.Manager
{
    public class SelectFromListBoxCommand : IDemoCommand
    {
        private ListView roomRenovationListView;
        private int index;
        public SelectFromListBoxCommand(ListView roomRenovationListView, int index)
        {
            this.roomRenovationListView = roomRenovationListView;
            this.index = index;
        }
        public void execute()
        {
            App.Current.Dispatcher.Invoke((Action)delegate {
                roomRenovationListView.SelectedIndex = index;
            });
        }
    }
}
