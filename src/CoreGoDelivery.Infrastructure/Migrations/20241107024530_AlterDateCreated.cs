using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreGoDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterDateCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID_LICENSE_TYPE",
                schema: "dbgodelivery",
                table: "tb_licenceDriver",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "CREATED_BY",
                schema: "dbgodelivery",
                table: "tb_rentalPlan",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DATE_CREATED",
                schema: "dbgodelivery",
                table: "tb_rentalPlan",
                type: "timestamptz",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATE_START",
                schema: "dbgodelivery",
                table: "tb_rental",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATE_RETURNED_TO_BASE",
                schema: "dbgodelivery",
                table: "tb_rental",
                type: "timestamptz",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATE_ESTIMATED_RETURN",
                schema: "dbgodelivery",
                table: "tb_rental",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATE_END",
                schema: "dbgodelivery",
                table: "tb_rental",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "CREATED_BY",
                schema: "dbgodelivery",
                table: "tb_rental",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DATE_CREATED",
                schema: "dbgodelivery",
                table: "tb_rental",
                type: "timestamptz",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATE_CREATED",
                schema: "dbgodelivery",
                table: "tb_notificationMotorcycle",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "CREATED_BY",
                schema: "dbgodelivery",
                table: "tb_notificationMotorcycle",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CREATED_BY",
                schema: "dbgodelivery",
                table: "tb_motorcycle",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DATE_CREATED",
                schema: "dbgodelivery",
                table: "tb_motorcycle",
                type: "timestamptz",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CREATED_BY",
                schema: "dbgodelivery",
                table: "tb_modelMotorcycle",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DATE_CREATED",
                schema: "dbgodelivery",
                table: "tb_modelMotorcycle",
                type: "timestamptz",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CREATED_BY",
                schema: "dbgodelivery",
                table: "tb_licenceDriver",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DATE_CREATED",
                schema: "dbgodelivery",
                table: "tb_licenceDriver",
                type: "timestamptz",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EXPIRY_DATE",
                schema: "dbgodelivery",
                table: "tb_licenceDriver",
                type: "timestamptz",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ID_FK_DELIVERIER",
                schema: "dbgodelivery",
                table: "tb_licenceDriver",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ISSUE_DATE",
                schema: "dbgodelivery",
                table: "tb_licenceDriver",
                type: "timestamptz",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATE_BIRTH",
                schema: "dbgodelivery",
                table: "tb_deliverier",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "CREATED_BY",
                schema: "dbgodelivery",
                table: "tb_deliverier",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DATE_CREATED",
                schema: "dbgodelivery",
                table: "tb_deliverier",
                type: "timestamptz",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CREATED_BY",
                schema: "dbgodelivery",
                table: "tb_rentalPlan");

            migrationBuilder.DropColumn(
                name: "DATE_CREATED",
                schema: "dbgodelivery",
                table: "tb_rentalPlan");

            migrationBuilder.DropColumn(
                name: "CREATED_BY",
                schema: "dbgodelivery",
                table: "tb_rental");

            migrationBuilder.DropColumn(
                name: "DATE_CREATED",
                schema: "dbgodelivery",
                table: "tb_rental");

            migrationBuilder.DropColumn(
                name: "CREATED_BY",
                schema: "dbgodelivery",
                table: "tb_notificationMotorcycle");

            migrationBuilder.DropColumn(
                name: "CREATED_BY",
                schema: "dbgodelivery",
                table: "tb_motorcycle");

            migrationBuilder.DropColumn(
                name: "DATE_CREATED",
                schema: "dbgodelivery",
                table: "tb_motorcycle");

            migrationBuilder.DropColumn(
                name: "CREATED_BY",
                schema: "dbgodelivery",
                table: "tb_modelMotorcycle");

            migrationBuilder.DropColumn(
                name: "DATE_CREATED",
                schema: "dbgodelivery",
                table: "tb_modelMotorcycle");

            migrationBuilder.DropColumn(
                name: "CREATED_BY",
                schema: "dbgodelivery",
                table: "tb_licenceDriver");

            migrationBuilder.DropColumn(
                name: "DATE_CREATED",
                schema: "dbgodelivery",
                table: "tb_licenceDriver");

            migrationBuilder.DropColumn(
                name: "EXPIRY_DATE",
                schema: "dbgodelivery",
                table: "tb_licenceDriver");

            migrationBuilder.DropColumn(
                name: "ID_FK_DELIVERIER",
                schema: "dbgodelivery",
                table: "tb_licenceDriver");

            migrationBuilder.DropColumn(
                name: "ISSUE_DATE",
                schema: "dbgodelivery",
                table: "tb_licenceDriver");

            migrationBuilder.DropColumn(
                name: "CREATED_BY",
                schema: "dbgodelivery",
                table: "tb_deliverier");

            migrationBuilder.DropColumn(
                name: "DATE_CREATED",
                schema: "dbgodelivery",
                table: "tb_deliverier");

            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "dbgodelivery",
                table: "tb_licenceDriver",
                newName: "ID_LICENSE_TYPE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATE_START",
                schema: "dbgodelivery",
                table: "tb_rental",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATE_RETURNED_TO_BASE",
                schema: "dbgodelivery",
                table: "tb_rental",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATE_ESTIMATED_RETURN",
                schema: "dbgodelivery",
                table: "tb_rental",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATE_END",
                schema: "dbgodelivery",
                table: "tb_rental",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATE_CREATED",
                schema: "dbgodelivery",
                table: "tb_notificationMotorcycle",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATE_BIRTH",
                schema: "dbgodelivery",
                table: "tb_deliverier",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");
        }
    }
}
