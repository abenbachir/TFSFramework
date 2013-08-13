
namespace TeamFoundation.Common.Proxies
{
    using System.Collections.Generic;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    public interface ITFSAttachmentProxy
    {
        Attachment GetAttachment(int workItemId, int index);

        IEnumerable<Attachment> GetAttachmentsByWorkItem(int workItemId);

        void CreateAttachment(int workItemId, string localPath, string comment);
    }
}
