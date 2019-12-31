import { Component, OnInit } from '@angular/core';

import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {ModalReservationComponent} from "../../../modals/modal-reservation/modal-reservation.component";

@Component({
  selector: 'app-all-reservations-status-list',
  templateUrl: './all-reservations-status-list.component.html',
  styleUrls: ['./all-reservations-status-list.component.css']
})
export class AllReservationsStatusListComponent implements OnInit {

  // test data
  tablePending = [
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
      "status": "Pending"
    },{
      "id": 3,
      "date":"11-12-2019",
      "timeStart": "10:00",
      "timeEnd": "12:00",
      "duration": "2:00",
      "resource": "Printer3",
      "status": "Pending"
    },{
      "id": 4,
      "date":"11-12-2019",
      "timeStart": "10:00",
      "timeEnd": "12:00",
      "duration": "2:00",
      "resource": "Printer4",
      "status": "Pending"
    },{
      "id": 5,
      "date":"11-12-2019",
      "timeStart": "10:00",
      "timeEnd": "12:00",
      "duration": "2:00",
      "resource": "Printer4",
      "status": "Pending"
    },{
      "id": 6,
      "date":"11-12-2019",
      "timeStart": "10:00",
      "timeEnd": "12:00",
      "duration": "2:00",
      "resource": "Printer4",
      "status": "Pending"
    }
  ];

  // test data
  tableChecked = [
    {
      "id": 1,
      "date":"11-12-2019",
      "timeStart": "10:00",
      "timeEnd": "12:00",
      "duration": "2:00",
      "resource": "Printer1",
      "status": "Accepted"
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
      "status": "Accepted"
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
