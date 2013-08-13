using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Common;
using Microsoft.TeamFoundation.Framework.Common;

namespace TeamFoundation.Common.Factories
{
    
    public class TfsConnectionFactory : IDisposable
    {
        private Dictionary<Uri, Object> m_serverCache = new Dictionary<Uri, object>();
        private static object m_syncRoot = new Object();
        private static volatile TfsConnectionFactory m_instance;

        public static TfsConnectionFactory Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        m_instance = new TfsConnectionFactory();
                    }
                }
                return m_instance;
            }
        }

        protected TfsConnectionFactory()
        {
            
        }
        ~TfsConnectionFactory()
        {
            Dispose();
        }
        public TfsTeamProjectCollection GetTeamProjectCollection(Uri uri, TfsClientCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public TfsTeamProjectCollection GetTeamProjectCollection(Uri uri, ICredentials credentials)
        {
            TFCommonUtil.CheckForNull(uri, "uri");
            string text = UriUtility.GetInvariantAbsoluteUri(uri);

            TfsTeamProjectCollection tfsTeamProjectCollection = null;
            if (text.EndsWith(LocationServiceConstants.CollectionLocationServiceRelativePath, StringComparison.OrdinalIgnoreCase))
            {
                string relativePath = text.Remove(text.Length - LocationServiceConstants.CollectionLocationServiceRelativePath.Length);
                text = UriUtility.TrimEndingPathSeparator(relativePath);
            }
            lock (m_serverCache)
            {
                uri = new Uri(text);
                if (m_serverCache.ContainsKey(uri))
                {
                    tfsTeamProjectCollection = m_serverCache[uri] as TfsTeamProjectCollection;
                }
                if (tfsTeamProjectCollection == null || tfsTeamProjectCollection.Disposed)
                {
                    tfsTeamProjectCollection = new TfsTeamProjectCollection(uri, credentials);
                    m_serverCache[uri] = tfsTeamProjectCollection;
                }
            }
            return tfsTeamProjectCollection;
        }


        public void Dispose()
        {
            foreach (var item in m_serverCache)
                ((IDisposable)item.Value).Dispose();
            m_serverCache.Clear();
            GC.SuppressFinalize(this);
        }
    }
}
