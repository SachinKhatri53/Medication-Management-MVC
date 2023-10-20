using Medication_Management_System.Data;
using Medication_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;

namespace Medication_Management_System.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ApplicationContext _context;
        private static List<Schedule> _scheduleList;
        public static List<Schedule> ScheduleList { get => _scheduleList; set => _scheduleList = value; }

        public ScheduleController(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }
        public IActionResult Index()
        {
            ScheduleList = new();
            if (TempData.ContainsKey("FilteredScheduleList"))
            {
                string _serializedList = TempData["FilteredScheduleList"] as string;
                ScheduleList = JsonConvert.DeserializeObject<List<Schedule>>(_serializedList);
                TempData.Keep("FilteredScheduleList");
                ViewData["SelectedValue"] = TempData["SelectedValue"] as string;
            }
            else
            {
                ScheduleList = _context.schedule.Include(s => s.Medication).ToList();
            }
            
            return View(ScheduleList);
        }

        [HttpPost]
        public ActionResult ScheduleFilter(string scheduleFilter)
        {
            string? filter = scheduleFilter;
            List<Schedule> _filteredScheduleList;
           
            if(filter == "All")
            {
                _filteredScheduleList = _context.schedule.Include(s => s.Medication).ToList();

            }
            else if (filter == "Missed")
            {
                _filteredScheduleList = _context.schedule.Where(s => s.Status.Equals("Missed")).Include(s => s.Medication).ToList();
            }
            else if (filter == "Taken")
            {
                _filteredScheduleList = _context.schedule.Where(s => s.Status.Equals("Taken")).Include(s => s.Medication).ToList();
            }
            else
            {
                _filteredScheduleList = _context.schedule.Where(s => s.Status.Equals("Pending")).Include(s => s.Medication).ToList();
            }
            string _serializedList = JsonConvert.SerializeObject(_filteredScheduleList, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None
            });
            TempData["FilteredScheduleList"] = _serializedList;
            TempData["SelectedValue"] = scheduleFilter;
            return RedirectToAction(nameof(Index));
        }
        public IActionResult AddSchedule()
        {
            var mediactionList = _context.medication.ToList();
            var medications = mediactionList;
            var medicationListItems = medications.Select(m => new SelectListItem
            {
                Value = m.MedicationId.ToString(),
                Text = m.MedicationName
            });
            ViewBag.MedicationList = medicationListItems;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSchedule(Schedule schedule)
        {
            try
            {
                Medication? medication = _context.medication.ToList().FirstOrDefault(m => m.MedicationId.Equals(schedule.Medication.MedicationId));
                schedule.Medication = medication;
                schedule.Status = "Pending";
                Guid uniqueGuid = Guid.NewGuid();
                schedule.Title = uniqueGuid.ToString();
                Report report = new()
                {
                    MedicationName = medication.MedicationName,
                    Amount = medication.Amount,
                    Duration = medication.Duration,
                    Frequency = medication.Frequency,
                    Mode = medication.Mode,
                    Time = medication.Time,
                    PrescribedBy = medication.PrescribedBy,
                    IssuedDate = medication.IssuedDate,
                    ScheduleDate = schedule.Date,
                    ScheduleTime = schedule.ScheduleTime,
                    Status = schedule.Status,
                    ScheduleTitle = schedule.Title
                };
                _context.schedule.Add(schedule);
                _context.report.Add(report);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditSchedule(int id)
        {
            Schedule? schedule = new();
            schedule = ScheduleList.FirstOrDefault(s => s.ScheduleId.Equals(id));

            return View(schedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSchedule(int id, Schedule schedule)
        {
            try
            {
                schedule.Medication = _context.medication.FirstOrDefault(m => m.MedicationId.Equals(schedule.Medication.MedicationId));
                _context.schedule.Update(schedule);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }

        }
        
        public ActionResult ChangeStatus(int id, Schedule? schedule, Report? report)
        {
            schedule = _context.schedule.FirstOrDefault(s => s.ScheduleId.Equals(id));
            report = _context.report.FirstOrDefault(r => r.ScheduleTitle.Equals(schedule.Title));
            if (schedule != null)
            {
                schedule.Status = "Taken";
                report.Status = "Taken";
                _context.schedule.Update(schedule);
                _context.report.Update(report);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public ActionResult DeleteSchedule(int id)
        {
            Schedule? schedule = new();
            schedule = ScheduleList.FirstOrDefault(s => s.ScheduleId.Equals(id));
            if (schedule != null)
            {
                _context.schedule.Remove(schedule);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        
    }
}
