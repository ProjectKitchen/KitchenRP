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
  showMeta = true;

  constructor(private activeModal: NgbActiveModal, private resourceService: ResourceService) {
    this.rTypes$ = this.resourceService.getAllTypes();
  }

  ngOnInit() {
    this.changedR = {...this.Data, resourceType:{...this.Data.resourceType}};

    let m = this.Data.metaData.rootElement;

    let meta = [];
    for(let key in m){
        meta.push({key: key, value: m[key]});
    }
    this.meta = meta;
  }

  save(){
    // metadata array to object
    let meta: any = this.meta.reduce<any>((o, kv) => {
        o[kv.key] = kv.value;
        return o;
    }, {});

    if (this.Add === undefined || !this.Add) {
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
        this.changedR.metaData = meta; // stringify actual data that is sent to backend
        console.log(this.changedR);
        this.resourceService.update(this.changedR).subscribe(x => this.refresh());
      }
    } else {
      this.changedR.metaData = meta;
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
      // console.log("delete");
      this.resourceService.delete(this.Data.id).subscribe(x => this.refresh());
      this.activeModal.close();
  }

  addMeta(){
      this.meta.push({"": ""});
      //console.log(this.meta[""]);
      this.reloadMeta();
  }

  removeMeta(data){
      let metaDataIndex: number = +this.getKeyByValue(this.meta, data);
      this.meta.splice(metaDataIndex, 1);
      //this.reloadMeta();
  }

  reloadMeta(){
      this.showMeta = false;
      setTimeout(() => this.showMeta = true);
  }

  getKeyByValue(object, value) {
    return Object.keys(object).find(key => object[key] === value);
  }

  /* Useless because database mixes up order... ------
  moveMetaUp(data){
      let metaDataIndex = this.getKeyByValue(this.meta, data);
      if(metaDataIndex > 0){
          let temp = this.meta[metaDataIndex];
          this.meta[metaDataIndex] = this.meta[metaDataIndex - 1];
          this.meta[metaDataIndex - 1] = temp;

          this.reloadMeta();
      }
  }

  moveMetaDown(data){
      let metaDataIndex = this.getKeyByValue(this.meta, data);
      if(metaDataIndex < this.meta.length - 1){
          let temp = this.meta[metaDataIndex];
          this.meta[metaDataIndex] = this.meta[metaDataIndex + 1];
          this.meta[metaDataIndex + 1] = temp;

          this.reloadMeta();
      }
  }
  ---------------------------------------------------------*/
}
