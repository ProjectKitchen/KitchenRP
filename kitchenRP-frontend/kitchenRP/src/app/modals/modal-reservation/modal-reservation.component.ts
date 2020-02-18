import {Component, OnInit, Input} from '@angular/core';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import {ReservationService} from "../../services/reservation/reservation.service";
import {AuthService} from "../../services/auth/auth.service";

@Component({
    selector: 'app-modal-reservation',
    templateUrl: './modal-reservation.component.html',
    styleUrls: ['./modal-reservation.component.css']
})
export class ModalReservationComponent implements OnInit {
    refresh: () => void;

    @Input()
    Add: boolean;
    @Input() Data;
    isModOrAdmin: boolean = false;
    isLoggedInUser: boolean = false;
    currentUserId: number;

    hourStep = 1;
    minuteStep = 1;

    reservationId: number;
    dateField;
    date = new Date();
    duration = {hour: 0, minute: 0};
    timeStart = {hour: 0, minute: 0};

    resourceId: number;
    resourceName: string;
    userId: number;
    userName: string;
    status: string;

    constructor(private activeModal: NgbActiveModal,
                private reservationService: ReservationService,
                private authService: AuthService) {
    }

    saveReservation() {
        let startDate = new Date(this.dateField.year + "-" + this.dateField.month + "-" + this.dateField.day);
        // let startDate = new Date(this.date);
        startDate.setHours(this.timeStart.hour);
        startDate.setMinutes(this.timeStart.minute);

        let endDate = new Date(startDate.getTime()
            + (1000 * 60) * this.duration.minute);

        this.reservationService.create({
            allowNotifications: true,
            startTime: startDate.toISOString(),
            endTime: endDate.toISOString(),
            resourceId: this.resourceId,
            userId: this.userId
        }).subscribe(
            _ => {
                this.activeModal.close();
                this.refresh();
            }
        )
    }

    delete(){
        this.reservationService.delete(this.reservationId).subscribe(_ => this.refresh());
        this.activeModal.close();
    }

    accept() {
        this.reservationService.accept(this.reservationId, this.currentUserId).subscribe(_ => this.refresh());
        this.activeModal.close();
    }

    deny() {
        this.reservationService.deny(this.reservationId, this.currentUserId).subscribe(_ => this.refresh());
        this.activeModal.close();
    }

    ngOnInit() {
        this.authService.isModerator().subscribe(val => this.isModOrAdmin = val);
        this.authService.currentUser$.subscribe(val => {
            if (val.id == this.userId) {
                this.isLoggedInUser = true;
                this.currentUserId = val.id;
            }
        });
        if (!this.Add) {
            this.hourStep = 0;
            this.minuteStep = 0;
        }
    }

}
