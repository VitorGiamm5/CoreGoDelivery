using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreGoDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InicialBase3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID_FK_MODEL_MOTOCYCLE",
                schema: "dbgodelivery",
                table: "tb_modelMotocycle",
                newName: "ID_MODEL_MOTOCYCLE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID_MODEL_MOTOCYCLE",
                schema: "dbgodelivery",
                table: "tb_modelMotocycle",
                newName: "ID_FK_MODEL_MOTOCYCLE");
        }
    }
}
