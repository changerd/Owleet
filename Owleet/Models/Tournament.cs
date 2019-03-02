using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Owleet.Models
{
    public class Tournament : IEntity
    {
        public Tournament()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public int Prize { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateStop { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
