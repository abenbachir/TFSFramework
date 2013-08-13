
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using TeamFoundation.Common.Proxies;

namespace TeamFoundation.Common
{
    public static class Constants
    {
        public static int DefaultEntityPageSize = 20;
        public static Dictionary<string, Type> ProxyTypes = new Dictionary<string, Type>(){ 
           { "AreaPathdRepository",     typeof(TFSBuildProxy) },
           { "AttachementRepository",   typeof(TFSAttachmentProxy) },
           { "BranchRepository",        typeof(TFSBranchProxy) },
           { "BuildRepository",         typeof(TFSBuildProxy) },
           { "BuildDefinitionRepository", typeof(TFSBuildDefinitionProxy) },
           { "ChangeRepository",        typeof(TFSChangeProxy) },
           { "ChangesetRepository",     typeof(TFSChangesetProxy) },
           { "IterationPathRepository", typeof(TFSIterationPathProxy) },
           { "LinkRepository",          typeof(TFSLinkProxy) },
           { "ProjectRepository",       typeof(TFSProjectProxy) },
           { "QueryRepository",         typeof(TFSQueryProxy) },
           { "UserRepository",          typeof(TFSUserProxy) },
           { "WorkItemRepository",      typeof(TFSWorkItemProxy) }
        };
        public static class TFS
        {
            private static HybridDictionary _fieldLookup;

            static TFS()
            {
                _fieldLookup = new HybridDictionary();
                _fieldLookup.Add("Id", "[System.Id]");
                _fieldLookup.Add("Project", "[System.TeamProject]");
                _fieldLookup.Add("Type", "[System.WorkItemType]");
                _fieldLookup.Add("AreaPath", "[System.AreaPath]");
                _fieldLookup.Add("IterationPath", "[System.IterationPath]");
                _fieldLookup.Add("Revision", "[System.Rev]");
                _fieldLookup.Add("Priority", "[Microsoft.VSTS.Common.Priority]");
                _fieldLookup.Add("Severity", "[Microsoft.VSTS.Common.Severity]");
                _fieldLookup.Add("StackRank", "[Microsoft.VSTS.Common.StackRank]");
                _fieldLookup.Add("AssignedTo", "[System.AssignedTo]");
                _fieldLookup.Add("CreatedDate", "[System.CreatedDate]");
                _fieldLookup.Add("CreatedBy", "[System.CreatedBy]");
                _fieldLookup.Add("ChangedDate", "[System.ChangedDate]");
                _fieldLookup.Add("ChangedBy", "[System.ChangedBy]");
                _fieldLookup.Add("ResolvedBy", "[Microsoft.VSTS.Common.ResolvedBy]");
                _fieldLookup.Add("Title", "[System.Title]");
                _fieldLookup.Add("State", "[System.State]");
                _fieldLookup.Add("Reason", "[System.Reason]");
                _fieldLookup.Add("CompletedWork", "[Microsoft.VSTS.Scheduling.CompletedWork]");
                _fieldLookup.Add("RemainingWork", "[Microsoft.VSTS.Scheduling.RemainingWork]");
                _fieldLookup.Add("Description", "[System.Description]");
                _fieldLookup.Add("ReproSteps", "[Microsoft.VSTS.TCM.ReproSteps]");
                _fieldLookup.Add("FoundInBuild", "[Microsoft.VSTS.Build.FoundIn]");
                _fieldLookup.Add("IntegratedInBuild", "[Microsoft.VSTS.Build.IntegrationBuild]");
                _fieldLookup.Add("AttachedFileCount", "[System.AttachedFileCount]");
                _fieldLookup.Add("HyperLinkCount", "[System.HyperLinkCount]");
                _fieldLookup.Add("RelatedLinkCount", "[System.RelatedLinkCount]");
                _fieldLookup.Add("Risk", "[Microsoft.VSTS.Common.Risk]");
                _fieldLookup.Add("StoryPoints", "[Microsoft.VSTS.Scheduling.StoryPoints]");
                _fieldLookup.Add("OriginalEstimate", "[Microsoft.VSTS.Scheduling.OriginalEstimate]");
                _fieldLookup.Add("BacklogPriority", "[Microsoft.VSTS.Common.BacklogPriority]");
                _fieldLookup.Add("BusinessValue", "[Microsoft.VSTS.Common.BusinessValue]");
                _fieldLookup.Add("Effort", "[Microsoft.VSTS.Scheduling.Effort]");
                _fieldLookup.Add("Blocked", "[Microsoft.VSTS.CMMI.Blocked]");
                _fieldLookup.Add("Size", "[Microsoft.VSTS.Scheduling.Size]");
            }
            
            public static string FieldLookup(string field)
            {
                if (_fieldLookup.Contains(field))
                {
                    return (string)_fieldLookup[field];
                }

                return field;
            }
        }
    }
}
