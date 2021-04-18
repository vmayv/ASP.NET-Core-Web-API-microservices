using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Dapper;
using MetricsManager.DAL.Models;
using Microsoft.Data.Sqlite;

namespace MetricsManager.DAL.Repositories
{
    public interface IAgentsRepository : IAgents<AgentInfo>
    {

    }
    public class AgentsRepository : IAgentsRepository
    {
        public string GetAgentAddressFromId(int id)
        {
            using (var connection = new SqliteConnection(SQLParams.ConnectionString))
            {
               return connection.QuerySingle("SELECT AgentAddress FROM agents WHERE AgentId = @AgentId", new { AgentId = id });
            }
        }

        public IList<AgentInfo> GetAgentList()
        {
            using (var connection = new SqliteConnection(SQLParams.ConnectionString))
            {
                var response = connection.Query<AgentInfo>("SELECT AgentId, AgentAddress FROM agents").ToList();

                return response;
            }
        }

        public void RegisterAgent(AgentInfo agent)
        {
            using (var connection = new SqliteConnection(SQLParams.ConnectionString))
            {
                connection.Execute("INSERT INTO agents(agentAddress) VALUES(@agentAddress)", new { agentAddress = agent.AgentAddress });
            }
        }
    }
}
