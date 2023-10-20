using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Medication_Management_System.Models
{
    public class Schedule
    {
        private int _scheduleId;
        public int ScheduleId
        {
            get { return _scheduleId; }
            set { _scheduleId = value; }
        }
        private string? _scheduleTime;
        [Required(ErrorMessage = "Please select time.")]
        public string? ScheduleTime
        {
            get { return _scheduleTime; }
            set { _scheduleTime = value; }
        }
        private string? _date;
        [Required(ErrorMessage = "Please select date.")]
        public string? Date
        {
            get { return _date; }
            set { _date = value; }
        }
        private Medication? _medication;
        public Medication? Medication
        {
            get { return _medication; }
            set { _medication = value; }
        }
        private string? _status;
        public string? Status
        {
            get { return _status; }
            set { _status = value; }
        }
        private string? _title;
        public string? Title
        {
            get { return _title; }
            set { _title = value; }
        }

    }
}
