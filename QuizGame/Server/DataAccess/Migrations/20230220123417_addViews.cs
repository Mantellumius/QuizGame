using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizGame.Server.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Quizzes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Views",
                table: "Quizzes");
        }
    }
}
