using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Management.Migrations
{
    /// <inheritdoc />
    public partial class PositionNameAndDepartmentName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Department_DepartmentId1",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Positions_PositionId1",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepartmentId1",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PositionId1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DepartmentId1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PositionId1",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "Employees",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Employees",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");


            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Department_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Positions_PositionId",
                table: "Employees",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Department_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Positions_PositionId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PositionId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PositionName",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "PositionId",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentId",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId1",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PositionId1",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId1",
                table: "Employees",
                column: "DepartmentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId1",
                table: "Employees",
                column: "PositionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Department_DepartmentId1",
                table: "Employees",
                column: "DepartmentId1",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Positions_PositionId1",
                table: "Employees",
                column: "PositionId1",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
