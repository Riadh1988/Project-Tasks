using AutoMapper;
using Project_Task_API.Models;

namespace Project_Task_API.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Entities.ProjectTask, Models.TaskDto>().ReverseMap();
            CreateMap<TaskForCreatingDto, Entities.ProjectTask>(); 
            CreateMap<Models.TaskForUpdatingDto, Entities.ProjectTask>().ReverseMap();
        }
    }
}
