 namespace Medication_Management_System.Models
{
    public class Report
    {
        private int _reportId;
        public int ReportId { get => _reportId; set => _reportId = value; }
        private string? _scheduleTitle;
        public string? ScheduleTitle { get => _scheduleTitle; set => _scheduleTitle = value; }
        private string? _medicationName;
        public string? MedicationName { get => _medicationName; set => _medicationName = value; }
        private string? _duration;
        public string? Duration { get => _duration; set => _duration = value; }
        private string? _amount;
        public string? Amount { get => _amount; set => _amount = value; }
        private string? _mode;
        public string? Mode { get => _mode; set => _mode = value; }
        private int _frequency;
        public int Frequency { get => _frequency; set => _frequency = value; }
        private string? _time;
        public string? Time {  get => _time; set => _time = value; }
        private string? _prescribedBy;
        public string? PrescribedBy { get => _prescribedBy; set => _prescribedBy = value; }
        private string? _issuedDate;
        public string? IssuedDate { get => _issuedDate; set => _issuedDate = value; }
        private string? _scheduleTime;
        public string? ScheduleTime { get => _scheduleTime; set => _scheduleTime = value; }
        private string? _scheduleDate;
        public string? ScheduleDate { get => _scheduleDate; set => _scheduleDate = value; }
        private string? _status;
        public string? Status { get => _status; set => _status = value; }
    }
}
