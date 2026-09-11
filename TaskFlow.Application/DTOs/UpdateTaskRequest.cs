using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.DTOs
{
    public  class UpdateTaskRequest
    {

        
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Status { get; set; } 
        public int Priority { get; set; } 
        public int? AssigneeId { get; set; }
        public int ProjectId { get; set; }
    }
}
