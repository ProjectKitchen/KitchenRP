import { Component, OnInit, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {ModalDeviceComponent} from "../../../../modals/modal-device/modal-device.component";

import {Resource} from "../../../../types/resource";

  // test data
  const table: Resource [] = [
    {
      "id": 1,
      "name":"Printer1",
      "meta": "PLA, 20x20",
      "type": "3D-Printer",
      "description": "123456"
    },{
      "id": 2,
      "name":"Printer2",
      "meta": "123",
      "type": "3D-Printer",
      "description": "aoewifaoewf"
    },{
      "id": 3,
      "name":"Printer3",
      "meta": "PLA, 20x20",
      "type": "3D-Printer",
      "description": "aoweifaowef"
    },{
      "id": 4,
      "name":"Printer4",
      "meta": "PLA, 20x20",
      "type": "3D-Printer",
      "description": "öoawnfeoawnef"
    },{
      "id": 5,
      "name":"Printer5",
      "meta": "PLA, 20x20",
      "type": "3D-Printer",
      "description": "eäsrbmserb"
    },{
      "id": 6,
      "name":"Printer6",
      "meta": "PLA, 20x20",
      "type": "3D-Printer",
      "description": "aüoaiefüoaef"
    }
  ];

  function search(text: string, pipe: PipeTransform): Resource[] {
    return table.filter(device => {
    const term = text.toLowerCase();
    return device.name.toLowerCase().includes(term)
        || device.meta.toLowerCase().includes(term)
        || device.type.toLowerCase().includes(term);
        //|| pipe.transform(device.id).includes(term); // ID search?
    });
  }

@Component({
  selector: 'app-device-management',
  templateUrl: './device-management.component.html',
  styleUrls: ['./device-management.component.css'],
  providers: [DecimalPipe]
})
export class DeviceManagementComponent implements OnInit {

  devices$: Observable<Resource[]>;
  filter = new FormControl('');

  constructor(private modalService: NgbModal, pipe: DecimalPipe) {
    this.devices$ = this.filter.valueChanges.pipe(
      startWith(''),
      map(text => search(text, pipe))
    );
  }

  ngOnInit() {
  }

  openModal(tableRow) {
    const modalRef = this.modalService.open(ModalDeviceComponent, { windowClass : "modal-size-xl"});
    modalRef.componentInstance.Data = tableRow;
  }
}
