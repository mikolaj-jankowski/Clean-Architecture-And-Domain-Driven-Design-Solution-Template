using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.And.DDD.Template.Domian.Customers
{
    public sealed record Address (string Street, string HouseNumber, string FlatNumber, string Country, string PostalCode);
}
