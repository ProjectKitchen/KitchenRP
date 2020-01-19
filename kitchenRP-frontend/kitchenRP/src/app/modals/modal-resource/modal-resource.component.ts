import {Component, Input, OnInit} from '@angular/core';
import {NgbActiveModal} from "@ng-bootstrap/ng-bootstrap";
import { Resource } from '../../types/resource'

@Component({
  selector: 'app-modal-resource',
  templateUrl: './modal-resource.component.html',
  styleUrls: ['./modal-resource.component.css']
})
export class ModalResourceComponent implements OnInit {

  @Input() Data;

  changedR: Resource = {};

  constructor(private activeModal: NgbActiveModal) { }

  ngOnInit() {
    this.changedR.displayName = this.Data.displayName;
    this.changedR.resourceType = this.Data.resourceType;
    this.changedR.metaData = this.Data.metaData;
    this.changedR.description = this.Data.description;
  }

  save(){
      console.log(this.changedR);
  }

}
