using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IAgents<T> where T : class
    {
        IList<T> GetAgentList();

        void RegisterAgent(T agent);

        string GetAgentAddressFromId(int id);
    }
}
