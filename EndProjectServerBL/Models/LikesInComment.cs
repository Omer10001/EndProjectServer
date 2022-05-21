using System;
using System.Collections.Generic;

#nullable disable

namespace EndProjectServerBL.Models
{
    public partial class LikesInComment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public bool IsLiked { get; set; }
        public bool IsDisliked { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual User User { get; set; }
    }
}
