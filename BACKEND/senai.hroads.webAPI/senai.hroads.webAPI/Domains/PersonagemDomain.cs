using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace senai.hroads.webAPI.Domains
{
    [Table("Personagens")]
    public class PersonagemDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idPersonagem { get; set; }

        //
        [Column(TypeName = "VARCHAR(150)")]
        [Required(ErrorMessage = "O nome do personagem é obrigatório!")]
        public string nomePersonagem { get; set; }

        //
        [Required(ErrorMessage = "O máximo de vida do personagem é obrigatório!")]
        public int maxVida { get; set; }

        //
        [Required(ErrorMessage = "O máximo de mana do personagem é obrigatório!")]
        public int maxMana { get; set; }

        //
        [Column(TypeName = "DATE")]
        [DataType(DataType.Date)]
        public DateTime dataAtualizacao { get; set; }
        
        //
        [Column(TypeName = "DATE")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A data de criação do personagem é obrigatório!")]
        public DateTime dataCriacao { get; set; }

        //
        public int idClasse { get; set; }

        //
        [ForeignKey("idClasse")]
        public ClasseDomain classe { get; set; }
    }
}
