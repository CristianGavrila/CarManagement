using System;
using System.Collections.Generic;

#nullable disable

namespace CarManagement.Models
{
    public partial class BodyStyle
    {
        public BodyStyle()
        {
            Cars = new HashSet<Car>();
        }

        public int IdBody { get; set; }
        public string Body { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
