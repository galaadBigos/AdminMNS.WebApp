using AdminMNS.WebApp.Data.Entities;
using AdminMNS.WebApp.Models.ViewModel.Account;
using AdminMNS.WebApp.Models.ViewModel.User;
using AdminMNS.WebApp.Options;
using AdminMNS.WebApp.Services.Abstractions;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Web;

namespace AdminMNS.WebApp.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<MailOptions> _options;
        private readonly UserManager<User> _userManager;

        private string _subjectConfirmationEmail;
        private string _bodyConfirmationEmail;

		public EmailSender(IOptions<MailOptions> options, UserManager<User> userManager)
        {
            _options = options;
            _userManager = userManager;

            _subjectConfirmationEmail = "Confirmation de votre email";
            _bodyConfirmationEmail = "Cliquez sur le lien pour valider votre email : ";
		}

        public async Task SendCreateUserConfirmationEmail(User user, CreateUserViewModel model, ControllerBase controller)
        {
            string? confirmUrl = await GetConfirmUrl(user, model.Email, controller);
            await SendMail(model.Email, _subjectConfirmationEmail, _bodyConfirmationEmail + confirmUrl);
        }

		public async Task SendRegisterUserConfirmationEmail(User user, RegisterViewModel model, ControllerBase controller)
		{
			string? confirmUrl = await GetConfirmUrl(user, model.Email, controller);
			await SendMail(model.Email, _subjectConfirmationEmail, _bodyConfirmationEmail + confirmUrl);
		}

		public async Task SendMail(string to, string subject, string body)
        {
            MimeMessage message = CreateEmailMessage(to, subject);
			CreateEmailBodyMessage(message, body);
			await CreateSmtpClient(message);
		}

        private MimeMessage CreateEmailMessage(string to, string subject)
        {
			MimeMessage message = new MimeMessage();
			message.From.Add(MailboxAddress.Parse(_options.Value.Email));
			message.To.Add(MailboxAddress.Parse(to));
			message.Subject = subject;

            return message;
		}

        private void CreateEmailBodyMessage(MimeMessage message, string body)
        {
			BodyBuilder bodyBuilder = new BodyBuilder();
			bodyBuilder.TextBody = body;
			message.Body = bodyBuilder.ToMessageBody();
		}

        private async Task CreateSmtpClient(MimeMessage message)
        {
			using SmtpClient client = new SmtpClient();
			await client.ConnectAsync(_options.Value.Server, _options.Value.Port, _options.Value.Ssl);
			await client.AuthenticateAsync(_options.Value.Login, _options.Value.Password);
			await client.SendAsync(message);
			await client.DisconnectAsync(true);
		}

        private async Task<string?> GetConfirmUrl(User user, string email, ControllerBase controller)
        {
			string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			var routeValues = new { email, code = HttpUtility.UrlEncode(code) };
			return controller.Url.Action("ConfirmEmail", "Account", routeValues, controller.Request.Scheme);
		}
    }
}
