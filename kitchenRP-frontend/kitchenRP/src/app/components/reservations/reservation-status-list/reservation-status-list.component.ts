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
  selector: 'app-reservation-status-list',
  templateUrl: './reservation-status-list.component.html',
  styleUrls: ['./reservation-status-list.component.css'],
  providers: [DecimalPipe]
})
export class ReservationStatusListComponent implements OnInit {
  data: Reservation[] = [];
  reservations$: Observable<Reservation[]>;
  filter = new FormControl('');

  constructor(private reservationService: ReservationService, private modalService: NgbModal, pipe: DecimalPipe) {
    this.reservations$ = this.reservationService.getBy({id: 1})
        .pipe(
            tap(reservations => this.data = reservations),
            flatMap(r => this.filter.valueChanges
                .pipe(
                    startWith(''),
                    map(text => this.search(text, pipe))
                )
            )
        )
  }

  ngOnInit() {
  }

  search(text: string, pipe: PipeTransform): Reservation[] {
    return this.data.filter(reservation => {
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

  openModalAdd() {
    const modalRef = this.modalService.open(ModalReservationComponent, { windowClass : "modal-size-lg"});
    modalRef.componentInstance.Data = {id: "", date: "", duration: "", startTime: "", endTime: "", userId: "", resourceId: "", status: ""};
    modalRef.componentInstance.Add = true;
  }

}
