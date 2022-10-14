using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Exame.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PRODUTO",
                columns: table => new
                {
                    COD_PRODUTO = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    DES_PRODUTO = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    STA_STATUS = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO", x => x.COD_PRODUTO);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTO_COSIF",
                columns: table => new
                {
                    COD_PRODUTO = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    COD_COSIF = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    COD_CLASSIFICACAO = table.Column<string>(type: "char(6)", unicode: false, fixedLength: true, maxLength: 6, nullable: true),
                    STA_STATUS = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO_COSIF", x => new { x.COD_PRODUTO, x.COD_COSIF });
                    table.ForeignKey(
                        name: "FK__PRODUTO_C__COD_P__3B75D760",
                        column: x => x.COD_PRODUTO,
                        principalTable: "PRODUTO",
                        principalColumn: "COD_PRODUTO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MOVIMENTO_MANUAL",
                columns: table => new
                {
                    DAT_MES = table.Column<decimal>(type: "numeric(2,0)", nullable: false),
                    DAT_ANO = table.Column<decimal>(type: "numeric(4,0)", nullable: false),
                    NUM_LANCAMENTO = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    COD_PRODUTO = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    COD_COSIF = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    DES_DESCRICAO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DAT_MOVIMENTO = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    COD_USUARIO = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    VAL_VALOR = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOVIMENTO_MANUAL", x => new { x.DAT_MES, x.DAT_ANO, x.NUM_LANCAMENTO, x.COD_PRODUTO, x.COD_COSIF });
                    table.ForeignKey(
                        name: "FK__MOVIMENTO_MANUAL__534D60F1",
                        columns: x => new { x.COD_PRODUTO, x.COD_COSIF },
                        principalTable: "PRODUTO_COSIF",
                        principalColumns: new[] { "COD_PRODUTO", "COD_COSIF" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MOVIMENTO_MANUAL_COD_PRODUTO_COD_COSIF",
                table: "MOVIMENTO_MANUAL",
                columns: new[] { "COD_PRODUTO", "COD_COSIF" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MOVIMENTO_MANUAL");

            migrationBuilder.DropTable(
                name: "PRODUTO_COSIF");

            migrationBuilder.DropTable(
                name: "PRODUTO");
        }
    }
}
