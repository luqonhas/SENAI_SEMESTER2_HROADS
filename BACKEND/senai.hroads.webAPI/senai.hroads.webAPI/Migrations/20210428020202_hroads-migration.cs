using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace senai.hroads.webAPI.Migrations
{
    public partial class hroadsmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    idClasse = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personagens", x => x.idPersonagem);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    idClasse = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomeClasse = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    PersonagemDomainidPersonagem = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.idClasse);
                    table.ForeignKey(
                        name: "FK_Classes_Personagens_PersonagemDomainidPersonagem",
                        column: x => x.PersonagemDomainidPersonagem,
                        principalTable: "Personagens",
                        principalColumn: "idPersonagem",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoHabilidades",
                columns: table => new
                {
                    idTipoHabilidade = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomeTipoHabilidade = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    HabilidadeDomainidHabilidade = table.Column<int>(type: "int", nullable: true),
                    UsuarioDomainidUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoHabilidades", x => x.idTipoHabilidade);
                    table.ForeignKey(
                        name: "FK_TipoHabilidades_Usuarios_UsuarioDomainidUsuario",
                        column: x => x.UsuarioDomainidUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "idClasse", "PersonagemDomainidPersonagem", "nomeClasse" },
                values: new object[,]
                {
                    { 1, null, "Bárbaro" },
                    { 2, null, "Cruzado" },
                    { 3, null, "Caçadora de Demônios" },
                    { 4, null, "Monge" },
                    { 5, null, "Necromancer" },
                    { 6, null, "Feiticeiro" },
                    { 7, null, "Arcanista" }
                });

            migrationBuilder.InsertData(
                table: "TipoHabilidades",
                columns: new[] { "idTipoHabilidade", "HabilidadeDomainidHabilidade", "UsuarioDomainidUsuario", "nomeTipoHabilidade" },
                values: new object[,]
                {
                    { 1, null, null, "Ataque" },
                    { 2, null, null, "Defesa" },
                    { 3, null, null, "Cura" },
                    { 4, null, null, "Magia" }
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
                columns: new[] { "idPersonagem", "dataAtualizacao", "dataCriacao", "idClasse", "maxMana", "maxVida", "nomePersonagem" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 80, 100, "DeuBug" },
                    { 2, new DateTime(2021, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2016, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 100, 70, "BitBug" },
                    { 3, new DateTime(2021, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 60, 75, "Fer7" }
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
                name: "IX_Classes_PersonagemDomainidPersonagem",
                table: "Classes",
                column: "PersonagemDomainidPersonagem");

            migrationBuilder.CreateIndex(
                name: "IX_Habilidades_idTipoHabilidade",
                table: "Habilidades",
                column: "idTipoHabilidade");

            migrationBuilder.CreateIndex(
                name: "IX_Personagens_idClasse",
                table: "Personagens",
                column: "idClasse");

            migrationBuilder.CreateIndex(
                name: "IX_TipoHabilidades_HabilidadeDomainidHabilidade",
                table: "TipoHabilidades",
                column: "HabilidadeDomainidHabilidade");

            migrationBuilder.CreateIndex(
                name: "IX_TipoHabilidades_UsuarioDomainidUsuario",
                table: "TipoHabilidades",
                column: "UsuarioDomainidUsuario");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ClasseHabilidades_Classes_idClasse",
                table: "ClasseHabilidades",
                column: "idClasse",
                principalTable: "Classes",
                principalColumn: "idClasse",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClasseHabilidades_Habilidades_idHabilidade",
                table: "ClasseHabilidades",
                column: "idHabilidade",
                principalTable: "Habilidades",
                principalColumn: "idHabilidade",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Personagens_Classes_idClasse",
                table: "Personagens",
                column: "idClasse",
                principalTable: "Classes",
                principalColumn: "idClasse",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TipoHabilidades_Habilidades_HabilidadeDomainidHabilidade",
                table: "TipoHabilidades",
                column: "HabilidadeDomainidHabilidade",
                principalTable: "Habilidades",
                principalColumn: "idHabilidade",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personagens_Classes_idClasse",
                table: "Personagens");

            migrationBuilder.DropForeignKey(
                name: "FK_TipoHabilidades_Habilidades_HabilidadeDomainidHabilidade",
                table: "TipoHabilidades");

            migrationBuilder.DropTable(
                name: "ClasseHabilidades");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Personagens");

            migrationBuilder.DropTable(
                name: "Habilidades");

            migrationBuilder.DropTable(
                name: "TipoHabilidades");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "TipoUsuarios");
        }
    }
}
