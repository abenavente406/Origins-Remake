using System.IO;
using System.Windows.Input;
using Microsoft.Win32;

namespace LayerMapEditor
{
    public partial class MainWindow
    {

        private void CommandBinding_Exit(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void CommandBinding_New(object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void CommandBinding_Save(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "level1";
            saveFile.DefaultExt = ".slf";
            saveFile.Filter = "SLF Level File (.slf)|*.slf";

            bool? result = saveFile.ShowDialog(this);
            if (result == true)
            {
                level.Save(saveFile.FileName);
            }
        }

        private void CommandBinding_Open(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.FileName = "";
            openFile.DefaultExt = "";
            openFile.Filter = "SLF Level File (.slf)|*.slf";

            bool? result = openFile.ShowDialog(this);
            if (result == true)
            {
                string fileName = openFile.FileName;
            }
        }

        private void CommandBinding_Undo(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void CommandBinding_Redo(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void CommandBinding_Cut(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void CommandBinding_Copy(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void CommandBinding_Paste(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
