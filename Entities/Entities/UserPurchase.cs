using Entities.Enums;
using Entities.Notifications;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Table("USER_PURCHASE")]
    public class UserPurchase : Notifies //*ENTIDADE COMPRAS DO USUARIO
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


        [NotMapped]
        [DisplayName("Quantidade Total")]
        public int QtyProducts { get; set; }

        [NotMapped]
        [DisplayName("Valor Total")]
        public decimal TotalValue { get; set; }

        [NotMapped]
        [DisplayName("Endereço de Entrega")]
        public string? AddressComplet { get; set; }

        [NotMapped]        
        public List<Product> ListProducts { get; set; }
    }
}
