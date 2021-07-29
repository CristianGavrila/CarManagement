using System;
using System.Collections.Generic;

#nullable disable

namespace CarManagement.Models
{
    public partial class CarFuel
    {
        public CarFuel()
        {
            Cars = new HashSet<Car>();
        }

        public int IdFuel { get; set; }
        public string Fuel { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
