﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EndProjectServerBL.Models
{
    public partial class Comment
    {
        public Comment()
        {
            InverseRepliedTo = new HashSet<Comment>();
            LikesInComments = new HashSet<LikesInComment>();
        }

        public int Id { get; set; }
        public int PostId { get; set; }
        public int? RepliedToId { get; set; }
        public int UserId { get; set; }
        public int NumOfLikes { get; set; }
        public string Text { get; set; }
        public DateTime TimeCreated { get; set; }

        public virtual Post Post { get; set; }
        public virtual Comment RepliedTo { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> InverseRepliedTo { get; set; }
        public virtual ICollection<LikesInComment> LikesInComments { get; set; }
    }
}
