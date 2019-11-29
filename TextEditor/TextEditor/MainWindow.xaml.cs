using KMA.APRZP2019.TextEditorProject.TextEditor.Services;
using KMA.APRZP2019.TextEditorProject.TextEditor.Tools;
using KMA.APRZP2019.TextEditorProject.TextEditor.Tools.interfaces;
using KMA.APRZP2019.TextEditorProject.TextEditor.ViewModels;
using System.Windows.Controls;

namespace KMA.APRZP2019.TextEditorProject.TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IContentWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var navigationModel = new NavigationModel(this);
            NavigationManager.Instance.Initialize(navigationModel);
            var mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;
            mainWindowViewModel.StartApplication();
        }

        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }
    }
}
