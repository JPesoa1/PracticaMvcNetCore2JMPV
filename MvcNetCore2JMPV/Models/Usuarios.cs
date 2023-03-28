using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNetCore2JMPV.Models
{


    [Table("USUARIOS")]
    public class Usuarios
    {
        [Key]
        [Column("idUsuario")]
        public int IdUsuario { get; set; }


        [Column("Nombre")]
        public string Nombre { get; set; }

        [Column("Apellidos")]
        public string Apellidos { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Pass")]
        public string Pass { get; set; }

        [Column("Foto")]
        public string Foto{ get; set; }
    }
}
