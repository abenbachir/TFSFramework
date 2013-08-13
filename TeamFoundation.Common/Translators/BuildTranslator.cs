using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFoundation.Common.Entities;

namespace TeamFoundation.Common.Translators
{
    public static class BuildTranslator
    {
        public static Build ToModel(this Microsoft.TeamFoundation.Build.Client.IBuildDetail buildDetail)
        {
            if (buildDetail == null)
            {
                throw new ArgumentNullException("buildDetail");
            }

            return new Build()
            {
                Project = buildDetail.TeamProject,
                Definition = buildDetail.BuildDefinition.Name,
                Number = buildDetail.BuildNumber,
                Reason = buildDetail.Reason.ToString(),
                Quality = buildDetail.Quality,
                Status = buildDetail.Status.ToString(),
                RequestedBy = buildDetail.RequestedBy,
                RequestedFor = buildDetail.RequestedFor,
                LastChangedBy = buildDetail.LastChangedBy,
                StartTime = buildDetail.StartTime,
                FinishTime = buildDetail.FinishTime,
                LastChangedOn = buildDetail.LastChangedOn,
                BuildFinished = buildDetail.BuildFinished,
                DropLocation = buildDetail.DropLocation,
                Errors = string.Join(Environment.NewLine, Microsoft.TeamFoundation.Build.Client.InformationNodeConverters.GetBuildErrors(buildDetail).Select(e => e.Message)),
                Warnings = string.Join(Environment.NewLine, Microsoft.TeamFoundation.Build.Client.InformationNodeConverters.GetBuildWarnings(buildDetail).Select(w => w.Message))
            };
        }
    }
}
