import { Component, OnInit, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {ModalUserComponent} from "../../../../modals/modal-user/modal-user.component";

import {User} from "../../../../types/user";
import {UserService} from "../../../../services/user/user.service";

  // test data
  /*const table: User [] = [
    {
      "id": 1,
      "username":"Matthias",
      "role": "Member",
      "email": "bla@bla.com"
    },{
      "id": 2,
      "username":"Matthias",
      "role": "Member",
      "email": "bla@bla.com"
    },{
      "id": 3,
      "username":"Oliver",
      "role": "Admin",
      "email": "bla@bla.com"
    },{
      "id": 4,
      "username":"Daniel",
      "role": "Moderator",
      "email": "bla@bla.com"
    },{
      "id": 5,
      "username":"Hallo",
      "role": "Member",
      "email": "bla@bla.com"
    },{
      "id": 6,
      "username":"Viktor",
      "role": "Member",
      "email": "bla@bla.com"
    },{
      "id": 7,
      "username":"Hallo2",
      "role": "Member",
      "email": "bla@bla.com"
    },{
      "id": 8,
      "username":"Hallo3",
      "role": "Member",
      "email": "bla@bla.com"
    },{
      "id": 9,
      "username":"Hallo4",
      "role": "Member",
      "email": "bla@bla.com"
    },{
      "id": 10,
      "username":"Hallo5",
      "role": "Member",
      "email": "bla@bla.com"
    },
  ];*/
  var table: User[] = [];

  function search(text: string, pipe: PipeTransform): User[] {
    return table.filter(user => {
    const term = text.toLowerCase();
    return user.sub.toLowerCase().includes(term)
        || user.role.toLowerCase().includes(term)
        || user.email.toLowerCase().includes(term);
        //|| pipe.transform(user.id).includes(term); // ID search?
    });
  }

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css'],
  providers: [DecimalPipe]
})
export class UserManagementComponent implements OnInit {

  users$: Observable<User[]>;
  filter = new FormControl('');

  constructor(private userService: UserService, private modalService: NgbModal, pipe: DecimalPipe) {
    this.users$ = this.filter.valueChanges.pipe(
      startWith(''),
      map(text => search(text, pipe))
    );
    this.loadData();
  }

  ngOnInit() {
  }

  loadData() {
    console.log("Load Users here:");
    //var users = this.userService.getAll();
    //users.subscribe(val => console.log(val));
    //this.users$ = user;

    var user = this.userService.get(3);
    user.subscribe(val => {
      console.log(val);
    });

  }

  openModal(tableRow) {
    const modalRef = this.modalService.open(ModalUserComponent, { windowClass : "modal-size-lg"});
    modalRef.componentInstance.Data = tableRow;
  }

}
