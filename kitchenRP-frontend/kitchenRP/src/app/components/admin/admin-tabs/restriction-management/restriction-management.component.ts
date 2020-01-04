import { Component, OnInit } from '@angular/core';

import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {ModalRestrictionComponent} from "../../../../modals/modal-restriction/modal-restriction.component";

@Component({
  selector: 'app-restriction-management',
  templateUrl: './restriction-management.component.html',
  styleUrls: ['./restriction-management.component.css']
})
export class RestrictionManagementComponent implements OnInit {

  // test data
  table = [
    {
      "id": 1,
      "dateFrom":"10-10-2020",
      "dateTo": "11-10-2020",
      "resource": "Printer1"
    },{
      "id": 2,
      "dateFrom":"10-10-2020",
      "dateTo": "11-10-2020",
      "resource": "Printer2"
    },{
      "id": 3,
      "dateFrom":"10-10-2020",
      "dateTo": "11-10-2020",
      "resource": "Printer3"
    },{
      "id": 4,
      "dateFrom":"10-10-2020",
      "dateTo": "11-10-2020",
      "resource": "Printer4"
    }
  ];

  constructor(private modalService: NgbModal) { }

  ngOnInit() {
  }

  openModal(tableRow) {
    const modalRef = this.modalService.open(ModalRestrictionComponent, { windowClass : "modal-size-xl"});
    modalRef.componentInstance.Data = tableRow;
  }

}
