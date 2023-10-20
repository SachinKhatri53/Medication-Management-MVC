using Medication_Management_System.Data;
using Medication_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Medication_Management_System.Controllers
{
    public class ReportController : Controller
    {
        private static List<Report>? _reportList;
        private ApplicationContext _context;
        public ReportController(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }
        public IActionResult Index()
        {
            _reportList = new(_context.report.ToList());
            return View(_reportList);
        }
    }
}
