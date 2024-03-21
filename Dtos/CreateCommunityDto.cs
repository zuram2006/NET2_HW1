using Reddit.Models;
using System.ComponentModel.DataAnnotations;

namespace Reddit.Dtos
{
    public class CreateCommunityDto
    {
        [Key]
        public int ID { get; set; }
        public User Owner { get; set; }
        public string Name { get; set; }
    }
}
