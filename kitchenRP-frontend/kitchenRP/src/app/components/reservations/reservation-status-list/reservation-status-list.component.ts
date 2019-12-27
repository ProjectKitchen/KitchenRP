import { Component, OnInit } from '@angular/core';

import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {ModalReservationComponent} from "../../../modals/modal-reservation/modal-reservation.component";

@Component({
  selector: 'app-reservation-status-list',
  templateUrl: './reservation-status-list.component.html',
  styleUrls: ['./reservation-status-list.component.css']
})
export class ReservationStatusListComponent implements OnInit {

  // test data
  table = [
    {
      "id": 1,
      "date":"11-12-2019",
      "timeStart": "10:00",
      "timeEnd": "12:00",
      "duration": "2:00",
      "resource": "Printer1",
      "status": "Pending"
    },{
      "id": 2,
      "date":"11-12-2019",
      "timeStart": "10:00",
      "timeEnd": "12:00",
      "duration": "2:00",
      "resource": "Printer2",
      "status": "Accepted"
    },{
      "id": 3,
      "date":"11-12-2019",
      "timeStart": "10:00",
      "timeEnd": "12:00",
      "duration": "2:00",
      "resource": "Printer3",
      "status": "Denied"
    },{
      "id": 4,
      "date":"11-12-2019",
      "timeStart": "10:00",
      "timeEnd": "12:00",
      "duration": "2:00",
      "resource": "Printer4",
      "status": "Pending"
    }
  ];

  constructor(private modalService: NgbModal) { }

  ngOnInit() {
  }

  openModal(tableRow) {
    const modalRef = this.modalService.open(ModalReservationComponent, { windowClass : "modal-size-xl"});
    modalRef.componentInstance.Data = tableRow;
  }

}
