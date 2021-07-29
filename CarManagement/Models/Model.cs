using System;
using System.Collections.Generic;

#nullable disable

namespace CarManagement.Models
{
    public partial class Model
    {
        public Model()
        {
            Cars = new HashSet<Car>();
        }

        public int IdModel { get; set; }
        public string ModelName { get; set; }
        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
