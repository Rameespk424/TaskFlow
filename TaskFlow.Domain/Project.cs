using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Domain
{
    public class Project
    {

        public int Id { get; private set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; private set; }
        public int? OwnerId { get; private set; } = 0;
    }
}
