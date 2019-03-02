using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Owleet.Models
{
    public class Test
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public bool IsTimeLimit { get; set; }
        public bool IsPrivate { get; set; }
        public int ErrorCount { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public Test()
        {
            Questions = new HashSet<Question>();
        }
    }
}
