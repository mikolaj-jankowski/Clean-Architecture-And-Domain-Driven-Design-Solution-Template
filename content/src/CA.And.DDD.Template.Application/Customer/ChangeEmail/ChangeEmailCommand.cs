namespace CA.And.DDD.Template.Application.Customer.ChangeEmail
{
    public sealed record ChangeEmailCommand(string OldEmail, string NewEmail);
}
