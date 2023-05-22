using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Notifications
{
    public class Notifies
    {
        public Notifies()
        {
            Notifications = new List<Notifies>();
        }

        [NotMapped]
        public string? NameProperty { get; set; }

        [NotMapped]
        public string? Message { get; set; }

        [NotMapped]
        public List<Notifies>? Notifications { get; set; }


        public bool ValidatePropertyString(string value, string nameProperty)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(nameProperty))
            {
                Notifications.Add(new Notifies
                {
                    Message = "Campo obrigatorio",
                    NameProperty = nameProperty,
                });

                return false;
            }
            return true;
        }

        public bool ValidatePropertyInt(int value, string nameProperty)
        {
            if (value < 1 || string.IsNullOrWhiteSpace(nameProperty))
            {
                Notifications.Add(new Notifies
                {
                    Message = "Valor deve ser maior que 0.",
                    NameProperty = nameProperty,
                });

                return false;
            }
            return true;
        }

        public bool ValidatePropertyDecimal(decimal value, string nameProperty)
        {
            if (value < 1 || string.IsNullOrWhiteSpace(nameProperty))
            {
                Notifications.Add(new Notifies
                {
                    Message = "Valor deve ser maior que 0.",
                    NameProperty = nameProperty,
                });

                return false;
            }
            return true;
        }

    }
}
