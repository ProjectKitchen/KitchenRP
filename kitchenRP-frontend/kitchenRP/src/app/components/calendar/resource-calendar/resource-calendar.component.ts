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
            this.cal.eventClicked.subscribe(event => this.openReservationModal(event));
        });
    }

    handleUpdate({start, end}) {
        this.currentDateSpan$.next({start, end});
    }

    handleDateRangeSelection(range: { start: Date, end: Date }) {

        //console.log("range form event:");
        //console.log(range);
        this.lastSelectedRange = range;
        //this.openNewReservationModal();
    }

    openNewReservationModal() {
        const ref = this.modalService.open(ModalReservationComponent,{ windowClass : "modal-size-lg"});
        ref.componentInstance.Add = true;
        ref.componentInstance.date = this.lastSelectedRange.start;
        ref.componentInstance.dateString = this.lastSelectedRange.start.getFullYear() + "-" + this.lastSelectedRange.start.getMonth() + "-" + this.lastSelectedRange.start.getDate();
        // ref.componentInstance.dateField = {
        //     year: this.lastSelectedRange.start.getFullYear(),
        //     month: this.lastSelectedRange.start.getMonth(),
        //     day: this.lastSelectedRange.start.getDate(),
        // };

        // ref.componentInstance.timeStart.hour = this.lastSelectedRange.start.getHours();
        // ref.componentInstance.timeStart.minute = this.lastSelectedRange.start.getMinutes();
        let now = new Date();
        ref.componentInstance.timeStart.hour = now.getHours();
        ref.componentInstance.timeStart.minute = now.getMinutes();

        const milliDiff = this.lastSelectedRange.end.getTime() - this.lastSelectedRange.start.getTime();
        const minuteDiff = milliDiff / (1000 * 60);
        const hourDiff = minuteDiff / 60;
        // ref.componentInstance.duration = {hour: Math.floor(minuteDiff), minute: Math.floor(minuteDiff)};
        ref.componentInstance.duration = {hour: Math.floor(0), minute: Math.floor(60)};

        ref.componentInstance.status = "";
        this.resource$.subscribe(r => {
            ref.componentInstance.resourceId = r.id;
            ref.componentInstance.resourceName = r.displayName;
        });
        this.authService.currentUser$.subscribe(u => {
            ref.componentInstance.userId = u.id;
            ref.componentInstance.userName = u.sub;
        });

        ref.result.then(
            _ => {
                this.refreshSubject.next(true)
            }
        )
    }

    openReservationModal(event: any) {
        const ref = this.modalService.open(ModalReservationComponent,{ windowClass : "modal-size-lg"});
        ref.componentInstance.Add = false;

        ref.componentInstance.date = event.start;
        ref.componentInstance.timeStart.hour = event.start.getHours();
        ref.componentInstance.timeStart.minute = event.start.getMinutes();

        const milliDiff = event.end.getTime() - event.start.getTime();
        const minuteDiff = milliDiff / (1000 * 60);
        const hourDiff = minuteDiff / 60;
        ref.componentInstance.duration = {hour: Math.floor(minuteDiff), minute: Math.floor(minuteDiff)};

        ref.componentInstance.status = event.status;
        this.resourceService.getById(event.extendedProps.reservationId).subscribe(r => {
            ref.componentInstance.resourceId = r.id;
            ref.componentInstance.resourceName = r.displayName;
        });
        this.userService.getById(event.extendedProps.userId).subscribe(u => {
            ref.componentInstance.userId = u.id;
            ref.componentInstance.userName = u.sub;
        });


        ref.result.then(
            _ => {
                this.refreshSubject.next(true)
            }
        )
    }

    ngOnInit() {

    }

}
