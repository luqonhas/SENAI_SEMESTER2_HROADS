using Microsoft.EntityFrameworkCore;
using senai.hroads.webAPI.Domains;
using System;

namespace senai.hroads.webAPI.Contexts
{
    /// <summary>
    /// classe responsável pelo contexto do projeto, faz a comunicação entre a API e o Banco de Dados
    /// </summary>
    public class HroadsContext : DbContext
    {
        // define as entidades/domains/tabelas no BD
        // o "DbSet" irá setar uma Domain para virar um tabela no BD
        public DbSet<ClasseDomain> Classes { get; set; }
        public DbSet<TipoHabilidadeDomain> TipoHabilidades { get; set; }
        public DbSet<HabilidadeDomain> Habilidades { get; set; }
        public DbSet<ClasseHabilidadeDomain> ClasseHabilidades { get; set; }
        public DbSet<PersonagemDomain> Personagens { get; set; }
        public DbSet<TipoUsuarioDomain> TipoUsuarios { get; set; }
        public DbSet<UsuarioDomain> Usuarios { get; set; }

        /// <summary>
        /// define as opções de "construção" do BD
        /// </summary>
        /// <param name="optionsBuilder"> objeto com as configurações definidas </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // aqui está logando no nosso SQL Server
            optionsBuilder.UseSqlServer("Server=DESKTOP-HMTUR0P; Database=T_HROADS; user Id=SA; pwd=Soufoda2;");
            // executa a função acima
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// define as informações que estarão dentro de cada tabela (um INSERT)
        /// </summary>
        /// <param name="modelBuilder"> objeto com as modelagens das informações das tabelas definidas </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // define as entidades/tabelas/domains já com os dados (ou seja, para ser enviado ao BD já com seus dados)
            // o ".HasData" serve especificar que existe um dado e para inserir efetivamente este dado
            modelBuilder.Entity<ClasseDomain>().HasData(
                new ClasseDomain
                {
                    idClasse = 1,
                    nomeClasse = "Bárbaro"
                },
                new ClasseDomain
                {
                    idClasse = 2,
                    nomeClasse = "Cruzado"
                },
                new ClasseDomain
                {
                    idClasse = 3,
                    nomeClasse = "Caçadora de Demônios"
                },
                new ClasseDomain
                {
                    idClasse = 4,
                    nomeClasse = "Monge"
                },
                new ClasseDomain
                {
                    idClasse = 5,
                    nomeClasse = "Necromancer"
                },
                new ClasseDomain
                {
                    idClasse = 6,
                    nomeClasse = "Feiticeiro"
                },
                new ClasseDomain
                {
                    idClasse = 7,
                    nomeClasse = "Arcanista"
                });


            modelBuilder.Entity<TipoHabilidadeDomain>().HasData(
                new TipoHabilidadeDomain
                {
                    idTipoHabilidade = 1,
                    nomeTipoHabilidade = "Ataque"
                },
                new TipoHabilidadeDomain
                {
                    idTipoHabilidade = 2,
                    nomeTipoHabilidade = "Defesa"
                },
                new TipoHabilidadeDomain
                {
                    idTipoHabilidade = 3,
                    nomeTipoHabilidade = "Cura"
                },
                new TipoHabilidadeDomain
                {
                    idTipoHabilidade = 4,
                    nomeTipoHabilidade = "Magia"
                });


            modelBuilder.Entity<HabilidadeDomain>().HasData(
                new HabilidadeDomain
                {
                    idHabilidade = 1,
                    idTipoHabilidade = 1,
                    nomeHabilidade = "Lança Mortal"
                },
                new HabilidadeDomain
                {
                    idHabilidade = 2,
                    idTipoHabilidade = 2,
                    nomeHabilidade = "Escudo Supremo"
                },
                new HabilidadeDomain
                {
                    idHabilidade = 3,
                    idTipoHabilidade = 3,
                    nomeHabilidade = "Recuperar Vida"
                });
            modelBuilder.Entity<PersonagemDomain>().HasData(
                new PersonagemDomain
                {
                    idPersonagem = 1,
                    idClasse = 1,
                    nomePersonagem = "DeuBug",
                    maxVida = 100,
                    maxMana = 80,
                    dataAtualizacao = Convert.ToDateTime("02/03/2021"),
                    dataCriacao = Convert.ToDateTime("18/01/2019")
                },
                new PersonagemDomain
                {
                    idPersonagem = 2,
                    idClasse = 4,
                    nomePersonagem = "BitBug",
                    maxVida = 70,
                    maxMana = 100,
                    dataAtualizacao = Convert.ToDateTime("02/03/2021"),
                    dataCriacao = Convert.ToDateTime("17/03/2016")
                },
                new PersonagemDomain
                {
                    idPersonagem = 3,
                    idClasse = 7,
                    nomePersonagem = "Fer7",
                    maxVida = 75,
                    maxMana = 60,
                    dataAtualizacao = Convert.ToDateTime("02/03/2021"),
                    dataCriacao = Convert.ToDateTime("18/03/2018")
                });

            // essa expressão lambda com o "entity" (que pode ser qualquer nome) é para conseguir utilizar o .HasData e o .HasIndex (ou seja, é para poder usar dois métodos contrutores ao mesmo tempo)
            // o ".HasIndex" nesse caso é para utilizar o .IsUnique do email (o UNIQUE)
            modelBuilder.Entity<TipoUsuarioDomain>(entity =>
            {
                entity.HasData(
                new TipoUsuarioDomain
                {
                    idTipoUsuario = 1,
                    permissao = "ADMINISTRADOR"
                },
                new TipoUsuarioDomain
                {
                    idTipoUsuario = 2,
                    permissao = "JOGADOR",
                });

                // cria um índice que define que o campo permissão é único
                entity.HasIndex(tpu => tpu.permissao).IsUnique();
            });

            modelBuilder.Entity<UsuarioDomain>(entity =>
            {
                entity.HasData(
                new UsuarioDomain
                {
                    idUsuario = 1,
                    email = "admin@admin.com",
                    senha = "admin",
                    idTipoUsuario = 1
                },
                new UsuarioDomain
                {
                    idUsuario = 2,
                    email = "jogador@jogador.com",
                    senha = "jogador",
                    idTipoUsuario = 2
                });

                // cria um índice que define que o campo email é único
                entity.HasIndex(u => u.email).IsUnique();
            });

            // executa todos os "INSERTs" acima
            base.OnModelCreating(modelBuilder);
        }

        // depois de terminar todos os "INSERTs", é preciso abrir o "Console do Gerenciador de Pacotes":

        // esse comando irá criar uma classe de migração chamada "Migrations", que a cada nova alteração no BD (por exemplo, remover uma tabela, criar outra tabela) será uma nova "migration", será tipo um histórico de tudo que foi feito no BD
        // 1º Comando:
        // Add-Migration hroads-migrations

        // Comando:                             Add-Migration
        // Nome da Migration (nome do BD)       hroads-migrations


        // após criar a classe "Migrations":
        // 2º Comando:
        // Update-Database

        // assim, a "migration" será aplicada (com o a última alteração que fica dentro da pasta Migrations) e será criado uma Database com todas as informações que colocamos
    }
}
