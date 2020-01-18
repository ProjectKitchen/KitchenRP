import { Component, OnInit, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith, tap, flatMap } from 'rxjs/operators';

import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {ModalUserComponent} from "../../../../modals/modal-user/modal-user.component";

import {User} from "../../../../types/user";
import {UserService} from "../../../../services/user/user.service";

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css'],
  providers: [DecimalPipe]
})
export class UserManagementComponent implements OnInit {
  data: User[] = [];
  users$: Observable<User[]>;
  filter = new FormControl('');

  constructor(private userService: UserService, private modalService: NgbModal, pipe: DecimalPipe) {
    this.users$ = this.userService.getAll()
        .pipe(
            tap(users => this.data = users),
            flatMap(r => this.filter.valueChanges
                .pipe(
                    startWith(''),
                    map(text => this.search(text, pipe))
                )
            )
        )
  }

  ngOnInit() {
  }

  openModal(tableRow) {
    const modalRef = this.modalService.open(ModalUserComponent, { windowClass : "modal-size-lg"});
    modalRef.componentInstance.Data = tableRow;
  }

  search(text: string, pipe: PipeTransform): User[] {
    return this.data.filter(user => {
    const term = text.toLowerCase();
    return user.sub.toLowerCase().includes(term)
        || user.role.toLowerCase().includes(term)
        || user.email.toLowerCase().includes(term);
        //|| pipe.transform(user.id).includes(term); // ID search?
    });
  }

}
