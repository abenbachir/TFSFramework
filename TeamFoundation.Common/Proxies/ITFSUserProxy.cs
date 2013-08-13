
namespace TeamFoundation.Common.Proxies
{
    using System.Collections.Generic;
    using TeamFoundation.Common.Entities;

    public interface ITFSUserProxy
    {
        User GetUserByUserName(string userName);
    }
}