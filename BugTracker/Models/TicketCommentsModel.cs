﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketCommentsModel
    {
        public TicketCommentsModel()
        {
            Created = DateTime.Now;
        }
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset Created { get; set; }
        public int TicketId { get; set; }
        public virtual TicketModel Ticket { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}

