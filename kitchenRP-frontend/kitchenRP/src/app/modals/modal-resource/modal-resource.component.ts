import {Component, Input, OnInit} from '@angular/core';
import {NgbActiveModal} from "@ng-bootstrap/ng-bootstrap";
import {Resource, ResourceType} from '../../types/resource';
import {ResourceService} from '../../services/resource/resource.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-modal-resource',
  templateUrl: './modal-resource.component.html',
  styleUrls: ['./modal-resource.component.css']
})
export class ModalResourceComponent implements OnInit {
  refresh: () => void;

  @Input() Data;
  changedR: Resource;

  rTypes$: Observable<ResourceType[]>;

  constructor(private activeModal: NgbActiveModal, private resourceService: ResourceService) {
    this.rTypes$ = this.resourceService.getAllTypes();
  }

  ngOnInit() {
    this.changedR = {...this.Data, resourceType:{...this.Data.resourceType}};
  }

  save(){
    let changed: boolean = false;
    if(JSON.stringify(this.changedR) != JSON.stringify(this.Data)){
        changed = true;
    }

    if(changed){
        console.log("update");
        this.resourceService.update(this.changedR).subscribe(x => this.refresh());
    }
    this.activeModal.close();
  }

}
