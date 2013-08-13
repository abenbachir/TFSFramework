
namespace TeamFoundation.Common.Repositories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Services;
    using System.Globalization;
    using System.Linq;
    
    using TeamFoundation.Common.Entities;
    using TeamFoundation.Common.ExpressionVisitors;
    using TeamFoundation.Common.Proxies;

    public class UserRepository : IRepository<User>
    {
        private readonly ITFSUserProxy proxy;

        public UserRepository(ITFSUserProxy proxy)
        {
            this.proxy = proxy;
        }

        public User Find(string userName)
        {
            return this.proxy.GetUserByUserName(userName);
        }

        public IEnumerable<User> FindAll()
        {
            throw new DataServiceException(501, "Not Implemented", "Service does not currently list all users. Try getting a specific user (e.g. Users('user@live.com') or Users('domain:user') -- replace backslash with ':' character)", "en-US", null);
        }

        public User Find(int id)
        {
            throw new NotImplementedException();
        }

        public User FindOneBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> FindBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }
    }
}
