
namespace TeamFoundation.Common.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Data.Services;
    using System.Data.Services.Common;


    public class Branch
    {
        public string Path { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public IEnumerable<Changeset> Changesets { get; set; }
    }
}
