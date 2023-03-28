using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNetCore2JMPV.Models
{

    [Table("LIBROS")]
    public class Libros
    {
        [Key]
        [Column("idLibro")]
        public int IdLibro { get; set; }

        [Column("Titulo")]
        public string Titulo{ get; set; }

        [Column("Autor")]
        public string Autor { get; set; }

        [Column("Editorial")]
        public string Editorial { get; set; }

        [Column("Portada")]
        public string Portada { get; set; }

        [Column("Resumen")]
        public string Resumen { get; set; }

        [Column("Precio")]
        public int Precio { get; set; }

        [Column("idGenero")]
        public int IdGenero { get; set; }
    }
}
