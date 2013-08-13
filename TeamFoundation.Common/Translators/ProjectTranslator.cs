using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFoundation.Common.Entities;
using Microsoft.TeamFoundation.Server;

namespace TeamFoundation.Common.Translators
{
    public static class ProjectTranslator
    {
        public static Project ToModel(this ProjectInfo projectInfo, string collectionName)
        {
            if (projectInfo == null)
            {
                throw new ArgumentNullException("projectInfo");
            }

            return new Project()
            {
                Collection = collectionName,
                Name = projectInfo.Name,
            };
        }
    }
}
