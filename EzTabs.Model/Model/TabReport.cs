﻿using EzTabs.Model.Model.BaseClasses;

namespace EzTabs.Model.Model
{
    public class TabReport : Dated
    {
        public string? Text { get; set; }
        public Guid UserId { get; set; }
        public Guid TabId { get; set; }
        public User? User { get; set; }
        public Tab? Tab { get; set; }
    }
}
