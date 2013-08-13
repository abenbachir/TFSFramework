using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFoundation.Common.Entities;

namespace TeamFoundation.Common.Translators
{
    public static class BranchTranslator 
    {
        public static Branch ToModel(this Microsoft.TeamFoundation.VersionControl.Client.BranchObject tfsBranch)
        {
            if (tfsBranch == null)
            {
                throw new ArgumentNullException("tfsBranch");
            }

            return new Branch
            {
                Path = EntityTranslator.EncodePath(tfsBranch.Properties.RootItem.Item),
                Description = tfsBranch.Properties.Description,
                DateCreated = tfsBranch.DateCreated
            };
        }
    }
}
