using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KMA.APRZP2019.TextEditorProject.TextEditor
{
    public static class CustomCommands
    {
        static RoutedUICommand selectAll = new RoutedUICommand("Select All", 
            "SelectAll", 
            typeof(CustomCommands),
            new InputGestureCollection()
                {
                    new KeyGesture(Key.A, ModifierKeys.Control)
                });

        static RoutedUICommand clearAll = new RoutedUICommand("Clear All",
            "ClearAll",
            typeof(CustomCommands));

        public static RoutedUICommand SelectAll
        {
            get
            {
                return selectAll;
            }
        }

        public static RoutedUICommand ClearAll
        {
            get
            {
                return clearAll;
            }
        }
    }
}
