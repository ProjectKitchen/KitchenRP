import { Component, OnInit, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {ModalReservationComponent} from "../../../modals/modal-reservation/modal-reservation.component";

import {Reservation} from "../../../types/reservation";

  // test data
  const table: Reservation[] = [
    {
      "id": 1,
      "startTime": "10:00",
      "endTime": "12:00",
      "userId": 3,
      "resourceId": 2,
      "status": "Pending"
    }
  ];

  function search(text: string, pipe: PipeTransform): Reservation[] {
    return table.filter(reservation => {
    const term = text.toLowerCase();
    return  reservation.startTime.toLowerCase().includes(term)
        || reservation.endTime.toLowerCase().includes(term)
        || reservation.status.toLowerCase().includes(term);
        //|| pipe.transform(reservation.id).includes(term); // ID search?
    });
  }

@Component({
  selector: 'app-reservation-status-list',
  templateUrl: './reservation-status-list.component.html',
  styleUrls: ['./reservation-status-list.component.css'],
  providers: [DecimalPipe]
})
export class ReservationStatusListComponent implements OnInit {

  reservations$: Observable<Reservation[]>;
  filter = new FormControl('');

  constructor(private modalService: NgbModal, pipe: DecimalPipe) {
    this.reservations$ = this.filter.valueChanges.pipe(
      startWith(''),
      map(text => search(text, pipe))
    );
  }

  ngOnInit() {
  }

  openModal(tableRow) {
    const modalRef = this.modalService.open(ModalReservationComponent, { windowClass : "modal-size-lg"});
    modalRef.componentInstance.Data = tableRow;
  }

}
