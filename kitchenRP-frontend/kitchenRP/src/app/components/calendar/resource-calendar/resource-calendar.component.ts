import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {flatMap, map, switchMap} from "rxjs/operators";
import {ResourceService} from "../../../services/resource/resource.service";
import {Observable, ReplaySubject} from "rxjs";
import {Resource} from "../../../types/resource";
import {log} from "util";
import {Reservation} from "../../../types/reservation";
import {ReservationService} from "../../../services/reservation/reservation.service";
import {FullcalComponent} from "../fullcal/fullcal.component";

@Component({
    selector: 'app-resource-calendar',
    templateUrl: './resource-calendar.component.html',
    styleUrls: ['./resource-calendar.component.css']
})
export class ResourceCalendarComponent implements OnInit {

    private resource$: Observable<Resource>;

    private reservations$: Observable<Reservation[]>;

    private currentDateSpan$ = new ReplaySubject<{ start: Date, end: Date }>();

    @ViewChild('calendar', {static: false})
    private cal: FullcalComponent;

    constructor(private route: ActivatedRoute, private resourceService: ResourceService, private reservationService: ReservationService) {
        this.resource$ = route.params.pipe(
            flatMap(params => resourceService.getById(+params['id']))
        );

        this.reservations$ = this.resource$
            .pipe(
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
            console.log(r);
            r.forEach(res => this.cal.addReservation(res))
        });
    }

    handleUpdate({start, end}) {
        this.currentDateSpan$.next({start, end});
    }

    ngOnInit() {
    }

}
