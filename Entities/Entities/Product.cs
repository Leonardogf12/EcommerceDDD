using Entities.Notifications;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Table("PRODUCT")]
    public class Product : Notifies //*ENTIDADE DE PRODUTO
    {       
        [Column("PRO_ID", Order = 0)]
        [DisplayName("Código")]
        public int Id { get; set; }

        [Column("PRO_NAME")]
        [DisplayName("Nome")]
        [MaxLength(255)]
        public string? Name { get; set; }

        [Column("PRO_DESCRIPTION")]
        [DisplayName("Descrição")]
        [MaxLength(150)]
        public string? Description { get; set; }

        [Column("PRO_OBSERVATION")]
        [DisplayName("Observação")]
        [MaxLength(2000)]
        public string? Observation { get; set; }

        [Column("PRO_VALUE")]
        [DisplayName("Valor")]
        public decimal Value { get; set; }

        [Column("PRO_STOCK")]
        [DisplayName("Estoque")]
        public int Stock { get; set; }

        [DisplayName("Usuário")]
        [ForeignKey("User")]
        [Column(Order = 1)]
        public string? UserId { get; set; }
        public virtual User? User { get; set; } //*FK

        [Column("PRO_STATUS")]
        [DisplayName("Status")]
        public bool Status { get; set; }

        [Column("PRO_DATE_REGISTER")]
        [DisplayName("Data de Cadastro")]        
        public DateTime DateRegister { get; set; }

        [Column("PRO_DATE_CHANGE")]
        [DisplayName("Data de Alteração")]
        public DateTime DateChange { get; set; }

        [NotMapped]
        public int IdProductCart { get; set; }

        [NotMapped]
        public int QtyBuy { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        [Column("PRO_URL_IMAGE")]
        public string? Url { get; set; }
    }
}
