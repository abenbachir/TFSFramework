
namespace TeamFoundation.Common.Proxies
{
    using System;
    using System.Data.Services;
    using System.Linq;
    using System.Net;
    using TeamFoundation.Common.ExpressionVisitors;
    using TeamFoundation.Common.Factories;
    using Microsoft.TeamFoundation;
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    public class TFSBaseProxy : IDisposable
    {
        private TfsConnection m_tfsConnection;

        public TFSBaseProxy(Uri tfsCollection, ICredentials credentials)
        {
            if (tfsCollection.ToString().ToLowerInvariant().Contains("visualstudio.com"))
            {
                this.TfsConnection = TFSBaseProxy.SetupTFSConnection(tfsCollection, credentials);
            }
            else
            {
                //this.TfsConnection = new TfsTeamProjectCollection(tfsCollection, credentials);
                this.TfsConnection = TfsConnectionFactory.Instance.GetTeamProjectCollection(tfsCollection, credentials);
            }
        }

        protected TfsConnection TfsConnection
        {
            get { return this.m_tfsConnection; }
            set { this.m_tfsConnection = value; }
        }

        //specific to hosted TFS service 
        private static TfsTeamProjectCollection SetupTFSConnection(Uri tfsCollectionUri, ICredentials credentials)
        {
            UriBuilder uriBuild = new UriBuilder(tfsCollectionUri);
            //using domain from creds to specify visualstudio.com account
            string domain = (credentials as NetworkCredential).Domain;

            string accountAndHost = domain + "." + uriBuild.Host;
            uriBuild.Host = accountAndHost;
            tfsCollectionUri = uriBuild.Uri;

            NetworkCredential fullCreds = credentials as NetworkCredential;
            NetworkCredential fixedCreds = new NetworkCredential(fullCreds.UserName, fullCreds.Password);

            BasicAuthToken bTok = new BasicAuthToken(fixedCreds);
            BasicAuthCredential baCred = new BasicAuthCredential(bTok);

            TfsClientCredentials tfsClientCreds = new TfsClientCredentials(baCred);

            TfsTeamProjectCollection conn = TfsConnectionFactory.Instance.GetTeamProjectCollection(tfsCollectionUri, tfsClientCreds);

            conn.Connect(Microsoft.TeamFoundation.Framework.Common.ConnectOptions.None);
            conn.EnsureAuthenticated();

            return conn;
        }

        public static bool CollectionExists(Uri collectionUri, ICredentials credentials, out bool isAuthorized)
        {
            TfsTeamProjectCollection collection = null;

            try
            {
                if (collectionUri.ToString().ToLowerInvariant().Contains("visualstudio.com"))
                {
                    collection = TFSBaseProxy.SetupTFSConnection(collectionUri, credentials);
                }
                else
                {
                    collection = TfsConnectionFactory.Instance.GetTeamProjectCollection(collectionUri, credentials);
                }

                collection.EnsureAuthenticated();
                isAuthorized = true;

                return true;
            }
            catch (TeamFoundationServerUnauthorizedException)
            {
                isAuthorized = false;

                return true;
            }
            catch
            {
                isAuthorized = false;

                return false;
            }
            finally
            {
                if (collection != null)
                {
                    collection.Dispose();
                }
                collection = null;
            }
        }

        public static bool IsAuthenticated(Uri tfsUri, ICredentials credentials)
        {
            return true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected WorkItemCollection QueryWorkItems(string wiql)
        {
            var workItemServer = this.TfsConnection.GetService<WorkItemStore>();
            Query q = new Query(workItemServer, wiql, null, false);
            return q.RunQuery();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (this.m_tfsConnection != null)
                //{
                //    this.m_tfsConnection.Dispose();
                //    this.m_tfsConnection = null;
                //}
            }
        }
        
        protected Uri GetTfsWebAccessArtifactUrl(Uri uri)
        {
            var hyperlinkService = this.TfsConnection.GetService<TswaClientHyperlinkService>();

            // For CodePlex TFS we need to convine the base URL with the path and query.
            return new Uri(this.TfsConnection.Uri, hyperlinkService.GetArtifactViewerUrl(uri).PathAndQuery);
        }
        
        protected virtual string GetFilterNodeKey(FilterNode rootFilterNode)
        {
            string filterNodeKey = string.Empty;
            if (rootFilterNode != null)
            {
                foreach (FilterNode f in rootFilterNode)
                {
                    filterNodeKey = filterNodeKey + f.Key.ToLower() + ":" + f.Value.Trim().ToLower() + ":" + f.Sign.ToString() + "-";
                }
            }

            return filterNodeKey;
        }
    }
}
