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

        public CreatePdfService(ApprovedTeamService approvedTeam, PhaseService phase)
        {
            _phase = phase;
            _approvedTeam = approvedTeam;
        }
        public async Task<byte[]> GetAllAsync()
        {
            var renderer = new ChromePdfRenderer();
            var approvedTeams = await _approvedTeam.GetAllAsync();
            Console.WriteLine("\n\n\nOutput of application...");
            Console.WriteLine(approvedTeams.Count);
            var phases = await _phase.GetAllAsync();
            string html = "<h1> Project Details </h1>";
            foreach (var item in approvedTeams)
            {
                System.Console.Write(item);
                html += "<p>" + item.Duration +"</p>";
                html += "<p>" + item.AvailabilityPercentage + "</p>";
                html += "<p>" + item.NumberOfResources + "</p>";
            }
            foreach (var item in phases)
            {
                html += "<p>" + item.ApprovalDate + "</p><p>" + item.Comments + "</p><p>" + item.Description + "</p>";
            }
            var document = await renderer.RenderHtmlAsPdfAsync(html);

            // Convert the PDF document to a byte array
                byte[] documentBytes=document.BinaryData;
            var memoryStream = new MemoryStream(documentBytes);
            return memoryStream.ToArray();
            

        }
    }
}
