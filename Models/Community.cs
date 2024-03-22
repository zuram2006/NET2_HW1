using System.ComponentModel.DataAnnotations;

namespace Reddit.Models
{
    public class Community
    {
        [Key]
        public int ID { get; set; }
        public User Owner { get; set; } = new User();
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ICollection<User> Subscribers { get; set; } = new List<User>();


    }
}
