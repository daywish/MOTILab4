using Microsoft.EntityFrameworkCore.Migrations;

namespace MOTILab4.Migrations
{
    public partial class f : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Elector",
                columns: table => new
                {
                    ElectorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElectorRatio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elector", x => x.ElectorId);
                });

            migrationBuilder.CreateTable(
                name: "VotingObject",
                columns: table => new
                {
                    VotingObjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VotingObjectName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotingObject", x => x.VotingObjectId);
                });

            migrationBuilder.CreateTable(
                name: "ElectorChoise",
                columns: table => new
                {
                    ElectorChoiseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectorId = table.Column<int>(type: "int", nullable: false),
                    VotingObjectId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectorChoise", x => x.ElectorChoiseId);
                    table.ForeignKey(
                        name: "FK_ElectorChoise_Elector_ElectorId",
                        column: x => x.ElectorId,
                        principalTable: "Elector",
                        principalColumn: "ElectorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectorChoise_VotingObject_VotingObjectId",
                        column: x => x.VotingObjectId,
                        principalTable: "VotingObject",
                        principalColumn: "VotingObjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectorChoise_ElectorId",
                table: "ElectorChoise",
                column: "ElectorId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectorChoise_VotingObjectId",
                table: "ElectorChoise",
                column: "VotingObjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectorChoise");

            migrationBuilder.DropTable(
                name: "Elector");

            migrationBuilder.DropTable(
                name: "VotingObject");
        }
    }
}
