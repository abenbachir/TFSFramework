using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFoundation.Common.Entities;

namespace TeamFoundation.Common.Translators
{
    public static class ChangeTranslator 
    {
        public static Change ToModel(this Microsoft.TeamFoundation.VersionControl.Client.Change tfsChange, string collectionName, int changesetId)
        {
            if (tfsChange == null)
            {
                throw new ArgumentNullException("tfsChange");
            }

            return new Change()
            {
                Collection = collectionName,
                Changeset = changesetId,
                Path = EntityTranslator.EncodePath(tfsChange.Item.ServerItem),
                Type = tfsChange.Item.ItemType.ToString(),
                ChangeType = tfsChange.ChangeType.ToString()
            };
        }
    }
}
