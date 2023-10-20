using Medication_Management_System.Data;
using Medication_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Medication_Management_System.Controllers
{
    public class MedicationController : Controller
    {
        private ApplicationContext _context;
        private static List<Medication> _medicationList;
        public static List<Medication> MedicationList { get => _medicationList; set => _medicationList = value; }

        public MedicationController(ApplicationContext applicationContext)
        {
            _context = applicationContext;  
        }

        // GET: MedicationController
        public ActionResult Index()
        {
            MedicationList = new();
            MedicationList = _context.medication.ToList();
            return View(MedicationList);
        }

        // GET: MedicationController/Create
        public ActionResult AddMedication()
        {
            return View();
        }

        // POST: MedicationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMedication(Medication medication)
        {
            try
            {
                if (MedicationList.Any(m => m.MedicationName == medication.MedicationName))
                {
                    ModelState.AddModelError("MedicationName", "Medication with this name already exists.");
                    return View();
                }
                if (!String.IsNullOrEmpty(medication.Time) && medication.Time.Equals("Select"))
                {
                    ModelState.AddModelError("Time", "Please select time.");
                    return View();
                }
                _context.medication.Add(medication);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        } 

        // GET: MedicationController/Edit/5
        public ActionResult EditMedication(int id)
        {

            Medication? medication = new();
            medication = MedicationList.FirstOrDefault(m => m.MedicationId.Equals(id));
            return View(medication);
        }

        // POST: MedicationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMedication(int id, Medication medication)
        {
            try
            {
                Medication? medicationToUpdate = MedicationList.FirstOrDefault(m => m.MedicationId.Equals(id));
                if (medicationToUpdate != null)
                {
                    medicationToUpdate.MedicationId = id;
                    medicationToUpdate.MedicationName = medication.MedicationName;
                    medicationToUpdate.Duration = medication.Duration;
                    medicationToUpdate.IssuedDate = medication.IssuedDate;
                    medicationToUpdate.Mode = medication.Mode;
                    medicationToUpdate.Amount = medication.Amount;
                    medicationToUpdate.Frequency = medication.Frequency;
                    medicationToUpdate.PrescribedBy = medication.PrescribedBy;
                    medicationToUpdate.Time = medication.Time;
                    _context.medication.Update(medicationToUpdate);
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MedicationController/Delete/5
        public ActionResult DeleteMedication(int id)
        {
            Medication? medication = new();
            medication = MedicationList.FirstOrDefault(m => m.MedicationId.Equals(id));
            if (medication != null)
            {
                _context.medication.Remove(medication);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
