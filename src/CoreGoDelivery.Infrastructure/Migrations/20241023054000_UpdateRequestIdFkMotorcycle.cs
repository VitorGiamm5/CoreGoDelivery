using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreGoDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRequestIdFkMotorcycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_rental_tb_deliverier_ID_FK_DELIVERIER",
                schema: "dbgodelivery",
                table: "tb_rental");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_rental_tb_motocycle_ID_FK_MOTOCYCLE",
                schema: "dbgodelivery",
                table: "tb_rental");

            migrationBuilder.AlterColumn<string>(
                name: "ID_FK_MOTOCYCLE",
                schema: "dbgodelivery",
                table: "tb_rental",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ID_FK_DELIVERIER",
                schema: "dbgodelivery",
                table: "tb_rental",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_rental_tb_deliverier_ID_FK_DELIVERIER",
                schema: "dbgodelivery",
                table: "tb_rental",
                column: "ID_FK_DELIVERIER",
                principalSchema: "dbgodelivery",
                principalTable: "tb_deliverier",
                principalColumn: "ID_DELIVERIER");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_rental_tb_motocycle_ID_FK_MOTOCYCLE",
                schema: "dbgodelivery",
                table: "tb_rental",
                column: "ID_FK_MOTOCYCLE",
                principalSchema: "dbgodelivery",
                principalTable: "tb_motocycle",
                principalColumn: "ID_MOTOCYCLE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_rental_tb_deliverier_ID_FK_DELIVERIER",
                schema: "dbgodelivery",
                table: "tb_rental");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_rental_tb_motocycle_ID_FK_MOTOCYCLE",
                schema: "dbgodelivery",
                table: "tb_rental");

            migrationBuilder.AlterColumn<string>(
                name: "ID_FK_MOTOCYCLE",
                schema: "dbgodelivery",
                table: "tb_rental",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ID_FK_DELIVERIER",
                schema: "dbgodelivery",
                table: "tb_rental",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_rental_tb_deliverier_ID_FK_DELIVERIER",
                schema: "dbgodelivery",
                table: "tb_rental",
                column: "ID_FK_DELIVERIER",
                principalSchema: "dbgodelivery",
                principalTable: "tb_deliverier",
                principalColumn: "ID_DELIVERIER",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_rental_tb_motocycle_ID_FK_MOTOCYCLE",
                schema: "dbgodelivery",
                table: "tb_rental",
                column: "ID_FK_MOTOCYCLE",
                principalSchema: "dbgodelivery",
                principalTable: "tb_motocycle",
                principalColumn: "ID_MOTOCYCLE",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
