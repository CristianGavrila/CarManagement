using CarManagement.Models;
using CarManagement.ViewModels;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarManagement.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CarsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Car> objList = _db.Cars;

            foreach (var obj in objList)
            {
                obj.Location = _db.CarLocations.FirstOrDefault(u => u.IdLocation == obj.LocationId);
                obj.Body = _db.BodyStyles.FirstOrDefault(u => u.IdBody == obj.BodyId);
                obj.Detail = _db.Details.FirstOrDefault(u => u.IdDetail == obj.DetailId);
                obj.Model = _db.Models.FirstOrDefault(u => u.IdModel == obj.ModelId);
                if (obj.Model != null)
                    obj.Model.Manufacturer =
                        _db.Manufacturers.FirstOrDefault(u => u.IdManufacturer == obj.Model.ManufacturerId);
                obj.Year = _db.ModelYears.FirstOrDefault(u => u.IdYear == obj.YearId);
            }
            return View(objList);
        }

        //CREATE
        //Get
        public IActionResult Create()
        {
            CarViewModel carViewModel = new CarViewModel()
            {
                Car = new Car(),
                Detail = new Detail(),
                ManufacturerDropDown = _db.Manufacturers.Select(man => new SelectListItem
                {
                    Text = man.ManufacturerName,
                    Value = man.IdManufacturer.ToString()
                }),
                ModelDropDown = _db.Models.Select(mod => new SelectListItem
                {
                    Text = mod.ModelName,
                    Value = mod.IdModel.ToString()
                }),
                YearDropDown = _db.ModelYears.Select(y => new SelectListItem
                {
                    Text = y.Year.ToString(),
                    Value = y.IdYear.ToString()
                }),
                FuelDropDown = _db.CarFuels.Select(f => new SelectListItem
                {
                    Text = f.Fuel,
                    Value = f.IdFuel.ToString()
                }),
                LocationDropDown = _db.CarLocations.Select(l => new SelectListItem
                {
                    Text = l.Location,
                    Value = l.IdLocation.ToString()
                }),
                BodyDropDown = _db.BodyStyles.Select(b => new SelectListItem
                {
                    Text = b.Body,
                    Value = b.IdBody.ToString()
                })
            };
            return View(carViewModel);
        }

        //Post
        [HttpPost]
        public IActionResult Create(CarViewModel carViewModel)
        {
            if (ModelState.IsValid)
            {
                _db.Cars.Add(carViewModel.Car);
                _db.Details.Add(carViewModel.Detail);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carViewModel);
        }
        
        //UPDATE
        //Get
        public IActionResult Update(int? id)
        {
            CarViewModel carViewModel = new CarViewModel()
            {
                Car = new Car(),
                Detail = new Detail(),
                ManufacturerDropDown = _db.Manufacturers.Select(man => new SelectListItem
                {
                    Text = man.ManufacturerName,
                    Value = man.IdManufacturer.ToString()
                }),
                ModelDropDown = _db.Models.Select(mod => new SelectListItem
                {
                    Text = mod.ModelName,
                    Value = mod.IdModel.ToString()
                }),
                YearDropDown = _db.ModelYears.Select(y => new SelectListItem
                {
                    Text = y.Year.ToString(),
                    Value = y.IdYear.ToString()
                }),
                FuelDropDown = _db.CarFuels.Select(f => new SelectListItem
                {
                    Text = f.Fuel,
                    Value = f.IdFuel.ToString()
                }),
                LocationDropDown = _db.CarLocations.Select(l => new SelectListItem
                {
                    Text = l.Location,
                    Value = l.IdLocation.ToString()
                }),
                BodyDropDown = _db.BodyStyles.Select(b => new SelectListItem
                {
                    Text = b.Body,
                    Value = b.IdBody.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }
            carViewModel.Car = _db.Cars.Find(id);
            carViewModel.Detail = _db.Details.Find(carViewModel.Car.DetailId);
            if (carViewModel.Car == null)
            {
                return NotFound();
            }
            return View(carViewModel);
        }

        //Post
        [HttpPost]
        public IActionResult Update(CarViewModel carViewModel)
        {
            if (ModelState.IsValid)
            {
                _db.Cars.Update(carViewModel.Car);
                _db.Details.Update(carViewModel.Detail);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carViewModel);
        }


        //DELETE
        //Post
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            CarViewModel carViewModel = new CarViewModel()
            {
                Car = new Car(),
                Detail = new Detail()
            };

            carViewModel.Car = _db.Cars.Find(id);
            carViewModel.Detail = _db.Details.Find(carViewModel.Car.DetailId);

            if (carViewModel.Car == null)
            {
                return NotFound();
            }

            _db.Cars.Remove(carViewModel.Car);
            _db.Details.Remove(carViewModel.Detail);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        
        //EXPORT
        public IActionResult Export()
        {
            IEnumerable<Car> carList = _db.Cars;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Cars");
                var currentRow = 1;

                #region Header
                worksheet.Cell(currentRow, 1).Value = "Car Id";
                worksheet.Cell(currentRow, 2).Value = "Detail Id";
                worksheet.Cell(currentRow, 3).Value = "Bodystyle Id";
                worksheet.Cell(currentRow, 4).Value = "Location Id";
                worksheet.Cell(currentRow, 5).Value = "Year Id";
                worksheet.Cell(currentRow, 6).Value = "Fuel Id";
                #endregion

                #region Body
                foreach (var car in carList)
                {
                    currentRow ++;
                    worksheet.Cell(currentRow, 1).Value = car.IdCar;
                    worksheet.Cell(currentRow, 2).Value = car.DetailId;
                    worksheet.Cell(currentRow, 3).Value = car.BodyId;
                    worksheet.Cell(currentRow, 4).Value = car.LocationId;
                    worksheet.Cell(currentRow, 5).Value = car.YearId;
                    worksheet.Cell(currentRow, 6).Value = car.FuelId;
                }
                #endregion

                    using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Car List.xlsx"
                        );
                }
            }
        }
    }
}
