using System.ComponentModel.DataAnnotations.Schema;

namespace Medication_Management_System.Models
{
    [NotMapped]
    public class DashboardFacts
    {
        /*public int TotalMedication { get; set; }*/
        public int TotalSchedule { get; set; }
        public int MissedSchedule { get; set; }
        public List<Schedule>? TodayMedication { get; set; }
        public List<Medication>? TotalMedication { get; set; }
        public int BeforeMeal { get; set; }
        public int AfterMeal { get; set; }
        public int Morning { get; set; }
        public int Afternoon { get; set; }
        public int Evening { get; set; }
        public int Oral { get; set; }
        public int Opthalmic { get; set; }
        public int Otic { get; set; }
    }
}
