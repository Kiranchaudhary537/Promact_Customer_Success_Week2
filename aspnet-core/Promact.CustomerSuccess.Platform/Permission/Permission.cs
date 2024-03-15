

using Volo.Abp.Authorization.Permissions;

namespace Promact.CustomerSuccess.Platform.Permission
{
    public class Permission : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var project = context.AddGroup("project");
            project.AddPermission("project_read");
            project.AddPermission("project_create");
            project.AddPermission("project_update");
            project.AddPermission("project_delete");


            var projectUpdate = context.AddGroup("Project Update");
            projectUpdate.AddPermission("Project Update Read");
            projectUpdate.AddPermission("Project Update Create");
            projectUpdate.AddPermission("Project Update Update");
            projectUpdate.AddPermission("Project Update Delete");

            var projectResource = context.AddGroup("Project Resource");
            projectResource.AddPermission("Project Resource Read");
            projectResource.AddPermission("Project Resource Create");
            projectResource.AddPermission("Project Resource Update");
            projectResource.AddPermission("Project Resource Delete");

            var approvedTeam = context.AddGroup("Approved Team");
            approvedTeam.AddPermission("Approved Team Read");
            approvedTeam.AddPermission("Approved Team Create");
            approvedTeam.AddPermission("Approved Team Update");
            approvedTeam.AddPermission("Approved Team Delete");

            var clientFeedback = context.AddGroup("Client Feedback");
            clientFeedback.AddPermission("Client Feedback Read");
            clientFeedback.AddPermission("Client Feedback Create");
            clientFeedback.AddPermission("Client Feedback Update");
            clientFeedback.AddPermission("Client Feedback Delete");

            var meetingMinute = context.AddGroup("Meeting Minute");
            meetingMinute.AddPermission("Meeting Minute Read");
            meetingMinute.AddPermission("Meeting Minute Create");
            meetingMinute.AddPermission("Meeting Minute Update");
            meetingMinute.AddPermission("Meeting Minute Delete");
        }
    }
}



