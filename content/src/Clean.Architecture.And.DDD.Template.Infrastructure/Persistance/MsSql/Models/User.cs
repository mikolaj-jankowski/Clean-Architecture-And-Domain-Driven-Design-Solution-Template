namespace Clean.Architecture.And.DDD.Template.Infrastructure.Database.MsSql.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}
