
namespace TeamFoundation.Common.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    public class TFSAttachmentProxy : TFSBaseProxy, ITFSAttachmentProxy
    {
        public TFSAttachmentProxy(Uri uri, ICredentials credentials)
            : base(uri, credentials)
        {
        }

        public Attachment GetAttachment(int workItemId, int index)
        {
            var wiql = string.Format(CultureInfo.InvariantCulture, "SELECT [System.Id] FROM WorkItems WHERE [System.Id] = {0}", workItemId);
            var workItem = this.QueryWorkItems(wiql)
                                            .Cast<WorkItem>()
                                            .FirstOrDefault();

            if ((workItem != null) && (workItem.Attachments.Count > index))
            {
                return workItem.Attachments
                                .Cast<Attachment>()
                                .OrderBy(a => a.AttachedTimeUtc)
                                .ElementAt(index);
            }

            return null;
        }

        public IEnumerable<Attachment> GetAttachmentsByWorkItem(int workItemId)
        {
            var wiql = string.Format(CultureInfo.InvariantCulture, "SELECT [System.Id] FROM WorkItems WHERE [System.Id] = {0}", workItemId);
            var workItem = this.QueryWorkItems(wiql)
                                            .Cast<WorkItem>()
                                            .FirstOrDefault();
            return workItem.Attachments
                            .Cast<Attachment>()
                            .OrderBy(a => a.AttachedTimeUtc)
                            .ToArray();
        }

        public void CreateAttachment(int workItemId, string localPath, string comment)
        {
            if (!File.Exists(localPath))
                throw new FileNotFoundException(localPath);

            var wiql = string.Format(CultureInfo.InvariantCulture, "SELECT [System.Id] FROM WorkItems WHERE [System.Id] = {0}", workItemId);

            var workItem = this.QueryWorkItems(wiql).Cast<WorkItem>().FirstOrDefault();

            workItem.Attachments.Add(new Attachment(localPath, comment));
            workItem.Save();
        }


    }
}