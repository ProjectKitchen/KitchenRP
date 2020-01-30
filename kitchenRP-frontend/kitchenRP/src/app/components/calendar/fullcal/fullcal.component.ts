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

    public events: any[] = [];

    @Output()
    public currentDateRange = new EventEmitter<{start:Date, end:Date}>();
    @Output()
    public dateRangeSelected = new EventEmitter<{start: Date, end: Date}>();
    @Output()
    public eventClicked = new EventEmitter<any>();

    dateChanged(event){
        this.currentDateRange.emit({
            start: event.view.currentStart,
            end: event.view.currentEnd,
            });
    }

    dateClickedHandler(event){
        let end = new Date(event.date);
        if(event.allDay === true){
            end.setDate(event.date.getDate() +1);

        }else{
            end.setHours(event.date.getHours() +1)
        }
        this.dateRangeSelected.emit(
            {start: event.date, end: end}
        );
    }

    dateRangeSelectedCallback(event){
        this.dateRangeSelected.emit(
            {start: event.start, end: event.end}
        )
    }

    eventClickedHandler(event){
        this.eventClicked.next(event.event);
    }

    public addReservations(reservations: any[]){
        this.events = reservations.map(reservation => { return {
            start: reservation.startTime,
            end: reservation.endTime,
            reservationId: reservation.id,
            userId: reservation.owner.id,
            userName: reservation.owner.sub,
            resourceId: reservation.reservedResource.id,
            resourceName: reservation.reservedResource.displayName,
            status: reservation.status,
            backgroundColor: "#3369dd"
        }});
    }

    public dateRangeUnselect(event){
        //this.dateRangeSelected.emit(
        //    {start: new Date(), end: new Date()}
        //)
    }

}
