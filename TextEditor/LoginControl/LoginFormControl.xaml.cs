using KMA.APRZP2019.TextEditorProject.TextEditor;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LoginControl
{
    /// <summary>
    /// Interaction logic for LoginFormControl.xaml
    /// </summary>
    public partial class LoginFormControl : UserControl
    {
        public LoginFormControl()
        {
            InitializeComponent();
            //DataContext = new LogInViewModel();
        }

        private void LogIn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Username.Text) || String.IsNullOrWhiteSpace(Password.Password))
            {
                MessageBox.Show("Login or password is empty!");
                return;
            };
            MessageBox.Show($"Login successful for user {Username.Text}");
            
        }

        private void Register_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Username.Text) || String.IsNullOrWhiteSpace(Password.Password))
            {
                MessageBox.Show("Login or password is empty!");
                return;
            }
            MessageBox.Show($"User with name {Username.Text} was created");
            // todo switch to main window
        }
    }
}
