import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';

import dayGridPlugin from "@fullcalendar/daygrid";
import timeGrigPlugin from '@fullcalendar/timegrid';
import interactionPlugin from '@fullcalendar/interaction';
import {DateRangeInput} from "@fullcalendar/core/datelib/date-range";
import {FullCalendarComponent} from "@fullcalendar/angular";
import {EventSourceInput} from "@fullcalendar/core/structs/event-source";
import {ReplaySubject} from "rxjs";
import {Reservation} from "../../../types/reservation";

@Component({
    selector: 'app-fullcal',
    templateUrl: './fullcal.component.html',
    styleUrls: ['./fullcal.component.css']
})
export class FullcalComponent implements OnInit {
    ngOnInit(): void {
    }

    calendarPlugins = [dayGridPlugin, timeGrigPlugin, interactionPlugin];
    calendarWeekends = true;

    headerConfig = {
        left: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek',
        center: 'title',
        right: 'today prev,next'
    };
    buttonConfig = {
        today: 'Today',
        month: 'Month',
        week: 'Week',
        day: 'Day'
    };

    private events: any[] = [];

    @Output()
    public currentDateRange = new EventEmitter<{start:Date, end:Date}>();


    dateChanged(event){
        this.currentDateRange.emit({
            start: event.view.currentStart,
            end: event.view.currentEnd,
            });
    }

    dateClickedHandler(event){
        console.log(event)
    }
    eventClickedHandler(event){
        console.log(event)
    }

    public addReservation(reservation: Reservation){
        let event = {
            start: reservation.startTime,
            end: reservation.endTime,
            backgroundColor: "#3369dd"
        };
        if(this.events.map(e => JSON.stringify(e)).indexOf(JSON.stringify(event)) === -1){
            this.events.push(event);
        }
    }

}
