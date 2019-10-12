using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool _isSaved = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeFontOptions();
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
              mMainTextBox.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, e.AddedItems[0]);

            mFontSizesList.SelectionChanged += (s, e) =>
              mMainTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, e.AddedItems[0]);
        }

        private void SelectAll_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SelectAll_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mMainTextBox.SelectAll();
        }

        private void ClearAll_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ClearAll_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mMainTextBox.Document.Blocks.Clear();
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!_isSaved)
            {
                //SaveChangesDialog();
                Save_Executed(sender, e);
            }
            mMainTextBox.Document.Blocks.Clear();
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(mMainTextBox.Document.ContentStart, mMainTextBox.Document.ContentEnd);
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
                    TextRange range = new TextRange(mMainTextBox.Document.ContentStart, mMainTextBox.Document.ContentEnd);
                    range.Save(fileStream, DataFormats.Rtf);
                }
            }
        }

    }
}
