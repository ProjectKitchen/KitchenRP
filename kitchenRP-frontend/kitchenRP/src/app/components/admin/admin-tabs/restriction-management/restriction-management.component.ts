import { Component, OnInit, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { FormControl } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { tap, map, startWith, flatMap, repeatWhen  } from 'rxjs/operators';

import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {ModalRestrictionComponent} from "../../../../modals/modal-restriction/modal-restriction.component";

import {Restriction, RestrictionData, NewRestriction } from "../../../../types/restriction";
import {RestrictionService} from "../../../../services/restriction/restriction.service";


@Component({
  selector: 'app-restriction-management',
  templateUrl: './restriction-management.component.html',
  styleUrls: ['./restriction-management.component.css'],
  providers: [DecimalPipe]
})
export class RestrictionManagementComponent implements OnInit {
    private refreshSubject = new Subject<any>();

    data: Restriction [] = [];
    restrictions$: Observable<Restriction[]>;
    filter = new FormControl('');

  constructor(private restrictionService: RestrictionService, private modalService: NgbModal, pipe: DecimalPipe) {
      this.restrictions$ = this.restrictionService.getAll()
      .pipe(
          repeatWhen(_ => this.refreshSubject.asObservable()),
          tap(restrictions => this.data = restrictions),
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

  openModal(tableRow) {
    const modalRef = this.modalService.open(ModalRestrictionComponent, { windowClass : "modal-size-lg"});
    modalRef.componentInstance.Data = tableRow;
    modalRef.componentInstance.refresh = () => this.refresh();
  }

  openModalAdd() {
    const modalRef = this.modalService.open(ModalRestrictionComponent, { windowClass : "modal-size-lg"});
    modalRef.componentInstance.Data = {id: "-", displayError: ""};
    modalRef.componentInstance.Add = true;
    modalRef.componentInstance.refresh = () => this.refresh();
  }

  search(text: string, pipe: PipeTransform): Restriction[] {
    return this.data.filter(restriction => {
    const term = text.toLowerCase();
    return restriction.dateFrom.toLowerCase().includes(term)
        || restriction.dateTo.toLowerCase().includes(term);
        //|| restriction.resource.toLowerCase().includes(term);
        //|| pipe.transform(restriction.id).includes(term); // ID search?
    });
  }

  refresh(): void{
        this.refreshSubject.next(null);
  }
}
