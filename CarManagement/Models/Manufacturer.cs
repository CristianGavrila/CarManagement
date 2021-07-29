using System;
using System.Collections.Generic;

#nullable disable

namespace CarManagement.Models
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            Models = new HashSet<Model>();
        }

        public int IdManufacturer { get; set; }
        public string ManufacturerName { get; set; }

        public virtual ICollection<Model> Models { get; set; }
    }
}
