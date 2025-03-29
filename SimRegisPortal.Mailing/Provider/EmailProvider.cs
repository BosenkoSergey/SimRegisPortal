using SimRegisPortal.Core.AppSettings;
using SimRegisPortal.Core.AppSettings.Interfaces;
using SimRegisPortal.Mailing.Contracts;
using SimRegisPortal.Mailing.Models.Common;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Localization;
using SimRegisPortal.Core.Resources;

namespace SimRegisPortal.Mailing.Provider
{
    public class EmailProvider : IEmailProvider
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly IStringLocalizer<EmailTemplates> _localizer;
        private readonly AppSettings _appSettings;
        private readonly EmailAddress _sender;

        public EmailProvider(
            ISendGridClient sendGridClient,
            IAppSettingsProvider appSettingsProvider,
            IStringLocalizer<EmailTemplates> localizer)
        {
            _sendGridClient = sendGridClient;
            _localizer = localizer;
            _appSettings = appSettingsProvider.GetAppSettings<AppSettings>();
            _sender = new EmailAddress(
                _appSettings.ExternalServices.SendGrid.SenderEmail,
                _appSettings.ExternalServices.SendGrid.SenderName);
        }

        public async Task SendUserCreatedEmailAsync(UserCredentialsEmailDto message)
        {
            object[] templateArgs = {
                message.Recipient.Name,
                message.Login,
                message.Password,
                _appSettings.CompanyInfo.WebsiteUrl,
                _appSettings.CompanyInfo.Name
            };

            var commonMessage = new CommonEmailDto()
            {
                Recipient = message.Recipient,
                Subject = _localizer["UserCreated.Subject"],
                PlainTextContent = string.Format(_localizer["UserCreated.PlainText"], templateArgs),
                HtmlContent = string.Format(_localizer["UserCreated.Html"], templateArgs)
            };

            await SendEmailAsync(commonMessage);
        }

        public async Task SendPasswordResetEmailAsync(UserCredentialsEmailDto message)
        {
            object[] templateArgs = {
                message.Recipient.Name,
                message.Login,
                message.Password,
                _appSettings.CompanyInfo.WebsiteUrl,
                _appSettings.CompanyInfo.Name
            };

            var commonMessage = new CommonEmailDto()
            {
                Recipient = message.Recipient,
                Subject = _localizer["PasswordReset.Subject"],
                PlainTextContent = string.Format(_localizer["PasswordReset.PlainText"], templateArgs),
                HtmlContent = string.Format(_localizer["PasswordReset.Html"], templateArgs)
            };

            await SendEmailAsync(commonMessage);
        }

        private async Task SendEmailAsync(CommonEmailDto message)
        {
            var recipient = new EmailAddress(message.Recipient.Email, message.Recipient.Name);
            var sendGridMessage = MailHelper.CreateSingleEmail(_sender, recipient,
                message.Subject, message.PlainTextContent, message.HtmlContent);
            var response = await _sendGridClient.SendEmailAsync(sendGridMessage);
        }
    }
}
