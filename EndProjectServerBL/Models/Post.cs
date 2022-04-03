using System;
using System.Collections.Generic;

#nullable disable

namespace EndProjectServerBL.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            LikesInPosts = new HashSet<LikesInPost>();
            TagsInPosts = new HashSet<TagsInPost>();
        }

        public int Id { get; set; }
        public int TopicId { get; set; }
        public int UserId { get; set; }
        public int NumOfLikes { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public DateTime TimeCreated { get; set; }

        public virtual Topic Topic { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<LikesInPost> LikesInPosts { get; set; }
        public virtual ICollection<TagsInPost> TagsInPosts { get; set; }
    }
}
