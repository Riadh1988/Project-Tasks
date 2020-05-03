using AutoMapper;

namespace Project_Task_API.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Entities.Projects, Models.ProjectDto>();
            CreateMap<Models.createProjrctDto, Entities.Projects>();
        }
    }
}
