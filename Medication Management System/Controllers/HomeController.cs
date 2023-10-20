using Medication_Management_System.Data;
using Medication_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Medication_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;
        private List<Schedule> schedules;

        public HomeController(ILogger<HomeController> logger, ApplicationContext applicationContext)
        {
            _logger = logger;
            _context = applicationContext;
        }

        public IActionResult Index()
        {
            schedules = new List<Schedule>();
            schedules =  _context.schedule.Where(s => s.Status.Equals("Pending")).Include(s => s.Medication).ToList();
            foreach (Schedule schedule in schedules)
            {
                if (CompareDate(schedule.Date).Equals(-1) && CompareTime(schedule.ScheduleTime).Equals(-1))
                {
                    Report report = new();
                    report = _context.report.FirstOrDefault(r => r.ScheduleTitle.Equals(schedule.Title));
                    report.Status = "Missed";
                    schedule.Status = "Missed";
                    _context.schedule.Update(schedule);
                    _context.report.Update(report);
                    _context.SaveChanges();
                }
            }
            return View(DFacts());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static int CompareDate(string date)
        {
            DateTime scheduleDate = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime currentDate = DateTime.Now.Date;
            int result = DateTime.Compare(currentDate, scheduleDate);
            if (result < 0) return 1;
            else if (result > 0) return -1;
            else return 0;
        }

        public static int CompareTime(string time)
        {
            TimeSpan scheduleTime = TimeSpan.ParseExact(time, "hh\\:mm", CultureInfo.InvariantCulture);
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            int result = TimeSpan.Compare(currentTime, scheduleTime);
            if (result < 0) return 1;
            else if (result > 0) return -1;
            else return 0;
        }
        public int CompareTimeInterval(string time)
        {
            int result = -1;
            if (!string.IsNullOrEmpty(time) && DateTime.TryParse(time, out DateTime parsedTime))
            {
                if (parsedTime.Hour >= 12 && parsedTime.Hour < 18)
                {
                    result = 0;
                }
                else if (parsedTime.Hour >= 18)
                {
                    result = 1;
                }
            }
            return result;
        }

        private DashboardFacts DFacts()
        {
            DashboardFacts dashboardFacts = new()
            {
                TotalMedication = _context.medication.ToList(),
                TotalSchedule = _context.schedule.Count(),
                MissedSchedule = _context.schedule.Where(s => s.Status.Equals("Missed")).Count(),
                BeforeMeal = _context.medication.Where(m => m.Time.Equals("Before Meal")).Count(),
                AfterMeal = _context.medication.Where(m => m.Time.Equals("After Meal")).Count(),
                Morning = 0,
                Afternoon = 0,
                Evening = 0,
                Oral = _context.medication.Where(m => m.Mode.Equals("Oral")).Count(),
                Opthalmic = _context.medication.Where(m => m.Mode.Equals("Opthalmic")).Count(),
                Otic = _context.medication.Where(m => m.Mode.Equals("Otic")).Count(),
            };
            List<Schedule> totalMedication = new List<Schedule>(_context.schedule.Include(s => s.Medication).ToList());
            List<Schedule> todaysMedication = new();
            if (!totalMedication.Count.Equals(0))
            {
                foreach (Schedule schedule in totalMedication)
                {
                    if (CompareDate(schedule.Date).Equals(0) && CompareTime(schedule.ScheduleTime).Equals(1))
                    {
                        todaysMedication.Add(schedule);
                    }
                    if (CompareDate(schedule.Date).Equals(0) && CompareTimeInterval(schedule.ScheduleTime).Equals(-1))
                    {
                        dashboardFacts.Morning++; 
                    }
                    if (CompareDate(schedule.Date).Equals(0) && CompareTimeInterval(schedule.ScheduleTime).Equals(0))
                    {
                        dashboardFacts.Afternoon++;
                    }
                    if (CompareDate(schedule.Date).Equals(0) && CompareTimeInterval(schedule.ScheduleTime).Equals(1))
                    {
                        dashboardFacts.Evening++;
                    }
                }
            }
            dashboardFacts.TodayMedication = todaysMedication;
            return dashboardFacts;
        }
    }
}