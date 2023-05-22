using Entities.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("Product")]
    public class Product : Notifies
    {
        [Column("PRO_ID")]
        [DisplayName("Código")]
        public int Id { get; set; }

        [Column("PRO_NAME")]
        [DisplayName("Nome")]
        public string? Name { get; set; }

        [Column("PRO_VALUE")]
        [DisplayName("Valor")]
        public decimal Value { get; set; }

        [Column("PRO_STATE")]
        [DisplayName("Estado")]
        public bool? State { get; set; }
    }
}
