namespace CA.And.DDD.Template.Application.Customer.ChangeEmail
{
    public sealed record ChangeEmailCommand(Guid CustomerId, string NewEmail);
}
