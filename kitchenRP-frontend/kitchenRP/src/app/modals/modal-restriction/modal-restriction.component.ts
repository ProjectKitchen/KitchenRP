import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Restriction, RestrictionData } from '../../types/restriction';
import { RestrictionService } from '../../services/restriction/restriction.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-modal-restriction',
  templateUrl: './modal-restriction.component.html',
  styleUrls: ['./modal-restriction.component.css']
})
export class ModalRestrictionComponent implements OnInit {
  refresh: () => void;

  @Input() Data;
  @Input() Add: boolean;
  changedR: Restriction;

  constructor(private activeModal: NgbActiveModal, private restrictionService: RestrictionService) { }

  ngOnInit() {
    this.changedR = {...this.Data, restrictionData:{...this.Data.restrictionData}};
  }

  save(){
    if (this.Add === undefined || !this.Add) {
      // setup for changed check => copy data, so they are not overwritten
      let changed: boolean = false;
      let compChanged = {...this.changedR};
      let compData = {...this.Data};
      // compare
      if(JSON.stringify(compChanged) != JSON.stringify(compData)){
        changed = true;
      }

      if(changed){
        console.log("update");
        console.log(this.changedR);
        this.restrictionService.update(this.changedR).subscribe(x => this.refresh());
      }
    } else {
      this.restrictionService.create({
        dateFrom: this.changedR.dateFrom,
        dateTo: this.changedR.dateTo,
        ignoreYear: this.changedR.ignoreYear,
        resourceId: this.changedR.resourceId,
        displayError: this.changedR.displayError,
        restrictionData: null // TODO
      }).subscribe(x => this.refresh());
    }
    this.activeModal.close();
  }

  delete(){
      // console.log("delete");
      this.restrictionService.delete(this.Data.id).subscribe(x => this.refresh());
      this.activeModal.close();
  }

}
