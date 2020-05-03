using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Task_API.Entities
{
    public class ProjectTask
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }


        [ForeignKey("ProjectTSId")]
        public Projects Project { get; set; }
        public Guid ProjectTSId { get; set; }
    }
}
