using Autofac.Core;
using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Service;
using Promact.CustomerSuccess.Platform.Services.Service;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using System;
using Microsoft.AspNetCore.Authorization;
namespace Promact.CustomerSuccess.Platform.Services
    {
    public class CreatePdfService : ApplicationService
    {
        private readonly ApprovedTeamService _approvedTeam;
        private readonly PhaseService _phase;
        private readonly ClientFeedbackService _clientFeedback;
        private readonly EscalationMatrixService _escalationMatrix;
        private readonly AuditHistoryService _auditHistory;
        private readonly MeetingMinuteService _meetingMinute;
        private readonly OverviewService _overview;
        private readonly ProjectBudgetService _projectBudget;
        private readonly ProjectResourcesService _projectResources;
        private readonly ProjectUpdateService _projectUpdate;
        private readonly RiskProfileService _riskProfile;
        private readonly SprintService _sprint;
        private readonly StakeAndScopeService _stakeAndScopeService;
        private readonly StakeholderService _stakeholder;
        private readonly ProjectService _project;

        public CreatePdfService(
            ProjectService project,
            ProjectBudgetService projectBudget ,
            ProjectResourcesService projectResources,
            SprintService sprintService,
            StakeholderService stakeholder,
            ClientFeedbackService clientFeedback,
            RiskProfileService riskProfile,
            EscalationMatrixService escalationMatrix,
            AuditHistoryService auditHistory,
            ApprovedTeamService approvedTeam, 
            PhaseService phase,
            MeetingMinuteService meetingMinute,
            StakeAndScopeService stakeAndScope,
            OverviewService overview)
        {
            _phase = phase;
            _approvedTeam = approvedTeam;
            _overview = overview;
            _stakeAndScopeService = stakeAndScope;
            _auditHistory = auditHistory;
            _escalationMatrix = escalationMatrix;
            _riskProfile = riskProfile;
            _sprint = sprintService;
            _stakeholder=stakeholder;
            _clientFeedback = clientFeedback;
            _projectResources = projectResources;
            _projectBudget = projectBudget;
            _project = project;
            _meetingMinute = meetingMinute;
        }
        public async Task<byte[]> GetByIdAsync(Guid id)
        {
            var renderer = new ChromePdfRenderer();

            var allprojects = await _project.GetProjectAsync();
            var projects = allprojects.Where(item => item.Id == id).ToList();


            var allApprovedTeams = await _approvedTeam.GetAllAsync();
            var projectApprovedTeams = allApprovedTeams.Where(item => item.ProjectId == id).ToList();

            var allphase = await _phase.GetAllAsync();
            var phase = allphase.Where(item => item.ProjectId == id).ToList();

            var clientFeedbacks = await _clientFeedback.GetAllAsync();
            var projectclientfeedback = clientFeedbacks.Where(item => item.ProjectId == id).ToList();

            var allAuditHistory = await _auditHistory.GetAllAsync();
            var auditHistories = allAuditHistory.Where(item => item.ProjectId == id).ToList();
            
            var allEscalationMatrix = await _escalationMatrix.GetAllAsync();
            var escalationMatrices = allEscalationMatrix.Where(item => item.ProjectId == id).ToList();

            var allMeetingMinute = await _meetingMinute.GetAllAsync();
            var meetingMinutes = allMeetingMinute.Where(item => item.ProjectId == id).ToList();

            var allOverviews = await _overview.GetAllAsync();
            var overview = allOverviews.Where(item => item.ProjectId == id).ToList();

            var allStakeAndScopes = await _stakeAndScopeService.GetAllAsync();
            var stakeAndScopes = allStakeAndScopes.Where(item => item.ProjectId == id).ToList();

            var allRiskProfile = await _riskProfile.GetAllAsync();
            var riskProfiles = allRiskProfile.Where(item => item.ProjectId == id).ToList();


            var allProjectResources = await _projectResources.GetAllAsync();
            var projectResource = allProjectResources.Where(item => item.ProjectId == id).ToList();

            //var allProjectUpdate = await _projectUpdate.GetAllAsync();
            //var projectUpdates = allProjectUpdate.Where(item => item.ProjectId == id).ToList();


            string html = "<h1> Project Details </h1>";
            html+="<br>";
            html += "<table border=\"1\" style=\"border-collapse: collapse; width: 100%;\">";
            html += "<tr><th>Project Name</th>" +
                "<th>Description</th>" +
                "<th>Member</th><" +
                "th>Status</th>" +
                "<th>Project Manager</th></tr>";
            
            foreach(var item in projects)
            {
                html += "<tr>";
                html += "<td>" + item.Name + "</td>";
                html += "<td>" + item.Description + "</td>";
                html += "<td>" + item.Member + "</td>";
                html += "<td>" + item.Status + "</td>";
                html += "<td>" + item.ProjectManager + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            html += "<br>";

            html += "<h4>Project Overview</h4>";
            html += "<table border=\"1\" style=\"border-collapse: collapse; width: 100%;\">";
            html += "<tr><th>Project Brief</th><th>Project Goals</th><th>Project Objectives</th><th>Project Purpose</th></tr>";
            foreach (var item in overview)
            {
                html += "<tr>";
                html +="<td>"+item.Brief+"</td>";
                html += "<td>" + item.Goals + "</td>";
                html +="<td>" + item.Objectives + "</td>";
                html += "<td>" + item.Purpose + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            html+="<br>";
            html += "<h4>Stake and scope</h4>";
            html += "<br>";
            html += "<table border=\"1\" style=\"border-collapse: collapse; width: 100%;\">";
            html += "<tr><th>Project Stake</th><th>Project Scope</th></tr>";
            foreach (var item in stakeAndScopes)
            {
                html += "<tr>";
                html += "<td>" + item.Stake + "</td>";
                html += "<td>" + item.Scope + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            html += "<br>";

            html += "<h4>Approved Team</h4>";
            html += "<table border=\"1\" style=\"border-collapse: collapse; width: 100%;\">";
            html += "<tr><th>Role</th><th>Number of Resources</th><th>Duration</th><th>AvailabilityPercentage</th></tr>";
            foreach (var item in projectApprovedTeams)
            {
                html += "<tr>";
                html += "<td>" + item.Role + "</td>";
                html += "<td>" + item.NumberOfResources + "</td>";
                html += "<td>" + item.Duration + "</td>";
                html += "<td>" + item.AvailabilityPercentage + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            html += "<br>";
            html += "<h4>Stake and scope</h4>";
            html += "<br>";
            html += "<table border=\"1\" style=\"border-collapse: collapse; width: 100%;\">";
            html += "<tr><th>Title</th><th>Status</th><th>Approval Date</th>" +
                "<th>Completion Date</th><th>Comments</th></tr>";
            foreach (var item in phase)
            {
                html += "<tr>";
                html += "<td>" + item.Title + "</td>";
                html += "<td>" + item.Status + "</td>";
                html += "<td>" + item.ApprovalDate + "</td>";
                html += "<td>" + item.CompletionDate + "</td>";
                html += "<td>" + item.Comments + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            html += "<br>";

            html += "<h4>Audit History</h4>";
            html += "<table border=\"1\" style=\"border-collapse: collapse; width: 100%;\">";
            html += "<tr><th>Date of Audit</th><th>Reviewed By</th><th>Status</th><th>Reviewed Section</th><th>Comment Queries</th><th>Action Item</th></tr>";
            foreach (var item in auditHistories)
            {
                html += "<tr>";
                html += "<td>" + item.DateOfAudit + "</td>";
                html += "<td>" + item.ReviewedBy + "</td>";
                html += "<td>" + item.Status + "</td>";
                html += "<td>" + item.Section + "</td>";
                html += "<td>" + item.CommentQueries + "</td>";
                html += "<td>" + item.ActionItem + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            html += "<br>";

            html += "<h4>Escalation Matrix</h4>";
            html += "<br>";
            html += "<table border=\"1\" style=\"border-collapse: collapse; width: 100%;\">";
            html += "<tr><th>Escalation Level</th><th>Type</th></tr>";
            foreach (var item in escalationMatrices)
            {
                html += "<tr>";
                html += "<td>" + item.Level + "</td>";
                html += "<td>" + item.EscalationType + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            html += "<br>";

            html += "<h4>Risk Profile</h4>";
            html += "<table border=\"1\" style=\"border-collapse: collapse; width: 100%;\">";
            html += "<tr><th>Risk Type</th><th>Severity</th><th>Impact</th></tr>";
            
            foreach (var item in riskProfiles)
            {
                html += "<tr>";
                html += "<td>" + item.RiskType + "</td>";
                html += "<td>" + item.Severity + "</td>";
                html += "<td>" + item.Impact + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            html += "<br>";

            //html += "<h4>Project Udpate</h4>";
            //html += "<table border=\"1\" style=\"border-collapse: collapse; width: 100%;\">";
            //html += "<tr><th>Date</th><th>GeneralUpdates</th><th>Impact</th></tr>";
            //foreach (var item in projectUpdates)
            //{
            //    html += "<tr>";
            //    html += "<td>" + item.Date + "</td>";
            //    html += "<td>" + item.GeneralUpdates + "</td>";
            //    html += "</tr>";
            //}

            html += "<h4>Project Resources</h4>";
            html += "<table border=\"1\" style=\"border-collapse: collapse; width: 100%;\">";
            html += "<tr><th>Allocation Percentage</th><th>Start Date</th><th>End Date</th><th>Role</th><th>Name</th></tr>";
            foreach (var item in projectResource)
            {
                html += "<tr>";
                html += "<td>" + item.AllocationPercentage + "</td>";
                html += "<td>" + item.Start + "</td>";
                html += "<td>" + item.End + "</td>";
                html += "<td>" + item.Role + "</td>";
                html += "<td>" + item.Name + "</td>";
                html += "</tr>";
            }

            var document = await renderer.RenderHtmlAsPdfAsync(html);

            // Convert the PDF document to a byte array
                byte[] documentBytes=document.BinaryData;
            var memoryStream = new MemoryStream(documentBytes);
            return memoryStream.ToArray();
            

        }
    }
}
