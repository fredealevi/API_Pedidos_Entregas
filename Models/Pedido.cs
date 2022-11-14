using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace tech_test_payment_api.Models
{
    public class Pedido
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public virtual List<Item> Itens { get; set; }
        
        public virtual Vendedor Vendedor { get; set; } 
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EnumStatusPedido Status { get; set; }


    }
}