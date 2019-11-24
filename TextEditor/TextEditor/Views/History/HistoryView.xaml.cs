using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.TextEditor.Services;
using KMA.APRZP2019.TextEditorProject.TextEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Views.History
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class HistoryView : UserControl
    {
        public HistoryView()
        {
            InitializeComponent();
            HistoryViewModel historyViewModel = new HistoryViewModel();
            DataContext = historyViewModel;
        }

    }
}
