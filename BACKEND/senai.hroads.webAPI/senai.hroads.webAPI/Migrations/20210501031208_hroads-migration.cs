using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace senai.hroads.webAPI.Migrations
{
    public partial class hroadsmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    idClasse = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomeClasse = table.Column<string>(type: "VARCHAR(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.idClasse);
                });

            migrationBuilder.CreateTable(
                name: "TipoHabilidades",
                columns: table => new
                {
                    idTipoHabilidade = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomeTipoHabilidade = table.Column<string>(type: "VARCHAR(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoHabilidades", x => x.idTipoHabilidade);
                });

            migrationBuilder.CreateTable(
                name: "TipoUsuarios",
                columns: table => new
                {
                    idTipoUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    permissao = table.Column<string>(type: "VARCHAR(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuarios", x => x.idTipoUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Habilidades",
                columns: table => new
                {
                    idHabilidade = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomeHabilidade = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    idTipoHabilidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habilidades", x => x.idHabilidade);
                    table.ForeignKey(
                        name: "FK_Habilidades_TipoHabilidades_idTipoHabilidade",
                        column: x => x.idTipoHabilidade,
                        principalTable: "TipoHabilidades",
                        principalColumn: "idTipoHabilidade",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    senha = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    idTipoUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_TipoUsuarios_idTipoUsuario",
                        column: x => x.idTipoUsuario,
                        principalTable: "TipoUsuarios",
                        principalColumn: "idTipoUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClasseHabilidades",
                columns: table => new
                {
                    idClasseHabilidade = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idClasse = table.Column<int>(type: "int", nullable: true),
                    idHabilidade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasseHabilidades", x => x.idClasseHabilidade);
                    table.ForeignKey(
                        name: "FK_ClasseHabilidades_Classes_idClasse",
                        column: x => x.idClasse,
                        principalTable: "Classes",
                        principalColumn: "idClasse",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClasseHabilidades_Habilidades_idHabilidade",
                        column: x => x.idHabilidade,
                        principalTable: "Habilidades",
                        principalColumn: "idHabilidade",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personagens",
                columns: table => new
                {
                    idPersonagem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomePersonagem = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    maxVida = table.Column<int>(type: "int", nullable: false),
                    maxMana = table.Column<int>(type: "int", nullable: false),
                    dataAtualizacao = table.Column<DateTime>(type: "DATE", nullable: false),
                    dataCriacao = table.Column<DateTime>(type: "DATE", nullable: false),
                    idClasse = table.Column<int>(type: "int", nullable: false),
                    usuarioidUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personagens", x => x.idPersonagem);
                    table.ForeignKey(
                        name: "FK_Personagens_Classes_idClasse",
                        column: x => x.idClasse,
                        principalTable: "Classes",
                        principalColumn: "idClasse",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personagens_Usuarios_usuarioidUsuario",
                        column: x => x.usuarioidUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "idClasse", "nomeClasse" },
                values: new object[,]
                {
                    { 1, "Bárbaro" },
                    { 2, "Cruzado" },
                    { 3, "Caçadora de Demônios" },
                    { 4, "Monge" },
                    { 5, "Necromancer" },
                    { 6, "Feiticeiro" },
                    { 7, "Arcanista" }
                });

            migrationBuilder.InsertData(
                table: "TipoHabilidades",
                columns: new[] { "idTipoHabilidade", "nomeTipoHabilidade" },
                values: new object[,]
                {
                    { 1, "Ataque" },
                    { 2, "Defesa" },
                    { 3, "Cura" },
                    { 4, "Magia" }
                });

            migrationBuilder.InsertData(
                table: "TipoUsuarios",
                columns: new[] { "idTipoUsuario", "permissao" },
                values: new object[,]
                {
                    { 1, "ADMINISTRADOR" },
                    { 2, "JOGADOR" }
                });

            migrationBuilder.InsertData(
                table: "Habilidades",
                columns: new[] { "idHabilidade", "idTipoHabilidade", "nomeHabilidade" },
                values: new object[,]
                {
                    { 1, 1, "Lança Mortal" },
                    { 2, 2, "Escudo Supremo" },
                    { 3, 3, "Recuperar Vida" }
                });

            migrationBuilder.InsertData(
                table: "Personagens",
                columns: new[] { "idPersonagem", "dataAtualizacao", "dataCriacao", "idClasse", "maxMana", "maxVida", "nomePersonagem", "usuarioidUsuario" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 80, 100, "DeuBug", null },
                    { 2, new DateTime(2021, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2016, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 100, 70, "BitBug", null },
                    { 3, new DateTime(2021, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 60, 75, "Fer7", null }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "idUsuario", "email", "idTipoUsuario", "senha" },
                values: new object[,]
                {
                    { 1, "admin@admin.com", 1, "admin" },
                    { 2, "jogador@jogador.com", 2, "jogador" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClasseHabilidades_idClasse",
                table: "ClasseHabilidades",
                column: "idClasse");

            migrationBuilder.CreateIndex(
                name: "IX_ClasseHabilidades_idHabilidade",
                table: "ClasseHabilidades",
                column: "idHabilidade");

            migrationBuilder.CreateIndex(
                name: "IX_Habilidades_idTipoHabilidade",
                table: "Habilidades",
                column: "idTipoHabilidade");

            migrationBuilder.CreateIndex(
                name: "IX_Personagens_idClasse",
                table: "Personagens",
                column: "idClasse");

            migrationBuilder.CreateIndex(
                name: "IX_Personagens_usuarioidUsuario",
                table: "Personagens",
                column: "usuarioidUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_TipoUsuarios_permissao",
                table: "TipoUsuarios",
                column: "permissao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_email",
                table: "Usuarios",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_idTipoUsuario",
                table: "Usuarios",
                column: "idTipoUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClasseHabilidades");

            migrationBuilder.DropTable(
                name: "Personagens");

            migrationBuilder.DropTable(
                name: "Habilidades");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "TipoHabilidades");

            migrationBuilder.DropTable(
                name: "TipoUsuarios");
        }
    }
}
