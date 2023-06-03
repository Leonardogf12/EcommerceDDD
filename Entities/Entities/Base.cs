using Entities.Notifications;
using System.ComponentModel;

namespace Entities.Entities
{
    public class Base : Notifies //*ENTIDADE BASE PARA AS DEMAIS
    {
        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Nome")]
        public string? Name { get; set; }
    }
}
