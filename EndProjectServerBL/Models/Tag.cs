using System;
using System.Collections.Generic;

#nullable disable

namespace EndProjectServerBL.Models
{
    public partial class Tag
    {
        public Tag()
        {
            TagsInPosts = new HashSet<TagsInPost>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TagsInPost> TagsInPosts { get; set; }
    }
}
