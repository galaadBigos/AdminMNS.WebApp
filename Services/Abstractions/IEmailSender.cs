using AdminMNS.WebApp.Data.Entities;
using AdminMNS.WebApp.Models.ViewModel.Account;
using AdminMNS.WebApp.Models.ViewModel.User;
using Microsoft.AspNetCore.Mvc;

namespace AdminMNS.WebApp.Services.Abstractions
{
    public interface IEmailSender
    {
		Task SendCreateUserConfirmationEmail(User user, CreateUserViewModel model, ControllerBase controller);
		Task SendRegisterUserConfirmationEmail(User user, RegisterViewModel model, ControllerBase controller);
		Task SendMail(string to, string subject, string body);

	}
}
