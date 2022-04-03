using System;
using System.Collections.Generic;

#nullable disable

namespace EndProjectServerBL.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            LikesInPosts = new HashSet<LikesInPost>();
            Posts = new HashSet<Post>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<LikesInPost> LikesInPosts { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
