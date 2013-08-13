using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TeamFoundation.Common.Entities;

namespace TeamFoundation.Common.Translators
{
    public static class AreaPathTranslator
    {
        public static AreaPath ToModel(this XmlNode node, IEnumerable<AreaPath> subAreas)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            var pathElements = node.Attributes["Path"].Value.Trim('\\').Split('\\');
            if (pathElements != null && pathElements.Length > 1 && pathElements.ElementAt(1).Equals("Area"))
            {
                var parsedAreaPath = pathElements.ToList();
                parsedAreaPath.RemoveAt(1);
                pathElements = parsedAreaPath.ToArray();
            }

            return new AreaPath() { Name = node.Attributes["Name"].Value, Path = EntityTranslator.EncodePath(string.Join("\\", pathElements)), SubAreas = subAreas };
        }
    }
}
