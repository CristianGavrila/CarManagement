using System;
using System.Collections.Generic;

#nullable disable

namespace CarManagement.Models
{
    public partial class CarLocation
    {
        public CarLocation()
        {
            Cars = new HashSet<Car>();
        }

        public int IdLocation { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
