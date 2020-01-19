import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { User } from '../../types/user'

@Component({
  selector: 'app-modal-user',
  templateUrl: './modal-user.component.html',
  styleUrls: ['./modal-user.component.css']
})
export class ModalUserComponent implements OnInit {

  @Input() Data;

  changedUser: User = {};

  constructor(private activeModal: NgbActiveModal) {
  }

  ngOnInit() {
      this.changedUser.sub = this.Data.sub;
  }

  save(){
      console.log(this.changedUser.sub);
  }
}
