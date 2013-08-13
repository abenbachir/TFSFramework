
namespace TeamFoundation.Common.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Web;
    using TeamFoundation.Common.Entities;
    using Microsoft.TeamFoundation.VersionControl.Client;

    public class TFSBranchProxy : TFSBaseProxy, ITFSBranchProxy
    {
        public TFSBranchProxy(Uri uri, ICredentials credentials)
            : base(uri, credentials)
        {
        }

        public BranchObject GetBranch(string path)
        {            
            var versionControlServer = this.TfsConnection.GetService<VersionControlServer>();

            var identifier = new Microsoft.TeamFoundation.VersionControl.Client.ItemIdentifier(path);
            return versionControlServer.QueryBranchObjects(identifier, RecursionType.None)
                                                .Where(b => !b.Properties.RootItem.IsDeleted)
                                                .SingleOrDefault();
        }

        public IEnumerable<BranchObject> GetBranchesByProjectCollection()
        {
            var versionControlServer = this.TfsConnection.GetService<VersionControlServer>();
            var rootObjects = versionControlServer.QueryRootBranchObjects(RecursionType.None);

            return rootObjects.SelectMany(r => versionControlServer
                                                .QueryBranchObjects(r.Properties.RootItem, RecursionType.Full)
                                                .Where(b => !b.Properties.RootItem.IsDeleted))
                                                .ToArray();
        }

        public IEnumerable<BranchObject> GetBranchesByProject(string projectName)
        {
            var versionControlServer = this.TfsConnection.GetService<VersionControlServer>();
            var rootObjects = versionControlServer.QueryRootBranchObjects(RecursionType.None);
            var projectPath = string.Format(CultureInfo.InvariantCulture, "$/{0}/", projectName);

            return rootObjects.SelectMany(r => versionControlServer
                                                .QueryBranchObjects(r.Properties.RootItem, RecursionType.Full)
                                                .Where(b => !b.Properties.RootItem.IsDeleted && b.Properties.RootItem.Item.StartsWith(projectPath, StringComparison.OrdinalIgnoreCase))).ToArray();
        }


    }
}
