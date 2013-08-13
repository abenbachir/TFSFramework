
namespace TeamFoundation.Common.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.Services;
    using System.Globalization;
    using System.Net;
    using System.Xml;
    using TeamFoundation.Common.Entities;
    using TeamFoundation.Common.Translators;
    using Microsoft.TeamFoundation.Common;
    using Microsoft.TeamFoundation.Server;

    public class TFSIterationPathProxy : TFSBaseProxy, ITFSIterationPathProxy
    {
        public TFSIterationPathProxy(Uri uri, ICredentials credentials) : base(uri, credentials)
        { }

        public IEnumerable<IterationPath> GetAllIterationPaths()
        {
            var css = this.TfsConnection.GetService<ICommonStructureService3>();
            var allStructures = css.ListAllProjects().SelectMany(p => css.ListStructures(p.Uri));
            var iterationPathsXml = css.GetNodesXml(allStructures.Where(s => s.StructureType.Equals(StructureType.ProjectLifecycle)).Select(a => a.Uri).ToArray(), true);
            var rootIterationPaths = iterationPathsXml.ChildNodes.Cast<XmlNode>().Where(a => a.FirstChild != null)
                .SelectMany(a => a.FirstChild.ChildNodes.Cast<XmlNode>()
                    .SelectMany(c => this.ParseIterationPathFromNodes(c)));

            return ExtractAllIterationPaths(rootIterationPaths).ToArray();
        }

        public IEnumerable<IterationPath> GetSubIterations(string rootIterationName)
        {
            var css = this.TfsConnection.GetService<ICommonStructureService3>();
            var allStructures = css.ListAllProjects().SelectMany(p => css.ListStructures(p.Uri));
            var iterationPathsXml = css.GetNodesXml(allStructures.Where(s => s.StructureType.Equals(StructureType.ProjectLifecycle)).Select(a => a.Uri).ToArray(), true);
            var iterations = ExtractAllIterationPaths(iterationPathsXml.ChildNodes.Cast<XmlNode>().Where(a => a.FirstChild != null)
                .SelectMany(a => a.FirstChild.ChildNodes.Cast<XmlNode>()
                    .SelectMany(c => this.ParseIterationPathFromNodes(c))));

            var encodedPath = EntityTranslator.EncodePath(string.Format(CultureInfo.InvariantCulture, "{0}\\", rootIterationName.TrimEnd('\\')));
            if (iterations.SingleOrDefault(a => a.Path.Equals(rootIterationName.TrimEnd('\\'))) == null)
            {
                throw new Exception(string.Format(CultureInfo.InvariantCulture, "The IterationPath specified could not be found: {0}", rootIterationName));
            }

            return iterations.Where(a => a.Path.StartsWith(encodedPath, StringComparison.OrdinalIgnoreCase)).ToArray();
        }

        public IEnumerable<IterationPath> GetIterationsByProject(string projectName)
        {
            var css = this.TfsConnection.GetService<ICommonStructureService3>();
            var allStructures = css.ListStructures(css.GetProjectFromName(projectName).Uri);
            var iterationPathsXml = css.GetNodesXml(allStructures.Where(s => s.StructureType.Equals(StructureType.ProjectLifecycle)).Select(a => a.Uri).ToArray(), true);
            var rootIterationPaths = iterationPathsXml.ChildNodes.Cast<XmlNode>().Where(a => a.FirstChild != null)
                .SelectMany(a => a.FirstChild.ChildNodes.Cast<XmlNode>().
                    SelectMany(c => this.ParseIterationPathFromNodes(c)));

            return ExtractAllIterationPaths(rootIterationPaths).ToArray();
        }

        private static IEnumerable<IterationPath> ExtractAllIterationPaths(IEnumerable<IterationPath> iterations)
        {
            var allIterations = new List<IterationPath>();
            if (iterations != null)
            {
                allIterations.AddRange(iterations.SelectMany(a => ExtractAllIterationPaths(a.SubIterations)));
                allIterations.AddRange(iterations);
            }

            return allIterations;
        }

        private IEnumerable<IterationPath> ParseIterationPathFromNodes(XmlNode currentNode)
        {
            var results = new List<IterationPath>();
            var subAreas = default(IEnumerable<IterationPath>);

            if (currentNode.ChildNodes != null)
            {
                var childrenNode = currentNode.ChildNodes.Cast<XmlNode>().SingleOrDefault(n => n.Name.Equals("Children"));
                if (childrenNode != null)
                {
                    subAreas = childrenNode.ChildNodes.Cast<XmlNode>().SelectMany(n => this.ParseIterationPathFromNodes(n));
                }
            }

            if (currentNode.Attributes["Name"] != null && currentNode.Attributes["Path"] != null)
            {
                results.Add(currentNode.ToModel(subAreas));
            }

            return results;
        }
    }
}
