
namespace TeamFoundation.Common.Proxies
{
    using System.Collections.Generic;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    public interface ITFSLinkProxy
    {
        IEnumerable<Link> GetLinksByWorkItem(int workItemId);
    }
}