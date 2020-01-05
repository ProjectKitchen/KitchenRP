import { Component, OnInit, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {ModalRestrictionComponent} from "../../../../modals/modal-restriction/modal-restriction.component";

import {Restriction} from "../../../../types/restriction";

  // test data
  const table: Restriction[] = [
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

  function search(text: string, pipe: PipeTransform): Restriction[] {
    return table.filter(restriction => {
    const term = text.toLowerCase();
    return restriction.dateFrom.toLowerCase().includes(term)
        || restriction.dateTo.toLowerCase().includes(term)
        || restriction.resource.toLowerCase().includes(term);
        //|| pipe.transform(restriction.id).includes(term); // ID search?
    });
  }

@Component({
  selector: 'app-restriction-management',
  templateUrl: './restriction-management.component.html',
  styleUrls: ['./restriction-management.component.css'],
  providers: [DecimalPipe]
})
export class RestrictionManagementComponent implements OnInit {

  restrictions$: Observable<Restriction[]>;
  filter = new FormControl('');

  constructor(private modalService: NgbModal, pipe: DecimalPipe) {
    this.restrictions$ = this.filter.valueChanges.pipe(
      startWith(''),
      map(text => search(text, pipe))
    );
  }

  ngOnInit() {
  }

  openModal(tableRow) {
    const modalRef = this.modalService.open(ModalRestrictionComponent, { windowClass : "modal-size-xl"});
    modalRef.componentInstance.Data = tableRow;
  }

}
