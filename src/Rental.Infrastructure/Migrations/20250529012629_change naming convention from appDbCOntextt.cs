using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changenamingconventionfromappDbCOntextt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_customers_",
                table: "customers_");

            migrationBuilder.DropPrimaryKey(
                name: "PK_activitiy_logs_",
                table: "activitiy_logs_");

            migrationBuilder.RenameTable(
                name: "customers_",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "activitiy_logs_",
                newName: "activitylogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_activitylogs",
                table: "activitylogs",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_activitylogs",
                table: "activitylogs");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "customers_");

            migrationBuilder.RenameTable(
                name: "activitylogs",
                newName: "activitiy_logs_");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customers_",
                table: "customers_",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_activitiy_logs_",
                table: "activitiy_logs_",
                column: "id");
        }
    }
}
