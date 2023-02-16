using Microsoft.AspNetCore.Mvc;
using MvcCoreSqlOracleHospitales.Repositories;
using MvcCoreSqlOracleHospitales2.Models;

namespace MvcCoreSqlOracleHospitales.Controllers
{
    public class HospitalesController : Controller
    {

        private IRepository repo;

        public HospitalesController(IRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Hospital> hospitales = this.repo.GetHospitales();
            return View(hospitales);
        }

        public IActionResult Details(int idhospital)
        {
            Hospital hospital = this.repo.FindHospital(idhospital);
            return View(hospital);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Hospital hosp)
        {
            this.repo.InsertHosp(hosp.Nombre, hosp.Direccion
                , hosp.Telefono, hosp.Num_Cama);
            return RedirectToAction("Index");
        }
       
        public IActionResult Update(int idhospital)
        {
            Hospital hospital = this.repo.FindHospital(idhospital);
            return View(hospital);
        }

        [HttpPost]
        public IActionResult Update(Hospital hosp)
        {
            this.repo.Update(hosp.IdHospital,hosp.Nombre, hosp.Direccion
                , hosp.Telefono, hosp.Num_Cama);
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int idhospital)
        {
            this.repo.Delete(idhospital);
            return RedirectToAction("Index");
        }
    }
}
