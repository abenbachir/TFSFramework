// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

namespace TeamFoundation.Common.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TeamFoundation.Common.Proxies;
    using Microsoft.TeamFoundation.Server;

    public class ProjectRepository : IRepository<ProjectInfo>
    {
        private readonly ITFSProjectProxy m_proxy;

        public ProjectRepository(ITFSProjectProxy proxy)
        {
            m_proxy = proxy;
        }

        public ProjectInfo Find(string name)
        {
            return m_proxy.GetProjectsByProjectCollection().SingleOrDefault(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<ProjectInfo> FindAll()
        {
            return m_proxy.GetProjectsByProjectCollection();
        }

        public ProjectInfo Find(int id)
        {
            throw new NotImplementedException();
        }

        public ProjectInfo FindOneBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProjectInfo> FindBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }
    }
}
