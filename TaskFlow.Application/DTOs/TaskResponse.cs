using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.DTOs
{
    public class TaskResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Priority {  get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public int? AssigneeId { get; set; }
        public int ProjectId {  get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
