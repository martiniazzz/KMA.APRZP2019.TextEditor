using KMA.APRZP2019.TextEditorProject.TextEditor.ViewModels;
using System.Windows.Controls;

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
