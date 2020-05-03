using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Task_API.Models
{
    public abstract class TaskManipulationDto
    {
        public string Title { get; set; } 
        public virtual string Description { get; set; }
    }
}
