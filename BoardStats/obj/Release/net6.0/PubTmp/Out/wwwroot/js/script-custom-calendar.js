

var routeUrl = location.protocol + "//" + location.host;

$(document).ready(function () {

    $("#appointmentDate").kendoDateTimePicker({
        value: new Date(),
        dateInput: false
    });

    $("#appointmentDate3").kendoDateTimePicker({
        value: new Date(),
        dateInput: false
    });
    InitializeCalendar();



});
var calendar;
function InitializeCalendar() {
    try {

var calendarEl = document.getElementById('calendar');
if (calendarEl == null) {
    return;
}
calendar = new FullCalendar.Calendar(calendarEl, {
    initialView: 'dayGridMonth',
    headerToolbar: {
        left: 'prev,next,today',
        center: 'title',
        right: 'dayGridMonth,timeGridWeek,timeGridDay'
    },
    selectable: true,
    editable: false,
    
    select: function (event) {
        onShowModal(event, null);
    },
    eventDisplay: 'block',
    
    events: function (fetchInfo, successCallback, failureCallback) {
        $.ajax({
            url: routeUrl + '/Calendar/GetCalendarData',
            type: 'GET',
            dataType: 'JSON',
            success: function (response) {
                var events = [];
                
                if (response.status === 1) {
                    $.each(response.dataenum, function (i, data) {
                        events.push({
                         
                            title: data.gameName,
                            gameName: data.gameName,
                            start: data.startDate,
                            id: data.matchId,
                            backgroundColor: data.isPlayed ? "#28a745" : "#dc3545",
                            borderColor: "#162466",
                            textColor: "white"
                          
                        });

            
                    })
                }
                successCallback(events);
            },
            error: function (xhr) {
                $.notify("Error", "error");
            }
        });
    },
    eventClick: function (info) {
        getEventDetailsByEventId(info.event);
    }


});
        calendar.render();





    }
    catch (e) {
        alert(e);
    }
}


function onShowModal(obj, isEventDetail) {

    if (isEventDetail != null) {

       
        $("#appointmentDate2").val(obj.startDate);
        $("#title2").val(obj.gameName);
      
        $("#id2").val(obj.matchId);
        $("#idGame2").val(obj.IdGame);
        $("#btnSubmit").addClass("d-none");
        $("#appointmentDetail").modal("show");
    }
    else {
        $("#appointmentDate").val(obj.startStr + " " + new moment().format("hh:mm A"));
        $("#appointmentDate3").val(obj.startStr + " " + new moment().format("hh:mm A"));
        $("#id").val(0);
        $("#idGame").val(0);
        $("#title").val('');
        $("#title3").val('');
        $("#btnSubmit").addClass("d-none");
        $("#appointmentInput").modal("show");
        $("#id3").val(0);
        $("#idGame3").val(0);
    }

   
    
}



function onCloseModal() {

  

    $("#id").val(0);

    $("#title").val('');

    $("#description").val('');

    $("#appointmentDate").val('');

    $("#id2").val(0);

    $("#title2").val('');

    $("#description2").val('');

    $("#appointmentDate2 ").val('');

    $("#appointmentInput").modal("hide");
    $("#appointmentDetail").modal("hide");

    

}


function getEventDetailsByEventId(info) {


    const gggg = {
        "url": `${routeUrl}/Calendar/GetCalendarDataById/${info.id}`,
        "method": "GET",
        "timeout": 0,
        "data": JSON.stringify(info),
        "headers": {
            "Content-Type": "application/json; charset=utf-8"
        }

    };

    $.ajax(gggg).done(res => {
        if (res.dataenum !== undefined) {
            onShowModal(res.dataenum, true);
        }
        successCallback(events);

    }).fail(() => {
        $.notify("Error", "error");

    })





}

function onSubmitForm() {

    var requestData = {
        MatchId: $("#id").val(),
        GameName: $("#title").val(),

        StartDate: $("#appointmentDate").val(),
        
       IdGame: $("#idGame").val()
      

    };

    const payload = {
        "url": `${routeUrl}/Calendar/SaveCalendarData`,
        "method": "POST",
        "timeout": 0,
        "data": JSON.stringify(requestData),
        "headers": {
            "Content-Type": "application/json; charset=utf-8"
        }

    };

    $.ajax(payload).done(res => {
        if (res.status === 1 || res.status === 2) {
          calendar.refetchEvents();
           

            $.notify(res.message, "success");
            onCloseModal();
            calendar.refetchEvents();
        } else {
            $.notify(res.message, "error");
        }
    }).fail(() => {
        $.notify("Error", "error");
        onCloseModal();
    })
    
   
}

function onSubmitForm3() {

    var requestData = {
        MatchId: $("#id3").val(),
        GameName: $("#title3").val(),

        StartDate: $("#appointmentDate3").val(),

        IdGame: $("#idGame3").val()


    };

    const payload = {
        "url": `${routeUrl}/Calendar/SaveCalendarData2`,
        "method": "POST",
        "timeout": 0,
        "data": JSON.stringify(requestData),
        "headers": {
            "Content-Type": "application/json; charset=utf-8"
        }

    };

    $.ajax(payload).done(res => {
        if (res.status === 1 || res.status === 2) {
            calendar.refetchEvents();


            $.notify(res.message, "success");
            onCloseModal();
            calendar.refetchEvents();
        } else {
            $.notify(res.message, "error");
        }
    }).fail(() => {
        $.notify("Error", "error");
        onCloseModal();
    })


}


function onSubmitForm2() {

    var f = $("#id2").val();
    window.location.href = '/Matches/Update/'+f; 
   
   

}



