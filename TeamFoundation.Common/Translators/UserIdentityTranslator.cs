using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFoundation.Common.Entities;

namespace TeamFoundation.Common.Translators
{
    public static class UserIdentityTranslator
    {
        public static User ToModel(this Microsoft.TeamFoundation.Framework.Client.TeamFoundationIdentity tfsIdentity, string userName)
        {
            return new User
            {
                DisplayName = tfsIdentity.DisplayName,
                UserName = userName,
                Id = tfsIdentity.TeamFoundationId.ToString()
            };
        }
    }
}
