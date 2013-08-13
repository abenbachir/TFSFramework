
namespace TeamFoundation.Common.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Xml;
    using TeamFoundation.Common.Translators;
    using TeamFoundation.Common.Entities;
    using Microsoft.TeamFoundation.Common;
    using Microsoft.TeamFoundation.Server;

    public class TFSAreaPathProxy : TFSBaseProxy, ITFSAreaPathProxy
    {
        public TFSAreaPathProxy(Uri uri, ICredentials credentials)
            : base(uri, credentials)
        {
        }

        public IEnumerable<AreaPath> GetAllAreaPaths()
        {
            return this.RequestAllAreaPaths();
        }

        public IEnumerable<AreaPath> GetAreaPathsByProject(string projectName)
        {
            return this.RequestAllAreaPathsByProject(projectName);
        }

        public IEnumerable<AreaPath> GetSubAreas(string rootAreaName)
        {
            return this.RequestSubAreas(rootAreaName);
        }

        public IEnumerable<AreaPath> RequestAllAreaPaths()
        {
            var css = this.TfsConnection.GetService<ICommonStructureService3>();
            var allStructures = css.ListAllProjects().SelectMany(p => css.ListStructures(p.Uri));
            var areaPathsXml = css.GetNodesXml(allStructures.Where(s => s.StructureType.Equals(StructureType.ProjectModelHierarchy)).Select(a => a.Uri).ToArray(), true);
            var rootAreaPaths = areaPathsXml.ChildNodes.Cast<XmlNode>().Where(a => a.FirstChild != null).SelectMany(a => a.FirstChild.ChildNodes.Cast<XmlNode>().SelectMany(c => this.ParseAreaPathFromNodes(c)));

            return ExtractAllAreaPaths(rootAreaPaths).ToArray();
        }

        public IEnumerable<AreaPath> RequestAllAreaPathsByProject(string projectName)
        {
            var css = this.TfsConnection.GetService<ICommonStructureService3>();
            var allStructures = css.ListStructures(css.GetProjectFromName(projectName).Uri);
            var areaPathsXml = css.GetNodesXml(allStructures.Where(s => s.StructureType.Equals(StructureType.ProjectModelHierarchy)).Select(a => a.Uri).ToArray(), true);
            var rootAreaPaths = areaPathsXml.ChildNodes.Cast<XmlNode>().Where(a => a.FirstChild != null).SelectMany(a => a.FirstChild.ChildNodes.Cast<XmlNode>().SelectMany(c => this.ParseAreaPathFromNodes(c)));

            return ExtractAllAreaPaths(rootAreaPaths).ToArray();
        }

        public IEnumerable<AreaPath> RequestSubAreas(string rootAreaName)
        {
            var css = this.TfsConnection.GetService<ICommonStructureService3>();
            var allStructures = css.ListAllProjects().SelectMany(p => css.ListStructures(p.Uri));
            var areaPathsXml = css.GetNodesXml(allStructures.Where(s => s.StructureType.Equals(StructureType.ProjectModelHierarchy)).Select(a => a.Uri).ToArray(), true);
            var areas = ExtractAllAreaPaths(areaPathsXml.ChildNodes.Cast<XmlNode>().Where(a => a.FirstChild != null).SelectMany(a => a.FirstChild.ChildNodes.Cast<XmlNode>().SelectMany(c => this.ParseAreaPathFromNodes(c))));
  
            var encodedPath = EntityTranslator.EncodePath(string.Format(CultureInfo.InvariantCulture, "{0}\\", rootAreaName.TrimEnd('\\')));
            if (areas.SingleOrDefault(a => a.Path.Equals(rootAreaName.TrimEnd('\\'))) == null)
            {
                throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "The AreaPath specified could not be found: {0}", rootAreaName));
            }

            return areas.Where(a => a.Path.StartsWith(encodedPath, StringComparison.OrdinalIgnoreCase)).ToArray();
        }             

        private static IEnumerable<AreaPath> ExtractAllAreaPaths(IEnumerable<AreaPath> areas)
        {
            var allAreas = new List<AreaPath>();
            if (areas != null)
            {
                allAreas.AddRange(areas.SelectMany(a => ExtractAllAreaPaths(a.SubAreas)));
                allAreas.AddRange(areas);
            }

            return allAreas;
        }

        private IEnumerable<AreaPath> ParseAreaPathFromNodes(XmlNode currentNode)
        {
            var results = new List<AreaPath>();
            var subAreas = default(IEnumerable<AreaPath>);

            if (currentNode.ChildNodes != null)
            {
                var childrenNode = currentNode.ChildNodes.Cast<XmlNode>().SingleOrDefault(n => n.Name.Equals("Children"));
                if (childrenNode != null)
                {
                    subAreas = childrenNode.ChildNodes.Cast<XmlNode>().SelectMany(n => this.ParseAreaPathFromNodes(n));
                }
            }

            if (currentNode.Attributes["Name"] != null && currentNode.Attributes["Path"] != null)
            {
                results.Add(currentNode.ToModel(subAreas));
            }

            return results;
        }

        private string GetAllAreaPathsKey()
        {
            return string.Format(CultureInfo.InvariantCulture, "TFSAreaPathProxy.GetAllAreaPaths");
        }

        private string GetAreaPathsByProjectKey(string projectName)
        {
            return string.Format(CultureInfo.InvariantCulture, "TFSAreaPathProxy.GetAreaPathByProject_{0}", projectName);            
        }
    }
}
