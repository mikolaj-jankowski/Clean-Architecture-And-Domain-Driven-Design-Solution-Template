using System.ComponentModel.DataAnnotations;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Database.MsSql.Models
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Surname { get; set; }
        public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    }
}
