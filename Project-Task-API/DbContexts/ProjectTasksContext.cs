using Microsoft.EntityFrameworkCore;
using Project_Task_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Task_API.DbContexts
{
    public class ProjectTasksContext : DbContext
    {
        public ProjectTasksContext(DbContextOptions<ProjectTasksContext> options) : base(options)
        {

        }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Projects>().HasData(new Projects()
            {
                Id = Guid.Parse("84faaa9e-7712-4bab-a1d7-b7468b1b7669"),
                Name = ("ProjectName"),
                Description = ("ProjectDescription"),
                Code = ("ProjectCode")
            }, new Projects()
            {
                Id = Guid.Parse("f2118fd5-a17a-4a78-94d0-2cf842b737ff"),
                Name = ("ProjectName1"),
                Description = ("ProjectDescription1"),
                Code = ("ProjectCode1")
            }, new Projects()
            {
                Id = Guid.Parse("47d383cf-d037-47a7-af6a-43f3792df83c"),
                Name = ("ProjectName2"),
                Description = ("ProjectDescription2"),
                Code = ("ProjectCode2")
            }, new Projects()
            {
                Id = Guid.Parse("49dfddc7-01af-4689-87cd-cb1ebfd6db8e"),
                Name = ("ProjectName3"),
                Description = ("ProjectDescription3"),
                Code = ("ProjectCode3")
            }, new Projects()
            {
                Id = Guid.Parse("1d829086-34c5-4a2a-a582-f832f415c7a6"),
                Name = ("ProjectName4"),
                Description = ("ProjectDescription4"),
                Code = ("ProjectCode4")
            });

            modelBuilder.Entity<ProjectTask>().HasData(new ProjectTask
            {
                Id = Guid.Parse("8bf7b21b-9776-4331-9f4a-cb5a3e99b5f7"),
                Name = ("Task"),
                Description = ("Task Description"),
                ProjectTSId = Guid.Parse("1d829086-34c5-4a2a-a582-f832f415c7a6")
            }, new ProjectTask
            {
                Id = Guid.Parse("382ad33f-74b4-4d24-9c7c-93c7c438d658"),
                Name = ("Task2"),
                Description = ("Task Description2"),
                ProjectTSId = Guid.Parse("47d383cf-d037-47a7-af6a-43f3792df83c")
            }, new ProjectTask
            {
                Id = Guid.Parse("42e34542-1620-477d-8305-0411be62a569"),
                Name = ("Task3"),
                Description = ("Task Description3"),
                ProjectTSId = Guid.Parse("47d383cf-d037-47a7-af6a-43f3792df83c")
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
