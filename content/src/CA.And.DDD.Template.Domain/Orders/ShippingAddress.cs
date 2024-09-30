namespace CA.And.DDD.Template.Domain.Orders
{
    public sealed record ShippingAddress
    {
        public string Street { get; private set; }
        public string PostalCode { get; private set; }

        public ShippingAddress(string street, string postalCode) 
        {
            Street = street;
            PostalCode = postalCode;
        }
    }
}
