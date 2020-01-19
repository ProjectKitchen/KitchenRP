import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal-reservation',
  templateUrl: './modal-reservation.component.html',
  styleUrls: ['./modal-reservation.component.css']
})
export class ModalReservationComponent implements OnInit {

  @Input() Data;
  @Input() Add: boolean;

  constructor(private activeModal: NgbActiveModal) { }

  ngOnInit() {
  }

  save() {
    if (this.Add === undefined || !this.Add) {

    } else {

    }
  }

}
