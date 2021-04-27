using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace senai.hroads.webAPI.Domains
{
    [Table("Habilidades")]
    public class HabilidadeDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idHabilidade { get; set; }

        [Column(TypeName = "VARCHAR(150)")]
        [Required(ErrorMessage = "O nome da habilidade é obrigatório!")]
        public string nomeHabilidade { get; set; }

        public int idTipoHabilidade { get; set; }

        // ForeignKey("idTipoHabilidade") - Define a chave estrangeira (FK) e referencia em qual atributo "nativo" da HablidadeDomain terá o seu valor
        [ForeignKey("idTipoHabilidade")]
        public  TipoHabilidadeDomain tipoHabilidade { get; set; }
    }
}
