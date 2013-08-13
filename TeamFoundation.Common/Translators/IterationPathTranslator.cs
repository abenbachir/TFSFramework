using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TeamFoundation.Common.Entities;

namespace TeamFoundation.Common.Translators
{
    public static class IterationPathTranslator
    {
        public static IterationPath ToModel(this XmlNode node, IEnumerable<IterationPath> subIterations)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            var pathElements = node.Attributes["Path"].Value.Trim('\\').Split('\\');
            if (pathElements != null && pathElements.Length > 1 && pathElements.ElementAt(1).Equals("Iteration"))
            {
                var parsedIterationPath = pathElements.ToList();
                parsedIterationPath.RemoveAt(1);
                pathElements = parsedIterationPath.ToArray();
            }

            return new IterationPath() { Name = node.Attributes["Name"].Value, Path = EntityTranslator.EncodePath(string.Join("\\", pathElements)), SubIterations = subIterations };
        }
    }
}
