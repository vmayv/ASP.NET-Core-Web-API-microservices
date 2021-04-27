using MetricsManagerClient.Client;
using MetricsManagerClient.Requests;
using MetricsManagerClietn.DAL.Models;
using System;
using System.Collections.Generic;
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
            metricsManagerClient.GetAllAgents().Agents.ForEach(e => AgentsBox.Items.Add($"{e.AgentId}-{e.AgentAddress}"));
        }

        private void AgentsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int agentId = Convert.ToInt32(AgentsBox.SelectedItem.ToString().Split('-')[0]);
            string agentAddress = AgentsBox.SelectedItem.ToString().Split('-')[1];
            DateTimeOffset fromTime = DateTimeOffset.Parse("27.04.1980"); // значение для отладки
            DateTimeOffset toTime = DateTimeOffset.UtcNow;
            var cpuMetric = metricsManagerClient.GetAllCpuMetrics(new GetAllCpuMetricsRequest {AgentAddress = agentAddress, AgentId = agentId, FromTime = fromTime, ToTime = toTime });

            List<CpuMetric> metrics = new List<CpuMetric>();
            foreach (var metric in cpuMetric.Metrics)
            {
                metrics.Add(new CpuMetric {Time = metric.Time, AgentId = agentId, Value = metric.Value });
            }

            CpuChart.ColumnServiesValues[0].Values = (LiveCharts.IChartValues)metrics;
                        //CpuChart.ColumnServiesValues[0].Values = cpuMetrics..
        }
    }

}
