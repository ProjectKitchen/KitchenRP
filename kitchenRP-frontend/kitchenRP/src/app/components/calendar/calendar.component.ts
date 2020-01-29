import {Component, OnInit, ViewChild} from '@angular/core';
import {ReplaySubject} from "rxjs";
import {FullcalComponent} from "./fullcal/fullcal.component";
import {ActivatedRoute} from "@angular/router";
import {ResourceService} from "../../services/resource/resource.service";
import {ReservationService} from "../../services/reservation/reservation.service";

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {

  private currentDateSpan$ = new ReplaySubject<{ start: Date, end: Date }>();

  @ViewChild('calendar', {static: false})
  private cal: FullcalComponent;

  constructor(private route: ActivatedRoute, private resourceService: ResourceService, private reservationService: ReservationService) {
  }

  ngOnInit() {
  }

  handleUpdate({start, end}) {
    this.currentDateSpan$.next({start, end});
  }

}
