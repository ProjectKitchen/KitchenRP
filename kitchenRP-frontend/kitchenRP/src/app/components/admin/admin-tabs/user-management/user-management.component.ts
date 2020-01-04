import { Component, OnInit } from '@angular/core';

import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {ModalUserComponent} from "../../../../modals/modal-user/modal-user.component";

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {

  // test data
  table = [
    {
      "id": 1,
      "username":"Matthias",
      "role": "Member",
      "email": "bla@bla.com"
    },{
      "id": 2,
      "username":"Matthias",
      "role": "Member",
      "email": "bla@bla.com"
    },{
      "id": 3,
      "username":"Oliver",
      "role": "Admin",
      "email": "bla@bla.com"
    },{
      "id": 4,
      "username":"Daniel",
      "role": "Moderator",
      "email": "bla@bla.com"
    },{
      "id": 5,
      "username":"Hallo",
      "role": "Member",
      "email": "bla@bla.com"
    },{
      "id": 6,
      "username":"Viktor",
      "role": "Member",
      "email": "bla@bla.com"
    },{
      "id": 7,
      "username":"Hallo2",
      "role": "Member",
      "email": "bla@bla.com"
    },{
      "id": 8,
      "username":"Hallo3",
      "role": "Member",
      "email": "bla@bla.com"
    },{
      "id": 9,
      "username":"Hallo4",
      "role": "Member",
      "email": "bla@bla.com"
    },{
      "id": 10,
      "username":"Hallo5",
      "role": "Member",
      "email": "bla@bla.com"
    },
  ];

  constructor(private modalService: NgbModal) { }

  ngOnInit() {
  }

  openModal(tableRow) {
    const modalRef = this.modalService.open(ModalUserComponent, { windowClass : "modal-size-xl"});
    modalRef.componentInstance.Data = tableRow;
  }

}
