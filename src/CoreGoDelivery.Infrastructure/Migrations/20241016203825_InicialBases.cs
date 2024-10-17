using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreGoDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InicialBases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tb_motocycle_ID_FK_MODEL_MOTOCYCLE",
                schema: "dbgodelivery",
                table: "tb_motocycle",
                column: "ID_FK_MODEL_MOTOCYCLE");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_motocycle_tb_modelMotorcycle_ID_FK_MODEL_MOTOCYCLE",
                schema: "dbgodelivery",
                table: "tb_motocycle",
                column: "ID_FK_MODEL_MOTOCYCLE",
                principalSchema: "dbgodelivery",
                principalTable: "tb_modelMotorcycle",
                principalColumn: "ID_MODEL_MOTORCYCLE",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_motocycle_tb_modelMotorcycle_ID_FK_MODEL_MOTOCYCLE",
                schema: "dbgodelivery",
                table: "tb_motocycle");

            migrationBuilder.DropIndex(
                name: "IX_tb_motocycle_ID_FK_MODEL_MOTOCYCLE",
                schema: "dbgodelivery",
                table: "tb_motocycle");
        }
    }
}
