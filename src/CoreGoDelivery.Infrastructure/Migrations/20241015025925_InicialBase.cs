using System;
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
                name: "godeliverydb");

            migrationBuilder.CreateTable(
                name: "tb_licenceDriver",
                schema: "godeliverydb",
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
                name: "tb_modelMotocycle",
                schema: "godeliverydb",
                columns: table => new
                {
                    ID_FK_MODEL_MOTOCYCLE = table.Column<string>(type: "text", nullable: false),
                    NAME = table.Column<string>(type: "text", nullable: false),
                    NORMALIZED_NAME = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_modelMotocycle", x => x.ID_FK_MODEL_MOTOCYCLE);
                });

            migrationBuilder.CreateTable(
                name: "tb_rentalPlan",
                schema: "godeliverydb",
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
                schema: "godeliverydb",
                columns: table => new
                {
                    ID_DELIVERIER = table.Column<string>(type: "text", nullable: false),
                    FULL_NAME = table.Column<string>(type: "text", nullable: false),
                    CNPJ = table.Column<string>(type: "text", nullable: false),
                    DATE_BIRTH = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ID_FK_LICENSE_NUMBER = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_deliverier", x => x.ID_DELIVERIER);
                    table.ForeignKey(
                        name: "FK_tb_deliverier_tb_licenceDriver_ID_FK_LICENSE_NUMBER",
                        column: x => x.ID_FK_LICENSE_NUMBER,
                        principalSchema: "godeliverydb",
                        principalTable: "tb_licenceDriver",
                        principalColumn: "ID_LICENSE_DRIVER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_motocycle",
                schema: "godeliverydb",
                columns: table => new
                {
                    ID_MOTOCYCLE = table.Column<string>(type: "text", nullable: false),
                    YEAR_MANUFACTURE = table.Column<int>(type: "integer", nullable: false),
                    ID_PLATE_NORMALIZED = table.Column<string>(type: "text", nullable: false),
                    ID_FK_NODEL_MOTOCYCLE = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_motocycle", x => x.ID_MOTOCYCLE);
                    table.ForeignKey(
                        name: "FK_tb_motocycle_tb_modelMotocycle_ID_FK_NODEL_MOTOCYCLE",
                        column: x => x.ID_FK_NODEL_MOTOCYCLE,
                        principalSchema: "godeliverydb",
                        principalTable: "tb_modelMotocycle",
                        principalColumn: "ID_FK_MODEL_MOTOCYCLE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_rental",
                schema: "godeliverydb",
                columns: table => new
                {
                    ID_RENTAL = table.Column<string>(type: "text", nullable: false),
                    DATE_START = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DATE_END = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DATE_ESTIMATED_RETURN = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DATE_RETURNED_TO_BASE = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ID_FK_DELIVERIER = table.Column<string>(type: "text", nullable: false),
                    ID_FK_MOTOCYCLE = table.Column<string>(type: "text", nullable: false),
                    ID_FK_RENTAL_PLAN = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_rental", x => x.ID_RENTAL);
                    table.ForeignKey(
                        name: "FK_tb_rental_tb_deliverier_ID_FK_DELIVERIER",
                        column: x => x.ID_FK_DELIVERIER,
                        principalSchema: "godeliverydb",
                        principalTable: "tb_deliverier",
                        principalColumn: "ID_DELIVERIER",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_rental_tb_motocycle_ID_FK_MOTOCYCLE",
                        column: x => x.ID_FK_MOTOCYCLE,
                        principalSchema: "godeliverydb",
                        principalTable: "tb_motocycle",
                        principalColumn: "ID_MOTOCYCLE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_rental_tb_rentalPlan_ID_FK_RENTAL_PLAN",
                        column: x => x.ID_FK_RENTAL_PLAN,
                        principalSchema: "godeliverydb",
                        principalTable: "tb_rentalPlan",
                        principalColumn: "ID_RENTAL_PLAN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_deliverier_ID_FK_LICENSE_NUMBER",
                schema: "godeliverydb",
                table: "tb_deliverier",
                column: "ID_FK_LICENSE_NUMBER");

            migrationBuilder.CreateIndex(
                name: "IX_tb_motocycle_ID_FK_NODEL_MOTOCYCLE",
                schema: "godeliverydb",
                table: "tb_motocycle",
                column: "ID_FK_NODEL_MOTOCYCLE");

            migrationBuilder.CreateIndex(
                name: "IX_tb_rental_ID_FK_DELIVERIER",
                schema: "godeliverydb",
                table: "tb_rental",
                column: "ID_FK_DELIVERIER");

            migrationBuilder.CreateIndex(
                name: "IX_tb_rental_ID_FK_MOTOCYCLE",
                schema: "godeliverydb",
                table: "tb_rental",
                column: "ID_FK_MOTOCYCLE");

            migrationBuilder.CreateIndex(
                name: "IX_tb_rental_ID_FK_RENTAL_PLAN",
                schema: "godeliverydb",
                table: "tb_rental",
                column: "ID_FK_RENTAL_PLAN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_rental",
                schema: "godeliverydb");

            migrationBuilder.DropTable(
                name: "tb_deliverier",
                schema: "godeliverydb");

            migrationBuilder.DropTable(
                name: "tb_motocycle",
                schema: "godeliverydb");

            migrationBuilder.DropTable(
                name: "tb_rentalPlan",
                schema: "godeliverydb");

            migrationBuilder.DropTable(
                name: "tb_licenceDriver",
                schema: "godeliverydb");

            migrationBuilder.DropTable(
                name: "tb_modelMotocycle",
                schema: "godeliverydb");
        }
    }
}
