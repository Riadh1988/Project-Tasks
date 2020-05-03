using Project_Task_API.Entities;
using Project_Task_API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Task_API.Services
{
    public interface IProjectTasksRepository
    {
        IEnumerable<ProjectTask> GetTasks(Guid ProjectId);
        ProjectTask GetTask(Guid ProjectId, Guid ProjectTasksId);
        void AddTask(Guid ProjectId, ProjectTask projectTask);
        void UpdateTask(ProjectTask projectTask);
        void DeleteTask(ProjectTask projectTask);

        IEnumerable<Projects> GetProjects();
        Projects GetProject(Guid ProjectId);
        IEnumerable<Projects> GetProjects(IEnumerable<Guid> ProjectsIds);
        IEnumerable<Projects> GetProjects(ProjectResorcesParametres projectResorcesParametres);
        void AddProject(Projects project);
        void UpdateProject(Projects project);
        void DeleteProject(Projects project);

        bool ProjectExisit(Guid ProjectId);
        bool ProjectTaskExist(Guid ProjectTaskId);
        bool Save();

    }
}
