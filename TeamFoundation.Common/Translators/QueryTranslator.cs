using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFoundation.Common.Entities;

namespace TeamFoundation.Common.Translators
{
    public static class QueryTranslator
    {
        public static Query ToModel(this Microsoft.TeamFoundation.WorkItemTracking.Client.QueryDefinition tfsQueryItem, string path)
        {
            if (tfsQueryItem == null)
            {
                throw new ArgumentNullException("tfsQueryItem");
            }

            return new Query
            {
                Id = tfsQueryItem.Id.ToString(),
                Name = tfsQueryItem.Name,
                QueryText = tfsQueryItem.QueryText,
                Project = tfsQueryItem.Project.Name,
                Path = path,
                QueryType = tfsQueryItem.QueryType.ToString()
            };
        }
    }
}
