using System;
using System.Collections.Generic;

#nullable disable

namespace CarManagement.Models
{
    public partial class Car
    {
        public int IdCar { get; set; }
        public int DetailId { get; set; }
        public int ModelId { get; set; }
        public int FuelId { get; set; }
        public int BodyId { get; set; }
        public int LocationId { get; set; }
        public int YearId { get; set; }

        public virtual BodyStyle Body { get; set; }
        public virtual Detail Detail { get; set; }
        public virtual CarFuel Fuel { get; set; }
        public virtual CarLocation Location { get; set; }
        public virtual Model Model { get; set; }
        public virtual ModelYear Year { get; set; }
    }
}
