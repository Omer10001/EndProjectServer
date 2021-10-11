using System;
using System.Collections.Generic;

#nullable disable

namespace EndProjectServerBL.Models
{
    public partial class LikesInPost
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public bool IsLiked { get; set; }
        public bool IsDisliked { get; set; }
    }
}
