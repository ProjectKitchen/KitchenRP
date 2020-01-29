import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { User } from '../../types/user'
import { UserService } from '../../services/user/user.service'

@Component({
  selector: 'app-modal-user',
  templateUrl: './modal-user.component.html',
  styleUrls: ['./modal-user.component.css']
})
export class ModalUserComponent implements OnInit {
  refresh: () => void;

  @Input() Data;
  @Input() Add: boolean;
  checkboxChecked: boolean;
  isAdmin: boolean;
  //changedUser: User;

  constructor(private activeModal: NgbActiveModal, private userService: UserService) {
  }

  ngOnInit() {
    this.isAdmin = false;

    if(this.Data.role == "moderator"){
        this.checkboxChecked = true;
    }
    else if(this.Data.role == "user"){
        this.checkboxChecked = false;
    }
    else if(this.Data.role == "admin"){
        this.isAdmin = true;
    }
      //this.changedUser = {...this.Data};
  }

  save(){
    if (this.Add === undefined || !this.Add) {
      if (!this.isAdmin) {
        if (this.checkboxChecked && this.Data.role == "user") {
          this.userService.promote(this.Data.id).subscribe(x => this.refresh());
        } else if (!this.checkboxChecked && this.Data.role == "moderator") {
          this.userService.demote(this.Data.id).subscribe(x => this.refresh());
        }
      }
    } else {
      this.userService.create({uid: this.Data.sub, email: this.Data.email}).subscribe(x => this.refresh());
    }
    this.activeModal.close();
  }

  delete(){
    if(!this.isAdmin){
      console.log("delete");
      this.userService.delete(this.Data.id).subscribe(x => this.refresh());
      this.activeModal.close();
    }
  }
}
