using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SfCompulsory_cs.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID_USER = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CLIENT_NAME = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: true),
                    REGISTER_DATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    BET_MAX_VALUE = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    USERNAME = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    PASSWORD = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    USER_PIX_KEY = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID_USER);
                });

            migrationBuilder.CreateTable(
                name: "LOGS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    LEVEL = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    MESSAGE = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: false),
                    TIMESTAMP = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    USER_ID = table.Column<long>(type: "NUMBER(19)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOGS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LOGS_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "ID_USER");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LOGS_USER_ID",
                table: "LOGS",
                column: "USER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LOGS");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
