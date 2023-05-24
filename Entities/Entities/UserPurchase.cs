using Entities.Enums;
using Entities.Notifications;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Table("UserPurchase")]
    public class UserPurchase : Notifies
    {
        [Column("USXPU_ID", Order = 0)]
        [DisplayName("Código")]        
        public int Id { get; set; }

        [DisplayName("Usuário")]
        [ForeignKey("User")]
        [Column(Order = 1)]
        public string? UserId { get; set; } //*FK
        public virtual User? User { get; set; }

        [DisplayName("Produto")]
        [ForeignKey("Product")]
        [Column(Order = 2)]
        public int ProductId { get; set; } //*FK
        public virtual Product? Product { get; set; } 

        [Column("USXPU_STATUS")]
        [DisplayName("Status")]
        public EnumPurchaseStatus EnumPurchaseStatus { get; set; } //*ENUM

        [Column("USXPU_QTY")]
        [DisplayName("Quantidade")]
        public int QtyPurchase { get; set; }       
    }
}
