import { Component, OnInit, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith, tap, flatMap } from 'rxjs/operators';

import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {ModalReservationComponent} from "../../../modals/modal-reservation/modal-reservation.component";

import {Reservation} from "../../../types/reservation";
import {ReservationService} from "../../../services/reservation/reservation.service";
import {ResourceService} from "../../../services/resource/resource.service";
import {UserService} from "../../../services/user/user.service";

@Component({
  selector: 'app-all-reservations-status-list',
  templateUrl: './all-reservations-status-list.component.html',
  styleUrls: ['./all-reservations-status-list.component.css'],
  providers: [DecimalPipe]
})
export class AllReservationsStatusListComponent implements OnInit {
  pendingData: Reservation[] = [];
  checkedData: Reservation[] = [];

  pendingReservations$: Observable<Reservation[]>;
  checkedReservations$: Observable<Reservation[]>;
  filter = new FormControl('');

  constructor(private reservationService: ReservationService,
              private resourceService: ResourceService,
              private userService: UserService,
              private modalService: NgbModal, pipe: DecimalPipe) {
    this.pendingReservations$ = this.reservationService.getBy({statuses: "PENDING"})
        .pipe(
            tap(reservations => this.pendingData = reservations),
            flatMap(r => this.filter.valueChanges
                .pipe(
                    startWith(''),
                    map(text => this.search(text, pipe, this.pendingData))
                )
            )
        );

    this.checkedReservations$ = this.reservationService.getBy({statuses: "ACCEPTED,DENIED"})
        .pipe(
            tap(reservations => this.checkedData = reservations),
            flatMap(r => this.filter.valueChanges
                .pipe(
                    startWith(''),
                    map(text => this.search(text, pipe, this.checkedData))
                )
            )
        );
  }

  ngOnInit() {
  }

  search(text: string, pipe: PipeTransform, data: Reservation[]): Reservation[] {
    return data.filter(reservation => {
    const term = text.toLowerCase();
    return  reservation.startTime.toLowerCase().includes(term)
        || reservation.endTime.toLowerCase().includes(term)
        || reservation.owner.sub.toLowerCase().includes(term)
        || reservation.reservedResource.displayName.toLowerCase().includes(term)
        || reservation.status.status.toLowerCase().includes(term);
    });
  }

  openModal(tableRow) {
    const ref = this.modalService.open(ModalReservationComponent,{ windowClass : "modal-size-lg"});
      ref.componentInstance.Add = false;

      ref.componentInstance.reservationId = tableRow.id;
      let start = new Date(tableRow.startTime);
      let end = new Date(tableRow.endTime);
      ref.componentInstance.date = start;
      ref.componentInstance.timeStart.hour = start.getHours();
      ref.componentInstance.timeStart.minute = start.getMinutes();

      const milliDiff = end.getTime() - start.getTime();
      const minuteDiff = milliDiff / (1000 * 60);
      const hourDiff = minuteDiff / 60;
      ref.componentInstance.duration = {hour: Math.floor(minuteDiff), minute: Math.floor(minuteDiff)};

      ref.componentInstance.status = tableRow.status ? tableRow.status.status : "";
      ref.componentInstance.resourceId = tableRow.reservedResource ? tableRow.reservedResource.id : "";
      ref.componentInstance.resourceName = tableRow.reservedResource ? tableRow.reservedResource.displayName : "";
      ref.componentInstance.userId = tableRow.owner ? tableRow.owner.id : "";
      ref.componentInstance.userName = tableRow.owner ? tableRow.owner.sub : "";
  }

    timestampToReadable(ts: string) {
        return ts.replace(/[TZ]/g, " ");
    }
}
