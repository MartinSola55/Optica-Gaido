using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Optica_Gaido.Data;
using Optica_Gaido.Data.Repository.IRepository;

namespace Optica_Gaido.Controllers
{
    public class BrandController : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly ApplicationDbContext _context;

        public BrandController(IWorkContainer workContainer, ApplicationDbContext context)
        {
            _workContainer = workContainer;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_workContainer.Brand.GetAll());
        }


        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _workContainer.Brand.GetAll() });
        }
        #endregion
    }
}