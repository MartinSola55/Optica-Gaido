using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Repository
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly ApplicationDbContext _db;

        public DoctorRepository (ApplicationDbContext db) : base (db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetDropDownList()
        {
            IEnumerable<SelectListItem> doctors = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Seleccione un doctor", Disabled = true }
            };
            return doctors.Concat(_db.Doctors.Where(x => x.IsActive == true).OrderBy(x => x.Surname).ThenBy(x => x.Name).Select(i => new SelectListItem() {
                Text = i.Surname + ", " + i.Name,
                Value = i.ID.ToString(),
            }));
        }

        public void Update(Doctor doctor)
        {
            var dbObject = _db.Doctors.FirstOrDefault(x => x.ID == doctor.ID);
            if (dbObject != null)
            {
                dbObject.Name = doctor.Name;
                dbObject.Surname = doctor.Surname;
                dbObject.License = doctor.License;
                _db.SaveChanges();
            }
        }

        public bool IsDuplicated(Doctor doctor)
        {
            var dbObject = _db.Doctors.FirstOrDefault(
                x => ((x.Name.ToLower() == doctor.Name.ToLower() && x.Surname.ToLower() == doctor.Surname.ToLower()) || 
                x.License.ToLower() == doctor.License.ToLower()) && x.ID != doctor.ID);
            if (dbObject == null) return false;
            return true;
        }

        public void ChangeState(long id)
        {
            var dbObject = _db.Doctors.FirstOrDefault(x => x.ID == id);
            if (dbObject != null)
            {
                dbObject.IsActive = !dbObject.IsActive;
                _db.SaveChanges();
            }
        }
    }
}