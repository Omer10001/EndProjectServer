using System;
using System.Collections.Generic;
using System.Linq;
using EndProjectServerBL.Models;
using System.Threading.Tasks;

namespace EndProjectServer.DTO
{
    public class PostDTO
    {
        public Post Post { get; set; }
        public LikesInPost LikeInPost { get; set; }

    
        public PostDTO()
        {
            
         
        }

    }
}
