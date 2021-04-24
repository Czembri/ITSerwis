using Microsoft.EntityFrameworkCore.Migrations;

namespace ItSerwis.Service.Migrations
{
    public partial class dbschema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    LoginHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => x.UserID);
                });

            migrationBuilder.InsertData(
                table: "UserLogin",
                columns: new[] { "UserID", "Age", "FirstName", "LastName", "LoginHash", "PasswordHash" },
                values: new object[] { 1, 9999, "Serwisowy", "user", "de1c072cf178e96f3d7994747403ee22", "de1c072cf178e96f3d7994747403ee22" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLogin");
        }
    }
}
