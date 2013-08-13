using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFoundation.Common.Entities;

namespace TeamFoundation.Common.Translators
{
    public static class AttachmentTranslator
    {
        public static Attachment ToModel(this Microsoft.TeamFoundation.WorkItemTracking.Client.Attachment tfsAttachment, int workItemId, int index)
        {
            if (tfsAttachment == null)
            {
                throw new ArgumentNullException("tfsAttachment");
            }

            return new Attachment
            {
                Id = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", workItemId, index),
                WorkItemId = workItemId,
                Index = index,
                Name = tfsAttachment.Name,
                Extension = tfsAttachment.Extension,
                Comment = tfsAttachment.Comment,
                Length = tfsAttachment.Length,
                AttachedTime = tfsAttachment.AttachedTime,
                CreationTime = tfsAttachment.CreationTime,
                LastWriteTime = tfsAttachment.LastWriteTime,
                Uri = tfsAttachment.Uri.ToString()
            };
        }

        public static Microsoft.TeamFoundation.WorkItemTracking.Client.Attachment ToEntity(this Attachment attachmentModel, string path)
        {
            if (attachmentModel == null)
            {
                throw new ArgumentNullException("attachmentModel");
            }

            return new Microsoft.TeamFoundation.WorkItemTracking.Client.Attachment(path, attachmentModel.Comment);
        }
    }
}
