using KMA.APRZP2019.TextEditorProject.TextEditor.ViewModels;
using System.Windows.Controls;

namespace LoginControl
{
    /// <summary>
    /// Interaction logic for LoginFormControl.xaml
    /// </summary>
    public partial class LoginFormView : UserControl
    {
        public LoginFormView()
        {
            InitializeComponent();
            LoginViewModel loginViewModel = new LoginViewModel();
            DataContext = loginViewModel;
        }

    }
}
