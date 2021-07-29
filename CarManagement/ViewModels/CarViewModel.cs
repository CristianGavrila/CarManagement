using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarManagement.ViewModels
{
    public class CarViewModel
    {
        public Car Car { get; set; }

        public Detail Detail { get; set; }

        public Model Model { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public CarFuel CarFuel { get; set; }

        public BodyStyle BodyStyle { get; set; }

        public CarLocation CarLocation { get; set; }

        public ModelYear ModelYear { get; set; }

        public IEnumerable<SelectListItem> YearDropDown { get; set; }

        public IEnumerable<SelectListItem> ManufacturerDropDown { get; set; }

        public IEnumerable<SelectListItem> ModelDropDown { get; set; }

        public IEnumerable<SelectListItem> BodyDropDown { get; set; }

        public IEnumerable<SelectListItem> FuelDropDown { get; set; }

        public IEnumerable<SelectListItem> LocationDropDown { get; set; }
    }
}
