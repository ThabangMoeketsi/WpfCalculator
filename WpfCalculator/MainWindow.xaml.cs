using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CalculatorViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new CalculatorViewModel();
            DataContext = _viewModel;
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            _viewModel.AppendToDisplay(button?.Content?.ToString() ?? string.Empty);
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            _viewModel.SetOperator(button?.Content?.ToString() ?? string.Empty);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Clear();
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Calculate();
        }

        private void MemoryAdd_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MemoryAdd();
        }

        private void MemorySubtract_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MemorySubtract();
        }

        private void MemoryRecall_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MemoryRecall();
        }

        private void ShowHistory_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowHistory();
        }

    }
}