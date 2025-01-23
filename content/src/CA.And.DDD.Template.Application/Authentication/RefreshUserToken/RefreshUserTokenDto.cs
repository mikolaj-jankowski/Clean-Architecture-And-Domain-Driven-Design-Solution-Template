namespace CA.And.DDD.Template.Application.Authentication.ReLoginCustomer
{
    public sealed record RefreshUserTokenDto(string AccessToken, string RefreshToken);
}
