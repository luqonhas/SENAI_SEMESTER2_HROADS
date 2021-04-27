using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace senai.hroads.webAPI.Domains
{
    [Table("Usuarios")]
    public class UsuarioDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idUsuario { get; set; }

        [Column(TypeName = "VARCHAR(150)")]
        [Required(ErrorMessage = "o e-mail do usuário é obrigatório!")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Column(TypeName = "VARCHAR(150)")]
        [Required(ErrorMessage = "o senha do usuário é obrigatório!")]
        [DataType(DataType.Password)]
        public string senha { get; set; }
        
        public int idTipoUsuario { get; set; }

        [ForeignKey("idTipoUsuario")]
        public TipoUsuarioDomain tipoUsuario { get; set; }
    }
}
