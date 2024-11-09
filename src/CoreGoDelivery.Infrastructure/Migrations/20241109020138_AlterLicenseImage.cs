using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreGoDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterLicenseImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID_FK_DELIVERIER",
                schema: "dbgodelivery",
                table: "tb_licenceDriver");

            migrationBuilder.AddColumn<DateTime>(
                name: "DATE_UPDATE",
                schema: "dbgodelivery",
                table: "tb_licenceDriver",
                type: "timestamptz",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DATE_UPDATE",
                schema: "dbgodelivery",
                table: "tb_licenceDriver");

            migrationBuilder.AddColumn<string>(
                name: "ID_FK_DELIVERIER",
                schema: "dbgodelivery",
                table: "tb_licenceDriver",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
