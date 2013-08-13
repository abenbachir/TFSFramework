
namespace TeamFoundation.Common.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using Microsoft.TeamFoundation.Server;
    using TeamFoundation.Common.Translators;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    public class TFSQueryProxy : TFSBaseProxy, ITFSQueryProxy
    {
        public TFSQueryProxy(Uri uri, ICredentials credentials) : base(uri, credentials)
        {
        }

        public QueryDefinition GetQuery(Guid id)
        {
            var workItemServer = this.TfsConnection.GetService<WorkItemStore>();
            return workItemServer.GetQueryDefinition(id);
        }

        public IEnumerable<QueryDefinition> GetQueriesByProjectCollection()
        {
            var workItemServer = this.TfsConnection.GetService<WorkItemStore>();
            return workItemServer.Projects
                                 .Cast<Project>().Where(p => p.HasWorkItemReadRights)
                                 .SelectMany(p => GetQueriesInHierarchy(p.QueryHierarchy, p.Name));
        }

        public IEnumerable<QueryDefinition> GetQueriesByProject(string projectName)
        {
            var workItemServer = this.TfsConnection.GetService<WorkItemStore>();
            return GetQueriesInHierarchy(workItemServer.Projects[projectName].QueryHierarchy, projectName);
        }

        private static List<QueryDefinition> GetQueriesInHierarchy(IEnumerable<QueryItem> queries, string path = "")
        {
            var queryItems = new List<QueryDefinition>();
            foreach (QueryItem queryItem in queries)
            {
                var queryPath = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", path, queryItem.Name);
                if (queryItem is QueryFolder)
                {
                    queryItems.AddRange(GetQueriesInHierarchy(queryItem as QueryFolder, queryPath));
                }
                else
                {
                    queryItems.Add(queryItem as QueryDefinition);
                }
            }

            return queryItems;
        }

    }
}
