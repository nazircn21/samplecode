using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class insertEmployeemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create Procedure InsertEmployee

                                @EmployeeName varchar(50),
                                @City varchar(50),
                                @Address varchar(200),
                                @EmployeeSalary decimal(18,0)

                                as
                                Begin
                                Insert Into EmployeeModel(EmployeeName, City, Address, EmployeeSalary, InsertedDate)
                                values (@EmployeeName, @City, @Address, @EmployeeSalary, GETDATE())
                                End";
            migrationBuilder.Sql(procedure);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create Procedure InsertEmployee";
            migrationBuilder.Sql(procedure);
        }
    }
}
