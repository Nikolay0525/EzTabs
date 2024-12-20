﻿using EzTabs.Data.Domain.BaseModels;

namespace EzTabs.Data.Domain;

public sealed class Comment : MessageBase
{
    public Guid TabId { get; set; }
    public Guid ParentCommentId { get; set; } = Guid.Empty;
    public int Likes { get; set; } = 0;
    public Tab? Tab { get; set; }
    public List<CommentReport>? CommentReports { get; set; }
    public List<CommentRate>? CommentRates { get; set; }
}
