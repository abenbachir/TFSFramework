
namespace TeamFoundation.Common.Repositories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Services;
    using System.Globalization;
    using System.Linq;
    using TeamFoundation.Common.Proxies;
    using Microsoft.TeamFoundation.Common;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    public class AttachmentRepository : IRepository<Attachment>
    {
        private readonly ITFSAttachmentProxy m_proxy;

        public AttachmentRepository(ITFSAttachmentProxy proxy)
        {
            m_proxy = proxy;
        }

        public Attachment Find(int id)
        {
            throw new NotSupportedException();
        }
        public Attachment Find(string id)
        {
            TFCommonUtil.CheckForNull(id, "id"); 

            var ids = id.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (ids.Length != 2)
            {
                throw new ArgumentException("The id parameter must have the following pattern: 'workItemId-index'", "id");
            }

            var workItemId = 0;
            if (!int.TryParse(ids[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out workItemId))
            {
                throw new ArgumentException("The workItemId segment of the id parameter must be numeric", "id");
            }

            var index = 0;
            if (!int.TryParse(ids[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out index))
            {
                throw new ArgumentException("The index segment of the id parameter must be numeric", "id");
            }

            return this.m_proxy.GetAttachment(workItemId, index);
        }

        public Attachment FindOneBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Attachment> FindBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        // not supported
        public IEnumerable<Attachment> FindAll()
        {
            throw new NotSupportedException();
        }

        public IEnumerable<Attachment> FindByWorkItem(string workItemId)
        {
            var workItemIdNumber = 0;
            if (!int.TryParse(workItemId, NumberStyles.Integer, CultureInfo.InvariantCulture, out workItemIdNumber))
            {
                throw new ArgumentException("The workItemId parameter must be numeric", "workItemId");
            }

            return this.m_proxy.GetAttachmentsByWorkItem(workItemIdNumber);
        }
    }
}