using MetricsManagerClient.Client;
using MetricsManagerClient.Requests;
using System.Windows;
using System.Windows.Controls;

namespace MetricsManagerClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMetricsManagerClient metricsManagerClient;

        public MainWindow()
        {
            InitializeComponent();
            metricsManagerClient = new Client.MetricsManagerClient(new System.Net.Http.HttpClient());
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AgentsBox.Items.Clear();
            metricsManagerClient.GetAllAgents().Agents.ForEach(e => AgentsBox.Items.Add(e.AgentAddress));
        }

        private void AgentsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string agentUrl = AgentsBox.SelectedItem as string;
            
            var cpuMetric = metricsManagerClient.GetAllCpuMetrics(new GetAllCpuMetricsRequest { ManagerBaseAddress = agentUrl });
                        //CpuChart.ColumnServiesValues[0].Values = cpuMetrics..
        }
    }

}
