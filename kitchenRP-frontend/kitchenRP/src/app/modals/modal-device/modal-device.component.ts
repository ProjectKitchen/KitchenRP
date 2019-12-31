import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal-device',
  templateUrl: './modal-device.component.html',
  styleUrls: ['./modal-device.component.css']
})
export class ModalDeviceComponent implements OnInit {

  @Input() Data;

  constructor(private activeModal: NgbActiveModal) { }

  ngOnInit() {
  }

}
