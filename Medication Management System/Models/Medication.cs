using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medication_Management_System.Models
{
    public class Medication
    {
        private int _medicationId;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MedicationId
        {
            get { return _medicationId; }
            set { _medicationId = value; }
        }
        private string? _medicationName;
        [Display(Name = "Medication Name")]
        [Required(ErrorMessage = "Medication Name field is required.")]
        public string? MedicationName
        {
            get { return _medicationName; }
            set { _medicationName = value; }
        }
        private string? _duration;
        [Required]
        public string? Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
        private string? _amount;
        [Required]
        public string? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        private string? _mode;
        [Required]
        public string? Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }
        private int _frequency;
        [Required]
        public int Frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
        }
        private string? _time;
        [Display(Name = "Intake Time")]
        [Required(ErrorMessage = "Please select intake time.")]
        public string? Time
        {
            get { return _time; }
            set { _time = value; }
        }
        private string? _prescribedBy;
        [Display(Name = "Prescribed By")]
        [Required(ErrorMessage = "Prescribed By field is required.")]
        public string? PrescribedBy
        {
            get { return _prescribedBy; }
            set { _prescribedBy = value; }
        }
        private string? _issuedDate;
        [Display(Name = "Issued Date")]
        [Required(ErrorMessage = "Please select issued date.")]
        public string? IssuedDate
        {
            get { return _issuedDate; }
            set { _issuedDate = value; }
        }
        private List<Schedule>? _schedules;
        public List<Schedule>? Schedules
        {
            get { return _schedules; }
            set { _schedules = value; }
        }
    }
}
