using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TeamFoundation.Common.Exceptions;

namespace TeamFoundation.Common.Factories
{
    public class RepositoryFactory
    {
        private Dictionary<string, Object> m_repositories = new Dictionary<string, object>();

        public T GetRepository<T>(Object proxy)
        {
            string repoName = typeof(T).Name;
            if (m_repositories.ContainsKey(repoName))
                return (T)this.m_repositories[repoName];

            // create the repository
            T repository;
            lock (this.m_repositories)
            {
                repository = CreateRepository<T>(proxy);
                m_repositories.Add(repoName, repository);
            }
            return repository;
        }
        public static T CreateRepository<T>(Object proxy)
        {
            ConstructorInfo repositoryConstructor = typeof(T).GetConstructor(new Type[] { proxy.GetType() });
            if (repositoryConstructor == null)
                throw new InvalidOperationException("Type " + typeof(T).Name + " does not contain an appropriate constructor");

            T repository = (T)repositoryConstructor.Invoke(new object[] { proxy });
            return repository;
        }
    }
}
