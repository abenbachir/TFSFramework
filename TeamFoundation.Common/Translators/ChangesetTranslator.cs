using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFoundation.Common.Entities;

namespace TeamFoundation.Common.Translators
{
    public static class ChangesetTranslator
    {
        public static Changeset ToModel(this Microsoft.TeamFoundation.VersionControl.Client.Changeset tfsChangeset, Uri changesetWebEditorUrl)
        {
            if (tfsChangeset == null)
            {
                throw new ArgumentNullException("tfsChangeset");
            }

            if (changesetWebEditorUrl == null)
            {
                throw new ArgumentNullException("changesetWebEditorUrl");
            }

            return new Changeset()
            {
                ArtifactUri = tfsChangeset.ArtifactUri.ToString(),
                Comment = tfsChangeset.Comment,
                Committer = tfsChangeset.Committer,
                CreationDate = tfsChangeset.CreationDate,
                Id = tfsChangeset.ChangesetId,
                Owner = tfsChangeset.Owner,
                WebEditorUrl = changesetWebEditorUrl.ToString()
            };
        }
    }
}
