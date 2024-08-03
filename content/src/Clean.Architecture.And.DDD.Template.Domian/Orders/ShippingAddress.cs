namespace Clean.Architecture.And.DDD.Template.Domian.Orders
{
    public record ShippingAddress
    {
        public string Street { get; private set; }
        public string PostalCode { get; private set; }

        public ShippingAddress(string street, string postalCode) //validations
        {
            Street = street;
            PostalCode = postalCode;
        }
    }
}
