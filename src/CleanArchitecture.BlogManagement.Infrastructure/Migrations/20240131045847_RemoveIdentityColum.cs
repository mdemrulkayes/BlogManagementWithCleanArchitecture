using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.BlogManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIdentityColum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostTagId",
                table: "PostTag");

            migrationBuilder.DropColumn(
                name: "PostCategoryId",
                table: "PostCategory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PostTagId",
                table: "PostTag",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "PostCategoryId",
                table: "PostCategory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
