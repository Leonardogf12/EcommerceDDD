using Entities.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
