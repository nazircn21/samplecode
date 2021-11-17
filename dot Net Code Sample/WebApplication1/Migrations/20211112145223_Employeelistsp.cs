using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class Employeelistsp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create procedure Employeelist
                                @EmployeeID int
                                as 
                                Begin
                                Select EmployeeID, EmployeeName, City, Address, EmployeeSalary, InsertedDate, UpdatedDate from EmployeeModel
                                Where EmployeeID = @EmployeeID
                                End";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Drop Create procedure Employeelist";
            migrationBuilder.Sql(procedure);

        }
    }
}
