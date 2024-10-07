using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Business_Card.Migrations
{
    /// <inheritdoc />
    public partial class NewDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessCards",
                columns: table => new
                {
                    BusinessCardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessCard_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BusinessCard_Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BusinessCard_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessCard_Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BusinessCard_Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BusinessCard_Date_Of_Birth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", maxLength: 1400000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCards", x => x.BusinessCardId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessCards");
        }
    }
}
