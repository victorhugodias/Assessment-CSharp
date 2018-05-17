using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN
{
    [Table("Perfil")]
    public class Perfil
    {
        [Key]
        public int id { get; set; }


        public string idUsuario { get; set; }


        public string emailUsuario { get; set; }


        public string fotoPerfil { get; set; }

        public string PrimeiroNome { get; set; }

        public string UltimoNome { get; set; }

        public string Apelido { get; set; }

    }
}
