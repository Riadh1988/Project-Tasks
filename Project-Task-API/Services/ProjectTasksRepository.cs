using Project_Task_API.DbContexts;
using Project_Task_API.Entities;
using Project_Task_API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Task_API.Services
{
    public class ProjectTasksRepository : IProjectTasksRepository
    {
        private readonly ProjectTasksContext _context;

        public ProjectTasksRepository(ProjectTasksContext context)
        {
            _context = context ?? throw new ArgumentNullException (nameof(context));
        }

        public void AddProject(Projects project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));
            _context.Projects.Add(project);
        }

        public void AddTask(Guid ProjectId, ProjectTask projectTask)
        {
            if (ProjectId == Guid.Empty)
                throw new ArgumentNullException(nameof(ProjectId));

            if (projectTask == null)
                throw new ArgumentNullException(nameof(projectTask));
            
            projectTask.ProjectTSId = ProjectId;
            _context.ProjectTasks.Add(projectTask);
        }

        public void DeleteProject(Projects project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));
            _context.Projects.Remove(project);
        }

        public void DeleteTask(ProjectTask projectTask)
        {
            if (projectTask == null)
                throw new ArgumentNullException(nameof(projectTask));
            _context.ProjectTasks.Remove(projectTask);
        }

        public Projects GetProject(Guid ProjectId)
        {
            if (ProjectId == Guid.Empty)
                throw new ArgumentNullException(nameof(ProjectId));
            return _context.Projects.FirstOrDefault(b => b.Id == ProjectId);
        }

        public IEnumerable<Projects> GetProjects()
        {
            return _context.Projects.ToList();
        }

        public IEnumerable<Projects> GetProjects(IEnumerable<Guid> ProjectsIds)
        {
            if(ProjectsIds == null)
                throw new ArgumentNullException(nameof(ProjectsIds));
            return _context.Projects.Where(b => ProjectsIds.Contains(b.Id)).OrderBy(b => b.Name).ToList();
        }

        public IEnumerable<Projects> GetProjects(ProjectResorcesParametres projectResorcesParametres)
        {
            if (projectResorcesParametres == null)
                throw new ArgumentNullException(nameof(ProjectResorcesParametres));

            if (string.IsNullOrWhiteSpace(projectResorcesParametres.name) && string.IsNullOrWhiteSpace(projectResorcesParametres.searchQuery))
                return GetProjects();

            var collection = _context.Projects as IQueryable<Projects>;
            if(!string.IsNullOrWhiteSpace(projectResorcesParametres.name))
            {
                var name = projectResorcesParametres.name.Trim();
                collection = collection.Where(b => b.Name == name);
            }
            if (!string.IsNullOrWhiteSpace(projectResorcesParametres.searchQuery))
            {
                var searchQuery = projectResorcesParametres.searchQuery.Trim();
                collection = collection.Where(b => b.Name.Contains(searchQuery));
            }
            return collection.ToList();
        }

        public ProjectTask GetTask(Guid ProjectId, Guid ProjectTasksId)
        {
            if (ProjectId == Guid.Empty)
                throw new ArgumentNullException(nameof(ProjectId));

            if (ProjectTasksId == Guid.Empty)
                throw new ArgumentNullException(nameof(ProjectTasksId));
            return _context.ProjectTasks.Where(a => a.ProjectTSId == ProjectId && a.Id == ProjectTasksId).FirstOrDefault();
        }

        public IEnumerable<ProjectTask> GetTasks(Guid ProjectId)
        {
            if (ProjectId == Guid.Empty)
                throw new ArgumentNullException(nameof(ProjectId));

            return _context.ProjectTasks.Where(a => a.ProjectTSId == ProjectId).OrderBy(a => a.Name).ToList();
        }

        public bool ProjectExisit(Guid ProjectId)
        {
            if (ProjectId == Guid.Empty)
                throw new ArgumentNullException(nameof(ProjectId));
            return _context.Projects.Any(b => b.Id == ProjectId);
        }

        public bool ProjectTaskExist(Guid ProjectTaskId)
        {
            if(ProjectTaskId == Guid.Empty)
                throw new ArgumentNullException(nameof(ProjectTaskId));
            return _context.ProjectTasks.Any(a => a.Id == ProjectTaskId);
        }

        public bool Save()
        {
            return(_context.SaveChanges() >= 0);
        }

        public void UpdateProject(Projects project)
        {
            //not implemented
        }

        public void UpdateTask(ProjectTask projectTask)
        {
            //not implemented
        }
    }
}
