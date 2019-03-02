using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Owleet.Models
{
    public class UserAnswer
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}
