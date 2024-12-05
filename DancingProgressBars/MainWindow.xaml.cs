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

namespace DancingProgressBars
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ProgressBar> progressBars;
        private CancellationTokenSource cancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            int numBars;
            if (!int.TryParse(numBarsTextBox.Text, out numBars))
            {
                MessageBox.Show("Invalid number of progress bars.");
                return;
            }

            progressBars = new List<ProgressBar>();
            for (int i = 0; i < numBars; i++)
            {
                ProgressBar progressBar = new ProgressBar();
                progressBars.Add(progressBar);
                progressBarsListBox.Items.Add(progressBar);
            }


            cancellationTokenSource = new CancellationTokenSource();

 
            foreach (ProgressBar progressBar in progressBars)
            {
                Task.Run(() => DanceProgressBar(progressBar, cancellationTokenSource.Token));
            }
        }

        private void DanceProgressBar(ProgressBar progressBar, CancellationToken cancellationToken)
        {
            Random random = new Random();

            while (!cancellationToken.IsCancellationRequested)
            {
         
                int progress = random.Next(0, 101);
                progressBar.Dispatcher.Invoke(() => progressBar.Value = progress);

                Color color = Color.FromRgb((byte)random.Next(0, 256), (byte)random.Next(0, 256), (byte)random.Next(0, 256));
                progressBar.Dispatcher.Invoke(() => progressBar.Foreground = new SolidColorBrush(color));
                Thread.Sleep(random.Next(100, 500));
            }
        }
    }
}