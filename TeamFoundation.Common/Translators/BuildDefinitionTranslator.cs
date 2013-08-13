using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFoundation.Common.Entities;

namespace TeamFoundation.Common.Translators
{
    public static class BuildDefinitionTranslator
    {
        public static BuildDefinition ToModel(this Microsoft.TeamFoundation.Build.Client.IBuildDefinition buildDefinition)
        {
            if (buildDefinition == null)
            {
                throw new ArgumentNullException("buildDefinition");
            }

            return new BuildDefinition()
            {
                Project = buildDefinition.TeamProject,
                Definition = buildDefinition.Name
            };
        }
    }
}
