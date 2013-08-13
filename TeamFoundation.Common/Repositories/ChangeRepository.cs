

namespace TeamFoundation.Common.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Services;
    using System.Globalization;
    using System.Linq;
    using TeamFoundation.Common.ExpressionVisitors;
    using TeamFoundation.Common.Proxies;
    using Microsoft.TeamFoundation.VersionControl.Client;

    public class ChangeRepository : IRepository<Change>
    {
        private readonly ITFSChangeProxy m_proxy;

        public ChangeRepository(ITFSChangeProxy proxy)
        {
            this.m_proxy = proxy;
        }
       
        public Change Find(int id)
        {
            throw new NotImplementedException();
        }

        public Change Find(string id)
        {
            throw new NotImplementedException();
        }

        public Change FindOneBy(int changesetId, string path)
        {
            return m_proxy.GetChangesByChangeset(changesetId, 1)
                .SingleOrDefault(c => c.Item.ServerItem.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

        public Change FindOneBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Change> FindBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Change> FindBy(int changesetId)
        {
            return m_proxy.GetChangesByChangeset(changesetId); 
        }

        public IEnumerable<Change> FindAll()
        {
            throw new NotImplementedException("The 'Change' collection cannot be enumerated as a root collection. It should depend on a Changeset.");
        }
    }
}