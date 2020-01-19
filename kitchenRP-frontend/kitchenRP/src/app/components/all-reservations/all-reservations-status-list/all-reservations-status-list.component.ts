import { Component, OnInit, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith, tap, flatMap } from 'rxjs/operators';

import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {ModalReservationComponent} from "../../../modals/modal-reservation/modal-reservation.component";

import {Reservation} from "../../../types/reservation";
import {ReservationService} from "../../../services/reservation/reservation.service";

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

  constructor(private reservationService: ReservationService, private modalService: NgbModal, pipe: DecimalPipe) {
    this.pendingReservations$ = this.reservationService.getBy({status: "Pending"})
        .pipe(
            tap(reservations => this.pendingData = reservations),
            flatMap(r => this.filter.valueChanges
                .pipe(
                    startWith(''),
                    map(text => this.search(text, pipe, this.pendingData))
                )
            )
        );

    this.checkedReservations$ = this.reservationService.getBy({status: "Accepted,Denied"})
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
        || reservation.status.toLowerCase().includes(term);
        //|| pipe.transform(reservation.id).includes(term); // ID search?
    });
  }

  openModal(tableRow) {
    const modalRef = this.modalService.open(ModalReservationComponent, { windowClass : "modal-size-lg"});
    modalRef.componentInstance.Data = tableRow;
  }

}
