using Microsoft.EntityFrameworkCore.Migrations;

namespace Owleet.Data.Migrations
{
    public partial class Datamigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_Answers_AnswerId",
                table: "UserAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_AspNetUsers_UserId",
                table: "UserAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnswer",
                table: "UserAnswer");

            migrationBuilder.RenameTable(
                name: "UserAnswer",
                newName: "UserAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswer_AnswerId",
                table: "UserAnswers",
                newName: "IX_UserAnswers_AnswerId");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnswers",
                table: "UserAnswers",
                columns: new[] { "UserId", "AnswerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Answers_AnswerId",
                table: "UserAnswers",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_AspNetUsers_UserId",
                table: "UserAnswers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Answers_AnswerId",
                table: "UserAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_AspNetUsers_UserId",
                table: "UserAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnswers",
                table: "UserAnswers");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "UserAnswers",
                newName: "UserAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswers_AnswerId",
                table: "UserAnswer",
                newName: "IX_UserAnswer_AnswerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnswer",
                table: "UserAnswer",
                columns: new[] { "UserId", "AnswerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_Answers_AnswerId",
                table: "UserAnswer",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_AspNetUsers_UserId",
                table: "UserAnswer",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
