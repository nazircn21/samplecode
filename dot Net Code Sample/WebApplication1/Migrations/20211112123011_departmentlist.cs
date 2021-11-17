using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class departmentlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create procedure departmentlist
                               @DepartmentID int
                               as
                               begin
                               Select DepartmentID, DepoartmentName, PrimaryContactNumber, IsActive, InsertedDate, UpdatedDate from Department
                               where DepartmentID = @DepartmentID
                               End";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create procedure departmentlist";
            migrationBuilder.Sql(procedure);
        }
    }
}
