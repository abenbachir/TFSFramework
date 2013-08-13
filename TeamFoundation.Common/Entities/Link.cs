
namespace TeamFoundation.Common.Entities
{
    using System.Collections.Generic;
    using System.Data.Services.Common;
    using System;

    public class Link
    {
        public int SourceWorkItemId { get; set; }

        public string ArtifactLinkType { get; set; }
        public string BaseLinkType { get; set; }
        public string Comment { get; set; }

        //external link
        public string LinkedArtifactUri { get; set; }

        //hyperlink
        public string Location { get; set; }

        //related link
        public string LinkTypeEnd { get; set; }
        public int RelatedWorkItemId { get; set; }

        //work item link
        //public string AddedBy { get; set; }
        //public DateTime AddedDate { get; set; }
        //public DateTime ChangedDate { get; set; }
        //public string RemovedBy { get; set; }
        //public DateTime RemovedDate { get; set; }
        //public int SourceId { get; set; }
        //public int TargetWorkItemId { get; set; }
    }
}
