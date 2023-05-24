using Entities.Notifications;
using System.ComponentModel;

namespace Entities.Entities
{
    public class Base : Notifies
    {
        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Nome")]
        public string? Name { get; set; }
    }
}
