import {Component, Input, OnInit} from '@angular/core';
import {NgbActiveModal} from "@ng-bootstrap/ng-bootstrap";
import {Resource} from '../../types/resource';
import {ResourceService} from '../../services/resource/resource.service';

@Component({
  selector: 'app-modal-resource',
  templateUrl: './modal-resource.component.html',
  styleUrls: ['./modal-resource.component.css']
})
export class ModalResourceComponent implements OnInit {

  @Input() Data;

  changedR: Resource;

  constructor(private activeModal: NgbActiveModal) {
  }

  ngOnInit() {
    this.changedR = {...this.Data, resourceType:{...this.Data.resourceType}};
  }

  save(){
    console.log(this.changedR);
  }

}
