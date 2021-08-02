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

        public Car CarInitialize(Car car)
        {
            car.Model = _db.Models.FirstOrDefault(u => u.IdModel == car.ModelId);
            car.Model.Manufacturer = _db.Manufacturers.FirstOrDefault(u => u.IdManufacturer == car.Model.ManufacturerId);
            car.Fuel = _db.CarFuels.FirstOrDefault(u => u.IdFuel == car.FuelId);
            car.Body = _db.BodyStyles.FirstOrDefault(u => u.IdBody == car.BodyId);
            car.Location = _db.CarLocations.FirstOrDefault(u => u.IdLocation == car.LocationId);
            car.Year = _db.ModelYears.FirstOrDefault(u => u.IdYear == car.YearId);
            car.Detail = _db.Details.FirstOrDefault(u => u.IdDetail == car.DetailId);
            return car;
        }

        public IActionResult Index()
        {
            IEnumerable<Car> objList = _db.Cars;

            foreach (var obj in objList)
            {
                CarInitialize(obj);
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
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "Manufacturer";
                worksheet.Cell(currentRow, 3).Value = "Model";
                worksheet.Cell(currentRow, 4).Value = "Fuel";
                worksheet.Cell(currentRow, 5).Value = "Body";
                worksheet.Cell(currentRow, 6).Value = "Year";
                worksheet.Cell(currentRow, 7).Value = "Location";
                worksheet.Cell(currentRow, 8).Value = "Automatic";
                worksheet.Cell(currentRow, 9).Value = "Leather";
                worksheet.Cell(currentRow, 10).Value = "Sunroof";
                #endregion

                #region Body
                foreach (var car in carList)
                {
                    CarInitialize(car);                                                                      
                    currentRow ++;
                    worksheet.Cell(currentRow, 1).Value = car.IdCar;
                    worksheet.Cell(currentRow, 2).Value = car.Model.Manufacturer.ManufacturerName;
                    worksheet.Cell(currentRow, 3).Value = car.Model.ModelName;
                    worksheet.Cell(currentRow, 4).Value = car.Fuel.Fuel;
                    worksheet.Cell(currentRow, 5).Value = car.Body.Body;
                    worksheet.Cell(currentRow, 6).Value = car.Year.Year;
                    worksheet.Cell(currentRow, 7).Value = car.Location.Location;
                    worksheet.Cell(currentRow, 8).Value = car.Detail.Automatic;
                    worksheet.Cell(currentRow, 9).Value = car.Detail.Leather;
                    worksheet.Cell(currentRow, 10).Value = car.Detail.Sunroof;
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
