import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {flatMap, map, repeatWhen, switchMap} from "rxjs/operators";
import {ResourceService} from "../../../services/resource/resource.service";
import {Observable, ReplaySubject, Subject} from "rxjs";
import {Resource} from "../../../types/resource";
import {log} from "util";
import {Reservation} from "../../../types/reservation";
import {ReservationService} from "../../../services/reservation/reservation.service";
import {FullcalComponent} from "../fullcal/fullcal.component";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {ModalResourceComponent} from "../../../modals/modal-resource/modal-resource.component";
import {ModalReservationComponent} from "../../../modals/modal-reservation/modal-reservation.component";
import {ModalUserComponent} from "../../../modals/modal-user/modal-user.component";
import {AuthService} from "../../../services/auth/auth.service";
import {UserService} from "../../../services/user/user.service";

@Component({
    selector: 'app-resource-calendar',
    templateUrl: './resource-calendar.component.html',
    styleUrls: ['./resource-calendar.component.css']
})
export class ResourceCalendarComponent implements OnInit {

    private resource$: Observable<Resource>;

    private reservations$: Observable<Reservation[]>;

    private currentDateSpan$ = new ReplaySubject<{ start: Date, end: Date }>();

    private refreshSubject = new Subject<any>();

    @ViewChild('calendar', {static: false})
    private cal: FullcalComponent;

    private lastSelectedRange: { start: Date, end: Date } = {
        start: new Date(),
        end: new Date()
    };

    constructor(private route: ActivatedRoute,
                private resourceService: ResourceService,
                private userService: UserService,
                private reservationService: ReservationService,
                private modalService: NgbModal,
                private authService: AuthService) {
        this.resource$ = route.params.pipe(
            flatMap(params => resourceService.getById(+params['id']))
        );

        this.reservations$ = this.resource$
            .pipe(
                repeatWhen(_ => this.refreshSubject.asObservable()),
                flatMap((resource) => this.currentDateSpan$.pipe(
                    map(dateSpan => {
                        return {resource, dateSpan}
                    })
                )),
                switchMap(data => this.reservationService.getBy({
                    startTime: data.dateSpan.start.toISOString(),
                    endTime: data.dateSpan.end.toISOString(),
                    resourceId: data.resource.id
                }))
            );

        this.reservations$.subscribe(r => {
            this.cal.addReservations(r);
            if (this.cal.eventClicked.observers.length === 0) {
                this.cal.eventClicked.subscribe(event => this.openReservationModal(event));
            }
        });
    }

    handleUpdate({start, end}) {
        this.currentDateSpan$.next({start, end});
    }

    handleDateRangeSelection(range: { start: Date, end: Date }) {
        this.lastSelectedRange = range;
    }

    openNewReservationModal() {
        const ref = this.modalService.open(ModalReservationComponent,{ windowClass : "modal-size-lg"});
        ref.componentInstance.Add = true;
        ref.componentInstance.date = this.lastSelectedRange.start;
        ref.componentInstance.dateField = {year: this.lastSelectedRange.start.getFullYear(), month: this.lastSelectedRange.start.getMonth()+1, day: this.lastSelectedRange.start.getDate()};

        const milliDiff = this.lastSelectedRange.end.getTime() - this.lastSelectedRange.start.getTime();
        const minuteDiff = milliDiff / (1000 * 60);
        const hourDiff = minuteDiff / 60;
        if (minuteDiff !== 1440 && minuteDiff !== 0) {
            ref.componentInstance.duration = {hour: Math.floor(minuteDiff), minute: Math.floor(minuteDiff)};
        } else {
            ref.componentInstance.duration = {hour: Math.floor(0), minute: Math.floor(60)};
        }

        if (this.lastSelectedRange && minuteDiff !== 1440) {
            ref.componentInstance.timeStart.hour = this.lastSelectedRange.start.getHours();
            ref.componentInstance.timeStart.minute = this.lastSelectedRange.start.getMinutes();
        } else {
            let now = new Date();
            ref.componentInstance.timeStart.hour = now.getHours();
            ref.componentInstance.timeStart.minute = now.getMinutes();
        }

        ref.componentInstance.status = "NEW";
        this.resource$.subscribe(r => {
            if (ref.componentInstance) {
                ref.componentInstance.resourceId = r.id;
                ref.componentInstance.resourceName = r.displayName;
            }
        });
        this.authService.currentUser$.subscribe(u => {
            ref.componentInstance.userId = u.id;
            ref.componentInstance.userName = u.sub;
        });

        ref.componentInstance.refresh = () => this.refreshSubject.next(null);
    }

    openReservationModal(event: any) {
        const ref = this.modalService.open(ModalReservationComponent,{ windowClass : "modal-size-lg"});
        ref.componentInstance.Add = false;

        ref.componentInstance.reservationId = event.extendedProps.reservationId;
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

        ref.componentInstance.refresh = () => this.refreshSubject.next(null);
    }

    ngOnInit() {

    }
}
