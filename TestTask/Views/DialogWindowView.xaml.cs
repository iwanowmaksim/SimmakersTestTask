using System.Windows;

namespace TestTask
{
    public partial class DialogWindowView : Window
    {
        public DialogWindowView()
        {
            InitializeComponent();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
