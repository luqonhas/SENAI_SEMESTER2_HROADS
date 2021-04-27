using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace senai.hroads.webAPI.Domains
{
    [Table("ClasseHabilidades")]
    public class ClasseHabilidadeDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idClasseHabilidade { get; set; }

        public int? idClasse { get; set; }

        [ForeignKey("idClasse")]
        public ClasseDomain classe { get; set; }

        public int? idHabilidade { get; set; }

        [ForeignKey("idHabilidade")]
        public HabilidadeDomain habilidade { get; set; }
    }
}
