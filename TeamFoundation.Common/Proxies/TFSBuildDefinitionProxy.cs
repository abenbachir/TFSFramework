
namespace TeamFoundation.Common.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Data.Services;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Web;
    using Microsoft.TeamFoundation.Build.Client;

    public class TFSBuildDefinitionProxy : TFSBaseProxy, ITFSBuildDefinitionProxy
    {
        public TFSBuildDefinitionProxy(Uri uri, ICredentials credentials)
            : base(uri, credentials)
        {
        }

        public IEnumerable<IBuildDefinition> GetBuildDefinitionsByProject(string projectName)
        {
            return this.RequestBuildDefinitionsByProject(projectName);
        }

        public void QueueBuild(string projectName, string defintionName)
        {
            var buildServer = TfsConnection.GetService<IBuildServer>();
            var spec = CreateBuildDefinitionSpec(buildServer, projectName);
            var preliminaryResults = buildServer.QueryBuildDefinitions(spec).Definitions.Where(bd => bd.Name == defintionName).FirstOrDefault();

            if (preliminaryResults == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The Build Definition specified could not be found (Project: {0}, Definition: {1})", projectName, defintionName));
            }

            buildServer.QueueBuild(preliminaryResults);
        }

        private static IBuildDefinitionSpec CreateBuildDefinitionSpec(IBuildServer buildServer, string projectName)
        {
            var detailSpec = buildServer.CreateBuildDefinitionSpec(projectName);
            return detailSpec;
        }


        private IEnumerable<IBuildDefinition> RequestBuildDefinitionsByProject(string projectName)
        {
            var buildServer = TfsConnection.GetService<IBuildServer>();

            var spec = CreateBuildDefinitionSpec(buildServer, projectName);
            var preliminaryResults = buildServer.QueryBuildDefinitions(spec).Definitions;

            return preliminaryResults.ToArray();
        }
    }
}
