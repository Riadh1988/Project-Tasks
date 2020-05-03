using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project_Task_API.Helpers;
using Project_Task_API.Models;
using Project_Task_API.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Task_API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectTasksRepository _ProjectTasksRepository;
        private readonly IMapper _mapper;
        public ProjectController(IProjectTasksRepository ProjectTasksRepository, IMapper mapper)
        {
            _ProjectTasksRepository = ProjectTasksRepository ??
                throw new ArgumentNullException(nameof(ProjectTasksRepository));
            _mapper = mapper ??
                 throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetProjects([FromQuery] ProjectResorcesParametres projectResorcesParametres)
        {
            var ProjectsFromRepo = _ProjectTasksRepository.GetProjects(projectResorcesParametres);
            var ProjectDto = new List<ProjectDto>();
            return Ok(_mapper.Map<IEnumerable<ProjectDto>> (ProjectsFromRepo));
        }

        [HttpGet ("{projectId}", Name = "GetProject")]
        public IActionResult GetProject(Guid projectId)
        {
            var ProjectFromRepo = _ProjectTasksRepository.GetProject(projectId);

            if (ProjectFromRepo == null)
                return NotFound();

            return Ok(ProjectFromRepo);
        }

        [HttpPost]
        public ActionResult<ProjectDto> CreateProject([FromBody] createProjrctDto project)
        {
            var projectEntity = _mapper.Map<Entities.Projects>(project);
            _ProjectTasksRepository.AddProject(projectEntity);
            _ProjectTasksRepository.Save();

            var projectToreturn = _mapper.Map<ProjectDto>(projectEntity);
            return CreatedAtRoute("GetProject", new { projectId = projectToreturn.Id }, projectToreturn);
        }


        [HttpDelete("{projectId}")]
        public ActionResult DeleteProject(Guid projectId)
        {
            var projectFromRepo = _ProjectTasksRepository.GetProject(projectId);
            if (projectFromRepo == null)
                return NotFound();

            _ProjectTasksRepository.DeleteProject(projectFromRepo);
            _ProjectTasksRepository.Save();

            return NoContent();
        }
    }
}
