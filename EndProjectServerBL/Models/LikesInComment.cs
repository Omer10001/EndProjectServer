using System;
using System.Collections.Generic;

#nullable disable

namespace EndProjectServerBL.Models
{
    public partial class LikesInComment
    {
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public bool IsLiked { get; set; }
        public bool IsDisliked { get; set; }
    }
}
