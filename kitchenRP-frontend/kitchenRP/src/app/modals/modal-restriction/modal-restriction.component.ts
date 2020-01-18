import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal-restriction',
  templateUrl: './modal-restriction.component.html',
  styleUrls: ['./modal-restriction.component.css']
})
export class ModalRestrictionComponent implements OnInit {

  @Input() Data;

  constructor(private activeModal: NgbActiveModal) { }

  ngOnInit() {
  }

}
