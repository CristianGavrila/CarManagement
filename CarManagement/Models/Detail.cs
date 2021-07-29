using System;
using System.Collections.Generic;

#nullable disable

namespace CarManagement.Models
{
    public partial class Detail
    {
        public Detail()
        {
            Cars = new HashSet<Car>();
        }

        public int IdDetail { get; set; }
        public decimal EngineCapacity { get; set; }
        public bool Sunroof { get; set; }
        public bool Leather { get; set; }
        public bool Automatic { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
