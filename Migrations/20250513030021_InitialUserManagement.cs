using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SampleUserManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialUserManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastModifiedAt", "LastName", "Status", "UserId" },
                values: new object[,]
                {
                    { new Guid("3b5baaf6-0921-4cf2-3ee8-08dd913eff83"), new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "chisxuan777@gmail.com", "Chris", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ng", 1, "UID00001" },
                    { new Guid("5db0fca6-4387-4d01-3ee9-08dd913eff83"), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "xav1002@gmail.com", "Xavier", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lim", 1, "UID00002" },
                    { new Guid("b120d162-ece4-4290-3eea-08dd913eff83"), new DateTime(2025, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "joeylim002@gmail.com", "Joey", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lim", 1, "UID00003" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
