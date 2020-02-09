using System;
using System.Collections.Generic;

namespace BlindDating.Models
{
    public partial class DatingProfile
    {
        public DatingProfile()
        {
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Bio { get; set; }
        public string UserAccountId { get; set; }

    }
}
