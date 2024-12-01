using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataModel.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    number = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__B9BE370F40A936FB", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    account_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account_number = table.Column<long>(type: "bigint", nullable: false),
                    routing_number = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    balance = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__accounts__46A222CD28174167", x => x.account_id);
                    table.ForeignKey(
                        name: "FK__accounts__user_i__30F848ED",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });
            

            migrationBuilder.CreateTable(
                name: "cards",
                columns: table => new
                {
                    card_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    account_id = table.Column<long>(type: "bigint", nullable: false),
                    number = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    expiration = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    cvv = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cards__BDF201DD35D80F04", x => x.card_id);
                    table.ForeignKey(
                        name: "FK__cards__account_i__35BCFE0A",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK__cards__user_id__34C8D9D1",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "entries",
                columns: table => new
                {
                    entry_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account_id = table.Column<long>(type: "bigint", nullable: false),
                    amount = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__entries__810FDCE13F7A44B9", x => x.entry_id);
                    table.ForeignKey(
                        name: "FK__entries__account__33D4B598",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "transfers",
                columns: table => new
                {
                    transfer_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    from_account_id = table.Column<long>(type: "bigint", nullable: false),
                    to_account_id = table.Column<long>(type: "bigint", nullable: false),
                    amount = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__transfer__78E6FD33B9BF9DEC", x => x.transfer_id);
                    table.ForeignKey(
                        name: "FK__transfers__from___31EC6D26",
                        column: x => x.from_account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK__transfers__to_ac__32E0915F",
                        column: x => x.to_account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_user_id",
                table: "accounts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_cards_account_id",
                table: "cards",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_cards_user_id",
                table: "cards",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_entries_account_id",
                table: "entries",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_loans_user_id",
                table: "loans",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_transfers_from_account_id",
                table: "transfers",
                column: "from_account_id");

            migrationBuilder.CreateIndex(
                name: "IX_transfers_to_account_id",
                table: "transfers",
                column: "to_account_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cards");

            migrationBuilder.DropTable(
                name: "entries");

            migrationBuilder.DropTable(
                name: "transfers");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
