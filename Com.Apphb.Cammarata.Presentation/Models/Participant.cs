using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Com.Apphb.Cammarata.Presentation.Models
{
    public class Participant : Entity
    {
        public int ParticipantId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AvatarPath { get; set; }
        public string Description { get; set; }
        public bool IsAdmin { get; set; }
    }
}