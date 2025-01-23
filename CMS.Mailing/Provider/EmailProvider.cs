using CMS.Core.AppSettings;
using CMS.Core.AppSettings.Interfaces;
using CMS.Mailing.Contracts;
using CMS.Mailing.Contracts.Common;
using CMS.Mailing.Resources;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CMS.Mailing.Provider
{
    public class EmailProvider : IEmailProvider
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly AppSettings _appSettings;
        private readonly EmailAddress _sender;

        public EmailProvider(ISendGridClient sendGridClient, IAppSettingsProvider appSettingsProvider)
        {
            _sendGridClient = sendGridClient;
            _appSettings = appSettingsProvider.GetAppSettings<AppSettings>();
            _sender = new EmailAddress(
                _appSettings.ExternalServices.SendGrid.SenderEmail,
                _appSettings.ExternalServices.SendGrid.SenderName);
        }

        public async Task SendUserCreatedEmailAsync(UserCreatedEmailDto message)
        {
            object[] templateArgs = {
                message.RecipientName,
                message.Login,
                message.Password,
                _appSettings.CompanyInfo.WebsiteUrl,
                _appSettings.CompanyInfo.Name
            };

            var commonMessage = new CommonEmailDto(
                message.RecipientEmail,
                message.RecipientName,
                EmailTemplates.UserCreated_Subject,
                string.Format(EmailTemplates.UserCreated_PlainText, templateArgs),
                string.Format(EmailTemplates.UserCreated_Html, templateArgs));

            await SendEmailAsync(commonMessage);
        }

        public async Task SendPasswordResetEmailAsync(PasswordResetEmailDto message)
        {
            object[] templateArgs = {
                message.RecipientName,
                message.Login,
                message.Password,
                _appSettings.CompanyInfo.WebsiteUrl,
                _appSettings.CompanyInfo.Name
            };

            var commonMessage = new CommonEmailDto(
                message.RecipientEmail,
                message.RecipientName,
                EmailTemplates.PasswordReset_Subject,
                string.Format(EmailTemplates.PasswordReset_PlainText, templateArgs),
                string.Format(EmailTemplates.PasswordReset_Html, templateArgs));

            await SendEmailAsync(commonMessage);
        }

        private async Task SendEmailAsync(CommonEmailDto message)
        {
            var recipient = new EmailAddress(message.RecipientEmail, message.RecipientName);
            var sendGridMessage = MailHelper.CreateSingleEmail(_sender, recipient,
                message.Subject, message.PlainTextContent, message.HtmlContent);
            var response = await _sendGridClient.SendEmailAsync(sendGridMessage);
        }
    }
}
