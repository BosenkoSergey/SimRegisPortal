using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SimRegisPortal.Application.Models.Mailing;
using SimRegisPortal.Application.Models.Mailing.Common;
using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Application.Settings;
using SimRegisPortal.Core.Resources;

namespace SimRegisPortal.Application.Services;

public sealed class EmailService : IEmailService
{
    private readonly ISendGridClient _sendGridClient;
    private readonly IStringLocalizer<EmailTemplates> _localizer;
    private readonly AppSettings _appSettings;
    private readonly EmailAddress _sender;

    public EmailService(
        ISendGridClient sendGridClient,
        IOptions<AppSettings> options,
        IStringLocalizer<EmailTemplates> localizer)
    {
        _sendGridClient = sendGridClient;
        _localizer = localizer;
        _appSettings = options.Value;
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
