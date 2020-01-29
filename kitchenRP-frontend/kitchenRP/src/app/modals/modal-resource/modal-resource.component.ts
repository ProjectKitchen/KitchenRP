import {Component, Input, OnInit} from '@angular/core';
import {NgbActiveModal} from "@ng-bootstrap/ng-bootstrap";
import {NewResource, Resource, ResourceType} from '../../types/resource';
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
  @Input() Add: boolean;
  changedR: Resource;

  rTypes$: Observable<ResourceType[]>;
  meta = [];

  constructor(private activeModal: NgbActiveModal, private resourceService: ResourceService) {
    this.rTypes$ = this.resourceService.getAllTypes();
  }

  ngOnInit() {
    this.changedR = {...this.Data, resourceType:{...this.Data.resourceType}};

    let m = JSON.parse(JSON.stringify(this.Data.metaData)).rootElement;
    let meta = [];
    for(let key in m){
        meta.push({key: key, value: m[key]});
    }
    this.meta = meta;
  }

  save(){
    if (this.Add === undefined || !this.Add) {
      // metadata array to object
      let meta: any = this.meta.reduce<any>((o, kv) => {
            o[kv.key] = kv.value;
            return o;
        }, {});

      // setup for changed check => copy data, so they are not overwritten
      let changed: boolean = false;
      let compChanged = {...this.changedR};
      compChanged.metaData = JSON.stringify(meta); // changed meta data string
      let compData = {...this.Data};
      compData.metaData = JSON.stringify(compData.metaData.rootElement); // data meta data string
      // compare
      if(JSON.stringify(compChanged) != JSON.stringify(compData)){
        changed = true;
      }

      if(changed){
        console.log("update");
        this.changedR.metaData = JSON.stringify(meta); // stringify actual data that is sent to backend
        this.resourceService.update(this.changedR).subscribe(x => this.refresh());
      }
    } else {
      this.resourceService.create({
        displayName: this.changedR.displayName,
        description: this.changedR.description,
        metaData: this.changedR.metaData,
        resourceTypeName: this.changedR.resourceType['type']
      }).subscribe(x => this.refresh());
    }
    this.activeModal.close();
  }

  delete(){
      console.log("delete");
      this.resourceService.delete(this.Data.id).subscribe(x => this.refresh());
      this.activeModal.close();
  }

}
