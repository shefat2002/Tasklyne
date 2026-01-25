using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasklyne.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixed_entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedTasks_TaskList_TaskListId",
                table: "AssignedTasks");

            migrationBuilder.DropTable(
                name: "TaskList");

            migrationBuilder.DropIndex(
                name: "IX_AssignedTasks_TaskListId",
                table: "AssignedTasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskList_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskList_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTasks_TaskListId",
                table: "AssignedTasks",
                column: "TaskListId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_EmployeeId",
                table: "TaskList",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_ProjectId",
                table: "TaskList",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedTasks_TaskList_TaskListId",
                table: "AssignedTasks",
                column: "TaskListId",
                principalTable: "TaskList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
