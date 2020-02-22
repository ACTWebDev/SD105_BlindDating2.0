using System;
using System.Collections.Generic;

namespace BlindDating.Models
{
    public partial class MailMessage
    {
        public int Id { get; set; }
        public int? FromProfileId { get; set; }
        public int? ToProfileId { get; set; }
        public string MessageTitle { get; set; }
        public string MessageText { get; set; }
        public bool IsRead { get; set; }

        public DatingProfile FromProfile { get; set; }
        public DatingProfile ToProfile { get; set; }
    }
}
