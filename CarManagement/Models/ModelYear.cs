using System;
using System.Collections.Generic;

#nullable disable

namespace CarManagement.Models
{
    public partial class ModelYear
    {
        public ModelYear()
        {
            Cars = new HashSet<Car>();
        }

        public int IdYear { get; set; }
        public short Year { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
