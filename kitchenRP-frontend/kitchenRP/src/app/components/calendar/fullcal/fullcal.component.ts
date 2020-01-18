import { Component, OnInit } from '@angular/core';

import dayGridPlugin from "@fullcalendar/daygrid";
import timeGrigPlugin from '@fullcalendar/timegrid';
import interactionPlugin from '@fullcalendar/interaction';

@Component({
  selector: 'app-fullcal',
  templateUrl: './fullcal.component.html',
  styleUrls: ['./fullcal.component.css']
})
export class FullcalComponent implements OnInit {

  calendarPlugins = [dayGridPlugin, timeGrigPlugin, interactionPlugin];
  calendarWeekends = false;

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

  calendarEvents = [
    { title: 'event 1', date: '2020-01-12' },
    { title: 'event 2', date: '2020-01-13' }
  ];

  constructor() { }

  ngOnInit() {
  }

  dateClickedHandler(e) {
    console.log(e);
    this.addEvent();
  }

  addEvent() {
    this.calendarEvents.push(
      { title: 'event 3', date: '2020-01-15' }
    );
  }

  eventClickedHandler(e) {
    console.log(e);
  }

}
