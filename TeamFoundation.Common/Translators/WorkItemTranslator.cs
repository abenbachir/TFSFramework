

namespace TeamFoundation.Common.Translators
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TeamFoundation.Common.Entities;

    public static class WorkItemTranslator
    {
        private static string GetFieldValueByReference(this Microsoft.TeamFoundation.WorkItemTracking.Client.FieldCollection fields, string referenceName)
        {
            var field = fields
                            .Cast<Microsoft.TeamFoundation.WorkItemTracking.Client.Field>()
                            .SingleOrDefault(f => f.ReferenceName.Equals(referenceName, StringComparison.OrdinalIgnoreCase));

            return (field != null) && (field.Value != null)
                ? field.Value.ToString()
                : null;
        }

        private static void SetFieldValueByReference(this Microsoft.TeamFoundation.WorkItemTracking.Client.FieldCollection fields, string referenceName, object value)
        {
            var field = fields
                            .Cast<Microsoft.TeamFoundation.WorkItemTracking.Client.Field>()
                            .SingleOrDefault(f => f.ReferenceName.Equals(referenceName, StringComparison.OrdinalIgnoreCase));

            if (field != null)
            {
                field.Value = value;
            }
        }

        public static WorkItem ToModel(this Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem tfsWorkItem, Uri workItemWebEditorUrl)
        {
            if (tfsWorkItem == null)
            {
                throw new ArgumentNullException("tfsWorkItem");
            }

            if (workItemWebEditorUrl == null)
            {
                throw new ArgumentNullException("workItemWebEditorUrl");
            }

            WorkItem wi = new WorkItem();
            double parsedDouble; int parsedInt;

            //Base information to provide in addition to WorkItem fields
            wi.Id = tfsWorkItem.Id;
            wi.Project = tfsWorkItem.Project.Name;
            wi.Type = tfsWorkItem.Type.Name;
            wi.WebEditorUrl = workItemWebEditorUrl.ToString();

            //Base fields
            wi.AreaPath = tfsWorkItem.AreaPath;
            wi.IterationPath = tfsWorkItem.IterationPath;
            wi.Revision = tfsWorkItem.Revision;
            wi.Priority = tfsWorkItem.Fields.GetFieldValueByReference("Microsoft.VSTS.Common.Priority") ?? tfsWorkItem.Fields.GetFieldValueByReference("CodeStudio.Rank"); //CodeStudio.Rank is for codeplex support
            wi.Severity = tfsWorkItem.Fields.GetFieldValueByReference("Microsoft.VSTS.Common.Severity");
            double.TryParse(tfsWorkItem.Fields.GetFieldValueByReference("Microsoft.VSTS.Scheduling.RemainingWork"), out parsedDouble);
            wi.RemainingWork = parsedDouble;
            wi.AssignedTo = tfsWorkItem.Fields[Microsoft.TeamFoundation.WorkItemTracking.Client.CoreField.AssignedTo].Value.ToString();
            wi.CreatedDate = tfsWorkItem.CreatedDate;
            wi.CreatedBy = tfsWorkItem.CreatedBy;
            wi.ChangedDate = tfsWorkItem.ChangedDate;
            wi.ChangedBy = tfsWorkItem.ChangedBy;
            wi.ResolvedBy = tfsWorkItem.Fields.GetFieldValueByReference("Microsoft.VSTS.Common.ResolvedBy");
            wi.Title = tfsWorkItem.Title;
            wi.State = tfsWorkItem.State;
            wi.Reason = tfsWorkItem.Reason;
            double.TryParse(tfsWorkItem.Fields.GetFieldValueByReference("Microsoft.VSTS.Scheduling.CompletedWork"), out parsedDouble);
            wi.CompletedWork = parsedDouble;
            wi.Description = tfsWorkItem.Description;
            wi.ReproSteps = tfsWorkItem.Fields.GetFieldValueByReference("Microsoft.VSTS.TCM.ReproSteps");
            wi.FoundInBuild = tfsWorkItem.Fields.GetFieldValueByReference("Microsoft.VSTS.Build.FoundIn");
            wi.IntegratedInBuild = tfsWorkItem.Fields.GetFieldValueByReference("Microsoft.VSTS.Build.IntegrationBuild");
            wi.AttachedFileCount = tfsWorkItem.AttachedFileCount;
            wi.HyperLinkCount = tfsWorkItem.HyperLinkCount;
            wi.RelatedLinkCount = tfsWorkItem.RelatedLinkCount;

            //Agile
            wi.Risk = tfsWorkItem.Fields.GetFieldValueByReference("Microsoft.VSTS.Common.Risk");
            double.TryParse(tfsWorkItem.Fields.GetFieldValueByReference("Microsoft.VSTS.Scheduling.StoryPoints"), out parsedDouble);
            wi.StoryPoints = parsedDouble;

            //Agile and CMMI
            double.TryParse(tfsWorkItem.Fields.GetFieldValueByReference("Microsoft.VSTS.Scheduling.OriginalEstimate"), out parsedDouble);
            wi.OriginalEstimate = parsedDouble;

            //Scrum
            double.TryParse(tfsWorkItem.Fields.GetFieldValueByReference("Microsoft.VSTS.Common.BacklogPriority"), out parsedDouble);
            wi.BacklogPriority = parsedDouble;
            int.TryParse(tfsWorkItem.Fields.GetFieldValueByReference("Microsoft.VSTS.Common.BusinessValue"), out parsedInt);
            wi.BusinessValue = parsedInt;
            double.TryParse(tfsWorkItem.Fields.GetFieldValueByReference("Microsoft.VSTS.Scheduling.Effort"), out parsedDouble);
            wi.Effort = parsedDouble;

            //Scrum and CMMI
            wi.Blocked = tfsWorkItem.Fields.GetFieldValueByReference("Microsoft.VSTS.CMMI.Blocked");

            //CMMI
            double.TryParse(tfsWorkItem.Fields.GetFieldValueByReference("Microsoft.VSTS.Scheduling.Size"), out parsedDouble);
            wi.Size = parsedDouble;

            return wi;
        }

        public static Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem ToEntity(this WorkItem workItemModel, Microsoft.TeamFoundation.WorkItemTracking.Client.Project project)
        {
            if (workItemModel == null)
            {
                throw new ArgumentNullException("workItemModel");
            }

            if (project == null)
            {
                throw new ArgumentNullException("project");
            }

            var workItemTypes = project.WorkItemTypes;
            var type = workItemTypes[workItemModel.Type];
            var workItemEntity = new Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem(type);

            workItemEntity.UpdateFromModel(workItemModel);

            return workItemEntity;
        }

        public static void UpdateFromModel(this Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem workItemEntity, WorkItem workItemModel)
        {
            if (workItemEntity == null)
            {
                throw new ArgumentNullException("workItemEntity");
            }

            if (workItemModel == null)
            {
                throw new ArgumentNullException("workItemModel");
            }

            if (!string.IsNullOrWhiteSpace(workItemModel.AreaPath))
            {
                workItemEntity.AreaPath = workItemModel.AreaPath;
            }

            if (workItemEntity.Type.FieldDefinitions.TryGetByName("Assigned To") != null &&
                !string.IsNullOrWhiteSpace(workItemModel.AssignedTo))
            {
                workItemEntity.Fields["Assigned To"].Value = workItemModel.AssignedTo;
            }

            if (workItemEntity.Type.FieldDefinitions.TryGetByName("Backlog Priority") != null)
            {
                workItemEntity.Fields["Backlog Priority"].Value = workItemModel.BacklogPriority;
            }

            if (workItemEntity.Type.FieldDefinitions.TryGetByName("Blocked") != null)
            {
                workItemEntity.Fields["Blocked"].Value = workItemModel.Blocked;
            }

            if (workItemEntity.Type.FieldDefinitions.TryGetByName("Business Value") != null)
            {
                workItemEntity.Fields["Business Value"].Value = workItemModel.BusinessValue;
            }

            if (workItemEntity.Type.FieldDefinitions.TryGetByName("Completed Work") != null)
            {
                workItemEntity.Fields["Completed Work"].Value = workItemModel.CompletedWork;
            }

            if (!string.IsNullOrWhiteSpace(workItemModel.Description))
            {
                workItemEntity.Description = workItemModel.Description;
            }

            if (workItemEntity.Type.FieldDefinitions.TryGetByName("Effort") != null)
            {
                workItemEntity.Fields["Effort"].Value = workItemModel.Effort;
            }

            if (workItemEntity.Type.FieldDefinitions.TryGetByName("Found In") != null &&
                !string.IsNullOrWhiteSpace(workItemModel.FoundInBuild))
            {
                workItemEntity.Fields["Found In"].Value = workItemModel.FoundInBuild;
            }

            if (workItemEntity.Type.FieldDefinitions.TryGetByName("Integration Build") != null &&
                !string.IsNullOrWhiteSpace(workItemModel.IntegratedInBuild))
            {
                workItemEntity.Fields["Integration Build"].Value = workItemModel.IntegratedInBuild;
            }

            if (!string.IsNullOrWhiteSpace(workItemModel.IterationPath))
            {
                workItemEntity.IterationPath = workItemModel.IterationPath;
            }

            if (workItemEntity.Type.FieldDefinitions.TryGetByName("Original Estimate") != null)
            {
                workItemEntity.Fields["Original Estimate"].Value = workItemModel.OriginalEstimate;
            }

            if (!string.IsNullOrWhiteSpace(workItemModel.Priority))
            {
                int priority;
                if (int.TryParse(workItemModel.Priority, NumberStyles.Integer, CultureInfo.InvariantCulture, out priority))
                {
                    if (workItemEntity.Type.FieldDefinitions.TryGetByName("Priority") != null)
                    {
                        workItemEntity.Fields["Priority"].Value = priority;
                    }

                    // For CodePlex TFS
                    if (workItemEntity.Type.FieldDefinitions.TryGetByName("Rank") != null)
                    {
                        workItemEntity.Fields["Rank"].Value = EntityTranslator.GetPriorityDescription(priority);
                    }
                }
                else
                {
                    // For CodePlex TFS
                    if (workItemEntity.Type.FieldDefinitions.TryGetByName("Rank") != null)
                    {
                        workItemEntity.Fields["Rank"].Value = workItemModel.Priority;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(workItemModel.Reason))
            {
                workItemEntity.Reason = workItemModel.Reason;
            }

            if (workItemEntity.Type.FieldDefinitions.TryGetByName("Remaining Work") != null)
            {
                workItemEntity.Fields["Remaining Work"].Value = workItemModel.RemainingWork;
            }

            if (workItemEntity.Type.FieldDefinitions.TryGetByName("Repro Steps") != null &&
                !string.IsNullOrWhiteSpace(workItemModel.ReproSteps))
            {
                workItemEntity.Fields["Repro Steps"].Value = workItemModel.ReproSteps;
            }

            if (workItemEntity.Type.FieldDefinitions.TryGetByName("Risk") != null &&
                !string.IsNullOrWhiteSpace(workItemModel.Risk))
            {
                workItemEntity.Fields["Risk"].Value = workItemModel.Risk;
            }

            if (workItemEntity.Type.FieldDefinitions.TryGetByName("Severity") != null &&
                !string.IsNullOrWhiteSpace(workItemModel.Severity))
            {
                workItemEntity.Fields["Severity"].Value = workItemModel.Severity;
            }

            if (workItemEntity.Type.FieldDefinitions.TryGetByName("Size") != null)
            {
                workItemEntity.Fields["Size"].Value = workItemModel.Size;
            }

            if (workItemEntity.Type.FieldDefinitions.TryGetByName("Stack Rank") != null)
            {
                workItemEntity.Fields["Stack Rank"].Value = workItemModel.StackRank;
            }

            if (workItemEntity.Type.FieldDefinitions.TryGetByName("Story Points") != null)
            {
                workItemEntity.Fields["Story Points"].Value = workItemModel.StoryPoints;
            }

            if (!string.IsNullOrWhiteSpace(workItemModel.State))
            {
                workItemEntity.State = workItemModel.State;
            }

            if (!string.IsNullOrWhiteSpace(workItemModel.Title))
            {
                workItemEntity.Title = workItemModel.Title;
            }
        }
    }
}
