
namespace TeamFoundation.Common.Entities
{
    using System;
    using System.Collections.Generic;


    public class Changeset
    {
        public int Id { get; set; }

        public string ArtifactUri { get; set; }

        public string Comment { get; set; }

        public string Committer { get; set; }
        
        public DateTime CreationDate { get; set; }

        public string Owner { get; set; }

        public string Branch { get; set; }

        public string WebEditorUrl { get; set; }


        public IEnumerable<Change> Changes { get; set; }


        public IEnumerable<WorkItem> WorkItems { get; set; }
    }
}
