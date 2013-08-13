
namespace TeamFoundation.Common.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Data.Services;
    using System.Data.Services.Common;

    public class Build
    {
        // Represents the Project Name
        public string Project { get; set; }

        // Represents the build definition name
        public string Definition { get; set; }

        public string Number { get; set; }

        public string Reason { get; set; }

        public string Quality { get; set; }

        public string Status { get; set; }

        public string RequestedBy { get; set; }

        public string RequestedFor { get; set; }

        public string LastChangedBy { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime FinishTime { get; set; }

        public DateTime LastChangedOn { get; set; }

        public bool BuildFinished { get; set; }

        public string DropLocation { get; set; }

        public string Errors { get; set; }

        public string Warnings { get; set; }


        public IEnumerable<WorkItem> WorkItems { get; set; }


        public IEnumerable<Changeset> Changesets { get; set; }
    }
}
