using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreGoDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTbNotificationMotorcycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_notificationMotorcycle",
                schema: "dbgodelivery",
                columns: table => new
                {
                    ID_NOTIFICATION = table.Column<string>(type: "text", nullable: false),
                    ID_MOTORCYCLE = table.Column<string>(type: "text", nullable: false),
                    YEAR_MANUFACTURE = table.Column<int>(type: "integer", nullable: false),
                    DATE_CREATED = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_notificationMotorcycle", x => x.ID_NOTIFICATION);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_notificationMotorcycle",
                schema: "dbgodelivery");
        }
    }
}
