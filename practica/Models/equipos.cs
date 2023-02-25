using System.ComponentModel.DataAnnotations;

namespace practica.Models
{
    public class equipos
    {
        [Key]
        public int id { set; get; }
        public string nombre { set; get; }

        public decimal costo { set; get; }
    }
}
