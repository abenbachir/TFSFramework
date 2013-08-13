
namespace TeamFoundation.Common.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.Services;
    using System.Data.Services.Common;
    

    public class IterationPath
    {
        public string Path { get; set; }
        public string Name { get; set; }


        public IEnumerable<IterationPath> SubIterations { get; set; }
    }
}
