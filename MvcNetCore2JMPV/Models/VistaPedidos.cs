using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNetCore2JMPV.Models
{

    [Table("VISTAPEDIDOS")]
    public class VistaPedidos
    {
        [Key]
        [Column("IDVISTAPEDIDOS")]
        public Int64 IdVistaPedido { get; set; }



        [Column("idUsuario")]
        public int IdUsuario { get; set; }


        [Column("Nombre")]
        public string Nombre { get; set; }

        [Column("Apellidos")]
        public string Apellidos { get; set; }



        [Column("Titulo")]
        public string Titulo { get; set; }


        [Column("Precio")]
        public int Precio { get; set; }


        [Column("Portada")]
        public string Portada { get; set; }

        [Column("FECHA")]
        public DateTime Fecha{ get; set; }


        [Column("PRECIOFINAL")]
        public int PrecioFinal { get; set; }


    }
}
