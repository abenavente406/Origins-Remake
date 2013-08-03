using System.Windows;
using LayerMapEditor.Engine;

namespace LayerMapEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Level level;

        public MainWindow()
        {
            InitializeComponent();

            level = new Level(40, 40, 32, 32);
        }
    }
}
