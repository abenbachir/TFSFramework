
namespace TeamFoundation.Common.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using TeamFoundation.Common.Entities;
    using TeamFoundation.Common.Translators;
    using Microsoft.TeamFoundation.Server;
    using System.Globalization;
    using Microsoft.TeamFoundation.Framework.Client;
    using Microsoft.TeamFoundation.Framework.Common;

    public class TFSUserProxy : TFSBaseProxy, ITFSUserProxy
    {
        public TFSUserProxy(Uri uri, ICredentials credentials)
            : base(uri, credentials)
        {

        }

        public User GetUserByUserName(string userName)
        {
            //var key = string.Format(CultureInfo.InvariantCulture, "TFSUserProxy.GetUserByEmail_{0}", userName);

            throw new NotImplementedException();

            //return (User)HttpContext.Current.Items[key];
        }

        private User RequestUserByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            { throw new ArgumentNullException("userName"); }
            if (userName.Contains(":"))
            {
                userName = userName.Replace(":", "\\");
            }

            IIdentityManagementService gss = (IIdentityManagementService)this.TfsConnection.GetService(typeof(IIdentityManagementService));
            TeamFoundationIdentity identity = gss.ReadIdentity(IdentitySearchFactor.General, userName, MembershipQuery.Expanded, ReadIdentityOptions.ExtendedProperties);

            if (identity == null)
            {
                throw new System.Data.Services.DataServiceException(404, "Not Found", "User lookup failed", "en-US", null);
            }

            return identity.ToModel(userName);
        }
    }
}