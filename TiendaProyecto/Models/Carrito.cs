using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaProyecto.Models
{
    public class Carrito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCarrito { get; set; }
        public List<ItemCarrito> Items { get; set; }

        public Carrito()
        {
            Items = new List<ItemCarrito>();
        }
    }

    public class ItemCarrito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idItemCarrito { get; set; }
        public Product Product { get; set; }
        public Double Precio { get; set; }
        public int Cantidad { get; set; }

    }

    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
        }
    }
}
