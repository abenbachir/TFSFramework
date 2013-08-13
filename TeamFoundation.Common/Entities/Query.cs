
namespace TeamFoundation.Common.Entities
{
    using System.Collections.Generic;



    public class Query
    {
        // The Query Guid
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string QueryText { get; set; }

        public string Path { get; set; }
        
        public IEnumerable<WorkItem> WorkItems { get; set; }

        public string Project { get; set; }

        public string QueryType { get; set; }
    }
}
