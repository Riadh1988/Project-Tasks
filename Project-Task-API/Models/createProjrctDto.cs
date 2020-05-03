using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Task_API.Models
{
    public class createProjrctDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public ICollection<TaskForCreatingDto> Tasks { get; set; } = new List<TaskForCreatingDto>();
    }
}
