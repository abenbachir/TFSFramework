

namespace TeamFoundation.Common.Repositories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Services;
    using System.Globalization;
    using System.Linq;
    using TeamFoundation.Common.ExpressionVisitors;
    using TeamFoundation.Common.Proxies;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    public class LinkRepository : IRepository<Link>
    {
        private readonly ITFSLinkProxy m_proxy;

        public LinkRepository(ITFSLinkProxy proxy)
        {
            this.m_proxy = proxy;
        }

        //
        //TODO: add filtering support that makes its way to wiql
        public IEnumerable<Link> FindByWorkItem(string id)
        {
            var workItemId = 0;
            if (!int.TryParse(id, NumberStyles.Integer, CultureInfo.InvariantCulture, out workItemId))
            {
                throw new ArgumentException("The id parameter must be numeric", "workItemId");
            }

            return this.m_proxy.GetLinksByWorkItem(workItemId);
        }

        public Link Find(int id)
        {
            throw new NotImplementedException();
        }

        public Link Find(string id)
        {
            throw new NotImplementedException("Not Implemented");
        }        

        public Link FindOneBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Link> FindBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Link> FindAll()
        {
            throw new NotImplementedException("The 'Link' collection cannot be enumerated as a root collection. It should depend on a WorkItem.");
        }
    }
}
