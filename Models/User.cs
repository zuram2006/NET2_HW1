﻿using System.ComponentModel.DataAnnotations;

namespace Reddit.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Post>? Posts { get; set; } = new List<Post>();
        public virtual ICollection<Community>? subscribedCommunities { get; set; } = new List<Community>();

    }
}