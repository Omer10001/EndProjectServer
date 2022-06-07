using System;
using System.Collections.Generic;
using System.Linq;
using EndProjectServerBL.Models;
using System.Threading.Tasks;

namespace EndProjectServer.DTO
{
    public class CommentDTO
    {
        public Comment Comment { get; set; }
        public LikesInComment LikeInComment { get; set; }
        
    }
}
