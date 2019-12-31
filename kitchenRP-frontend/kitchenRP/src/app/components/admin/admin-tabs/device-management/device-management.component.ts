import { Component, OnInit } from '@angular/core';

import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {ModalDeviceComponent} from "../../../../modals/modal-device/modal-device.component";

@Component({
  selector: 'app-device-management',
  templateUrl: './device-management.component.html',
  styleUrls: ['./device-management.component.css']
})
export class DeviceManagementComponent implements OnInit {
// test data
  table = [
    {
      "id": 1,
      "name":"Printer1",
      "type": "3D-Printer",
      "material": "PLA",
      "size": "20x20",
      "status": "Working"
    },{
      "id": 2,
      "name":"Printer2",
      "type": "3D-Printer",
      "material": "PLA",
      "size": "20x20",
      "status": "Working"
    },{
      "id": 3,
      "name":"Printer3",
      "type": "3D-Printer",
      "material": "PLA",
      "size": "20x20",
      "status": "Working"
    },{
      "id": 4,
      "name":"Printer4",
      "type": "3D-Printer",
      "material": "PLA",
      "size": "20x20",
      "status": "Working"
    },{
      "id": 5,
      "name":"Printer5",
      "type": "3D-Printer",
      "material": "PLA",
      "size": "20x20",
      "status": "Working"
    },{
      "id": 6,
      "name":"Printer6",
      "type": "3D-Printer",
      "material": "PLA",
      "size": "20x20",
      "status": "Working"
    }
  ];

  constructor(private modalService: NgbModal) { }

  ngOnInit() {
  }

  openModal(tableRow) {
    const modalRef = this.modalService.open(ModalDeviceComponent, { windowClass : "modal-size-xl"});
    modalRef.componentInstance.Data = tableRow;
  }
}
