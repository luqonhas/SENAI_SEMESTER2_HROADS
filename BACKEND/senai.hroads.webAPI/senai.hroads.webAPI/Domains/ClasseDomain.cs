using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace senai.hroads.webAPI.Domains
{
    [Table("Classes")]
    public class ClasseDomain
    {
        // "Key" - Chave primária (PK)
        [Key]
        // "DatabaseGenerated(DatabaseGeneratedOption.Identity)" - Para gerar IDs automáticos, o famoso IDENTITY do SQL Server
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idClasse { get; set; }

        // "Column(TypeName = "VARCHAR(150)"" - O "Column" consegue mudar o nome da coluna/atributo e o seu tipo, no caso "string" (que não existe no BD) da prop abaixo para um tipo que exista e se encaixe no BD, que nesse caso pode ser o VARCHAR
        [Column(TypeName = "VARCHAR(150)")]
        // Required - Faz esse atributo ser obrigatório
        [Required(ErrorMessage = "O nome da classe é obrigatório!")]
        public string nomeClasse { get; set; }
    }
}
