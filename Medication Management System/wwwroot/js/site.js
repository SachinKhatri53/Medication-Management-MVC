// format date
function formatDate(inputDate) {
    const options = { year: 'numeric', month: 'long', day: 'numeric' };
    const date = new Date(inputDate);
    return date.toLocaleDateString('en-US', options);
}

//format time
function formatTime(inputTime) {
    const [hours, minutes] = inputTime.split(':');
    let period = 'AM';
    let formattedHours = parseInt(hours);

    if (formattedHours >= 12) {
        period = 'PM';
        if (formattedHours > 12) {
            formattedHours -= 12;
        }
    }
    return `${formattedHours}:${minutes} ${period}`;
}

// format date and time inside the table
{
    var dateElements = document.querySelectorAll(".table-date");
    var timeElements = document.querySelectorAll(".table-time");
    dateElements.forEach(function (dateElement) {
        dateElement.innerHTML = formatDate(dateElement.innerHTML);
    });
    timeElements.forEach(function (timeElement) {
        timeElement.innerHTML = formatTime(timeElement.innerHTML);
    });
}

// load data into the medication modal on clicking view icon
document.addEventListener("DOMContentLoaded", function () {
    var modal = document.getElementById("viewMedicationModal");
    modal.addEventListener("show.bs.modal", function (event) {
        var button = event.relatedTarget;
        var medicationDetails = button.getAttribute("data-medication-details");
        var medication = JSON.parse(medicationDetails);
        modal.querySelector(".medication-name").innerHTML = medication.MedicationName;
        modal.querySelector(".duration").innerHTML = medication.Duration;
        modal.querySelector(".amount").innerHTML = medication.Amount;
        modal.querySelector(".frequency").innerHTML = medication.Frequency;
        modal.querySelector(".mode").innerHTML = medication.Mode;
        modal.querySelector(".time").innerHTML = medication.Time;
        modal.querySelector(".prescribed-by").innerHTML = medication.PrescribedBy;
        modal.querySelector(".issued-date").innerHTML = formatDate(medication.IssuedDate);
    });
});  

// delete medication
document.addEventListener("DOMContentLoaded", function () {
    var modal = document.getElementById("deleteMedicationModal");
    var deleteButton = modal.querySelector("#deleteMedicationButton");
    modal.addEventListener("show.bs.modal", function (event) {
        var button = event.relatedTarget;
        var medicationDetails = button.getAttribute("data-medication-details");
        var medication = JSON.parse(medicationDetails);
        deleteButton.addEventListener("click", function () {
            var form = document.createElement("form");
            form.method = "POST";
            form.action = `/Medication/DeleteMedication/${medication.MedicationId}`;
            document.body.appendChild(form);
            form.submit();
        });
    });
});

// load data into the schedule modal on clicking view icon
document.addEventListener("DOMContentLoaded", function () {
    var modal = document.getElementById("viewScheduleModal");
    modal.addEventListener("show.bs.modal", function (event) {
        var button = event.relatedTarget;
        var scheduleDetails = button.getAttribute("data-schedule-details");
        var schedule = JSON.parse(scheduleDetails);
        var modalTitle = modal.querySelector(".modal-title");
        var date = modal.querySelector(".date");
        var time = modal.querySelector(".time");
        var status = modal.querySelector(".status");
        modalTitle.textContent = schedule.Medication.MedicationName;
        time.innerHTML = '<b>Time:</b> ' + formatTime(schedule.ScheduleTime);
        date.innerHTML = '<b>Date:</b> ' + formatDate(schedule.Date);
        status.innerHTML = '<b>Status:</b> ' + schedule.Status;
    });
 });  

// delete schedule
document.addEventListener("DOMContentLoaded", function () {
    var modal = document.getElementById("deleteScheduleModal");
    var deleteButton = modal.querySelector("#deleteScheduleButton");
    modal.addEventListener("show.bs.modal", function (event) {
        var button = event.relatedTarget;
        var scheduleDetails = button.getAttribute("data-schedule-details");
        var schedule = JSON.parse(scheduleDetails);
        deleteButton.addEventListener("click", function () {
            var form = document.createElement("form");
            form.method = "POST";
            form.action = `/Schedule/DeleteSchedule/${schedule.ScheduleId}`;
            document.body.appendChild(form);
            form.submit();
        });
    });
});


// load data into the report modal on clicking view icon
document.addEventListener("DOMContentLoaded", function () {
    var modal = document.getElementById("viewReportModal");
    modal.addEventListener("show.bs.modal", function (event) {
        var button = event.relatedTarget;
        var reportDetails = button.getAttribute("data-report-details");
        var report = JSON.parse(reportDetails);
        modal.querySelector(".modal-title").innerHTML = report.MedicationName;
        modal.querySelector(".medication-name").innerHTML = report.MedicationName;
        modal.querySelector(".duration").innerHTML = report.Duration;
        modal.querySelector(".amount").innerHTML = report.Amount;
        modal.querySelector(".frequency").innerHTML = report.Frequency;
        modal.querySelector(".mode").innerHTML = report.Mode;
        modal.querySelector(".time").innerHTML = report.Time;
        modal.querySelector(".prescribed-by").innerHTML = report.PrescribedBy;
        modal.querySelector(".issued-date").innerHTML = report.IssuedDate;
        modal.querySelector(".schedule-time").innerHTML = formatTime(report.ScheduleTime);
        modal.querySelector(".date").innerHTML = formatDate(report.ScheduleDate);
        modal.querySelector(".status").innerHTML = report.Status;
        if (report.Status == "Pending") {
            modal.querySelector(".status").classList.add("text-secondary")
        }
        else if (report.Status == "Missed") {
            modal.querySelector(".status").classList.add("text-danger")
        }
        else {
            modal.querySelector(".status").classList.add("text-success")
        }
    });
});

// Notification Modal
document.querySelector(".notification").addEventListener("click", () => {
    if (document.querySelector(".notification-modal").style.display == "none") {
        document.querySelector(".notification-modal").style.display = "block"
    }
    else {
        document.querySelector(".notification-modal").style.display = "none"
    }
    
})