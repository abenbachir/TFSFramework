
namespace TeamFoundation.Common.Entities
{
    using System.Collections.Generic;
    using System.Data.Services.Common;

    public class Project
    {
        public string Name { get; set; }

        // Team Project Collection Name
        public string Collection { get; set; }


        public IEnumerable<Changeset> Changesets { get; set; }


        public IEnumerable<Build> Builds { get; set; }


        public IEnumerable<BuildDefinition> BuildDefinitions { get; set; }


        public IEnumerable<WorkItem> WorkItems { get; set; }


        public IEnumerable<Query> Queries { get; set; }


        public IEnumerable<Branch> Branches { get; set; }


        public IEnumerable<AreaPath> AreaPaths { get; set; }


        public IEnumerable<IterationPath> IterationPaths { get; set; }
    }
}
