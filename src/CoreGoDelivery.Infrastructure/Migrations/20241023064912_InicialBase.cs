using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoreGoDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InicialBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbgodelivery");

            migrationBuilder.CreateTable(
                name: "tb_licenceDriver",
                schema: "dbgodelivery",
                columns: table => new
                {
                    ID_LICENSE_DRIVER = table.Column<string>(type: "text", nullable: false),
                    ID_LICENSE_TYPE = table.Column<int>(type: "integer", nullable: false),
                    IMAGE_URL_REFERENCE = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_licenceDriver", x => x.ID_LICENSE_DRIVER);
                });

            migrationBuilder.CreateTable(
                name: "tb_modelMotorcycle",
                schema: "dbgodelivery",
                columns: table => new
                {
                    ID_MODEL_MOTORCYCLE = table.Column<string>(type: "text", nullable: false),
                    NAME = table.Column<string>(type: "text", nullable: false),
                    NORMALIZED_NAME = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_modelMotorcycle", x => x.ID_MODEL_MOTORCYCLE);
                });

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

            migrationBuilder.CreateTable(
                name: "tb_rentalPlan",
                schema: "dbgodelivery",
                columns: table => new
                {
                    ID_RENTAL_PLAN = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DAYS_QUANTITY = table.Column<int>(type: "integer", nullable: false),
                    DAYLI_COST = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_rentalPlan", x => x.ID_RENTAL_PLAN);
                });

            migrationBuilder.CreateTable(
                name: "tb_deliverier",
                schema: "dbgodelivery",
                columns: table => new
                {
                    ID_DELIVERIER = table.Column<string>(type: "text", nullable: false),
                    FULL_NAME = table.Column<string>(type: "text", nullable: false),
                    CNPJ = table.Column<string>(type: "text", nullable: false),
                    DATE_BIRTH = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ID_FK_LICENSE_DRIVER = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_deliverier", x => x.ID_DELIVERIER);
                    table.ForeignKey(
                        name: "FK_tb_deliverier_tb_licenceDriver_ID_FK_LICENSE_DRIVER",
                        column: x => x.ID_FK_LICENSE_DRIVER,
                        principalSchema: "dbgodelivery",
                        principalTable: "tb_licenceDriver",
                        principalColumn: "ID_LICENSE_DRIVER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_motorcycle",
                schema: "dbgodelivery",
                columns: table => new
                {
                    ID_MOTORCYCLE = table.Column<string>(type: "text", nullable: false),
                    YEAR_MANUFACTURE = table.Column<int>(type: "integer", nullable: false),
                    PLATE_NORMALIZED = table.Column<string>(type: "text", nullable: false),
                    ID_FK_MODEL_MOTORCYCLE = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_motorcycle", x => x.ID_MOTORCYCLE);
                    table.ForeignKey(
                        name: "FK_tb_motorcycle_tb_modelMotorcycle_ID_FK_MODEL_MOTORCYCLE",
                        column: x => x.ID_FK_MODEL_MOTORCYCLE,
                        principalSchema: "dbgodelivery",
                        principalTable: "tb_modelMotorcycle",
                        principalColumn: "ID_MODEL_MOTORCYCLE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_rental",
                schema: "dbgodelivery",
                columns: table => new
                {
                    ID_RENTAL = table.Column<string>(type: "text", nullable: false),
                    DATE_START = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DATE_END = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DATE_ESTIMATED_RETURN = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DATE_RETURNED_TO_BASE = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ID_FK_DELIVERIER = table.Column<string>(type: "text", nullable: true),
                    ID_FK_MOTORCYCLE = table.Column<string>(type: "text", nullable: true),
                    ID_FK_RENTAL_PLAN = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_rental", x => x.ID_RENTAL);
                    table.ForeignKey(
                        name: "FK_tb_rental_tb_deliverier_ID_FK_DELIVERIER",
                        column: x => x.ID_FK_DELIVERIER,
                        principalSchema: "dbgodelivery",
                        principalTable: "tb_deliverier",
                        principalColumn: "ID_DELIVERIER");
                    table.ForeignKey(
                        name: "FK_tb_rental_tb_motorcycle_ID_FK_MOTORCYCLE",
                        column: x => x.ID_FK_MOTORCYCLE,
                        principalSchema: "dbgodelivery",
                        principalTable: "tb_motorcycle",
                        principalColumn: "ID_MOTORCYCLE");
                    table.ForeignKey(
                        name: "FK_tb_rental_tb_rentalPlan_ID_FK_RENTAL_PLAN",
                        column: x => x.ID_FK_RENTAL_PLAN,
                        principalSchema: "dbgodelivery",
                        principalTable: "tb_rentalPlan",
                        principalColumn: "ID_RENTAL_PLAN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_deliverier_ID_FK_LICENSE_DRIVER",
                schema: "dbgodelivery",
                table: "tb_deliverier",
                column: "ID_FK_LICENSE_DRIVER");

            migrationBuilder.CreateIndex(
                name: "IX_tb_motorcycle_ID_FK_MODEL_MOTORCYCLE",
                schema: "dbgodelivery",
                table: "tb_motorcycle",
                column: "ID_FK_MODEL_MOTORCYCLE");

            migrationBuilder.CreateIndex(
                name: "IX_tb_rental_ID_FK_DELIVERIER",
                schema: "dbgodelivery",
                table: "tb_rental",
                column: "ID_FK_DELIVERIER");

            migrationBuilder.CreateIndex(
                name: "IX_tb_rental_ID_FK_MOTORCYCLE",
                schema: "dbgodelivery",
                table: "tb_rental",
                column: "ID_FK_MOTORCYCLE");

            migrationBuilder.CreateIndex(
                name: "IX_tb_rental_ID_FK_RENTAL_PLAN",
                schema: "dbgodelivery",
                table: "tb_rental",
                column: "ID_FK_RENTAL_PLAN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_notificationMotorcycle",
                schema: "dbgodelivery");

            migrationBuilder.DropTable(
                name: "tb_rental",
                schema: "dbgodelivery");

            migrationBuilder.DropTable(
                name: "tb_deliverier",
                schema: "dbgodelivery");

            migrationBuilder.DropTable(
                name: "tb_motorcycle",
                schema: "dbgodelivery");

            migrationBuilder.DropTable(
                name: "tb_rentalPlan",
                schema: "dbgodelivery");

            migrationBuilder.DropTable(
                name: "tb_licenceDriver",
                schema: "dbgodelivery");

            migrationBuilder.DropTable(
                name: "tb_modelMotorcycle",
                schema: "dbgodelivery");
        }
    }
}
