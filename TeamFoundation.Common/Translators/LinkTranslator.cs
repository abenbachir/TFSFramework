using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFoundation.Common.Entities;

namespace TeamFoundation.Common.Translators
{
    public static class LinkTranslator
    {
        public static Link ToModel(this Microsoft.TeamFoundation.WorkItemTracking.Client.Link tfsLink, Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore witStore, int workItemId)
        {
            if (tfsLink == null)
            {
                throw new ArgumentNullException("tfsLink");
            }
            if (witStore == null)
            {
                throw new ArgumentNullException("witStore");
            }

            var rLinkTypes = witStore.RegisteredLinkTypes;
            var wiTypes = witStore.WorkItemLinkTypes;

            Link newLink = new Link();
            newLink.SourceWorkItemId = workItemId;
            newLink.BaseLinkType = tfsLink.BaseType.ToString();
            newLink.ArtifactLinkType = tfsLink.ArtifactLinkType.Name;
            newLink.Comment = tfsLink.Comment;

            switch (tfsLink.BaseType)
            {
                case Microsoft.TeamFoundation.WorkItemTracking.Client.BaseLinkType.ExternalLink:
                    var exLink = tfsLink as Microsoft.TeamFoundation.WorkItemTracking.Client.ExternalLink;
                    newLink.LinkedArtifactUri = exLink.LinkedArtifactUri;
                    break;

                case Microsoft.TeamFoundation.WorkItemTracking.Client.BaseLinkType.Hyperlink:
                    var hyperLink = tfsLink as Microsoft.TeamFoundation.WorkItemTracking.Client.Hyperlink;
                    newLink.Location = hyperLink.Location;
                    break;

                case Microsoft.TeamFoundation.WorkItemTracking.Client.BaseLinkType.RelatedLink:
                    var relLink = tfsLink as Microsoft.TeamFoundation.WorkItemTracking.Client.RelatedLink;
                    newLink.LinkTypeEnd = relLink.LinkTypeEnd.Name;
                    newLink.RelatedWorkItemId = relLink.RelatedWorkItemId;
                    break;

                case Microsoft.TeamFoundation.WorkItemTracking.Client.BaseLinkType.WorkItemLink:
                    var wiLink = tfsLink as Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemLink;
                    //newLink.AddedBy = wiLink.AddedBy;
                    //newLink.AddedDate = wiLink.AddedDate;
                    //newLink.ChangedDate = wiLink.ChangedDate.Value;
                    //newLink.RemovedBy = wiLink.RemovedBy;
                    //newLink.RemovedDate = wiLink.RemovedDate;
                    //newLink.TargetWorkItemId = wiLink.TargetId;
                    break;
            }

            return newLink;
        }
    }
}
