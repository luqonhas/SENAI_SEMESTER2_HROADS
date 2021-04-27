using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace senai.hroads.webAPI.Domains
{
    [Table("TipoHabilidades")]
    public class TipoHabilidadeDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idTipoHabilidade { get; set; }

        [Column(TypeName = "VARCHAR(150)")]
        [Required(ErrorMessage = "A descrição do tipo da habilidade é obrigatório!")]
        public string nomeTipoHabilidade { get; set; }
    }
}
