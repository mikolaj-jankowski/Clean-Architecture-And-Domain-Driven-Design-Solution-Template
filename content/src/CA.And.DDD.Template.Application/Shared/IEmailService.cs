﻿namespace CA.And.DDD.Template.Application.Shared
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
