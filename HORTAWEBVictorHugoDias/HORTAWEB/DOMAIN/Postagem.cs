using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN
{
    [Table("Postagens")]
    public class postagem
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string idUsuario { get; set; }

        [Required]
        public string emailUsuario { get; set; }


        [Required]
        [MaxLength(140)]
        public string Conteudo { get; set; }

        public string imagem { get; set; }
    }

}