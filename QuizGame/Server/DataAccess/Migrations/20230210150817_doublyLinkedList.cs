using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizGame.Server.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class doublyLinkedList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Questions_NextId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_NextId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "NextId",
                table: "Questions",
                newName: "PreviousId");

            migrationBuilder.AddColumn<Guid>(
                name: "LastQuestionId",
                table: "Quizzes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_LastQuestionId",
                table: "Quizzes",
                column: "LastQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_PreviousId",
                table: "Questions",
                column: "PreviousId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Questions_PreviousId",
                table: "Questions",
                column: "PreviousId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Questions_LastQuestionId",
                table: "Quizzes",
                column: "LastQuestionId",
                principalTable: "Questions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Questions_PreviousId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Questions_LastQuestionId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_LastQuestionId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Questions_PreviousId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "LastQuestionId",
                table: "Quizzes");

            migrationBuilder.RenameColumn(
                name: "PreviousId",
                table: "Questions",
                newName: "NextId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_NextId",
                table: "Questions",
                column: "NextId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Questions_NextId",
                table: "Questions",
                column: "NextId",
                principalTable: "Questions",
                principalColumn: "Id");
        }
    }
}
