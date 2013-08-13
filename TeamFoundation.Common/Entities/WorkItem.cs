
namespace TeamFoundation.Common.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Data.Services;
    using System.Data.Services.Common;

    //Field references:
    //http://msdn.microsoft.com/en-us/library/ms194971.aspx

    public class WorkItem
    {
        //Base information to provide in addition to WorkItem fields
        public int Id { get; set; }
        public string Project { get; set; }
        public string Type { get; set; }
        public string WebEditorUrl { get; set; }

        //Base fields
        public string AreaPath { get; set; }
        public string IterationPath { get; set; }
        public int Revision { get; set; } //aka Rev
        public string Priority { get; set; }
        public string Severity { get; set; }
        public double StackRank { get; set; }
        public string AssignedTo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ChangedDate { get; set; }
        public string ChangedBy { get; set; }
        public string ResolvedBy { get; set; }
        public string Title { get; set; }
        public string State { get; set; } //valid states and transitions specific to template and work item
        public string Reason { get; set; }
        public double CompletedWork { get; set; }
        public double RemainingWork { get; set; }
        public string Description { get; set; }
        public string ReproSteps { get; set; }
        public string FoundInBuild { get; set; }
        public string IntegratedInBuild { get; set; }
        public int AttachedFileCount { get; set; }
        public int HyperLinkCount { get; set; }
        public int RelatedLinkCount { get; set; }

        //Agile
        public string Risk { get; set; }
        public double StoryPoints { get; set; }

        //Agile and CMMI
        public double OriginalEstimate { get; set; }

        //Scrum
        public double BacklogPriority { get; set; }
        public int BusinessValue { get; set; }
        public double Effort { get; set; }

        //Scrum and CMMI
        public string Blocked { get; set; }

        //CMMI
        public double Size { get; set; }


        
        public IEnumerable<Attachment> Attachments { get; set; }

        
        public IEnumerable<Link> Links { get; set; }
    }
}
