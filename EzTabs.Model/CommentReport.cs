﻿using EzTabs.Model.BaseModels;

namespace EzTabs.Model
{
    public class CommentReport
    {
        public string? Text { get; set; }
        public Guid UserId { get; set; }
        public Guid CommentId { get; set; }
        public User? User { get; set; }
        public Comment? Comment { get; set; }
        public DateTime DateOfCreation { get; private set; } = DateTime.Now;
    }
}
