
namespace TeamFoundation.Common.Proxies
{
    using System;
    using System.Collections.Generic;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    public interface ITFSQueryProxy
    {
        QueryDefinition GetQuery(Guid id);

        IEnumerable<QueryDefinition> GetQueriesByProject(string projectName);

        IEnumerable<QueryDefinition> GetQueriesByProjectCollection();        
    }
}
