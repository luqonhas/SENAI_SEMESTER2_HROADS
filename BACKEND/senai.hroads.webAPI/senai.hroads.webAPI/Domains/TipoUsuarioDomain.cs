using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace senai.hroads.webAPI.Domains
{
    [Table("TipoUsuarios")]
    public class TipoUsuarioDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idTipoUsuario { get; set; }

        [Column(TypeName = "VARCHAR(150)")]
        [Required(ErrorMessage = "A permissão do usuário é obrigatório!")]
        public string permissao { get; set; }
    }
}
