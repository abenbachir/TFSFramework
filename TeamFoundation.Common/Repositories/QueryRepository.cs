
namespace TeamFoundation.Common.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TeamFoundation.Common.Proxies;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    public class QueryRepository : IRepository<QueryDefinition>
    {
        private readonly ITFSQueryProxy m_proxy;

        public QueryRepository(ITFSQueryProxy proxy)
        {
            m_proxy = proxy;
        }
        public QueryDefinition Find(Guid guid)
        {
            return m_proxy.GetQuery(guid);
        }
        public QueryDefinition Find(string id)
        {
            Guid queryId;
            if (!Guid.TryParse(id, out queryId))
            {
                throw new ArgumentException("The parameter id should be a GUID", "id");
            }

            return m_proxy.GetQuery(queryId);
        }

        public IEnumerable<QueryDefinition> FindByProject(string projectName)
        {
            return m_proxy.GetQueriesByProject(projectName);
        }

        public IEnumerable<QueryDefinition> FindAll()
        {
            return m_proxy.GetQueriesByProjectCollection();
        }

        public QueryDefinition Find(int id)
        {
            throw new NotImplementedException();
        }

        public QueryDefinition FindOneBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QueryDefinition> FindBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }
    }
}
