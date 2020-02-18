import {Component, OnInit, ViewChild} from '@angular/core';
import {Observable, ReplaySubject, Subject} from "rxjs";
import {FullcalComponent} from "./fullcal/fullcal.component";
import {ActivatedRoute} from "@angular/router";
import {ResourceService} from "../../services/resource/resource.service";
import {ReservationService} from "../../services/reservation/reservation.service";
import {map, startWith, tap, flatMap, repeatWhen, switchMap} from 'rxjs/operators';
import {Reservation} from "../../types/reservation";
import {User} from "../../types/user";
import {AuthService} from "../../services/auth/auth.service";
import {ModalReservationComponent} from "../../modals/modal-reservation/modal-reservation.component";
import {UserService} from "../../services/user/user.service";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {

  private currentDateSpan$ = new ReplaySubject<{ start: Date, end: Date }>();
  private reservations$: Observable<Reservation[]>;
  private userId: number;
  private refreshSubject = new Subject<any>();

  @ViewChild('mycalendar', {static: false})
  private cal: FullcalComponent;

  constructor(private route: ActivatedRoute,
              private resourceService: ResourceService,
              private authService: AuthService,
              private userService: UserService,
              private reservationService: ReservationService,
              private modalService: NgbModal,) {
      this.authService.currentUser$.subscribe(u => this.userId = u.id);

      this.reservations$ = this.authService.currentUser$
          .pipe(
              repeatWhen(_ => this.refreshSubject.asObservable()),
              flatMap((user) => this.currentDateSpan$.pipe(
                  map(dateSpan => {
                      return {user, dateSpan}
                  })
              )),
              switchMap(data => this.reservationService.getBy({
                  startTime: data.dateSpan.start.toISOString(),
                  endTime: data.dateSpan.end.toISOString(),
                  userId: data.user.id
              }))
          );
      this.reservations$.subscribe(r => {
          this.cal.addReservations(r);
          if (this.cal.eventClicked.observers.length === 0) {
              this.cal.eventClicked.subscribe(event => this.openReservationModal(event));
          }
      });
  }

  ngOnInit() {
  }

  handleUpdate({start, end}) {
    this.currentDateSpan$.next({start, end});
  }

  openReservationModal(event: any) {
    const ref = this.modalService.open(ModalReservationComponent,{ windowClass : "modal-size-lg"});
    ref.componentInstance.Add = false;

    ref.componentInstance.date = event.start;
    ref.componentInstance.dateField = {year: event.start.getFullYear(), month: event.start.getMonth()+1, day: event.start.getDate()};
    ref.componentInstance.timeStart.hour = event.start.getHours();
    ref.componentInstance.timeStart.minute = event.start.getMinutes();

    const milliDiff = event.end.getTime() - event.start.getTime();
    const minuteDiff = milliDiff / (1000 * 60);
    const hourDiff = minuteDiff / 60;
    ref.componentInstance.duration = {hour: Math.floor(minuteDiff), minute: Math.floor(minuteDiff)};

    ref.componentInstance.status = event.extendedProps.status ? event.extendedProps.status.status : "";
    ref.componentInstance.resourceId = event.extendedProps.resourceId;
    this.resourceService.getById(event.extendedProps.resourceId).subscribe(r => {
        ref.componentInstance.resourceName = r.displayName;
    });
    ref.componentInstance.userId = event.extendedProps.userId;
    this.userService.getById(event.extendedProps.userId).subscribe(u => {
        ref.componentInstance.userName = u.sub;
    });


    ref.result.then(
        _ => {
            this.refreshSubject.next(true)
        }
    )
  }
}
