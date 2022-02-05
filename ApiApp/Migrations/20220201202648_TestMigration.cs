using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApiApp.Migrations
{
    public partial class TestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemsDataBase",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ItemName = table.Column<string>(type: "text", nullable: true),
                    CurrentCount = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    PicLink = table.Column<string>(type: "text", nullable: true),
                    ItemType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsDataBase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsDataBase",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    NewsHeader = table.Column<string>(type: "text", nullable: true),
                    NewsContent = table.Column<string>(type: "ntext", nullable: true),
                    NewsPictureLink = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsDataBase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersDataBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    UserPassword = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDataBase", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsDataBase");

            migrationBuilder.DropTable(
                name: "NewsDataBase");

            migrationBuilder.DropTable(
                name: "UsersDataBase");
        }
    }
}
