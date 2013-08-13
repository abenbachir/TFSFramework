namespace TeamFoundation.Common.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using Microsoft.TeamFoundation.VersionControl.Client;

        
    public class TFSChangeProxy : TFSBaseProxy, ITFSChangeProxy
    {
        public TFSChangeProxy(Uri uri, ICredentials credentials)
            : base(uri, credentials)
        {
        }

        public IEnumerable<Change> GetChangesByChangeset(int changesetId, int topRequestValue = int.MaxValue)
        {
            var versionControlServer = this.TfsConnection.GetService<VersionControlServer>();

            return versionControlServer.GetChangesForChangeset(changesetId, false, topRequestValue, null).ToArray();
        }
    

    }
}
