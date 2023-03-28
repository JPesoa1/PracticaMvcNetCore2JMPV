using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNetCore2JMPV.Models
{

    [Table("GENEROS")]
    public class Generos
    {

        [Key]
        [Column("idGenero")]
        public int IdGenero { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; }
    }
}
