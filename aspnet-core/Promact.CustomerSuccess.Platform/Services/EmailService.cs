using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Promact.CustomerSuccess.Platform.Entities;

namespace Promact.CustomerSuccess.Platform.Services
{
    public class EmailService : ITransientDependency
    {
        private readonly IConfiguration _configuration;
        private readonly StakeholderService _stakeholder;

        private readonly ProjectService _project;

        public EmailService(IConfiguration configuration, StakeholderService stakeholder, ProjectService project)
        {
            _configuration = configuration;
            _stakeholder = stakeholder;
            _project = project;
        }

        public async  Task sendAuditHistoryNotification(AuditHistory auditHistory, string type, Guid id)
        {

            List<Stakeholder> stakeholders = await _stakeholder.GetAllAsync();

            //filtering stakholder for projectid
            var projectStakeholders = stakeholders.Where(stakeholder => stakeholder.ProjectId == id).ToList();
            //var projectStakeholders = stakeholders;
            Project project = await _project.GetProjectByIdAsync(id);

            // Prepare email content
            String subject = "Audit History " + type + " Notification";
            String greeting = "<p> Please note that audit has been " + type + " and here is the audit summary:</p>";
            String message = "<table style='border-collapse: collapse;'>";
            message += "<tr><th colspan='2' style='padding: 5px; border: 1px solid black; border-collapse: collapse;'>Project Name:</td>";
            message += "<td colspan='2' style='padding: 5px; border: 1px solid black; border-collapse: collapse;'>" + project.Name + "</td></tr>";
            message += "<tr><th colspan='2' style='padding: 5px; border: 1px solid black; border-collapse: collapse;'>Reviewed Section:</td>";
            message += "<td colspan='2' style='padding: 5px; border: 1px solid black; border-collapse: collapse;'>" + auditHistory.Section + "</td></tr>";
            message += "<tr><th colspan='2' style='padding: 5px; border: 1px solid black; border-collapse: collapse;'>Reviewed By:</td>";
            message += "<td colspan='2' style='padding: 5px; border: 1px solid black; border-collapse: collapse;'>" + auditHistory.ReviewedBy + "</td></tr>";
            message += "<tr><th colspan='2' style='padding: 5px; border: 1px solid black; border-collapse: collapse;'>Status:</td>";
            message += "<td colspan='2' style='padding: 5px; border: 1px solid black; border-collapse: collapse;'>" + auditHistory.Status + "</td></tr>";
            message += "<tr><th colspan='2' style='padding: 5px; border: 1px solid black; border-collapse: collapse;'>Comments:</td>";
            message += "<td colspan='2' style='padding: 5px; border: 1px solid black; border-collapse: collapse;'>" + auditHistory.CommentQueries + "</td></tr>";
            message += "<tr><th colspan='2' style='padding: 5px; border: 1px solid black; border-collapse: collapse;'>Action Item:</td>";
            message += "<td colspan='2' style='padding: 5px; border: 1px solid black; border-collapse: collapse;'>" + auditHistory.ActionItem + "</td></tr>";
            message += "<tr><th colspan='2' style='padding: 5px; border: 1px solid black; border-collapse: collapse;'>Date of Audit:</td>";
            message += "<td colspan='2' style='padding: 5px; border: 1px solid black; border-collapse: collapse;'>" + auditHistory.DateOfAudit + "</td></tr>";
            message += "</table><br><br>";
            message += "<p>Thanks and Regards,<br>Promact Infotech Pvt Ltd</p>";

            // Send email to each stakeholder
            // for (Stakeholder stakeholder : stakeholders)
            // {
            //     sendEmail(stakeholder.getName(), stakeholder.getTitle(), stakeholder.getContact(), subject, greeting, message);
            // }

            foreach (Stakeholder stakeholder in projectStakeholders)
            {
                sendEmail(stakeholder.Name.ToString(),stakeholder.Title.ToString(),stakeholder.ContactEmail.ToString(),subject,greeting,message);
            }
        }

        private async Task sendEmail(string name, string title, string target, string subject, string greeting, string text)
        {
            SmtpClient client = new SmtpClient(_configuration["Settings:Abp.Mailing.Smtp.Host"], 587)
            {
                Credentials = new NetworkCredential(_configuration["Settings:Abp.Mailing.Smtp.UserName"], _configuration["Settings:Abp.Mailing.Smtp.Password"]),
                EnableSsl = true
            };
            MailAddress to = new MailAddress(target);
            MailAddress from = new MailAddress("kiranchaudhary537@gmail.com");

            // MailMessage message = new MailMessage(from, to)
            // {
            //     Subject = subject,
            //     Body = "<html><body><p>Hello " + title + " " + name + "</p>" + greeting + text + "</body></html>"
                
            // };
            var message = new MailMessage(from, to)
        {
            Subject = subject,
            Body = $"<html><body><p>Hello {title} {name},</p>{greeting}{text}</body></html>", 
            IsBodyHtml = true 
        };

            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

}
