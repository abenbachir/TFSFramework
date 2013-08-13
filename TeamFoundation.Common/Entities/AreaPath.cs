
namespace TeamFoundation.Common.Entities
{
    using System.Collections.Generic;


    public class AreaPath
    {
        public string Path { get; set; }

        public string Name { get; set; }


        public IEnumerable<AreaPath> SubAreas { get; set; }
    }
}
