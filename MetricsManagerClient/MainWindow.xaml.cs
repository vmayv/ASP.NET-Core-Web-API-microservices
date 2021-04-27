using MetricsManagerClient.Client;
using MetricsManagerClient.Requests;
using System;
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
            metricsManagerClient.GetAllAgents().Agents.ForEach(e => AgentsBox.Items.Add(e.AgentId));
        }

        private void AgentsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int agentId = (int)AgentsBox.SelectedItem;
            DateTimeOffset fromTime = DateTimeOffset.Parse("27.04.1980"); // значение для отладки
            DateTimeOffset toTime = DateTimeOffset.UtcNow;
            var cpuMetric = metricsManagerClient.GetAllCpuMetrics(new GetAllCpuMetricsRequest { AgentId = agentId, FromTime = fromTime, ToTime = toTime });
                        //CpuChart.ColumnServiesValues[0].Values = cpuMetrics..
        }
    }

}
