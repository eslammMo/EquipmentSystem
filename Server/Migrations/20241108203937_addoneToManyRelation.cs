using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentsSystem.Server.Migrations
{
    public partial class addoneToManyRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Equipments_Location",
                table: "Equipments");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_Model",
                table: "Equipments");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_Type",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Equipments");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Equipments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "Equipments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Equipments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_LocationId",
                table: "Equipments",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_ModelId",
                table: "Equipments",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_TypeId",
                table: "Equipments",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Locations_LocationId",
                table: "Equipments",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Models_ModelId",
                table: "Equipments",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Types_TypeId",
                table: "Equipments",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Locations_LocationId",
                table: "Equipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Models_ModelId",
                table: "Equipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Types_TypeId",
                table: "Equipments");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_LocationId",
                table: "Equipments");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_ModelId",
                table: "Equipments");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_TypeId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Equipments");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Equipments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Equipments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Equipments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_Location",
                table: "Equipments",
                column: "Location");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_Model",
                table: "Equipments",
                column: "Model");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_Type",
                table: "Equipments",
                column: "Type");
        }
    }
}
