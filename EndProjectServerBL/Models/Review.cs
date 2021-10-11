﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EndProjectServerBL.Models
{
    public partial class Review
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public int Score { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime TimeCreated { get; set; }

        public virtual Topic Topic { get; set; }
        public virtual User User { get; set; }
    }
}
