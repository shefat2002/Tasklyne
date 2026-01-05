using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasklyne.Data.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AssignedTasks_EmployeeId",
                table: "AssignedTasks",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedTasks_AspNetUsers_EmployeeId",
                table: "AssignedTasks",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedTasks_AspNetUsers_EmployeeId",
                table: "AssignedTasks");

            migrationBuilder.DropIndex(
                name: "IX_AssignedTasks_EmployeeId",
                table: "AssignedTasks");
        }
    }
}
