using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Task_API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ProjectTSId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTasks_Projects_ProjectTSId",
                        column: x => x.ProjectTSId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Code", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("84faaa9e-7712-4bab-a1d7-b7468b1b7669"), "ProjectCode", "ProjectDescription", "ProjectName" },
                    { new Guid("f2118fd5-a17a-4a78-94d0-2cf842b737ff"), "ProjectCode1", "ProjectDescription1", "ProjectName1" },
                    { new Guid("47d383cf-d037-47a7-af6a-43f3792df83c"), "ProjectCode2", "ProjectDescription2", "ProjectName2" },
                    { new Guid("49dfddc7-01af-4689-87cd-cb1ebfd6db8e"), "ProjectCode3", "ProjectDescription3", "ProjectName3" },
                    { new Guid("1d829086-34c5-4a2a-a582-f832f415c7a6"), "ProjectCode4", "ProjectDescription4", "ProjectName4" }
                });

            migrationBuilder.InsertData(
                table: "ProjectTasks",
                columns: new[] { "Id", "Description", "Name", "ProjectTSId" },
                values: new object[] { new Guid("382ad33f-74b4-4d24-9c7c-93c7c438d658"), "Task Description2", "Task2", new Guid("47d383cf-d037-47a7-af6a-43f3792df83c") });

            migrationBuilder.InsertData(
                table: "ProjectTasks",
                columns: new[] { "Id", "Description", "Name", "ProjectTSId" },
                values: new object[] { new Guid("42e34542-1620-477d-8305-0411be62a569"), "Task Description3", "Task3", new Guid("47d383cf-d037-47a7-af6a-43f3792df83c") });

            migrationBuilder.InsertData(
                table: "ProjectTasks",
                columns: new[] { "Id", "Description", "Name", "ProjectTSId" },
                values: new object[] { new Guid("8bf7b21b-9776-4331-9f4a-cb5a3e99b5f7"), "Task Description", "Task", new Guid("1d829086-34c5-4a2a-a582-f832f415c7a6") });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_ProjectTSId",
                table: "ProjectTasks",
                column: "ProjectTSId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectTasks");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
