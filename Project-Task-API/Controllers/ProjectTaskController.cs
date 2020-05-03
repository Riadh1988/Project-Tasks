using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Project_Task_API.Models;
using Project_Task_API.Services;
using System;
using System.Collections.Generic;

namespace Project_Task_API.Controllers
{
    [ApiController]
    [Route("api/projects/{projectId}/tasks")]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IProjectTasksRepository _ProjectTasksRepository;
        private readonly IMapper _mapper;
        public ProjectTaskController(IProjectTasksRepository ProjectTasksRepository, IMapper mapper)
        {
            _ProjectTasksRepository = ProjectTasksRepository ??
                throw new ArgumentNullException(nameof(ProjectTasksRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskDto>> GetTasksForProject(Guid ProjectId)
        {
            if (!_ProjectTasksRepository.ProjectExisit(ProjectId))
                return NotFound();
            var tasksFromRepo = _ProjectTasksRepository.GetTasks(ProjectId);
            return Ok(_mapper.Map<IEnumerable<TaskDto>>(tasksFromRepo));
        }

        [HttpGet("ProjectTasksId", Name ="GetTaskInProject")]
        public ActionResult<TaskDto> GetTaskInProject(Guid ProjectId, Guid ProjectTasksId)
        {
            if (!_ProjectTasksRepository.ProjectExisit(ProjectId))
                return NotFound();

            var taskFromRepo = _ProjectTasksRepository.GetTask(ProjectId, ProjectTasksId);
           
            if (taskFromRepo == null)
                return NotFound();

            return Ok(_mapper.Map<TaskDto>(taskFromRepo));
        }

        [HttpPost]
        public ActionResult<TaskDto> CreateTaskForProject(Guid projectId, [FromBody] TaskForCreatingDto tasks)
        {
            if (!_ProjectTasksRepository.ProjectExisit(projectId))
                return NotFound();

            var TaskEntity = _mapper.Map<Entities.ProjectTask>(tasks);
            _ProjectTasksRepository.AddTask(projectId, TaskEntity);
            _ProjectTasksRepository.Save();

            var taskToReturn = _mapper.Map<TaskDto>(TaskEntity);
            return CreatedAtRoute("GetTaskInProject", new{projectId = projectId,TaskId = taskToReturn.Id}, taskToReturn);
        }


        [HttpPut("{taskId}")]
        public ActionResult UpdatetaskForproject(Guid projectId, Guid taskId,
            [FromBody] TaskForUpdatingDto task)
        {
            if (!_ProjectTasksRepository.ProjectExisit(projectId))
                return NotFound();

            var taskFromRepo = _ProjectTasksRepository.GetTask(projectId, taskId);
            if (taskFromRepo == null)
            {
                var taskToAdd = _mapper.Map<Entities.ProjectTask>(task);
                taskToAdd.Id = taskId;
                _ProjectTasksRepository.AddTask(projectId, taskToAdd);
                _ProjectTasksRepository.Save();

                var taskToReturn = _mapper.Map<TaskDto>(taskToAdd);

                return CreatedAtRoute("GettaskForproject", new { projectId = projectId, taskId = taskToReturn.Id }, taskToReturn);
            }

            _mapper.Map(task, taskFromRepo);
            _ProjectTasksRepository.UpdateTask(taskFromRepo);
            _ProjectTasksRepository.Save();

            return NoContent();
        }

        [HttpPatch("{taskId}")]
        public ActionResult PartiallyUpdatetaskForproject(Guid projectId, Guid taskId,
           [FromBody] JsonPatchDocument<TaskForUpdatingDto> patchDocument)
        {
            if (!_ProjectTasksRepository.ProjectExisit(projectId))
                return NotFound();

            var taskFromRepo = _ProjectTasksRepository.GetTask(projectId, taskId);
            if (taskFromRepo == null)
            {
                var taskDto = new TaskForUpdatingDto();
                patchDocument.ApplyTo(taskDto);
                var taskToAdd = _mapper.Map<Entities.ProjectTask>(taskDto);
                taskToAdd.Id = taskId;

                _ProjectTasksRepository.AddTask(projectId, taskToAdd);
                _ProjectTasksRepository.Save();

                var taskToReturn = _mapper.Map<TaskDto>(taskToAdd);

                return CreatedAtRoute("GettaskForproject", new { projectId = projectId, taskId = taskToReturn.Id }, taskToReturn);
            }

            var taskToPatch = _mapper.Map<TaskForUpdatingDto>(taskFromRepo);
            patchDocument.ApplyTo(taskToPatch);

            if (!TryValidateModel(taskToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(taskToPatch, taskFromRepo);
            _ProjectTasksRepository.UpdateTask(taskFromRepo);
            _ProjectTasksRepository.Save();

            return NoContent();
        }

        [HttpDelete("{taskId}")]
        public ActionResult DeletetaskForproject(Guid projectId, Guid taskId)
        {
            if (!_ProjectTasksRepository.ProjectExisit(projectId))
                return NotFound();

            var taskFromRepo = _ProjectTasksRepository.GetTask(projectId, taskId);

            if (taskFromRepo == null)
                return NotFound();

            _ProjectTasksRepository.DeleteTask(taskFromRepo);
            _ProjectTasksRepository.Save();

            return NoContent();
        }


    }
}
