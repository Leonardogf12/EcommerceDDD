using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class User : IdentityUser
    {
       
        [Column("USR_CPF", Order = 0)]
        [MaxLength(50)]
        [DisplayName("CPF")]
        public string? Cpf { get; set; }

        [Column("USR_AGE")]
        [DisplayName("Idade")]
        public int Age { get; set; }

        [Column("USR_NAME")]
        [MaxLength(255)]
        [DisplayName("Nome")]
        public string? Name { get; set; }

        [Column("USR_CEP")]
        [MaxLength(255)]
        [DisplayName("Cep")]
        public string? Cep { get; set; }

        [Column("USR_ADDRESS")]
        [MaxLength(255)]
        [DisplayName("Endereço")]
        public string? Address { get; set; }

        [Column("USR_COMPLEMENT_ADDRESS")]
        [MaxLength(450)]
        [DisplayName("Complemento de Endereço")]
        public string? ComplementAddress { get; set; }

        [Column("USR_CELL_PHONE")]
        [MaxLength(20)]
        [DisplayName("Celular")]
        public string? CellPhone { get; set; }

        [Column("USR_TELEPHONE")]
        [MaxLength(20)]
        [DisplayName("Telefone")]
        public string? Telephone { get; set; }

        [Column("USR_STATUS")]       
        [DisplayName("Status")]
        public bool? Status { get; set; }

        [Column("USR_TYPE")]
        [DisplayName("Tipo")]
        public EnumUserType EnumUserType { get; set; } //*ENUM
    }
}
