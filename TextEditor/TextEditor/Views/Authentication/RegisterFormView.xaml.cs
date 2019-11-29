using KMA.APRZP2019.TextEditorProject.TextEditor.ViewModels;
using System.Windows.Controls;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Views.Authentication
{
    /// <summary>
    /// Interaction logic for RegisterFormView.xaml
    /// </summary>
    public partial class RegisterFormView : UserControl
    {
        public RegisterFormView()
        {
            InitializeComponent();
            RegisterViewModel registerViewModel = new RegisterViewModel();
            DataContext = registerViewModel;
        }
    }
}
