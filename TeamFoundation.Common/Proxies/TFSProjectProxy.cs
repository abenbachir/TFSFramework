
namespace TeamFoundation.Common.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using TeamFoundation.Common.Translators;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;
    using Microsoft.TeamFoundation.Server;

    public class TFSProjectProxy : TFSBaseProxy, ITFSProjectProxy
    {
        public TFSProjectProxy(Uri uri, ICredentials credentials)
            : base(uri, credentials)
        {
        }

        public IEnumerable<ProjectInfo> GetProjectsByProjectCollection()
        {
            var css = this.TfsConnection.GetService<ICommonStructureService3>();
            var teamProjects = css.ListAllProjects();
            return teamProjects;
        }
      


    }
}
