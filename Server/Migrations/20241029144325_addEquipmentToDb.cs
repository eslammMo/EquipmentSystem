using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentsSystem.Server.Migrations
{
    public partial class addEquipmentToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateOfParche = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Quantity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_DateOfParche",
                table: "Equipments",
                column: "DateOfParche");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_Location",
                table: "Equipments",
                column: "Location");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_Model",
                table: "Equipments",
                column: "Model");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_Name",
                table: "Equipments",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_Quantity",
                table: "Equipments",
                column: "Quantity");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_SerialNumber",
                table: "Equipments",
                column: "SerialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_Status",
                table: "Equipments",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_Type",
                table: "Equipments",
                column: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipments");
        }
    }
}
