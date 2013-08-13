
namespace TeamFoundation.Common.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using TeamFoundation.Common.Translators;
    using Microsoft.TeamFoundation.Server;
    using System.Globalization;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    public class TFSLinkProxy : TFSBaseProxy, ITFSLinkProxy
    {
        public TFSLinkProxy(Uri uri, ICredentials credentials)
            : base(uri, credentials)
        {

        }

        public IEnumerable<Link> GetLinksByWorkItem(int workItemId)
        {
            var wiql = string.Format(CultureInfo.InvariantCulture, "SELECT [System.Id] FROM WorkItems WHERE [System.Id] = {0}", workItemId);

            var workItemServer = this.TfsConnection.GetService<WorkItemStore>();

            var wiColl = this.QueryWorkItems(wiql);
            if (wiColl.Count == 0)
                return new List<Link>();

            return wiColl[0].Links.Cast<Link>().ToList();

        }

    }
}