
namespace TeamFoundation.Common.Entities
{
    using System;
    using System.Data.Services;
    using System.Data.Services.Common;

    using TeamFoundation.Common.Helpers;

    public class Attachment
    {
        public string Id { get; set; }

        public int WorkItemId { get; set; }

        public int Index { get; set; }

        public DateTime AttachedTime { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime LastWriteTime { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public string Comment { get; set; }

        public long Length { get; set; }

        public string Uri { get; set; }

        public string GetContentTypeForStreaming()
        {
            return RegistryHelper.GetMimeType(this.Extension);
        }

        public string GetStreamETag()
        {
            return "\"" + this.AttachedTime + "\"";
        }

        public Uri GetUrlForStreaming()
        {
            return !string.IsNullOrWhiteSpace(this.Uri) ? new Uri(this.Uri, UriKind.RelativeOrAbsolute) : new Uri("http://temp-uri", UriKind.RelativeOrAbsolute);
        }
    }
}
