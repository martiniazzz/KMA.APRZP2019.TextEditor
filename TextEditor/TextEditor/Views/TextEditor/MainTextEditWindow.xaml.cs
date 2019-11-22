using KMA.APRZP2019.TextEditorProject.TextEditor.ViewModels;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TextEditorView : UserControl
    {

        private bool _isSaved = true;

        public TextEditorView()
        {
            InitializeComponent();
            InitializeFontOptions();
            TextEditorViewModel textEditorViewModel = new TextEditorViewModel();
            DataContext = textEditorViewModel;
        }

        public bool IsSaved
        {
            get
            {
                return _isSaved;
            }
            set
            {
                _isSaved = value;
            }
        }

        private void InitializeFontOptions()
        {
            mFontsList.SelectionChanged += (s, e) => 
                MainTextBox.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, e.AddedItems[0]);
            
            mFontSizesList.SelectionChanged += (s, e) =>
                MainTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, e.AddedItems[0]);
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!_isSaved)
            {
                MessageBoxResult result = MessageBox.Show("Would you like to save the file?","Message", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes)
                {
                    Save_Executed(sender, e);
                }
                else if (result == MessageBoxResult.No)
                {
                    MainTextBox.Document.Blocks.Clear();
                }
            }
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!_isSaved)
            {
                MessageBoxResult result = MessageBox.Show("Would you like to save the file?", "Message", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Save_Executed(sender, e);
                }
                else if (result == MessageBoxResult.Cancel)
                    return;
            }

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(MainTextBox.Document.ContentStart, MainTextBox.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
                fileStream.Close();
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                using (FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create))
                {
                    TextRange range = new TextRange(MainTextBox.Document.ContentStart, MainTextBox.Document.ContentEnd);
                    range.Save(fileStream, DataFormats.Rtf);
                    _isSaved = true;
                }
            }
        }

        private void OnTextChanged(object sender, RoutedEventArgs e)
        {
            _isSaved = false;
        }

        private void OnTextBoxSelectionChanged(object sender, RoutedEventArgs e)
        {
            var textRange = new TextRange(MainTextBox.Selection.Start, MainTextBox.Selection.End);

            var fontFamily = textRange.GetPropertyValue(TextElement.FontFamilyProperty);
            mFontsList.SelectedItem = fontFamily;

            var fontSize = textRange.GetPropertyValue(TextElement.FontSizeProperty);
            mFontSizesList.Text = fontSize.ToString();

            if (!String.IsNullOrEmpty(textRange.Text))
            {
                TopBarButtonBold.IsChecked = textRange.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold);
                TopBarButtonItalic.IsChecked = textRange.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic);
                TopBarButtonUnderline.IsChecked = textRange.GetPropertyValue(Inline.TextDecorationsProperty).Equals(TextDecorations.Underline);
            }
        }

    }
}
