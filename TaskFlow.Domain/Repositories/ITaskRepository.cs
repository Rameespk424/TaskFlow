using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Domain.Repositories
{
    public interface ITaskRepository
    {
        TaskItem? GetById(int Id);
    }
}
