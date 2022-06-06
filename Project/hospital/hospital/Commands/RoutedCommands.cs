using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace hospital.Commands
{
    public static class RoutedCommands
    {
        public static readonly RoutedUICommand Edit_Command = new RoutedUICommand(
           "Edit Command",
           "EditCommand",
           typeof(RoutedCommands),
           new InputGestureCollection()
           {
                new KeyGesture(Key.Enter),
           }
           );

        public static readonly RoutedUICommand Enable = new RoutedUICommand(
            "Enable",
            "Enable",
            typeof(RoutedCommands)
            );

        public static readonly RoutedUICommand Komanda = new RoutedUICommand(
            "Komanda",
            "Komanda",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.K, ModifierKeys.Control),
            }
            );

        public static readonly RoutedUICommand Ugradjene = new RoutedUICommand(
            "Ugradjene Komande",
            "UgradjeneKomande",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.U, ModifierKeys.Control),
            }
            );
    }
}
